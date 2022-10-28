using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;
    private PlayerBehaviour playerBehaviour;
    private Vector3 initialPosition;

    private void Awake()
    {
        playerBehaviour = Player.GetComponent<PlayerBehaviour>();
    }

    private void Update()
    {
        bool playerInGround = playerBehaviour.GetGroundedStatus();
        if (!playerInGround)
        {
            Debug.Log("Static");
            transform.position = initialPosition;
        }
        else 
        {
            Debug.Log("Moving");
            transform.position = Player.transform.position + offset;
        }
        initialPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = CheckpointManager.cm.GetCheckpoint();
            return;
        }
        Destroy(other.gameObject);
    }
}
