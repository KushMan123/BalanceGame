using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundStatus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBehaviour>().SetGroundedStatus(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerBehaviour>().SetGroundedStatus(false);
        }
    }
}
