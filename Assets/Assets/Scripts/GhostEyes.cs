using System;
using UnityEngine;

public class GhostEyes : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Movement movement { get; private set; }
    public Sprite Up;
    public Sprite Down;
    public Sprite Left;
    public Sprite Right;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponentInParent<Movement>();
    }

    private void Update()
    {
        if (this.movement._direction == Vector2.up)
        {
            spriteRenderer.sprite = this.Up;
        }
        else if (this.movement._direction == Vector2.down)
        {
            spriteRenderer.sprite = this.Down;
        }
        else if (this.movement._direction == Vector2.left)
        {
            spriteRenderer.sprite = this.Left;
        }
        else if (this.movement._direction == Vector2.right)
        {
            spriteRenderer.sprite = this.Right;
        }
    }
}
