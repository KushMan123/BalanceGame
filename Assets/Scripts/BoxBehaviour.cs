using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    public float actualMass = 0.1f;
    public float heavyMass = 100f;
    private void Start()
    {
        GetComponent<Rigidbody>().mass = heavyMass;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool playerCanPush = collision.gameObject.GetComponent<PlayerBehaviour>().Push();
            if (playerCanPush)
            {
                GetComponent<Rigidbody>().mass = actualMass;
            }
            else
            {
                GetComponent<Rigidbody>().mass = heavyMass;
            }
        }
    }
}
