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
<<<<<<< HEAD
    private Animator pAnimator;

    
    public bool canMove;
    private bool grounded = false;
    private float xSpeed = 0f;
    private bool faceRight = true;
=======

    
    public bool canMove;
    private bool grounded;
    private float xSpeed;
>>>>>>> parent of f6fd616 (Revert "Merge branch 'master' of https://github.com/A-G-K/MarioReplica")
    // Start is called before the first frame update
    void Start()
    {
        playerT = gameObject.transform;
        canMove = true;
        rb2d = GetComponent<Rigidbody2D>();
<<<<<<< HEAD
        pAnimator = GetComponentInChildren<Animator>();
=======
>>>>>>> parent of f6fd616 (Revert "Merge branch 'master' of https://github.com/A-G-K/MarioReplica")
    }

    // Update is called once per frame
    void Update()
    {
        grounded = CheckGrounded();
<<<<<<< HEAD
        //Debug.Log("grounded = " + grounded);
        xSpeed = rb2d.velocity.x;

        //set animator parameters
        pAnimator.SetBool("grounded", grounded);
        pAnimator.SetFloat("speed", Mathf.Abs(xSpeed));

        //flip sprite around based on movement speed
        if (faceRight && xSpeed < 0)
            Flip();
        if (!faceRight && xSpeed > 0)
            Flip();

=======
        xSpeed = rb2d.velocity.x;

>>>>>>> parent of f6fd616 (Revert "Merge branch 'master' of https://github.com/A-G-K/MarioReplica")
        if (canMove)
        {

            //if falling, increase gravity
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            }
            else if (rb2d.velocity.y < 1)
            {

                rb2d.velocity += Vector2.up * Physics2D.gravity.y * 0.5f * fallMultiplier * Time.deltaTime;
            }
            //if ascending but not holding jump, increase gravity (this makes you jump higher when you hold the jump button
            else if (rb2d.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
            }


            //side to side friction
<<<<<<< HEAD
            if (grounded && !Input.GetButton("Horizontal"))
            {
                if (System.Math.Abs(rb2d.velocity.x) < 0.05)
                {
                    rb2d.velocity = Vector2.zero;
                }
                else if (rb2d.velocity.x > 0)
=======
            if (System.Math.Abs(rb2d.velocity.x) > 0.05 && !Input.GetButton("Horizontal"))
            {
                if (rb2d.velocity.x > 0)
>>>>>>> parent of f6fd616 (Revert "Merge branch 'master' of https://github.com/A-G-K/MarioReplica")
                {
                    rb2d.velocity += Vector2.left * hFrrictionMultiplier * Time.deltaTime;
                }
                else
                {
                    rb2d.velocity += Vector2.right * hFrrictionMultiplier * Time.deltaTime;
                }
            }

        
            //input button checks
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

    //changes velocity of player, accelTime determines how fast player achieves max speed
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb2d.velocity.y);
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, ref refVel, accelTime);
<<<<<<< HEAD
        //Debug.Log("Moving " + Input.GetAxisRaw("Horizontal"));
=======
>>>>>>> parent of f6fd616 (Revert "Merge branch 'master' of https://github.com/A-G-K/MarioReplica")
    }

    //makes player jump
    private void Jump()
    {
        if (grounded)
        {
            rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
        }
    }


    //checks if grounded
    private bool CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckT.position, groundCheckRadius, groundLayer);
<<<<<<< HEAD
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    //flips sprite
    private void Flip()
    {
        faceRight = !faceRight;
        Vector3 scale = playerT.localScale;
        scale.x *= -1;
        playerT.localScale = scale;
    }
=======
        if (colliders.Length > 0)
        {
            return true;
        }
        return false;
    }
>>>>>>> parent of f6fd616 (Revert "Merge branch 'master' of https://github.com/A-G-K/MarioReplica")
}
