using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInformation : MonoBehaviour
{
    [SerializeField]
    public string sceneName;

    [SerializeField]
    public string sceneState;

    private void Start()
    {

        switch (sceneState)
        {
            case "mainmenu":
                GameManagerScript.sceneState = SceneState.MAINMENU;
                break;
            case "cutscene":
                GameManagerScript.sceneState = SceneState.CUTSCENE;
                break;
            case "openworld":
                GameManagerScript.sceneState = SceneState.OPENWORLD;
                break;
            case "minigame":
                GameManagerScript.sceneState = SceneState.MINIGAME;
                break;
            default:
                Debug.LogError("SceneInformation script Error: SceneState not set");
                break;
                
        }
    }
}
