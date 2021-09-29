using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene = "CharacterCreation";
    public string continueGameScene = "PlayerLot";


    private void Awake()
    {
        SaveSystem.LoadPlayer();
    }

    private void Start()
    {
        GameManagerScript.sceneState = SceneState.MAINMENU;
        Debug.Log(PlayerData.scene);
        continueGameScene = PlayerData.scene;
    }

    public void NewGame ()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue ()
    {
        GamePreferencesManager.OnSavePrefs?.Invoke("MainMenu");
        SceneLoaderManager.OnSceneLoad(continueGameScene);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
