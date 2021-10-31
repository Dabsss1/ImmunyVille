using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] TextMeshProUGUI characterName;

    [SerializeField] int lettersPerSecond = 1;

    int currentline=0;
    bool isTyping=false;
    private IEnumerator typingCoroutine;

    string currentInformation = "";

    public static DialogManager Instance { get; private set; }


    void Awake()
    {
        Instance = this;
    }
    public void showDialog(Dialogs dialog, string characterName)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = dialog.Lines[currentline - 1];
            isTyping = false;
            return;
        }
        if (currentline>=dialog.Lines.Count)
        {
            GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
            dialogBox.SetActive(false);
            currentline = 0;
            return;
        }
            
        dialogBox.SetActive(true);
        this.characterName.text = characterName;
        typingCoroutine = TypeDialog(dialog.Lines[currentline]);
        StartCoroutine(typingCoroutine);
        currentline++;
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
        currentInformation = "";
    }
    
    public void showDialog(string information)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = currentInformation;
            currentInformation = "";
            isTyping = false;
            return;
        }
        if (dialogBox.activeSelf)
        {
            GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
            dialogBox.SetActive(false);
            currentInformation = "";
            return;
        }

        dialogBox.SetActive(true);
        currentInformation = information;
        typingCoroutine = TypeDialog(information);
        StartCoroutine(typingCoroutine);
    }
}
