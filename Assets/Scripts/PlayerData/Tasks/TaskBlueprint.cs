using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TaskBlueprint : MonoBehaviour
{
    public TextMeshProUGUI taskName;
    public TextMeshProUGUI taskInstruction;
    public TextMeshProUGUI taskProgress;

    public void SetData(TaskSlot slot)
    {
        taskName.text = slot.task.taskName;
        taskInstruction.text = slot.task.taskInstructions[slot.progressCounter];

        if (slot.done)
        {
            taskProgress.color = Color.green;
            if (slot.repeatTimer > 1)
                taskProgress.text = "Task redo in " + slot.repeatTimer + " days";
            else
                taskProgress.text = "Task redo in " + slot.repeatTimer + " day";
        }
        else if (slot.inProgress)
        {
            taskProgress.color = Color.red;
            taskProgress.text = "In Progress";
        }
    }
}
