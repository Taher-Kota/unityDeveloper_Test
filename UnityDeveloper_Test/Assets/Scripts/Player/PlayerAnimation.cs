using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private const string RUNNING_BOOL = "Running";
    private const string JUMP_BOOL = "Jump";
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(RUNNING_BOOL, PlayerMovement.instance.IsRunning());
        animator.SetBool(JUMP_BOOL, PlayerMovement.instance.IsJumping());
    }
}
