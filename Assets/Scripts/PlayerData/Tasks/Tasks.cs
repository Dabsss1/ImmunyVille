using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    public List<TaskSlot> taskSlots;

    public bool walkdone = false;

    public static Tasks Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void CompleteTask(TaskItem taskItem)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task.taskName == taskItem.taskName)
            {
                if (slot.progressCounter + 1 >= slot.task.taskInstructions.Count)
                {
                    slot.done = true;
                    slot.inProgress = false;

                    if (slot.task.rewardItem != null)
                        Inventory.Instance.ObtainItem(slot.task.rewardItem,slot.task.quantity);

                    if (slot.repeatable)
                    {
                        slot.repeatTimer = slot.task.daysToRedo;
                    }

                    if(slot.task.taskName == "Farm and Dog")
                    {
                        SpecialCounters.Instance.ObtainFarmAndDog();
                    }

                    if (slot.task.taskName == "Drink Water")
                    {
                        Stats.Instance.body += 30;
                        Stats.Instance.health += 30;
                    }

                    if (slot.task.taskName == "Walk 500steps")
                    {
                        if (walkdone)
                            return;
                        else
                        {
                            Stats.Instance.body += 30;
                            Stats.Instance.health += 30;
                            Stats.Instance.strength += 30;
                            Stats.Instance.confidence += 30;
                            walkdone = true;
                        }
                        
                    }
                }
                else
                {
                    slot.progressCounter++;
                }

                return;
            }
        }
    }

    public void InitiateTask(TaskItem taskItem)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task == taskItem)
            {
                slot.inProgress = true;
            }
        }
    }
    public int TaskProgressCounter(TaskItem task)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task == task)
            {
                return slot.progressCounter;
            }
        }
        Debug.Log("ProgressCounter not found!");
        return 0;
    }
    public bool TaskDone(TaskItem taskItem)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task == taskItem)
            {
                if (slot.done)
                    return true;
                else
                    return false;
            }
        }
        Debug.Log("Task not found!");
        return false;
    }

    public bool TaskInProgress(TaskItem taskItem)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task == taskItem)
            {
                if (slot.inProgress)
                    return true;
                else
                    return false;
            }
        }
        Debug.Log("Task not found!");
        return false;
    }

    public bool TaskNotStartedNorDone(TaskItem taskItem)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task == taskItem)
            {
                if (slot.done || slot.inProgress)
                    return false;
                else
                    return true;
            }
        }
        Debug.Log("Task not found! Method: Task not started nor done");
        return false;
    }

    public void ShowTaskDialog(TaskItem taskItem, string npcName)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.task == taskItem)
            {
                DialogManager.Instance.showQuestDialog(taskItem.questDialogs[slot.progressCounter],npcName,taskItem);
            }
        }
        
    }

    public bool IsQuestNpc(string npcName)
    {
        foreach(TaskSlot slot in taskSlots)
        {
            if (slot.task.questNpcs == null || slot.task.questNpcs.Count == 0)
                continue;
            if (slot.repeatable)
                continue;
            if (slot.inProgress && slot.task.questNpcs[slot.progressCounter] == npcName && (!slot.done))
            {
                DialogManager.Instance.showQuestDialog(slot.task.questDialogs[slot.progressCounter], npcName, slot.task);
                return true;
            }
        }
        return false;
    }

    public void PlayQuestNpc(string npcName)
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.repeatable)
                continue;
            if (slot.inProgress && slot.task.questNpcs[slot.progressCounter] == npcName)
            {
                DialogManager.Instance.showQuestDialog(slot.task.questDialogs[slot.progressCounter], npcName, slot.task);
            }
        }
    }

    public void ResetData()
    {
        foreach (TaskSlot slot in taskSlots)
        {
            if (slot.repeatable)
            {
                slot.repeatTimer = 0;
                slot.done = false;
                slot.inProgress = true;
            }
            else
            {
                slot.done = false;
                slot.inProgress = false;
            }
            slot.progressCounter = 0;
        }
    }
}

[System.Serializable]
public class TaskSlot
{
    [Header("Task Progress")]
    public TaskItem task;
    public bool done;
    public bool inProgress;
    public int progressCounter;

    [Header("Repeatable")]
    public bool repeatable;
    public int repeatTimer;
}
