using System;
using UnityEngine;
using UnityEngine.Events;


public class CollisionEventer : MonoBehaviour
{
    public event Action<Collision2D> OnCollisionEnter2DEvent;
    public event Action<Collision2D> OnCollisionStay2DEvent;
    public event Action<Collision2D> OnCollisionExit2DEvent;
    
    public Collider2D Collider2D { get; private set; }

    private void Awake()
    {
        Collider2D = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnCollisionEnter2DEvent?.Invoke(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        OnCollisionStay2DEvent?.Invoke(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        OnCollisionExit2DEvent?.Invoke(other);
    }
}