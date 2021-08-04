using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer,
        interactableLayer,
        portalLayer,
        doorPortal;

    public static GameLayers Instance
    {
        get; set;
    }

    private void Awake()
    {
        Instance = this;
    }

    public LayerMask SolidObjects
    {
        get => solidObjectsLayer;
    }
    public LayerMask Interactable
    {
        get => interactableLayer;
    }
    public LayerMask Portal
    {
        get => portalLayer;
    }

    public LayerMask DoorPortal
    {
        get => doorPortal;
    }
}
