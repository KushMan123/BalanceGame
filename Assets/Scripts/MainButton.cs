using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton : MonoBehaviour
{
    [SerializeField] private MainButtonController menuButtonController;
    private Animator animator;
    [SerializeField] private AnimatorFunction animatorFunction;
    [SerializeField] private int buttonIndex;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == buttonIndex)
        {
            animator.SetBool("selected", true);
            if (Input.GetKey(KeyCode.Return))
            {
                animator.SetBool("pressed", true);
            }
            else if (animator.GetBool("pressed"))
            {
                animator.SetBool("pressed", false);
                animatorFunction.disableOnce = true;
            }
        }
        else
        {
            animator.SetBool("selected", false);
            animatorFunction.disableOnce = false;
        }
    }
}
