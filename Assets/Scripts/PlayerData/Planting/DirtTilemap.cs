using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtTilemap : MonoBehaviour, Interactable
{
    public void Interact()
    {
        PlantingManager.Instance.InteractWithDirt();
    }
}
