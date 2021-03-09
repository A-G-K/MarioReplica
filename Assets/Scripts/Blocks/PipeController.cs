using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private GameObject otherPipe;
    [SerializeField] private GameObject player;
    [SerializeField] private PipeMovement pipeMovement;

    public enum PipeExit { Exit, Underground }
    [SerializeField] private PipeExit pipeExit; // Used to check where the pipe exit is so that the camera moves properly

    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //pipeMovement = player.GetComponent<PipeMovement>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if (!pipeMovement.movingInPipe)
            { 
                if(pipeExit == PipeExit.Underground)
                {
                    if (Input.GetAxisRaw("Vertical") < 0)
                    {
                        Teleport();
                    }
                }
                else if (pipeExit == PipeExit.Exit)
                {
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        Teleport();
                    }
                }
            }
        }
    }

    private void Teleport()
    {
        pipeMovement.Entry(gameObject.transform.position, otherPipe.transform.position, pipeExit);   // do animation
    }
}
