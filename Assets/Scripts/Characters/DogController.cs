using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DogController : MonoBehaviour
{
    public float moveSpeed = 5f;
    [HideInInspector]
    public bool isMoving;

    [HideInInspector]
    public CharacterAnimator animator;

    public static Action<Vector2> OnPlayerMove;

    private void OnEnable()
    {
        OnPlayerMove += MoveDog;
    }

    private void OnDisable()
    {
        OnPlayerMove -= MoveDog;
    }
    private void Start()
    {
        animator = GetComponent<CharacterAnimator>();

        if (Tasks.Instance.taskSlots[10].inProgress || Tasks.Instance.taskSlots[10].done)
        {
            StartCoroutine(SetDogPosition());
        }
    }

    IEnumerator SetDogPosition()
    {
        while (!GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE))
            yield return null;

        Vector3 spawnPosition = Player.Instance.transform.position;
        spawnPosition.y -= .5f;
        transform.position = spawnPosition;
    }
    private void Update()
    {
        animator.IsMoving = isMoving;
    }

    public void MoveDog(Vector2 playerPos)
    {
        if (Tasks.Instance.taskSlots[10].inProgress || Tasks.Instance.taskSlots[10].done)
        {
            if (!isMoving)
                StartCoroutine(Move(playerPos));
        }            
    }
    //move character
    public IEnumerator Move(Vector2 moveVector)
    {

        animator.MoveX = Mathf.Clamp(moveVector.x - transform.position.x, -1, 1);
        animator.MoveY = Mathf.Clamp(moveVector.y - transform.position.y, -1, 1);

        

        Vector3 targetPos = transform.position;
        targetPos.x = moveVector.x;
        targetPos.y = moveVector.y-.5f;

        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;


        isMoving = false;


        Vector3 faceDir = new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y);
        faceDir = faceDir - transform.position;
        faceDir.y -= .5f;
        setFaceDir(faceDir);
    }


    public void setFaceDir(int x, int y)
    {

        animator.MoveX = Mathf.Clamp(x, -1, 1);
        animator.MoveY = Mathf.Clamp(y, -1, 1);
    }

    public void setFaceDir(Vector2 faceDir)
    {
        animator.MoveX = Mathf.Clamp(faceDir.x, -1, 1);
        animator.MoveY = Mathf.Clamp(faceDir.y, -1, 1);
    }
}
