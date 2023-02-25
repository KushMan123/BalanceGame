using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public GameObject cameraRotationPoint;
    CameraRotation cameraRotation;
    PlayerFollow cameraPlayerFollow;
    public Vector3 newOffset;
    // Start is called before the first frame update
    void Start()
    {
        cameraRotation = cameraRotationPoint.GetComponent<CameraRotation>();
        cameraPlayerFollow = cameraRotationPoint.GetComponent<PlayerFollow>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Entered");
            cameraRotationPoint.transform.eulerAngles = new Vector3(0, -90, 0);
            cameraRotation.setCameraStateIndex(3);
            cameraRotation.SetCanCameraCanRotate(false);
            cameraPlayerFollow.setLerp(true);
            cameraPlayerFollow.offset = newOffset;
        }
    }
}
