using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedUIBlueprint : MonoBehaviour
{
    public Image seedIcon;
    public Text seedName;
    public Text seedQuantity;

    SeedSlot seed;

    public void SetData(SeedSlot seed)
    {
        this.seed = seed;
        seedIcon.sprite = seed.seed.seed;
        seedName.text = seed.seed.plantName;
        seedQuantity.text = $"{seed.quantity}";
    }

    public void PlantSeed()
    {
        PlantingManager.Instance.PlantSeed(seed);
    }
}
