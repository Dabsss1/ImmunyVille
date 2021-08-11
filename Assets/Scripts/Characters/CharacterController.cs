using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [HideInInspector]
    public bool isMoving;

    [HideInInspector]
    public Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("moveY",-1);
    }



    //move character
    public IEnumerator Move(Vector2 moveVector)
    {
        animator.SetFloat("moveX", Mathf.Clamp(moveVector.x, -1, 1));
        animator.SetFloat("moveY", Mathf.Clamp(moveVector.y, -1, 1));

        Vector3 targetPos = transform.position;
        targetPos.x += moveVector.x;
        targetPos.y += moveVector.y;

        if (!IsWalkable(targetPos))
            yield break;

        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }


    //check if tile is walkable
    public bool IsWalkable(Vector3 targetPos)
    {
        //add offset
        targetPos.y -= .5f;

        if (Physics2D.OverlapCircle(targetPos, .2f, GameLayers.Instance.SolidObjects | GameLayers.Instance.Interactable) != null)
            return false;
        else
            return true;
    }

    private void Update()
    {
        animator.SetBool("isMoving", isMoving);
    }

    public void setFaceDir(int x, int y)
    {
        animator.SetFloat("moveX", x);
        animator.SetFloat("moveY", y);
    }
}
