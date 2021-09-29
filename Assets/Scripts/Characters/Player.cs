using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //player stats
    [HideInInspector]
    public string gender;
    [HideInInspector]
    public string playerName;

    //character controller
    [HideInInspector]
    public CharacterController character;

    //input
    Vector2 inputPos;

    //dialog
    [HideInInspector]
    public GameObject dialogBox;

    //status
    public bool isSpawned = false;
    public string lastScene = "PlayerLot";
    public static Player Instance { get; private set; }

    private void Start()
    {
        gender = PlayerData.gender;
        playerName = PlayerData.playerName;
    }

    void Awake()
    {
        character = GetComponent<CharacterController>();

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //check if the state is in open world before moving
        if (GameManagerScript.sceneState == SceneState.MINIGAME)
        {
            gameObject.SetActive(false);
            return;
        }
        else if (GameManagerScript.sceneState != SceneState.OPENWORLD)
        {
            Destroy(gameObject);
            return;
        }

        //stop any movement when player is changing scenes
        if (GameManagerScript.state == OpenWorldState.SCENECHANGING)
            return;

        if (!character.isMoving)
        {
            if (inputPos != Vector2.zero)
            {
                IsPortal(inputPos);

                StartCoroutine(character.Move(inputPos));

                if (GameManagerScript.state != OpenWorldState.SCENECHANGING)
                    ChangeScenes();
            }
        }
        character.animator.SetBool("isMoving", character.isMoving);
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
        if (GameManagerScript.state != OpenWorldState.SCENECHANGING)
        {
            Debug.Log("Entering");
            portalCollider.GetComponent<PortalController>().OnInteractPortal();
        }
            
        GameManagerScript.state = OpenWorldState.SCENECHANGING;
        yield return null;
    }


    //interact the tile on front
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
            //call the interact function on the interactable
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
    
    //change scene after moving into a portal
    public void ChangeScenes()
    {
        Collider2D portalCollider = Physics2D.OverlapCircle(new Vector3(transform.position.x, transform.position.y - .5f), .2f, GameLayers.Instance.Portal);
        if (portalCollider != null)
        {
            GameManagerScript.state = OpenWorldState.SCENECHANGING;
            portalCollider.GetComponent<PortalController>().OnInteractPortal();
        }
    }
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
