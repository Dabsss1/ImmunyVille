using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameLoader : MonoBehaviour, Interactable
{
    [SerializeField] string minigameScene;
    // Start is called before the first frame update
    public void Interact()
    {
        SceneLoaderManager.OnMinigameLoad?.Invoke(minigameScene);
    }
}