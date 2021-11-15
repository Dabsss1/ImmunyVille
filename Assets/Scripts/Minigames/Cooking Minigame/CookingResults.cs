using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CookingResults : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] TaskItem task;

    [Header("Data")]
    public string recipeName;
    public string pickingTimeLeft;
    public int mistakes;
    public bool perfectIngredients;
    public string pickingIngredients;
    public string cookingQuality;

    public int tier;

    [Header("UI")]
    [SerializeField]
    TextMeshProUGUI recipeNameText;
    [SerializeField]
    TextMeshProUGUI minigameResults;
    [SerializeField]
    GameObject resultsPanel;

    [Header("Badge")]
    [SerializeField] GameObject badgePanel;
    [SerializeField] Image badgeIcon;
    [SerializeField] TextMeshProUGUI badgeName;

    [Header("Next Scene")]
    [SerializeField] string portalDestination;
    //UI elements
    public static CookingResults Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    public void ShowResults()
    {
        resultsPanel.SetActive(true);
        recipeNameText.text = recipeName;
        minigameResults.text = "TIME LEFT:   " +pickingTimeLeft+"\n"+
            "INGREDIENTS:   "+pickingIngredients+"\n"+
            "MISTAKES:   "+mistakes+"\n"+
            "COOKING QUALITY:   "+cookingQuality;

        CalculateResults();
        SaveToPlayerData();
    }
    
    void CalculateResults()
    {
        if (mistakes == 0 && perfectIngredients)
            tier = 3;
        else if (mistakes < 2)
            tier = 2;
        else if (mistakes < 4)
            tier = 1;
        else
            tier = 0;
    }

    void SaveToPlayerData()
    {
        if (Badges.Instance.badgeSlots[1].tier < tier)
        {
            Badges.Instance.badgeSlots[1].tier = tier;
            badgePanel.SetActive(true);

            DisplayNewBadgeInfo();
        }
    }

    void DisplayNewBadgeInfo()
    {
        switch (tier)
        {
            case 1:
                badgeIcon.sprite = Badges.Instance.badgeSlots[1].badge.bronzeIcon;
                badgeName.text = "Banana (Bronze)";
                break;
            case 2:
                badgeIcon.sprite = Badges.Instance.badgeSlots[1].badge.silverIcon;
                badgeName.text = "Banana (Silver)";
                break;
            case 3:
                badgeIcon.sprite = Badges.Instance.badgeSlots[1].badge.goldIcon;
                badgeName.text = "Banana (Gold)";
                break;
            default:
                Debug.Log("Error result tier");
                break;
        }
    }

    public void MinigameDone()
    {
        if (!Tasks.Instance.TaskDone(task))
        {
            Stats.Instance.health += 20;
            Stats.Instance.body += 10;
            Tasks.Instance.CompleteTask(task);
        }
            

        TimeManager.Instance.hour++;
        HungerThirst.Instance.IncreaseHunger(100);
        HungerThirst.Instance.IncreaseThirst(100);
        PlayerSceneInformation.Instance.previousScene = SceneInitiator.Instance.sceneName;
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
