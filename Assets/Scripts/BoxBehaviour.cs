using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehaviour : MonoBehaviour
{
    public float actualMass = 0.1f;
    public float heavyMass = 100f;
    public AudioClip pushSound;
    public AudioClip thudSound;
    private AudioSource boxSource;
    private void Start()
    {
        boxSource = GetComponent<AudioSource>();
        GetComponent<Rigidbody>().mass = heavyMass;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bool playerCanPush = collision.gameObject.GetComponent<PlayerBehaviour>().Push();
            if (playerCanPush)
            {
                if (gameObject.CompareTag("Box"))
                {
                    Debug.Log("pushing");
                    boxSource.clip = pushSound;
                    boxSource.loop = true;
                    boxSource.Play();
                }
                GetComponent<Rigidbody>().mass = actualMass;
            }
            else
            {
                boxSource.PlayOneShot(thudSound);
                GetComponent<Rigidbody>().mass = heavyMass;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        boxSource.Stop();
    }
}
