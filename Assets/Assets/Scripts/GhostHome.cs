using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostHome : GhostBehavior
{
    public Transform insideTransform;
    public Transform outsideTransform;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled && collision.gameObject.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            this.ghost._movement.SetDirection(-this.ghost._movement._direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost._movement.SetDirection(Vector2.up, true);
        this.ghost._movement._rigidbody.isKinematic = true;
        this.ghost._movement.enabled = false;

        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.insideTransform.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f;
        
        while (elapsed < duration)
        {
            Vector3 newPosition = Vector3.Lerp(position, this.outsideTransform.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        this.ghost._movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f : 1.0f, 0.0f), true);
        this.ghost._movement._rigidbody.isKinematic = false;
        this.ghost._movement.enabled = true;
    }
}
