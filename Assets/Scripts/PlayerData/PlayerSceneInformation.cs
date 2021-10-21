using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSceneInformation : MonoBehaviour
{
    public string previousScene = "";

    public bool QuickSave = false;

    public static PlayerSceneInformation Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
