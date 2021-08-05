using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player stats
    public string gender;
    public string playerName;

    //character controller
    public CharacterController character;

    //input
    Vector2 inputPos;

    //dialog
    public GameObject dialogBox;
    public static Action<Dialogs> NextDialog;
    public Dialogs dialog;



    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        gender = data.gender;
        playerName = data.playerName;
    }

    void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!character.isMoving)
        {
            if (inputPos != Vector2.zero)
            {
                if (IsPortal(inputPos))
                {
                    StartCoroutine(EnterPortal());
                }
                StartCoroutine(character.Move(inputPos));

                if (GameManagerScript.state != OpenWorldState.SCENECHANGING)
                    ChangeScenes();
            }
        }
        character.animator.SetBool("isMoving", character.isMoving);

        
    }
    IEnumerator EnterPortal()
    {
        Debug.Log("Change Scenes");
        yield return null
            ;
    }

    public bool IsPortal(Vector2 inputPos)
    {
        Vector3 targetPos = transform.position;
        targetPos.x += inputPos.x;
        targetPos.y += inputPos.y;

        if (Physics2D.OverlapCircle(new Vector3(targetPos.x, targetPos.y - .5f), .2f, GameLayers.Instance.DoorPortal) != null)
            return true;
        else
            return false;
    }
    
    
    void Interact (string button)
    {
        Debug.Log("Button Pressed");
        Vector3 facingDir = new Vector3(character.animator.GetFloat("moveX"), character.animator.GetFloat("moveY"));
        Vector3 interactPos = transform.position + facingDir;
        interactPos.y -= .5f;   
        Debug.Log(interactPos.x +"" + interactPos.y);

        Collider2D collider = Physics2D.OverlapCircle(interactPos, 0.2f, GameLayers.Instance.Interactable);
        if (collider != null)
        {
            
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
    
    public void ChangeScenes()
    {
        if (Physics2D.OverlapCircle(new Vector3(transform.position.x, transform.position.y - .5f), .2f, GameLayers.Instance.Portal) != null)
        {
            GameManagerScript.state = OpenWorldState.SCENECHANGING;
            Debug.Log("Changed Scene");
        }
    }

    /*
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
    */
    void OnEnable()
    {

        UIInputManager.OnDpadUp += MoveInput;
        UIInputManager.OnDpadDown += MoveInput;
        UIInputManager.OnDpadLeft += MoveInput;
        UIInputManager.OnDpadRight += MoveInput;

        UIInputManager.OnCrossButton += Interact;

        UIInputManager.OnDpadCancelled += MoveInput;

    }

    void OnDisable()
    {
        UIInputManager.OnDpadUp -= MoveInput;
        UIInputManager.OnDpadDown -= MoveInput;
        UIInputManager.OnDpadLeft -= MoveInput;
        UIInputManager.OnDpadRight -= MoveInput;

        UIInputManager.OnCrossButton -= Interact;

        UIInputManager.OnDpadCancelled -= MoveInput;

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
