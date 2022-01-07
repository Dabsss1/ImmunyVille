using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseShelve : MonoBehaviour, Interactable
{
    [SerializeField] InformationTipsItem tips;
    [SerializeField] TaskItem quizTask;

    public void Interact()
    {
        if (Tasks.Instance.TaskInProgress(quizTask))
        {
            WeeklyQuiz.Instance.StartQuizBee();
            return;
        }
        int rnd = UnityEngine.Random.Range(0, tips.tips.Count);

        DialogManager.Instance.showDialog(tips.tips[rnd]);
    }
}
