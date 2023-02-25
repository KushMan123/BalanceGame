using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LifeOrb : MonoBehaviour
{
    public GameObject sphere;
    public VisualEffect lifeOrbEffect;
    private AudioSource lifeSource;
    public AudioClip popSound;
    public Score score;
    // Start is called before the first frame update
    
    void Start()
    {
        lifeSource = GetComponent<AudioSource>();
        lifeOrbEffect.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lifeOrbEffect.Stop();
            Destroy(sphere);
            lifeSource.PlayOneShot(popSound);
            GameManager.gm.AddLife(1);
            score.AddScorePoints(500);
            StartCoroutine(DestroyOrb());
        }
    }

    private IEnumerator DestroyOrb()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    
}
