using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class CutsceneDialogs
{
    [SerializeField] List<String> lines;


    public List<string> Lines
    {
        get { return lines; }
    }

}