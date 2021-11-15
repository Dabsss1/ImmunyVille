using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainGenerator : MonoBehaviour
{
    public static RainGenerator Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
