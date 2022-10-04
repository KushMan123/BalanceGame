using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public State changeToState = State.Wood;
    private void OnTriggerEnter(Collider other)
    {
        // If the trigger is caused by player then change the state of the player
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour playerBehaviour = other.GetComponent<PlayerBehaviour>();
            if(playerBehaviour != null)
            {
                if(playerBehaviour.playerState != changeToState)
                {
                    playerBehaviour.StopMovement();
                    other.transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
                    StartCoroutine(playerBehaviour.InTransitionPlate(3f, changeToState));
                }
            }
        }
    }
}
