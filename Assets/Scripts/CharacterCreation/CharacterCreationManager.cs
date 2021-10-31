    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationManager : MonoBehaviour
{
    //player data
    public string gender="", playerName;

    public GameObject malePanel,femalePanel,
        maleSprite,femaleSprite;

    public GameObject maleController, femaleController;
    public InputField nameField;

    public string nextScene = "Introduction";


    void setActivePanel(string button)
    {
        if (button == "Square")
        {
            malePanel.SetActive(true);
            femalePanel.SetActive(false);
            maleController.GetComponent<CharacterController>().isMoving = true;
            femaleController.GetComponent<CharacterController>().isMoving = false;
            gender = "male";
        }
        else if (button == "Circle")
        {
            malePanel.SetActive(false);
            femalePanel.SetActive(true);
            maleController.GetComponent<CharacterController>().isMoving = false;
            femaleController.GetComponent<CharacterController>().isMoving = true;
            gender = "female";
        }

    }

    void confirm(string button)
    {
        playerName = nameField.text;
        if (gender == "")
        {
            Debug.Log("Gender not set");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(playerName))
            {
                if (gender == "male")
                    playerName = "Billy";
                else if (gender == "female")
                    playerName = "Alice";
            }

            PlayerDataManager.Instance.gender = gender;
            PlayerDataManager.Instance.playerName = playerName;
            SceneLoaderManager.OnSceneLoad(nextScene);
        }
    }

    private void OnEnable()
    {
        UIInputManager.OnSquareButton += setActivePanel;
        UIInputManager.OnCircleButton += setActivePanel;
        UIInputManager.OnStartButton += confirm;
    }

    private void OnDisable()
    {
        UIInputManager.OnSquareButton -= setActivePanel;
        UIInputManager.OnCircleButton -= setActivePanel;
        UIInputManager.OnStartButton -= confirm;
    }

    public void Return()
    {
        SceneLoaderManager.OnSceneLoad("MainMenu");
    }
}
