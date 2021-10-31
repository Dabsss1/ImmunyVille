using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenWorldState { EXPLORE, DIALOG, SETTINGS, SCENECHANGING}
public enum SceneState { MAINMENU, CUTSCENE, OPENWORLD, MINIGAME}
public class GameStateManager : MonoBehaviour
{
    public OpenWorldState openWorldState;

    public SceneState sceneState;

    public static GameStateManager Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ChangeGameState(OpenWorldState state)
    {
        openWorldState = state;
    }

    public void ChangeGameState(SceneState state)
    {
        sceneState = state;
    }

    public bool EqualsState(OpenWorldState state)
    {
        if (openWorldState == state)
            return true;
        else
            return false;
    }

    public bool EqualsState(SceneState state)
    {
        if (sceneState == state)
            return true;
        else
            return false;
    }
}
