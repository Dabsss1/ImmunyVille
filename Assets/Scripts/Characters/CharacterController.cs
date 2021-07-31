using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public bool isMoving;
    private Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();

        animator.SetFloat("moveY", -1);
        animator.SetBool("isMoving",isMoving);
    }
}
