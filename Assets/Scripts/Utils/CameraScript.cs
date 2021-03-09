using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform cameraT, cameraStartT, undergroundCamT, returnPipeT, playerT;
    [SerializeField] float moveOffset;
    public bool canCameraMove = true;

    // Start is called before the first frame update
    void Start()
    {
        cameraT.position = cameraStartT.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canCameraMove)
        {
            //camera will move right when player gets close to edge of screen, distance determined by moveOffset
            if (playerT.position.x > cameraT.position.x + moveOffset)
            {
                cameraT.position = new Vector3(playerT.position.x - moveOffset, cameraT.position.y, cameraT.position.z);
            }
        }
    }

    //move camera to underground level, turn off camera movement
    //use transform of empty object at position you want camera to teleport to
    public void MoveCamUnderground()
    {
        cameraT.position = undergroundCamT.position;
        canCameraMove = false;
    }

    //move camera to return pipe, turn on camera movement
    //use transform of empty object at position you want camera to teleport to
    public void MoveCameraReturnPipe()
    {
        cameraT.position = returnPipeT.position;
        canCameraMove = true;

    }
}
