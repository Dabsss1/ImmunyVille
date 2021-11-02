using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] string portalDestination;
    [SerializeField] string sfx;


    public void OnInteractPortal()
    {
        AudioManager.Instance.PlaySfx(sfx);
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
