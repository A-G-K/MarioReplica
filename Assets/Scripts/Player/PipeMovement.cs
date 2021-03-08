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
        playerCollider = gameObject.GetComponent<CapsuleCollider2D>();
        cameraScript = GameObject.FindGameObjectWithTag("Respawn").GetComponent<CameraScript>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Entry(float x, Vector2 otherPipe, int exit)
    {
        playerCollider.enabled = false;
        MovingInPipe(true);
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
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.position = new Vector2(x, gameObject.transform.position.y - 0.1f);
        }

        StartCoroutine(ExitAnim(otherPipe, exit));
    }

    private IEnumerator ExitAnim(Vector2 otherPipe, int exit)
    {
        gameObject.transform.position = new Vector2(otherPipe.x, otherPipe.y - 0.35f);
        //Debug.Log(gameObject.transform.position);
        MoveCamera(exit);

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.position = new Vector2(otherPipe.x, gameObject.transform.position.y + 0.1f);
        }

        playerCollider.enabled = true;
        MovingInPipe(false);
    }

    private void MoveCamera(int exit)
    {
        if (exit == 1) // Exit pipe is the entry to underground (above ground)
        {
            //cameraScript.MoveCamEntryPipe();
        }

        else if (exit == 2) // Exit pipe is the exit to the underground (above ground)
        {
            cameraScript.MoveCameraReturnPipe();
        }

        else if (exit == 3) // Exit pipe is underground
        {
            cameraScript.MoveCamUnderground();
        }
    }
}
