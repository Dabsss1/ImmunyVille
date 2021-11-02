using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName =("task/create new task"))]
[System.Serializable]
public class TaskItem : ScriptableObject
{
    [Header("Task Info")]
    public string taskName;
    public List<string> taskInstructions = new List<string>();

    [Header("Repeatable")]
    public int daysToRedo;

    [Header("Quest Informations")]
    public InventoryItem rewardItem;
    public int quantity;
    public List<string> questNpcs;
    public List<Dialogs> questDialogs;
}
