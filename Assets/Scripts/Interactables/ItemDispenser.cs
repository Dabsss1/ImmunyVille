using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDispenser : MonoBehaviour, Interactable
{
    public InventoryItem item;
    [SerializeField] int quantity = 1;
    [SerializeField] string prefix = "Obtained";

    public bool obtained;

    public void Interact()
    {
        if (!GameStateManager.Instance.EqualsState(OpenWorldState.DIALOG))
        {
            if (SpecialCounters.Instance.waterObtained)
                return;
            Inventory.Instance.ObtainItem(item, quantity);
            SpecialCounters.Instance.waterObtained = true;
        }
        string message = $"{prefix} {item.itemName} x{quantity}";

        DialogManager.Instance.showDialog(message);
    }
}
