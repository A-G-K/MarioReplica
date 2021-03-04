using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float groundCheckRadius = 0.05f, hFrrictionMultiplier = 1f, lowJumpMultiplier = 2f, fallMultiplier = 2f, jumpHeight = 5f, maxSpeed = 20f, accelTime = 0.5f;
    private Transform playerT;
    private Rigidbody2D rb2d;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckT;
    private Vector2 refVel = Vector2.zero;


    public bool canMove;
    private bool grounded;
    private float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        playerT = gameObject.transform;
        canMove = true;
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = CheckGrounded();
        xSpeed = rb2d.velocity.x;

        if (rb2d.velocity.y < 0)
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }



        if (System.Math.Abs(rb2d.velocity.x) > 0.05 && !Input.GetButton("Horizontal"))
        {
            if (rb2d.velocity.x > 0)
            {
                rb2d.velocity += Vector2.left * hFrrictionMultiplier * Time.deltaTime;
            }
            else
            {
                rb2d.velocity += Vector2.right * hFrrictionMultiplier * Time.deltaTime;
            }
        }


        if (canMove)
        {
            if (Input.GetButton("Horizontal"))
            {
                Move();
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        

    }


    private void Move()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb2d.velocity.y);
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, ref refVel, accelTime);
    }

    private void Jump()
    {
        if (grounded)
        {
            rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
        }
    }


    private bool CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckT.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            return true;
        }
        return false;
    }
}
