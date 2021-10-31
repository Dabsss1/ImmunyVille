using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitiator : MonoBehaviour
{
    public string sceneName;

    public SceneState state;

    public static SceneInitiator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    private void Start()
    {
        GameStateManager.Instance.sceneState = state;

        if (PlayerSceneInformation.Instance.fromContinue)
        {
            LoadSavePlayer();
        }
        else
        {
            switch (state)
            {
                case SceneState.MAINMENU:
                    DestroyPlayerAndResetInfo();
                    break;
                case SceneState.CUTSCENE:
                    break;
                case SceneState.OPENWORLD:
                    SpawnPlayerOpenWorld();
                    break;
                case SceneState.MINIGAME:
                    DestroyPlayerGameObject();
                    break;
                default:
                    Debug.LogError("SceneInformation script Error: SceneState not set");
                    break;
            }
        }
    }

    void DestroyPlayerAndResetInfo()
    {
        if (Player.Instance != null)
            Destroy(Player.Instance.gameObject);

        PlayerDataManager.Instance.ResetData();
        Inventory.Instance.ResetData();
        Stats.Instance.ResetData();
        Badges.Instance.ResetData();
        HungerThirst.Instance.ResetData();
    }

    void SpawnPlayerOpenWorld()
    {
        if (Player.Instance == null)
            Spawner.Instance.SpawnPlayer();
        else
            Spawner.Instance.RepositionPlayer();

        GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
    }

    void LoadSavePlayer()
    {
        Spawner.Instance.LoadSavePlayer();
        PlayerSceneInformation.Instance.fromContinue = false;
    }

    void DestroyPlayerGameObject()
    {
        if (Player.Instance == null)
            return;
        Destroy(Player.Instance.gameObject);
    }

}
