using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DIalogboxAnimatorFunction : MonoBehaviour
{
    [SerializeField] DialogBoxController dialogBoxController;
    public bool disableOnce;
    // Start is called before the first frame update

    void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce)
        {
            dialogBoxController.GetAudioSource().PlayOneShot(whichSound);
        }
    }
}
