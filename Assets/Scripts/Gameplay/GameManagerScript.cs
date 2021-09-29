using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenWorldState { EXPLORE, DIALOG, SETTINGS, SCENECHANGING}
public enum SceneState { MAINMENU, CUTSCENE, OPENWORLD, MINIGAME}
public class GameManagerScript : MonoBehaviour
{
    public static OpenWorldState state;

    public static SceneState sceneState;

    public static GameManagerScript Instance { get; private set; }
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
}
