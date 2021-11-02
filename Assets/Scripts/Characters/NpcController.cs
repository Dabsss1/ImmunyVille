using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NpcController : MonoBehaviour, Interactable
{
    [Header("Npc Profile")]
    public string characterName;
    [SerializeField] Dialogs dialog;

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
        if (canBeInitiated && (!Tasks.Instance.TaskDone(task)) && Tasks.Instance.TaskProgressCounter(task) == 0)
        {
            Tasks.Instance.ShowTaskDialog(task,characterName);
        }
        else if(Tasks.Instance.IsQuestNpc(characterName))
        {
            //Tasks.Instance.PlayQuestNpc(characterName);
        }
        else
            DialogManager.Instance.showDialog(dialog, characterName);

        FacePlayerCharacter();
    }

    void FacePlayerCharacter()
    {
        Vector3 faceDir = new Vector2(Player.Instance.transform.position.x, Player.Instance.transform.position.y);
        faceDir = faceDir - transform.position;
        characterController.setFaceDir((int)faceDir.x, (int)faceDir.y);
    }
}
