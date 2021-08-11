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
        StartCoroutine(TypeDialog(replaceNames(dialog.Lines[currentline].Substring(2))));
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
        line = line.Replace("Billy", PlayerData.playerName);

        if (PlayerData.gender == "female")
            line = line.Replace("Alice", "Billy");

        return line;
    }
}
