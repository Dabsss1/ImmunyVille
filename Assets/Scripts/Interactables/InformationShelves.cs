using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationShelves : MonoBehaviour, Interactable
{
    [SerializeField] InformationTipsItem tips;

    public void Interact()
    {
        int rnd = UnityEngine.Random.Range(0, tips.tips.Count);

        DialogManager.Instance.showDialog(tips.tips[rnd]);
    }
}
