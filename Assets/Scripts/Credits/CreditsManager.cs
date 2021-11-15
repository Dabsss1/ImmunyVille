using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    public void OpenLink()
    {

        Debug.Log("pressed");
        Application.OpenURL("https://www.zapsplat.com");
    }

    public void ReturnToMainMenu()
    {

        Debug.Log("pressed");
        SceneManager.LoadScene("MainMenu");
    }
}
