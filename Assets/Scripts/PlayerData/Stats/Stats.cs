using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float confidence;
    public float strength;
    public float body;

    public static Stats Instance { get; private set; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
