using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    PlayerControls controls;
    Vector2 inputPos;

    public float moveSpeed = 5f;
    public bool isMoving;

    public LayerMask buildingsLayer;
    public LayerMask portalLayer;
    private Animator animator;

    // Update is called once per frame
    void Update()
    {

        if (!isMoving)
        {
            if (inputPos != Vector2.zero)
            {
                animator.SetFloat("moveX", inputPos.x);
                animator.SetFloat("moveY", inputPos.y);
                
                Vector3 targetPos = transform.position;
                targetPos.x += inputPos.x;
                targetPos.y += inputPos.y;

                if (IsWalkable(new Vector3(targetPos.x, targetPos.y - 0.5f)))
                    StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    
    public bool IsWalkable (Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, .2f, buildingsLayer) != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }



    void Awake()
    {
        animator = GetComponent<Animator>();
        controls = new PlayerControls();

        controls.Player.Up.performed += ctx => inputPos.y = 1;
        controls.Player.Up.canceled += ctx => inputPos = Vector2.zero;

        controls.Player.Down.performed += ctx => inputPos.y = -1;
        controls.Player.Down.canceled += ctx => inputPos = Vector2.zero;

        controls.Player.Left.performed += ctx => inputPos.x = -1;
        controls.Player.Left.canceled += ctx => inputPos = Vector2.zero;

        controls.Player.Right.performed += ctx => inputPos.x = 1;
        controls.Player.Right.canceled += ctx => inputPos = Vector2.zero;

    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }
}
