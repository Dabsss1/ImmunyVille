using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneDialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] TextMeshProUGUI name;

    [SerializeField] int lettersPerSecond = 1;

    private IEnumerator typingCoroutine;
    int currentline = 0;
    bool isTyping = false;

    public static CutsceneDialogManager Instance { get; private set; }


    void Awake()
    {
        Instance = this;
    }
    
    public void showDialog(CutsceneDialogs dialog)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogText.text = replaceNames(dialog.Lines[currentline-1].Substring(2));
            isTyping = false;
            return;
        }
        if (currentline >= dialog.Lines.Count)
        {
            GameManagerScript.state = OpenWorldState.EXPLORE;
            dialogBox.SetActive(false);
            currentline = 0;
            CutsceneManager.OnDialogEnd?.Invoke();
            return;
        }

        dialogBox.SetActive(true);
        name.text = setDialogName(dialog.Lines[currentline]);
        typingCoroutine = TypeDialog(replaceNames(dialog.Lines[currentline].Substring(2)));
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
    }

    string setDialogName(string line)
    {
        if (line.StartsWith("b:"))
            return PlayerData.playerName;
        else if (line.StartsWith("n:"))
            return "Nurse";
        else if (line.StartsWith("d:"))
            return "Doctor";
        else if (line.StartsWith("a:"))
        {
            if (PlayerData.gender == "male")
                return "Alice";
            else
                return "Billy";
        }
        else
            return "Shin";

    }

    string replaceNames(string line)
    {
        Debug.Log("old line: " + line);
        line = line.Replace("playerName", PlayerData.playerName);
        Debug.Log("new line: " + line);
        if (PlayerData.gender == "female")
            line = line.Replace("partner", "Billy");
        else if(PlayerData.gender == "male")
            line = line.Replace("partner", "Alice");

        return line;
    }
}
