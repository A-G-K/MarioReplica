using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] GameObject OtherPipe;
    private GameObject player;
    private CameraScript cameraScript;

    public enum PipeExit { Entry, Underground, Exit }
    [SerializeField] private PipeExit pipeExit; // Used to check where the pipe exit is so that the camera moves properly

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Mario");
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(player.GetComponent<Rigidbody2D>().velocity.y == 0)
        {
            if(Input.GetAxisRaw("Vertical") < 0)
            {
                Teleport();
            }
        }
    }

    private void Teleport()
    {
        // do animation
        player.transform.position = OtherPipe.transform.position;

        if(pipeExit == PipeExit.Entry)
        {
            //cameraScript.MoveCamEntryPipe();
        }

        else if (pipeExit == PipeExit.Exit)
        {
            cameraScript.MoveCameraReturnPipe();
        }

        else if (pipeExit == PipeExit.Underground)
        {
            cameraScript.MoveCamUnderground();
        }

        // do exit animation
    }
}
