using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PopupIndicator : MonoBehaviour
{
    IEnumerator popupCoroutine;
    [SerializeField] GameObject popupGO;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI text;
    bool isDisplayed;
    IEnumerator displayCoroutine;

    [SerializeField] Sprite taskIcon;
    [SerializeField] Sprite inventory;
    [SerializeField] Sprite stats;
    [SerializeField] Sprite money;

    public static Action<string,string> OnObtain;

    private void OnEnable()
    {
        OnObtain += DisplayPopupIndicator;
    }
    private void OnDisable()
    {
        OnObtain -= DisplayPopupIndicator;
    }

    void DisplayPopupIndicator(string type, string text)
    {
        Sprite popupIcon;
        switch (type)
        {
            case "task":
                popupIcon = taskIcon;
                break;
            case "inventory":
                popupIcon = inventory;
                break;
            case "stats":
                popupIcon = stats;
                break;
            case "money":
                popupIcon = money;
                break;
            default:
                popupIcon = inventory;
                break;
        }

        if (!isDisplayed)
        {
            displayCoroutine = DisplayIndicator(popupIcon, text);
            StartCoroutine(displayCoroutine);
        }
        else
        {
            StopCoroutine(displayCoroutine);
            displayCoroutine = DisplayIndicator(popupIcon, text);
            StartCoroutine(displayCoroutine);
        }
    }

    IEnumerator DisplayIndicator(Sprite icon, string text)
    {
        isDisplayed = true;

        popupGO.SetActive(true);
        this.icon.sprite = icon;
        this.text.text = text;
        yield return new WaitForSeconds(3f);
        popupGO.SetActive(false);
        isDisplayed = false;
    }
}
