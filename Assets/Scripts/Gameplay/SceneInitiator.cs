using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitiator : MonoBehaviour
{
    public string sceneName;

    public string sceneState;


    public static SceneInitiator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    private void Start()
    {
        if (PlayerSceneInformation.Instance.QuickSave)
        {
            LoadQuickSavePlayer();
        }

        switch (sceneState)
        {
            case "mainmenu":
                break;
            case "cutscene":
                break;
            case "openworld":
                SpawnPlayerOpenWorld();
                break;
            case "minigame":
                DestroyPlayerGameObject();
                break;
            default:
                Debug.LogError("SceneInformation script Error: SceneState not set");
                break;
        }
    }

    void SpawnPlayerOpenWorld()
    {
        if (Player.Instance == null)
            Spawner.Instance.SpawnPlayer();
        else
            Spawner.Instance.RepositionPlayer();

        changeState();
    }

    void LoadQuickSavePlayer()
    {
        Spawner.Instance.LoadQuickSave();
    }

    void DestroyPlayerGameObject()
    {
        if (Player.Instance == null)
            return;
        Destroy(Player.Instance.gameObject);
    }

    static void changeState()
    {
        GameStateManager.state = OpenWorldState.EXPLORE;
    }
}
