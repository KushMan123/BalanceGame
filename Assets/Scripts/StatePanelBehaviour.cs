using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public State changeToState = State.Wood;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerState playerState = other.GetComponent<PlayerState>();
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerState.getState() != changeToState)
            {
                playerController.SetCanMove(false);
                playerController.rigidBody.velocity = Vector3.zero;
                playerController.rigidBody.angularVelocity = Vector3.zero;
                other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
                playerState.setState(changeToState);
                StartCoroutine(playerController.EnableMovement());
            }
        }
    }
}
