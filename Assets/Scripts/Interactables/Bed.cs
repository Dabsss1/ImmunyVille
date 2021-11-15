using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour, Interactable
{
    [SerializeField] int minimumHourToSleep;
    [SerializeField] string dialog;
    public void Interact()
    {
        if (TimeManager.Instance.hour < minimumHourToSleep)
            DialogManager.Instance.showDialog(dialog);
        else
            DayResetter.OnBedSleep?.Invoke();
    }
}
