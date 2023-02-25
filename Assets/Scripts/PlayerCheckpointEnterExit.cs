using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointEnterExit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour playerBehaviour = other.gameObject.GetComponent<PlayerBehaviour>();
            if (playerBehaviour.playerState == State.Cement)
            {
                playerBehaviour.SetMass(5f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerBehaviour playerBehaviour = other.gameObject.GetComponent<PlayerBehaviour>();
            if (playerBehaviour.playerState == State.Cement)
            {
                playerBehaviour.SetMass(8f);
            }
        }
    }
}
