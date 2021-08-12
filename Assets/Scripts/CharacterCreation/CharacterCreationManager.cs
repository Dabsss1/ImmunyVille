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
            maleController.GetComponent<Animator>().SetBool("isMoving",true);
            femaleController.GetComponent<Animator>().SetBool("isMoving", false);
            gender = "male";
        }
        else if (button == "Circle")
        {
            malePanel.SetActive(false);
            femalePanel.SetActive(true);
            maleController.GetComponent<Animator>().SetBool("isMoving", false);
            femaleController.GetComponent<Animator>().SetBool("isMoving", true);
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

            PlayerData.gender = gender;
            PlayerData.playerName = playerName;
            SaveSystem.SavePlayer();
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


}
