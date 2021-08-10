using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject allUI;
    public GameObject PhonePanel;

    public static Action OnScreenLoad;

    private void OnEnable()
    {
        UIInputManager.OnSquareButton += DisplayPhonePanel;
        OnScreenLoad += DisableUI;
    }

    private void OnDisable()
    {
        UIInputManager.OnSquareButton -= DisplayPhonePanel;
        OnScreenLoad -= DisableUI;
    }

    void DisplayPhonePanel(string button)
    {
        if (!PhonePanel.activeSelf)
            PhonePanel.SetActive(true);
        else
            PhonePanel.SetActive(false);
    }

    void DisableUI()
    {
        allUI.SetActive(false);
    }
}
