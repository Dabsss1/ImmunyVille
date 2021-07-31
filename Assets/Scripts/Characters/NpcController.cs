using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogs dialog;
    public void Interact()
    {
        DialogManager.Instance.showDialog(dialog);
    }


    void OnEnable()
    {
        Player.NextDialog += DialogManager.Instance.NextDialog;
    }

    void OnDisable()
    {
        Player.NextDialog -= DialogManager.Instance.NextDialog;
    }
}
