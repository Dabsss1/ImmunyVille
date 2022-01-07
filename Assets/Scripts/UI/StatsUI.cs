using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI confidenceText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] TextMeshProUGUI strenghtText;

    // Update is called once per frame
    void Update()
    {
        healthText.text = $"{Stats.Instance.health / 1500 * 100:00}%";
        confidenceText.text = $"{Stats.Instance.confidence / 1500*100:00}%";
        bodyText.text = $"{Stats.Instance.body / 1500 * 100:00}%";
        strenghtText.text = $"{Stats.Instance.strength / 1500 * 100:00}%";
    }
}
