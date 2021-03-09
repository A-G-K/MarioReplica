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
    SFXManager sfxManager;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        cameraScript = GameObject.FindGameObjectWithTag("Managers").GetComponentInChildren<CameraScript>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        sfxManager = GameObject.FindGameObjectWithTag("Managers").GetComponentInChildren<SFXManager>();
    }

    public void Entry(Vector2 EntryPipe, Vector2 otherPipe, PipeController.PipeExit exit)
    {
        playerCollider = gameObject.GetComponentInChildren<CapsuleCollider2D>();
        playerCollider.enabled = false;
        MovingInPipe(true);
        sfxManager.PlaySound(13);
        StartCoroutine(playerMovement.TempFreezeMovement(1.1f));
        StartCoroutine(EntryAnim(EntryPipe, otherPipe, exit));
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

    private IEnumerator EntryAnim(Vector2 EntryPipe, Vector2 otherPipe, PipeController.PipeExit exit)
    {
        //yield return new WaitForSeconds(1.4f);
        if (exit == PipeController.PipeExit.Exit)
        {
            for (int i = 0; i < 100; i++) // Moving right animation
            {
                yield return new WaitForSeconds(0.01f);
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.015f, EntryPipe.y - 2.565f);
            }
        }

        else if (exit == PipeController.PipeExit.Underground)
        {
            for (int i = 0; i < 100; i++) // Moving downwards animation
            {
                yield return new WaitForSeconds(0.01f);
                gameObject.transform.position = new Vector2(EntryPipe.x, gameObject.transform.position.y - 0.015f);
            }
        }

        StartCoroutine(ExitAnim(otherPipe, exit));
    }

    private IEnumerator ExitAnim(Vector2 otherPipe, PipeController.PipeExit exit)
    {
        gameObject.transform.position = new Vector2(otherPipe.x, otherPipe.y - 0.35f); // Set position of player to other pipe
        //Debug.Log(gameObject.transform.position);
        MoveCamera(exit); // Move camera to other pipe

        if (exit == PipeController.PipeExit.Exit) // If the other pipe it above ground
        {
            for (int i = 0; i < 100; i++) // Moving upwards animation
            {
                yield return new WaitForSeconds(0.01f);
                gameObject.transform.position = new Vector2(otherPipe.x, gameObject.transform.position.y + 0.01f);
            }
        }

        MovingInPipe(false);
        playerCollider.enabled = true;
    }

    private void MoveCamera(PipeController.PipeExit exit)
    {
        if (exit == PipeController.PipeExit.Exit) // Exit pipe is above ground
        {
            cameraScript.MoveCameraReturnPipe();
        }

        else if (exit == PipeController.PipeExit.Underground) // Exit pipe is underground
        {
            cameraScript.MoveCamUnderground();
        }
    }
}
