using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBlueprint : MonoBehaviour, Interactable
{
    public PlantItem plant;
    public Vector3 plantingSpot;
    public int daysLeftToGrow;

    public void SetData(PlantItem slot,Vector3 plantingSpot,int daysLeftToGrow)
    {
        
        transform.position = plantingSpot;
        if (daysLeftToGrow > 0)
            GetComponent<SpriteRenderer>().sprite = slot.seed;
        else
            GetComponent<SpriteRenderer>().sprite = slot.plant;

        this.plant = slot;
        this.plantingSpot = plantingSpot;
        this.daysLeftToGrow = daysLeftToGrow;
    }

    public void Interact()
    {
        if (daysLeftToGrow <= 0)
        {
            Plants.Instance.RemovePlant(this);
            Destroy(gameObject);
        }
    }
}
