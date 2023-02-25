using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogButton : MonoBehaviour
{
    [SerializeField] private DialogBoxController dialogBoxController;
    private Animator animator;
    [SerializeField] private DIalogboxAnimatorFunction animatorFunction;
    [SerializeField] private int buttonIndex;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogBoxController.index == buttonIndex)
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
