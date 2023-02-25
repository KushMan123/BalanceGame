using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirepointTrigger : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject fire;

    [Header("Sound Effect")]
    private AudioSource fireSource;
    public AudioClip fireStartSound;
    public AudioClip fireCracklingSound;

    private void Start()
    {
        fireSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            foreach(Transform firePoint in firePoints)
            {
                Instantiate(fire, firePoint.position, fire.transform.rotation);
                fireSource.PlayOneShot(fireStartSound);
                StartCoroutine(FireCracklingSound());
            }
        }
    }

    private IEnumerator FireCracklingSound()
    {
        yield return new WaitForSeconds(1f);
        fireSource.clip = fireCracklingSound;
        fireSource.Play();
    }
}
