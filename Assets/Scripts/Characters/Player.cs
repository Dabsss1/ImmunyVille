using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //character controller
    [HideInInspector]
    public CharacterController character;

    //input
    Vector2 inputPos;

    //dialog
    [HideInInspector]
    public GameObject dialogBox;

    //status
    public static Player Instance { get; private set; }

    void Awake()
    {
        character = GetComponent<CharacterController>();

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //stop any movement when player is not in Explore state
        if (!GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE))
            return;

        if (!character.isMoving)
        {
            if (inputPos != Vector2.zero)
            {
                IsPortal(inputPos);
                DogController.OnPlayerMove?.Invoke(new Vector2(transform.position.x,transform.position.y));

                StartCoroutine(character.Move(inputPos));      
                
                if (!GameStateManager.Instance.EqualsState(OpenWorldState.SCENECHANGING))
                    ChangeScenes();
            }
        }
        //character.animator.SetBool("isMoving", character.isMoving);

        character.animator.IsMoving = character.isMoving;
        
        if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE))
        {
            if (character.animator.IsMoving)
            {
                if (SceneInitiator.Instance.outdoor)
                    AudioManager.Instance.PlaySfx("FootstepsOutdoor");
                else
                    AudioManager.Instance.PlaySfx("FootstepsIndoor");
            }
            else
            {
                AudioManager.Instance.StopSfx("FootstepsOutdoor");
                AudioManager.Instance.StopSfx("FootstepsIndoor");
            }
        }
    }
        


    public void IsPortal(Vector2 inputPos)
    {
        Vector3 targetPos = transform.position;
        targetPos.x += inputPos.x;
        targetPos.y += inputPos.y;

        targetPos.y -= .5f;
        Collider2D portalCollider = Physics2D.OverlapCircle(targetPos, .2f, GameLayers.Instance.Portal);
        if (portalCollider != null)
            StartCoroutine(EnterPortal(portalCollider));
        else
            return;
    }
    IEnumerator EnterPortal(Collider2D portalCollider)
    {
        if (!GameStateManager.Instance.EqualsState(OpenWorldState.SCENECHANGING))
        {
            portalCollider.GetComponent<PortalController>().OnInteractPortal();
        }
        GameStateManager.Instance.ChangeGameState(OpenWorldState.SCENECHANGING);
        yield return null;
    }


    //interact the tile on front
    void Interact ()
    {
        if (GameStateManager.Instance.EqualsState(OpenWorldState.SETTINGS))
            return;

        //Vector3 facingDir = new Vector3(character.animator.GetFloat("moveX"), character.animator.GetFloat("moveY"));
        Vector3 facingDir = new Vector3(character.animator.MoveX, character.animator.MoveY);
        Vector3 interactPos = transform.position + facingDir;
        interactPos.y -= .5f;

        Collider2D collider = Physics2D.OverlapCircle(interactPos, 0.2f, GameLayers.Instance.Interactable);
        if (collider != null)
        {
            //call the interact function on the interactable
            collider.GetComponent<Interactable>()?.Interact();
        }
        else if (GameStateManager.Instance.EqualsState(OpenWorldState.DIALOG))
        {
            DialogManager.Instance.CloseDialog();
        }
    }
    
    //change scene after moving into a portal
    public void ChangeScenes()
    {
        Collider2D portalCollider = Physics2D.OverlapCircle(new Vector3(transform.position.x, transform.position.y - .5f), .2f, GameLayers.Instance.Portal);
        if (portalCollider != null)
        {
            GameStateManager.Instance.openWorldState = OpenWorldState.SCENECHANGING;
            portalCollider.GetComponent<PortalController>().OnInteractPortal();
        }
    }
    void OnEnable()
    {

        UIInputManager.OnDpadUp += MoveInput;
        UIInputManager.OnDpadDown += MoveInput;
        UIInputManager.OnDpadLeft += MoveInput;
        UIInputManager.OnDpadRight += MoveInput;

        InteractButton.OnInteract += Interact;

        UIInputManager.OnDpadCancelled += MoveInput;

    }

    void OnDisable()
    {
        UIInputManager.OnDpadUp -= MoveInput;
        UIInputManager.OnDpadDown -= MoveInput;
        UIInputManager.OnDpadLeft -= MoveInput;
        UIInputManager.OnDpadRight -= MoveInput;

        InteractButton.OnInteract -= Interact;

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
