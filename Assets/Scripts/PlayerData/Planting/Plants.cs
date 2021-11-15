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

    public void ObtainSeed(PlantItem seed, int quantity)
    {
        foreach(SeedSlot seedSlot in seedSlots)
        {
            if(seedSlot.seed == seed)
            {
                seedSlot.quantity += quantity;
            }
        }
    }

    public void AddPlant(PlantBlueprint plantedPlant)
    {
        PlantedPlantSlot plantSlot = new PlantedPlantSlot(plantedPlant);
        plantedSlots.Add(plantSlot);
    }

    //from Save File
    public void AddPlant(string plantName,float posX,float posY,float posZ,int daysLeftToGrow)
    {
        PlantedPlantSlot plant = new PlantedPlantSlot();

        foreach(SeedSlot seed in seedSlots)
        {
            if (plantName == seed.seed.plantName)
                plant.slot = seed.seed;
        }

        plant.plantingSpot.x = posX;
        plant.plantingSpot.y = posY;
        plant.plantingSpot.z = posZ;

        plant.daysLeftToGrow = daysLeftToGrow;

        plantedSlots.Add(plant);
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

    public void ResetData()
    {
        foreach(SeedSlot slot in seedSlots)
            slot.quantity = 0;

        plantedSlots.Clear();
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

    public PlantedPlantSlot()
    {

    }
}
