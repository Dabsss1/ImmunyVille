using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartController : MonoBehaviour, Interactable
{
    [SerializeField] string minigameScene;
    public void Interact()
    {
        PlayerData data = new PlayerData();
        Debug.Log("Loading Minigame");
        SceneLoaderManager.OnMinigameLoad?.Invoke(minigameScene);
    }
}
