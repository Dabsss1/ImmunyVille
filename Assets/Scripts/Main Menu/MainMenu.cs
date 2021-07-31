using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame ()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public void Continue ()
    {
        Debug.Log("Not Done yet but working");
    }
}
