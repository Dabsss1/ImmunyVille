using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StreetlightController : MonoBehaviour
{
    public int timeOn;
    public static StreetlightController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
    {
        if(TimeManager.Instance.hour < timeOn)
        {
            gameObject.SetActive(false);
        }
    }
}
