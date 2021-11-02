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
    //public Animator animator;
    public CharacterAnimator animator;

    public void Awake()
    {
        animator = GetComponent<CharacterAnimator>();
        //animator.SetFloat("moveY",-1);
    }



    //move character
    public IEnumerator Move(Vector2 moveVector)
    {
        //animator.SetFloat("moveX", Mathf.Clamp(moveVector.x, -1, 1));
        //animator.SetFloat("moveY", Mathf.Clamp(moveVector.y, -1, 1));

        animator.MoveX = Mathf.Clamp(moveVector.x, -1, 1);
        animator.MoveY = Mathf.Clamp(moveVector.y, -1, 1);

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

    public IEnumerator MoveNpc(List<Vector2> moveVectors, Vector2 faceDir)
    {
        foreach(Vector2 moveVector in moveVectors)
        {
            animator.MoveX = Mathf.Clamp(moveVector.x, -1, 1);
            animator.MoveY = Mathf.Clamp(moveVector.y, -1, 1);

            Vector3 targetPos = transform.position;
            targetPos.x += moveVector.x;
            targetPos.y += moveVector.y;

            //if (!IsWalkableNpc(targetPos))
                //continue;

            isMoving = true;

            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;

            isMoving = false;
        }

        setFaceDir(faceDir);
    }

    public IEnumerator MoveExit(List<Vector2> moveVectors)
    {
        foreach (Vector2 moveVector in moveVectors)
        {
            animator.MoveX = Mathf.Clamp(moveVector.x, -1, 1);
            animator.MoveY = Mathf.Clamp(moveVector.y, -1, 1);

            Vector3 targetPos = transform.position;
            targetPos.x += moveVector.x;
            targetPos.y += moveVector.y;

            //if (!IsWalkableNpc(targetPos))
                //continue;

            isMoving = true;

            while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetPos;

            isMoving = false;
        }

        Destroy(gameObject);
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

    public bool IsWalkableNpc(Vector3 targetPos)
    {
        //add offset
        targetPos.y -= .5f;

        if (Physics2D.OverlapCircle(targetPos, .2f, GameLayers.Instance.SolidObjects) != null)
            return false;
        else
            return true;
    }

    private void Update()
    {
        //animator.SetBool("isMoving", isMoving);

        animator.IsMoving = isMoving;
    }

    public void setFaceDir(int x, int y)
    {
        //animator.SetFloat("moveX", Mathf.Clamp(x, -1, 1));
        //animator.SetFloat("moveY", Mathf.Clamp(y, -1, 1));

        animator.MoveX = Mathf.Clamp(x, -1, 1);
        animator.MoveY = Mathf.Clamp(y, -1, 1);
    }

    public void setFaceDir(Vector2 faceDir)
    {
        //animator.SetFloat("moveX", Mathf.Clamp(x, -1, 1));
        //animator.SetFloat("moveY", Mathf.Clamp(y, -1, 1));

        animator.MoveX = Mathf.Clamp(faceDir.x, -1, 1);
        animator.MoveY = Mathf.Clamp(faceDir.y, -1, 1);
    }
}
