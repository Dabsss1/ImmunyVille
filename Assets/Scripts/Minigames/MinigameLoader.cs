using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLoader : MonoBehaviour, Interactable
{
    [SerializeField] string minigameScene;
    [SerializeField] TaskItem task;

    [SerializeField] string doneMessage;
    // Start is called before the first frame update
    public void Interact()
    {
        if (!Tasks.Instance.TaskDone(task))
        {
            GameStateManager.Instance.ChangeGameState(OpenWorldState.SCENECHANGING);
            SceneLoaderManager.OnMinigameLoad?.Invoke(minigameScene);
        }
        else
            DialogManager.Instance.showDialog(doneMessage,PlayerDataManager.Instance.playerName);        
    }
}
