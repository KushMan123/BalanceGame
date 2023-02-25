using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Material emmisiveMaterial;
    public Renderer[] emissionObjects;
    public float intensity= 10809.41f;
    public Transform spawnLocation;
    public AudioClip checkpointClip;
    public Score score;

    public int scoreValue=10;
    private bool shouldPlaySound = true;
    private AudioSource checkpointAudio;
    private Color blueColor = new Color(10, 64, 191);

    private void Start()
    {
        checkpointAudio = GetComponent<AudioSource>();
        foreach(Renderer symbol in emissionObjects)
        {
            emmisiveMaterial = symbol.GetComponent<Renderer>().material;
            emmisiveMaterial.SetColor("_EmissionColor", blueColor * 0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CheckpointManager.cm.UpdateCheckpoint(spawnLocation.position);
            
            foreach (Renderer symbol in emissionObjects)
            {
                emmisiveMaterial = symbol.GetComponent<Renderer>().material;
                emmisiveMaterial.SetColor("_EmissionColor", blueColor * 5f);
            }
            if (shouldPlaySound)
            {
                checkpointAudio.PlayOneShot(checkpointClip);
                shouldPlaySound = false;
                score.AddScorePoints(scoreValue);
            }
            
        }
    }
}
