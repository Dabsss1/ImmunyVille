using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerName;
    [SerializeField] TextMeshProUGUI statusCondition;
    [SerializeField] Sprite maleSprite, femaleSprite;
    [SerializeField] Image AvatarDisplay;

    [SerializeField] Slider healthSlider;
    [SerializeField] Slider bodySlider;
    [SerializeField] Slider strengthSlider;
    [SerializeField] Slider confidenceSlider;

    // Start is called before the first frame update
    void Start()
    {
        playerName.text = PlayerDataManager.Instance.playerName;

        if(PlayerDataManager.Instance.gender == "male")
        {
            AvatarDisplay.sprite = maleSprite;
        }
        else if (PlayerDataManager.Instance.gender == "female")
        {
            AvatarDisplay.sprite = femaleSprite;
        }

        UpdateStatsUI();
    }

    private void OnEnable()
    {
        UpdateStatsUI();
    }

    void UpdateStatsUI()
    {
        if (Stats.Instance.goodCondition)
        {
            statusCondition.text = "Status: Good";
            statusCondition.color = Color.green;
        }
        else
        {
            statusCondition.text = "Status: Sick";
            statusCondition.color = Color.red;
        }

        healthSlider.value = Stats.Instance.health;
        bodySlider.value = Stats.Instance.body;
        strengthSlider.value = Stats.Instance.strength;
        confidenceSlider.value = Stats.Instance.confidence;
    }
}
