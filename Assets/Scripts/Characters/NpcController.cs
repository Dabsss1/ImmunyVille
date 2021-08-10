using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcController : MonoBehaviour, Interactable
{
    public static Action OnInteractNpc;

    [SerializeField] Dialogs dialog;
    public void Interact()
    {
        Debug.Log("talked to npc");
        GameManagerScript.state = OpenWorldState.DIALOG;
        OnInteractNpc?.Invoke();
        DialogManager.Instance.showDialog(dialog);

    }
}
