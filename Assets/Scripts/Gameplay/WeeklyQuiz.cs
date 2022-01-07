using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WeeklyQuiz : MonoBehaviour
{
    public TaskItem weeklyTask;
    public GameObject quizBeeCanvas;
    public GameObject resultsPanel;
    public GameObject contentPanel;

    public GameObject check;

    public InventoryItem itemReward;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI[] choicesText = new TextMeshProUGUI[3];

    [SerializeField] TextMeshProUGUI resultsText;

    [SerializeField] QuizBeeQuestions[] questions = new QuizBeeQuestions[5];

    [Header("Game Settings")]
    public int currentQuestion = 0;
    public int correctAnswer = 0;

    public static WeeklyQuiz Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void StartQuizBee()
    {
        quizBeeCanvas.SetActive(true);
        Shuffle(questions);
        SetQuestion();
    }

    public void Shuffle(QuizBeeQuestions[] questions)
    {
        QuizBeeQuestions temp;

        for (int i = 0; i < questions.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, questions.Length);
            temp = questions[rnd];
            questions[rnd] = questions[i];
            questions[i] = temp;
        }
    }

    public void SetQuestion()
    {
        questionText.text = questions[currentQuestion].question;
        for (int i = 0; i < choicesText.Length; i++)
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
        Stats.Instance.confidence += 50;

        Tasks.Instance.CompleteTask(weeklyTask);
    }
}
