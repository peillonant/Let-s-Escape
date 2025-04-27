using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;

    private float f_PreviousX = 0;

    void Update()
    {
        if (animator.GetBool("b_InTheAir") && animator.GetBool("b_isAttached"))
        {
            if (f_PreviousX < gameObject.transform.position.x)
            {
                TriggerSwingFront();
            }
            else if (f_PreviousX > gameObject.transform.position.x)
            {
                TriggerSwingBack();
            }

            f_PreviousX = gameObject.transform.position.x;
        }
    }

    // Function to trigger the Jump Animation for the charact
    public void TriggerJump()
    {
        animator.SetTrigger("t_Jump");
    }

    // Function to trigger the Animation of the swinging From Left side of the ButtonBlock
    public void TriggerSwingFront()
    {
        TriggerJump();
        animator.SetBool("b_isAttached", true);
        animator.SetBool("b_InTheAir", true);
        animator.SetBool("b_MovingFront", true);
        animator.SetBool("b_MovingBack", false);
    }

    // Function to trigger the Animation of the swinging From right side of the ButtonBlock
    public void TriggerSwingBack()
    {
        animator.SetBool("b_MovingFront", false);
        animator.SetBool("b_MovingBack", true);
    }

    // Function to trigger the Animation of in the Air without any attached
    public void TriggerReleaseSwing()
    {
        animator.SetBool("b_isAttached", false);
        animator.SetBool("b_InTheAir", true);
        animator.SetBool("b_MovingFront", false);
        animator.SetBool("b_MovingBack", false);
        animator.ResetTrigger("t_Jump");
    }

    // Function to trigger the Animation when the Charact arrived on the Ground
    public void TriggerCushion()
    {
        animator.SetBool("b_InTheAir", false);
        animator.SetTrigger("t_OnGround");
    }

}
