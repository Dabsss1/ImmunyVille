using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string newGameScene = "CharacterCreation";
    
    
    //Loading save data
    public static bool dataLoaded = false;
    [Header("Continue UI Settings")]
    [SerializeField] GameObject playerDataPanel;
    [SerializeField] Sprite maleSprite, femaleSprite;
    [SerializeField] Image avatarIcon;
    [SerializeField] Text playerNameTextField, playerDateInfo;
    [SerializeField] GameObject loadingText;

    [SerializeField] Animator menuButtonsAnimator;

    private void Start()
    {
        menuButtonsAnimator.SetTrigger("Startup");
    }

    public void NewGame ()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue ()
    {
        StartCoroutine(LoadPlayerData());
        //SceneLoaderManager.OnSceneLoad(continueGameScene);
    }

    IEnumerator LoadPlayerData()
    {
        SaveSystem.LoadPlayer();
        while (dataLoaded == false)
        {
            yield return null;
        }

        loadingText.SetActive(false);
        playerDataPanel.SetActive(true);

        if (PlayerDataManager.Instance.gender == "male")
            avatarIcon.sprite = maleSprite;
        else
            avatarIcon.sprite = femaleSprite;

        playerNameTextField.text = PlayerDataManager.Instance.playerName;
        playerDateInfo.text = $"{TimeManager.Instance.season} Season - Day {TimeManager.Instance.day}";
    }

    public void ContinuePlayer()
    {
        SceneLoaderManager.OnSceneLoad(PlayerDataManager.Instance.savedScene);
        PlayerSceneInformation.Instance.fromContinue = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
