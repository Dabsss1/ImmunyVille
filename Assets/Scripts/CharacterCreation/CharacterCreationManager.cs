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


    public void setActivePanel(string button)
    {
        if (button == "male")
        {
            malePanel.SetActive(true);
            femalePanel.SetActive(false);
            maleController.GetComponent<CharacterController>().isMoving = true;
            femaleController.GetComponent<CharacterController>().isMoving = false;
            gender = "male";
        }
        else if (button == "female")
        {
            malePanel.SetActive(false);
            femalePanel.SetActive(true);
            maleController.GetComponent<CharacterController>().isMoving = false;
            femaleController.GetComponent<CharacterController>().isMoving = true;
            gender = "female";
        }

    }

    public void confirm()
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

    public void Return()
    {
        SceneLoaderManager.OnSceneLoad("MainMenu");
    }
}
