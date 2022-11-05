using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraState { Front, Back, Left, Right}

public class CameraBehaviour : MonoBehaviour
{
    public Transform player;
    [SerializeField] CameraState cameraState = CameraState.Front;

    // Update is called once per frame
    void LateUpdate()
    {

        FollowPlayer();   
    }

    void FollowPlayer()
    {
        transform.LookAt(player.position);
    }

    public CameraState GetCameraState()
    {
        return cameraState;
    }
    public void SetCameraState(CameraState state)
    {
        cameraState = state;
    }

}
