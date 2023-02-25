using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Completion : MonoBehaviour
{
    public SphereCollider sphereCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Completed");
            GameManager.gm.SetGameState(GameState.isCompleted);
        }
    }
}
