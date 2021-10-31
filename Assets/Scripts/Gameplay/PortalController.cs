using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] string portalDestination;


    public void OnInteractPortal()
    {
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
