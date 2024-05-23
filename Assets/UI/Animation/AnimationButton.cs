using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationButton : MonoBehaviour
{
    public Animator animator;
    public void AnimationStart()
    {
        animator.SetBool("Ch",true);
    }
}
