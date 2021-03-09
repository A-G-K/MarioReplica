using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class BasicEnemy : MonoBehaviour
{
    public float moveSpeed = 1f;
    [SerializeField] private bool isMovingLeft = true;
    [SerializeField] private Animator spriteAnimator;

    private Rigidbody2D rb;
    private ContactPoint2D[] contactCache = new ContactPoint2D[10];

    public bool IsMovingLeft
    {
        get { return isMovingLeft; }
        set
        {
            isMovingLeft = value;
            
            if (spriteAnimator != null)
                spriteAnimator.SetBool("IsMovingLeft", value);
        }
    }

    public bool CanMove { get; set; } = true;

    public Animator SpriteAnimator => spriteAnimator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // We need to do this, just in case we have an animator to setup
        IsMovingLeft = isMovingLeft;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            TryMoveOpposite(other);
        }
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            float horizontalMovement = isMovingLeft ? -moveSpeed : moveSpeed;
            rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void TryMoveOpposite(Collision2D other)
    {
        if (!CanMove) return;
        
        int contactCount = other.GetContacts(contactCache);

        for (int i = 0; i < contactCount; i++)
        {
            // Debug.Log($"{gameObject.name} with normal {contactCache[i].normal}");
            
            if (contactCache[i].normal.y < 0.5f)
            {
                if (contactCache[i].normal.x == 1f)
                {
                    IsMovingLeft = false;
                }
                else if (contactCache[i].normal.x == -1f)
                {
                    IsMovingLeft = true;
                }
                
                break;
            }
        }
    }
}