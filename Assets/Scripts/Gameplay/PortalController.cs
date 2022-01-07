using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] string portalDestination;
    [SerializeField] string sfx;

    [SerializeField] bool customPortal;
    [SerializeField] string previousDest;

    public void OnInteractPortal()
    {
        AudioManager.Instance.StopSfx("FootstepsOutdoor");
        AudioManager.Instance.StopSfx("FootstepsIndoor");


        AudioManager.Instance.PlaySfx(sfx);

        if(TimeManager.Instance.day == 20)
        {
            if (portalDestination == "TownSquare")
                portalDestination = "TSQuizBee";
        }
            
        if (customPortal)
        {
            SceneInitiator.Instance.sceneName = previousDest;
        }
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);

    }
}
