using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class StatePanelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("State Change Attributes")]
    public State changeToState = State.Wood;

    [Header("Material Change Attribute")]
    public Material Cement;
    public Material Wood;
    public Material NewsPaper;
    public GameObject CentreCircle;

    [Header("Visual Effects")]
    public VisualEffect orbTranistionVfx;

    [Header("Audio")]
    public AudioClip transformSound;
    private AudioSource panelSource;

    private void Start()
    {
        panelSource = GetComponent<AudioSource>();
        Renderer rd = CentreCircle.GetComponent<Renderer>();
        switch (changeToState)
        {
            case State.Wood:
                rd.material = Wood;
                break;
            case State.Cement:
                rd.material = Cement;
                break;
            case State.Newspaper:
                rd.material = NewsPaper;
                break;
        }
    }

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
                    Vector3 centrePosition = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
                    other.transform.position = Vector3.Lerp(other.transform.position, centrePosition, 2f) ;
                    Debug.Log("Playing");
                    orbTranistionVfx.Play();
                    panelSource.PlayOneShot(transformSound);
                    StartCoroutine(playerBehaviour.InTransitionPlate(3f, changeToState));
                    StartCoroutine(StopVFX());
                }
            }
        }
    }

    IEnumerator StopVFX()
    {
        yield return new WaitForSeconds(4f);
        orbTranistionVfx.Stop();
    }
}
