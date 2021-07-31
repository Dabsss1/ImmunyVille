    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterCreationManager : MonoBehaviour
{
    public GameObject malePanel,femalePanel,
        maleSprite,femaleSprite;

    public GameObject maleController, femaleController;
    void setActivePanel(string button)
    {
        if (button == "Square")
        {
            malePanel.SetActive(true);
            femalePanel.SetActive(false);

            maleController.GetComponent<Animator>().SetBool("isMoving",true);
            femaleController.GetComponent<Animator>().SetBool("isMoving", false);
        }
        else if (button == "Circle")
        {
            malePanel.SetActive(false);
            femalePanel.SetActive(true);

            maleController.GetComponent<Animator>().SetBool("isMoving", false);
            femaleController.GetComponent<Animator>().SetBool("isMoving", true);
        }

    }

    void confirm(string button)
    {
        SceneManager.LoadScene("NewGame");
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

    private void Awake()
    {
    }

}
