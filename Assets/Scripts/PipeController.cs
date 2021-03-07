using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private GameObject otherPipe;
    private GameObject player;
    private PipeMovement pipeMovement;

    public enum PipeExit { Entry, Underground, Exit }
    [SerializeField] private PipeExit pipeExit; // Used to check where the pipe exit is so that the camera moves properly

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pipeMovement = player.GetComponent<PipeMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (!pipeMovement.movingInPipe)
            { 
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    Teleport();
                }
            }
        }
    }

    private void Teleport()
    {
        int exit = 3;

        if (pipeExit == PipeExit.Entry) // Exit pipe is the entry to underground (above ground)
        {
            exit = 1;
        }

        else if (pipeExit == PipeExit.Exit) // Exit pipe is the exit to the underground (above ground)
        {
            exit = 2;
        }

        else if (pipeExit == PipeExit.Underground) // Exit pipe is underground
        {
            exit = 3;
        }

        pipeMovement.Entry(gameObject.transform.position.x, otherPipe.transform.position, exit);   // do animation
    }
}
