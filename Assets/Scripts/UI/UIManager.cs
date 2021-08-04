using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject PhonePanel;

    private void OnEnable()
    {
        UIInputManager.OnSquareButton += DisplayPhonePanel;
        
    }

    private void OnDisable()
    {
        UIInputManager.OnSquareButton -= DisplayPhonePanel;
    }

    void DisplayPhonePanel(string button)
    {
        if (!PhonePanel.activeSelf)
            PhonePanel.SetActive(true);
        else
            PhonePanel.SetActive(false);
    }

}
