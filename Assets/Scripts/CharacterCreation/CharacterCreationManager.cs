    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreationManager : MonoBehaviour
{
    //player data
    public string gender, playerName;

    public GameObject malePanel,femalePanel,
        maleSprite,femaleSprite;

    public GameObject maleController, femaleController;
    public InputField nameField;

    public string nextScene = "PlayerLot";

    PlayerData data = new PlayerData();

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
        if (gender == null)
        {
            Debug.Log("Gender not set");
        }
        else if (string.IsNullOrWhiteSpace(name))
        {
            Debug.Log("Input Name");
        }
        else
        {
            data.gender = gender;
            data.playerName = playerName;
            SceneLoaderManager.OnSceneLoad(nextScene, data);
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
