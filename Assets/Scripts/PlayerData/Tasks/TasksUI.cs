using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksUI : MonoBehaviour
{
    [SerializeField] GameObject contentGO;
    [SerializeField] TaskBlueprint taskBlueprint;

    private void Start()
    {
        UpdateTasksUI();
    }

    public void UpdateTasksUI()
    {
        foreach (Transform child in contentGO.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (TaskSlot slot in Tasks.Instance.taskSlots)
        {
            if ((slot.inProgress && slot.task.taskInstructions.Count != 0 )|| slot.repeatable)
            {
                TaskBlueprint taskBlueprintSlot = Instantiate(taskBlueprint, contentGO.transform);
                taskBlueprintSlot.SetData(slot);
            }
        }
    }

    private void OnEnable()
    {
        Debug.Log("Updating");
        UpdateTasksUI();
    }
}
