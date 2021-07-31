using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector2 inputPos;

    public float moveSpeed = 5f;
    public bool isMoving;

    public GameObject dialogBox;

    public LayerMask buildingsLayer;
    public LayerMask portalLayer;
    public LayerMask interactableLayer;
    private Animator animator;

    public static Action<Dialogs> NextDialog;
    public bool enteringPortal;
   
    public Dialogs dialog;
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
                {
                    StartCoroutine(Move(targetPos));
                }
                else if (IsPortal(targetPos))
                {
                    StartCoroutine(EnterPortal());
                }
                    
            }
        }

        animator.SetBool("isMoving", isMoving);
        
    }
    IEnumerator EnterPortal()
    {
        Debug.Log("Change Scenes");
        yield return null;
    }

    public bool IsPortal(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(new Vector3(targetPos.x, targetPos.y - .5f), .2f, portalLayer) != null)
        {
            Debug.Log("true");
            return true;
        }
        else
        {
            return false;
        }
    }
    void interact ()
    {
        Debug.Log("Button Pressed");
        Vector3 facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        Vector3 interactPos = transform.position + facingDir;
        interactPos.y -= .5f;
        Collider2D collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
            
        }
    }

    public void ChangeScenes()
    {

        if (Physics2D.OverlapCircle(new Vector3(transform.position.x, transform.position.y - .5f), .2f, portalLayer) != null)
        {
            Debug.Log("Changed Scene");
        }
    }

    public bool IsWalkable (Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, .2f, buildingsLayer | interactableLayer) != null)
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
        
        ChangeScenes();
    }



    void Awake()
    {
        animator = GetComponent<Animator>();

    }

    void XButtonPress()
    {
        if (!dialogBox.active)
        {
            interact();
        }
        else
        {
            dialogBox.SetActive(false);
            //NextDialog?.Invoke(dialog);
        }
    }
    void OnEnable()
    {

        UIInputManager.OnDpadUp += MoveInput;
        UIInputManager.OnDpadDown += MoveInput;
        UIInputManager.OnDpadLeft += MoveInput;
        UIInputManager.OnDpadRight += MoveInput;

        UIInputManager.OnDpadCancelled += MoveInput;
    }

    void OnDisable()
    {
        UIInputManager.OnDpadUp -= MoveInput;
        UIInputManager.OnDpadDown -= MoveInput;
        UIInputManager.OnDpadLeft -= MoveInput;
        UIInputManager.OnDpadRight -= MoveInput;

        UIInputManager.OnDpadCancelled -= MoveInput;
        //controls.Player.Disable();
    }


    void MoveInput(String direction)
    {
        switch (direction)
        {
            case "Up":
                inputPos.y = 1;
                break;
            case "Down":
                inputPos.y = -1;
                break;
            case "Left":
                inputPos.x = -1;
                break;
            case "Right":
                inputPos.x = 1;
                break;
            case "Cancelled":
                inputPos = Vector2.zero;
                break;
            default:
                Debug.Log("Input not registered");
                break;
        }

    }
}
