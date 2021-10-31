using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GymResultsManager : MonoBehaviour
{
    [Header("Data")]
    public int tier;
    public int perfectReps;
    public int goodReps;
    public int badReps;

    [Header("UI")]
    [SerializeField] GameObject resultsPanel;
    [SerializeField] TextMeshProUGUI resultsText;

    [Header("Badge")]
    [SerializeField] int badgeCounter;
    [SerializeField] GameObject badgePanel;
    [SerializeField] Image badgeIcon;
    [SerializeField] TextMeshProUGUI badgeName;

    [Header("Next Scene")]
    [SerializeField] string portalDestination;
    public static GymResultsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void DisplayResultsScreen()
    {
        resultsPanel.SetActive(true);

        resultsText.text = "Perfect Reps: " + perfectReps + "\n" +
            "Good Reps: " + goodReps + "\n" +
            "Bad Reps: " + badReps;

        CalculateResults();
        SaveToPlayerData();
    }

    void CalculateResults()
    {
        if (perfectReps == 10)
            tier = 3;
        else if (perfectReps > 5)
            tier = 2;
        else if (goodReps > 10)
            tier = 1;
        else
            tier = 0;
    }

    void SaveToPlayerData()
    {
        if (Badges.Instance.badgeSlots[badgeCounter].tier < tier)
        {
            Badges.Instance.badgeSlots[badgeCounter].tier = tier;
            badgePanel.SetActive(true);

            DisplayNewBadgeInfo();
        }
    }

    void DisplayNewBadgeInfo()
    {
        switch (tier)
        {
            case 1:
                badgeIcon.sprite = Badges.Instance.badgeSlots[badgeCounter].badge.bronzeIcon;
                badgeName.text = Badges.Instance.badgeSlots[badgeCounter].badge.badgeName + " (Bronze)";
                break;
            case 2:
                badgeIcon.sprite = Badges.Instance.badgeSlots[badgeCounter].badge.silverIcon;
                badgeName.text = Badges.Instance.badgeSlots[badgeCounter].badge.badgeName + " (Silver)";
                break;
            case 3:
                badgeIcon.sprite = Badges.Instance.badgeSlots[badgeCounter].badge.goldIcon;
                badgeName.text = Badges.Instance.badgeSlots[badgeCounter].badge.badgeName + " (Gold)";
                break;
            default:
                Debug.Log("Error result tier");
                break;
        }
    }

    public void MinigameDone()
    {
        TimeManager.Instance.hour++;
        HungerThirst.Instance.DecreaseHunger(30);
        HungerThirst.Instance.DecreaseThirst(70);
        PlayerSceneInformation.Instance.previousScene = SceneInitiator.Instance.sceneName;
        SceneLoaderManager.OnSceneLoad?.Invoke(portalDestination);
    }
}
