using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcController : MonoBehaviour, Interactable
{
    [Header("Npc Profile")]
    public string characterName;
    [SerializeField] Dialogs dialog;

    [Header("Events")]
    [SerializeField] InformationTipsItem nutritionTips;

    [Header("Npc Quests")]
    public TaskItem task;
    public int taskProbability;
    public bool canBeInitiated;

    CharacterController characterController;
    
    
    void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        if (task == null)
        {
            canBeInitiated = false;
        }
        else if (Tasks.Instance.TaskNotStartedNorDone(task))
        {
            int rnd = UnityEngine.Random.Range(0, 100);
            if (rnd + 1 <= taskProbability)
                canBeInitiated = true;
            else
                canBeInitiated = false;
        }
    }

    public void Interact()
    {
        if (characterController.isMoving)
            return;

        if (TimeManager.Instance.day == 15)
        {
            int rnd = UnityEngine.Random.Range(0, nutritionTips.tips.Count);
            DialogManager.Instance.showDialog("Happy Nutrition Day! "+nutritionTips.tips[rnd]);
            return;
        }

        if (canBeInitiated && (!Tasks.Instance.TaskDone(task)) && Tasks.Instance.TaskProgressCounter(task) == 0)
        {
            Tasks.Instance.ShowTaskDialog(task,characterName);
            if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE) && GameStateManager.Instance.EqualsState(SceneState.OPENWORLD))
                PopupIndicator.OnObtain?.Invoke("task", "New task");
        }
        else if(Tasks.Instance.IsQuestNpc(characterName))
        {
            //Tasks.Instance.PlayQuestNpc(characterName);
        }
        else
        {
            if (GameStateManager.Instance.EqualsState(OpenWorldState.EXPLORE) && GameStateManager.Instance.EqualsState(SceneState.OPENWORLD))
                Stats.Instance.confidence += 2;
            DialogManager.Instance.showDialog(dialog, characterName);
        }

        FacePlayerCharacter();
    }

    void FacePlayerCharacter()
    {
        Vector3 faceDir = new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y);
        faceDir = faceDir - transform.position;
        characterController.setFaceDir((int)faceDir.x, (int)faceDir.y);
    }
}
