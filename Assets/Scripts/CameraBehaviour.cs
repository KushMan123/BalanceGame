using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public Transform player;
    public Vector3 offest;

    // Update is called once per frame
    void LateUpdate()
    {
        //Follow the player
        FollowPlayer();   
    }

    void FollowPlayer()
    {
        transform.position = player.position + offest;
        transform.LookAt(player.position);
    }

}
