using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] string portalDestination;


    public void OnInteractPortal()
    {
        PlayerSceneInformation.Instance.previousScene = SceneInitiator.Instance.sceneName;
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
