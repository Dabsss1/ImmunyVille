using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;

    [SerializeField] int lettersPerSecond = 1;

    int currentline=0;
    public static DialogManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }
    public void showDialog(Dialogs dialog)
    {
        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(dialog.Lines[currentline]));
    }

    public IEnumerator TypeDialog(string line)
    {
        dialogText.text = "";
        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void NextDialog(Dialogs dialog)
    {
        currentline++;
        if(currentline< dialog.Lines.Count)
            StartCoroutine(TypeDialog(dialog.Lines[0]));
        else
        {
            dialogBox.SetActive(false);
        }
    }
            
}
