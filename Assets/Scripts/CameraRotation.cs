using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Camera gameCamera;
    [SerializeField] int cameraStateIndex = 0;
    bool canCameraRotate;
    CameraState[] clockwiseDirection = { CameraState.Front, CameraState.Left, CameraState.Back, CameraState.Right };

    private void Start()
    {
        canCameraRotate = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (canCameraRotate)
        {
            
            if (Input.GetKeyDown(KeyCode.L))
            {
                transform.Rotate(new Vector3(0, -90, 0));
                cameraStateIndex = cameraStateIndex - 1;
                if (cameraStateIndex < 0)
                {
                    cameraStateIndex = 3;
                }
                gameCamera.GetComponent<CameraBehaviour>().SetCameraState(clockwiseDirection[cameraStateIndex]);
            }
            else if (Input.GetKeyDown(KeyCode.J))
            {
                transform.Rotate(new Vector3(0, 90, 0));
                cameraStateIndex = cameraStateIndex + 1;
                if (cameraStateIndex >= clockwiseDirection.Length)
                {
                    cameraStateIndex = 0;
                }
                gameCamera.GetComponent<CameraBehaviour>().SetCameraState(clockwiseDirection[cameraStateIndex]);
            }
        }
        
    }

    public void SetCanCameraCanRotate(bool value)
    {
        canCameraRotate = value;
    }

    public void setCameraStateIndex(int value)
    {
        cameraStateIndex = value;
        gameCamera.GetComponent<CameraBehaviour>().SetCameraState(clockwiseDirection[cameraStateIndex]);
    }
}
