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

    public void QuickSave(GameObject prompt)
    {
        StartCoroutine(ShowPrompt(prompt));
        SaveSystem.SavePlayer();
    }

    public void ReturnToMainMenu()
    {
        SceneLoaderManager.OnSceneLoad?.Invoke("MainMenu");
    }

    public IEnumerator ShowPrompt(GameObject prompt)
    {
        prompt.SetActive(true);
        yield return new WaitForSeconds(1f);
        prompt.SetActive(false);
    }
}
