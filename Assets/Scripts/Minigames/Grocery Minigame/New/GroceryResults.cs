using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GroceryResults : MonoBehaviour
{
    [Header("Task")]
    [SerializeField] TaskItem task;

    [Header("Settings")]
    [SerializeField] int oneStarRequirement;
    [SerializeField] int twoStarRequirement;

    [Header("UI")]
    [SerializeField] GameObject success;
    [SerializeField] GameObject timeUp;
    [SerializeField] GameObject[] stars;
    [SerializeField] TextMeshProUGUI resultText;
    [SerializeField] GameObject resultsPanel;

    [Header("Badge")]
    [SerializeField] BadgeItem minigameBadge;
    [SerializeField] GameObject badgePanel;
    [SerializeField] Image badgeIcon;
    [SerializeField] TextMeshProUGUI badgeName;

    [Header("NextScene")]
    [SerializeField] string portalDestination;

    //tiers: 3 - gold, 2 - silver, 1 - bronze, 0 - none
    int resultTier;

    public static GroceryResults Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void ShowResults()
    {
        if (SupermarketManager.Instance.state == GameState.SUCCESS)
        {
            success.SetActive(true);
        }
        else if (SupermarketManager.Instance.state == GameState.FAIL)
        {
            timeUp.SetActive(true);
        }
        StartCoroutine(ResultsScreen());
    }

    IEnumerator ResultsScreen()
    {
        yield return new WaitForSeconds(3f);

        resultsPanel.SetActive(true);

        if (SupermarketManager.Instance.state == GameState.SUCCESS)
            resultText.text = "Perfect!";
        else if (SupermarketManager.Instance.state == GameState.FAIL)
            resultText.text = "Ingredients Found: " + SupermarketManager.Instance.ingredientsFound;

        CalculateResults();
        SaveToPlayerData();

        for (int i=0; i<resultTier; i++)
        {
            stars[i].SetActive(true);
            yield return new WaitForSeconds(.66f);
        }
    }

    public void CalculateResults()
    {
        //if all ingredients found
        if (SupermarketManager.Instance.ingredientsFound ==  IngredientsManager.Instance.ingredientItems.Length)
        {
            resultTier = 3;
        }
        else if (SupermarketManager.Instance.ingredientsFound >= twoStarRequirement)
        {
            resultTier = 2;
        }
        else if (SupermarketManager.Instance.ingredientsFound >= oneStarRequirement)
        {
            resultTier = 1;
        }
        else
        {
            resultTier = 0;
        }
    }

    public void SaveToPlayerData()
    {
        if (Badges.Instance.badgeSlots[0].tier < resultTier)
        {
            Badges.Instance.badgeSlots[0].tier = resultTier;
            badgePanel.SetActive(true);

            DisplayNewBadgeInfo();
        }
    }

    void DisplayNewBadgeInfo()
    {
        switch (resultTier)
        {
            case 1:
                badgeIcon.sprite = Badges.Instance.badgeSlots[0].badge.bronzeIcon;
                badgeName.text = "Apple (Bronze)";
                break;
            case 2:
                badgeIcon.sprite = Badges.Instance.badgeSlots[0].badge.silverIcon;
                badgeName.text = "Apple (Silver)";
                break;
            case 3:
                badgeIcon.sprite = Badges.Instance.badgeSlots[0].badge.goldIcon;
                badgeName.text = "Apple (Gold)";
                break;
            default:
                Debug.Log("Error result tier");
                break;
        }
    }

    public void GameFinish()
    {
        Tasks.Instance.CompleteTask(task);
        Stats.Instance.body += 50;
        Stats.Instance.confidence += 30;

        TimeManager.Instance.hour++;
        HungerThirst.Instance.DecreaseHunger(30);
        HungerThirst.Instance.DecreaseThirst(50);
        PlayerSceneInformation.Instance.previousScene = SceneInitiator.Instance.sceneName;
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }

}
