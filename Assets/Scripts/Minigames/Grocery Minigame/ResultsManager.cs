using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    [SerializeField] string portalDestination;

    public void GameFinish()
    {
        PlayerSceneInformation.Instance.previousScene = "GroceryGame";
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
