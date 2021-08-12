using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] string portalDestination;
    [SerializeField] public string prevScene;

    [SerializeField] GamePreferencesManager playerPrefs;

    public void OnInteractPortal()
    {
        playerPrefs.SavePrefs(prevScene);
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
