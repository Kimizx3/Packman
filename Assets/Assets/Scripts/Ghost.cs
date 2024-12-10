using System;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement _movement { get; private set; }
    public GhostHome _ghostHome { get; private set; }
    public GhostScatter _ghostScatter { get; private set; }
    public GhostChase _ghostChase { get; private set; }
    public GhostFrightened _ghostFrightened { get; private set; }
    public GhostBehavior _initialBehavior;
    public Transform target;
    
    public int points = 200;

    private void Awake()
    {
        this._movement = GetComponent<Movement>();
        this._ghostHome = GetComponent<GhostHome>();
        this._ghostChase = GetComponent<GhostChase>();
        this._ghostScatter = GetComponent<GhostScatter>();
        this._ghostFrightened = GetComponent<GhostFrightened>();
    }

    private void Start()
    {
        ResetGhostState();
    }

    public void ResetGhostState()
    {
        this.gameObject.SetActive(true);
        this._movement.ResetState();
        
        this._ghostFrightened.Disable();
        this._ghostChase.Disable();
        this._ghostScatter.Enable();
        
        if (this._ghostHome != this._initialBehavior)
        {
            this._ghostHome.Disable();
        }

        if (this._initialBehavior != null)
        {
            _initialBehavior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this._ghostFrightened.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
