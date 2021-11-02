using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutscenePart
{
    public CharacterController character;

    [Header("Movement")]
    public List<Vector2> movement;

    [Header("Dialog")]
    public Dialogs dialog;
}