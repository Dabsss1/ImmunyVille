using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogs
{
    [SerializeField] List<String> lines;

    public List<string> Lines
    {
        get { return lines; }
    }

    
}
