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
    private Animator pAnimator;
    private float scaleMagnitude;

    
    public bool canMove;
    private bool grounded = false;
    private float xSpeed = 0f;
    private bool faceRight = true;

    private RigidbodyConstraints2D currentConstraints;
    

    // Start is called before the first frame update
    void Start()
    {
        playerT = gameObject.transform;
        scaleMagnitude = playerT.localScale.x;
        canMove = true;
        rb2d = GetComponent<Rigidbody2D>();
        pAnimator = GetComponentInChildren<Animator>();

        currentConstraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.constraints = currentConstraints;

    }

    // Update is called once per frame
    void Update()
    {
        grounded = CheckGrounded();
        //Debug.Log("grounded = " + grounded);
        xSpeed = rb2d.velocity.x;

        //set animator parameters
        pAnimator.SetBool("grounded", grounded);
        pAnimator.SetFloat("speed", Mathf.Abs(xSpeed));

        //flip sprite around based on movement speed

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
            if (grounded && !Input.GetButton("Horizontal"))
            {
                if (System.Math.Abs(rb2d.velocity.x) < 0.05)
                {
                    rb2d.velocity = Vector2.zero;
                }
                else if (rb2d.velocity.x > 0)
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
                Debug.Log("PRESSED JUMP");
                Jump();
            }
        }
        

    }

    //changes velocity of player, accelTime determines how fast player achieves max speed
    private void Move()
    {
        Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * maxSpeed, rb2d.velocity.y);
        rb2d.velocity = Vector2.SmoothDamp(rb2d.velocity, targetVelocity, ref refVel, accelTime);
        //Debug.Log("Moving " + Input.GetAxisRaw("Horizontal"));

        if (faceRight && targetVelocity.x < 0)
        {
            faceRight = false;
            Vector3 scale = playerT.localScale;
            scale.x = scaleMagnitude * -1;
            playerT.localScale = scale;

        }

        if (!faceRight && targetVelocity.x > 0)
        {
            faceRight = true;
            Vector3 scale = playerT.localScale;
            scale.x = scaleMagnitude;
            playerT.localScale = scale;

        }
    }

    //makes player jump
    private void Jump()
    {
        Debug.Log("CHECKING");
        if (grounded)
        {
            Debug.Log("JUMPED");
            rb2d.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            grounded = false;
           
        }
    }


    //checks if grounded
    private bool CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckT.position, groundCheckRadius, groundLayer);
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

    //Returns direction of character
    public bool isFacingRight() { return faceRight; }

    public IEnumerator TempFreezeMovement(float delay)
    {
        currentConstraints = RigidbodyConstraints2D.FreezeAll;
        rb2d.constraints = currentConstraints;

        yield return new WaitForSeconds(delay);

        currentConstraints = RigidbodyConstraints2D.FreezeRotation;
        rb2d.constraints = currentConstraints;
    }
}
