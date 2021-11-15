using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitiator : MonoBehaviour
{
    public string sceneName;

    public SceneState state;
    public bool outdoor;

    public string backgroundMusic;

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
                    DestroyPlayerGameObject();
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

        if (outdoor)
        {
            if(!TimeManager.Instance.raining)
                RainGenerator.Instance.gameObject.SetActive(false);
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
        Tasks.Instance.ResetData();
        Plants.Instance.ResetData();

        TimeManager.Instance.ResetData();
    }

    void SpawnPlayerOpenWorld()
    {
        if (Player.Instance == null)
            Spawner.Instance.SpawnPlayer();
        else
            Spawner.Instance.RepositionPlayer();

        if(sceneName!="Mountain")
            Spawner.Instance.SpawnDog();

        GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
    }

    void LoadSavePlayer()
    {
        GameStateManager.Instance.ChangeGameState(OpenWorldState.EXPLORE);
        Spawner.Instance.LoadSavePlayer();
        PlayerSceneInformation.Instance.fromContinue = false;
    }

    void DestroyPlayerGameObject()
    {
        if (Player.Instance == null)
            return;
        Destroy(Player.Instance.gameObject);
    }

    private void OnEnable()
    {
        AudioManager.Instance.Play(backgroundMusic);
    }

    private void OnDisable()
    {
        AudioManager.Instance.Stop(backgroundMusic);
    }
}
