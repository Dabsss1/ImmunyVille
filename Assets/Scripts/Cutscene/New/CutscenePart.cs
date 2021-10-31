using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutscenePart
{
    public CharacterController character;

    [Header("Movement")]
    public List<NpcMovement> movement;

    [Header("Dialog")]
    public Dialogs dialog;
}

[System.Serializable]
public class NpcMovement 
{
    public int MoveX;
    public int MoveY;
}