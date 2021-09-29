using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] string portalDestination;
    [SerializeField] public string prevScene;

    [SerializeField] GamePreferencesManager playerPrefs;

    public void GameFinish()
    {
        playerPrefs.SavePrefs(prevScene);
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
