using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeMovement : MonoBehaviour
{
    public bool movingInPipe = false;
    private PlayerMovement playerMovement;
    private CapsuleCollider2D playerCollider;
    private CameraScript cameraScript;
    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        cameraScript = GameObject.FindGameObjectWithTag("Managers").GetComponentInChildren<CameraScript>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Entry(float x, Vector2 otherPipe, int exit)
    {
        playerCollider = gameObject.GetComponentInChildren<CapsuleCollider2D>();
        playerCollider.enabled = false;
        MovingInPipe(true);
        StartCoroutine(playerMovement.TempFreezeMovement(1.1f));
        StartCoroutine(EntryAnim(x, otherPipe, exit));
    }

    private void MovingInPipe(bool move)
    {
        movingInPipe = move;
        playerMovement.canMove = !move;
        
        if (move)
        {
            rigidBody.gravityScale = 0;
        } 
        else
        {
            rigidBody.gravityScale = 1;
        }
    }

    private IEnumerator EntryAnim(float x, Vector2 otherPipe, int exit)
    {
        //yield return new WaitForSeconds(1.4f);
        for (int i = 0; i < 10; i++) // Moving downwards animation
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.position = new Vector2(x, gameObject.transform.position.y - 0.1f);
        }

        StartCoroutine(ExitAnim(otherPipe, exit));
    }

    private IEnumerator ExitAnim(Vector2 otherPipe, int exit)
    {
        gameObject.transform.position = new Vector2(otherPipe.x, otherPipe.y - 0.35f); // Set position of player to other pipe
        //Debug.Log(gameObject.transform.position);
        MoveCamera(exit); // Move camera to other pipe

        if (exit == 1) // If the other pipe it above ground
        {
            for (int i = 0; i < 10; i++) // Moving upwards animation
            {
                yield return new WaitForSeconds(0.1f);
                gameObject.transform.position = new Vector2(otherPipe.x, gameObject.transform.position.y + 0.1f);
            }
        }

        MovingInPipe(false);
        playerCollider.enabled = true;
    }

    private void MoveCamera(int exit)
    {
        if (exit == 1) // Exit pipe is above ground
        {
            cameraScript.MoveCameraReturnPipe();
        }

        else if (exit == 2) // Exit pipe is underground
        {
            cameraScript.MoveCamUnderground();
        }
    }
}
