using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerThirst : MonoBehaviour
{
    public float hungerStat;
    public float thirstStat;

    public static HungerThirst Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
