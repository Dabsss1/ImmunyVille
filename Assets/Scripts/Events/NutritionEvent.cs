using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutritionEvent : MonoBehaviour
{
    void Start()
    {
        if (TimeManager.Instance.day == 15)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    void Update()
    {
    }
}
