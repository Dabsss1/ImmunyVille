using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject allUI;
    public GameObject phonePanel;
    public GameObject inventoryPanel;
    public GameObject tutorialPanel;

    public GameObject[] panels;

    public static Action OnScreenLoad,OnStartQuizBee,OnEndQuizBee;

    private void OnEnable()
    {
        OnScreenLoad += DisableUI;
        OnStartQuizBee += DisableUI;
        OnEndQuizBee += EnableUI;
    }

    private void OnDisable()
    {
        OnScreenLoad -= DisableUI;
        OnStartQuizBee -= DisableUI;
        OnEndQuizBee -= EnableUI;
    }

    public void DisplayPhonePanel()
    {
        if (inventoryPanel.activeSelf || tutorialPanel.activeSelf)
            return;

        if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE)|| GameStateManager.Instance.EqualsState(OpenWorldState.SETTINGS))
        {
            if (!phonePanel.activeSelf)
            {
                GameStateManager.Instance.ChangeGameState(OpenWorldState.SETTINGS);
                phonePanel.SetActive(true);
            }
            else
            {
                foreach (GameObject panel in panels)
                {
                    if (panel.activeSelf)
                        return;
                }
                GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
                phonePanel.SetActive(false);
            }
        }
    }

    public void DisplayInventoryPanel()
    {
        if (phonePanel.activeSelf || tutorialPanel.activeSelf)
            return;

        if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE) || GameStateManager.Instance.EqualsState(OpenWorldState.SETTINGS))
        {
            GameStateManager.Instance.ChangeGameState(OpenWorldState.SETTINGS);
            inventoryPanel.SetActive(true);
        }
    }

    public void DisplayTutorialPanel()
    {
        if (phonePanel.activeSelf || inventoryPanel.activeSelf)
            return;

        if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE) || GameStateManager.Instance.EqualsState(OpenWorldState.SETTINGS))
        {
            GameStateManager.Instance.ChangeGameState(OpenWorldState.SETTINGS);
            tutorialPanel.SetActive(true);
        }
    }

    public void CloseTutorialPanel()
    {
        GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
        tutorialPanel.SetActive(false);
    }

    void DisableUI()
    {
        allUI.SetActive(false);
    }

    void EnableUI()
    {
        allUI.SetActive(true);
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

    public void InteractSound()
    {
        AudioManager.Instance.PlaySfx("Interact");
    }

    public void ExitSound()
    {
        AudioManager.Instance.PlaySfx("Exit");
    }
}
