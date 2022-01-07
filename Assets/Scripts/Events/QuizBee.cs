using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuizBee : MonoBehaviour
{
    public GameObject quizBeeCanvas;
    public GameObject resultsPanel;
    public GameObject contentPanel;

    public GameObject check;

    public InventoryItem itemReward;

    [SerializeField] int quizBeeStart;

    [SerializeField] CharacterController[] npcs;
    [SerializeField] GameObject npcQuizBee;
    [SerializeField] GameObject npcExplore;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI[] choicesText = new TextMeshProUGUI[3];

    [SerializeField] TextMeshProUGUI resultsText;

    [SerializeField] QuizBeeQuestions[] questions = new QuizBeeQuestions[5];

    [Header("Game Settings")]
    public int currentQuestion = 0;
    public int correctAnswer = 0;

    void Start()
    {
        
    }

    private void OnEnable()
    {
        TimeManager.OnHourChanged += StartQuizBee;
    }

    private void OnDisable()
    {
        TimeManager.OnHourChanged -= StartQuizBee;
    }

    void StartQuizBee()
    {
        if (TimeManager.Instance.hour != quizBeeStart)
            return;

        UIManager.OnStartQuizBee?.Invoke();
        quizBeeCanvas.SetActive(true);

        StartCoroutine(RepositionPlayer());

        
    }

    IEnumerator RepositionPlayer()
    {
        Vector3 spawnPosition = new Vector3(4.5f, -1, 0);
        yield return new WaitForSeconds(1f);
        Player.Instance.transform.position = spawnPosition;
        Player.Instance.character.setFaceDir(0, 1);

        npcExplore.SetActive(false);
        npcQuizBee.SetActive(true);
        foreach (CharacterController c in npcs)
        {
            c.setFaceDir(0, 1);
        }

        SetQuestion();
    }

    
    public void SetQuestion()
    {
        questionText.text = questions[currentQuestion].question;
        for(int i = 0; i < choicesText.Length; i++)
        {
            choicesText[i].text = questions[currentQuestion].choices[i];
        }
    }
    public void SelectAnswer(int choice)
    {
        

        if (choice == questions[currentQuestion].correctAnswer)
        {
            correctAnswer++;
            StartCoroutine(CheckConfirm());
        }
        else
        {
            
        }

        currentQuestion++;
        if (currentQuestion >= 5)
        {
            contentPanel.SetActive(false);
            resultsPanel.SetActive(true);

            resultsText.text = $"Correct Answers: {correctAnswer}/5";
        }
        SetQuestion();

    }

    IEnumerator CheckConfirm()
    {
        check.SetActive(true);
        yield return new WaitForSeconds(1f);
        check.SetActive(false);
    }

    public void EndQuizBee()
    {
        quizBeeCanvas.SetActive(false);
        UIManager.OnEndQuizBee?.Invoke();
        npcExplore.SetActive(true);
        npcQuizBee.SetActive(false);

        TimeManager.Instance.hour = 16;
        Inventory.Instance.ObtainItem(itemReward,10);

        foreach (CharacterController c in npcs)
        {
            c.setFaceDir(0, -1);
        }
    }
}

[System.Serializable]
public class QuizBeeQuestions
{
    public string question;
    public string[] choices = new string[3];
    public int correctAnswer;
}
