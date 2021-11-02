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
        GameStateManager.Instance.ChangeGameState(OpenWorldState.DIALOG);
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
        GameStateManager.Instance.ChangeGameState(OpenWorldState.DIALOG);
        dialogBox.SetActive(true);
        this.characterName.text = "";
        currentInformation = information;
        typingCoroutine = TypeDialog(information);
        StartCoroutine(typingCoroutine);
    }

    public void showDialog(string information,string playerName)
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
        GameStateManager.Instance.ChangeGameState(OpenWorldState.DIALOG);
        dialogBox.SetActive(true);
        this.characterName.text = playerName;
        currentInformation = information;
        typingCoroutine = TypeDialog(information);
        StartCoroutine(typingCoroutine);
    }

    public void showQuestDialog(Dialogs dialog, string npcName,TaskItem task)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            string message1 = SetDialogSpeaker(dialog.Lines[currentline - 1],npcName);
            dialogText.text = message1;
            isTyping = false;
            return;
        }
        if (currentline >= dialog.Lines.Count)
        {
            GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
            dialogBox.SetActive(false);
            currentline = 0;
            Tasks.Instance.CompleteTask(task);
            return;
        }
        GameStateManager.Instance.ChangeGameState(OpenWorldState.DIALOG);
        dialogBox.SetActive(true);
        Tasks.Instance.InitiateTask(task);
        string message = SetDialogSpeaker(dialog.Lines[currentline],npcName);
        typingCoroutine = TypeQuestDialog(message);
        StartCoroutine(typingCoroutine);
        currentline++;
    }

    IEnumerator TypeQuestDialog(string line)
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

    public string SetDialogSpeaker(string line,string npcName)
    {
        if (line.StartsWith("p:"))
        {
            line = line.Replace("p:", "");
            this.characterName.text = PlayerDataManager.Instance.playerName;
        }
        else if (line.StartsWith("n:"))
        {
            line = line.Replace("n:", "");
            this.characterName.text = npcName;
        }
        else if (line.StartsWith("s:"))
        {
            line = line.Replace("s:", "");
            this.characterName.text = "";
        }
        else
        {
            Debug.Log("No speaker detected... Line: " + line);
        }

        if (line.Contains("***"))
            line = line.Replace("***",PlayerDataManager.Instance.playerName);
        return line;
    }
}
