using System;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Prepare()
    {
        animator.Play("Prepare", -1, 0f);
        // animator.Play("Ready", -1, 0f);
    }

    public void Throw()
    {
        animator.Play("Throw", -1, 0f);
        // animator.Play("Idle", -1, 0f);
    }
}
