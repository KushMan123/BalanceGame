using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorFunction : MonoBehaviour
{
    [SerializeField] MainButtonController mainButtonController;
    public bool disableOnce;
    // Start is called before the first frame update
    
    void PlaySound(AudioClip whichSound)
    {
        if (!disableOnce)
        {
            mainButtonController.GetAudioSource().PlayOneShot(whichSound);
        }
    }
}
