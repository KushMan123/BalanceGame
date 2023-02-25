using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
    public GameObject Player;
    public Vector3 offset;
    public AudioClip playerFallingSound;
    private PlayerBehaviour playerBehaviour;
    private Vector3 initialPosition;
    private AudioSource deathSource;

    private void Awake()
    {
        deathSource = GetComponent<AudioSource>();
        playerBehaviour = Player.GetComponent<PlayerBehaviour>();
    }

    private void Update()
    {
        bool playerInGround = playerBehaviour.GetGroundedStatus();
        bool playerInWater = playerBehaviour.GetWaterStatus();
        if (!(playerInGround||playerInWater))
        {
            transform.position = initialPosition;
        }
        else 
        {
            transform.position = Player.transform.position + offset;
        }
        initialPosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deathSource.PlayOneShot(playerFallingSound);
            other.gameObject.transform.position = CheckpointManager.cm.GetCheckpoint();
            GameManager.gm.AddLife(-1);
            return;
        }
        Destroy(other.gameObject);
    }
}
