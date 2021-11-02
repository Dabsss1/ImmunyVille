using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plants : MonoBehaviour
{
    public List<SeedSlot> seedSlots = new List<SeedSlot>();
    public List<PlantedPlantSlot> plantedSlots = new List<PlantedPlantSlot>();

    public static Plants Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void UseSeed(SeedSlot slot)
    {
        foreach(SeedSlot seed in seedSlots)
        {
            if (slot == seed)
            {
                seed.quantity--;
            }
        }
    }

    public void AddPlant(PlantBlueprint plantedPlant)
    {
        PlantedPlantSlot plantSlot = new PlantedPlantSlot(plantedPlant);
        plantedSlots.Add(plantSlot);
    }

    public void RemovePlant(PlantBlueprint plantedPlant)
    {
        foreach(PlantedPlantSlot plantSlot in plantedSlots)
        {
            if (plantedPlant.plantingSpot == plantSlot.plantingSpot)
            {
                plantedSlots.Remove(plantSlot);
                return;
            }
        }
    }
}
[System.Serializable]
public class SeedSlot
{
    public PlantItem seed;
    public int quantity;
}

[System.Serializable]
public class PlantedPlantSlot
{
    public PlantItem slot;
    public Vector3 plantingSpot;
    public int daysLeftToGrow;

    public PlantedPlantSlot(PlantBlueprint plantedPlant)
    {
        this.slot = plantedPlant.plant;
        this.plantingSpot = plantedPlant.plantingSpot;
        this.daysLeftToGrow = plantedPlant.daysLeftToGrow;
    }
}
