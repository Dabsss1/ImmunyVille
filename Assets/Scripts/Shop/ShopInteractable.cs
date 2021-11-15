using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteractable : MonoBehaviour, Interactable
{
    [SerializeField] GameObject shopPanel;

    public void Interact()
    {
        shopPanel.SetActive(true);
    }
}
