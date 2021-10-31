using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcController : MonoBehaviour, Interactable
{
    public static Action OnInteractNpc;

    public string characterName;
    [SerializeField] Dialogs dialog;

    CharacterController characterController;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Interact()
    {
        GameStateManager.Instance.ChangeGameState(OpenWorldState.DIALOG);
        OnInteractNpc?.Invoke();
        DialogManager.Instance.showDialog(dialog,characterName);

        Vector3 faceDir = new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y);
        faceDir = faceDir - transform.position;

        if (faceDir.x != 0)
        {
            characterController.setFaceDir((int)faceDir.x, (int)faceDir.y);
        }
            
        else if (faceDir.y != 0)
            characterController.setFaceDir((int)faceDir.x, (int)faceDir.y);
    }
}
