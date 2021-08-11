using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string newGameScene = "CharacterCreation";
    public string continueGameScene = "PlayerLot";

    public void NewGame ()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void Continue ()
    {
        SceneLoaderManager.OnSceneLoad(continueGameScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
