using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingManager : MonoBehaviour
{
    public GameObject seedSelectionUI;
    public PlantBlueprint plantBlueprint;

    public Transform plantContainer;

    public static PlantingManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        LoadPlants();
    }

    public void LoadPlants()
    {
        foreach (PlantedPlantSlot plantSlot in Plants.Instance.plantedSlots)
        {
            PlantBlueprint tempBlueprint = Instantiate(plantBlueprint, plantContainer);
            tempBlueprint.SetData(plantSlot.slot, plantSlot.plantingSpot, plantSlot.daysLeftToGrow);
        }
    }
    public void PlantSeed(SeedSlot slot)
    {
        Vector3 facingDir = new Vector3(Player.Instance.character.animator.MoveX, Player.Instance.character.animator.MoveY);
        Vector3 interactPos = Player.Instance.transform.position + facingDir;
        interactPos.y -= .5f;

        Plants.Instance.UseSeed(slot);
        seedSelectionUI.SetActive(false);

        PlantBlueprint tempBlueprint = Instantiate(plantBlueprint,plantContainer);
        tempBlueprint.SetData(slot.seed,interactPos,slot.seed.daysToGrow);

        Plants.Instance.AddPlant(tempBlueprint);
    }

    public void InteractWithDirt()
    {
        seedSelectionUI.SetActive(true);
    }
}
