using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathzone : MonoBehaviour
{
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
