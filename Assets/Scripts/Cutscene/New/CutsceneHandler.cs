using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CutsceneHandler : MonoBehaviour
{
    public Sprite maleSprite;
    public Sprite femaleSprite;
    public SpriteRenderer playerAvatar;

    public static Action OnFinishDialog;
    public List<CutscenePart> cutsceneParts;

    public int dialogCounter = 0;
    private void OnEnable()
    {
        OnFinishDialog += IncrementDialogCounter;
    }

    private void OnDisable()
    {
        OnFinishDialog -= IncrementDialogCounter;
    }

    void IncrementDialogCounter()
    {
        if (dialogCounter == 2)
        {
            float statsAverage = (Stats.Instance.health + Stats.Instance.body + Stats.Instance.confidence + Stats.Instance.strength) / 4;
            float checkupTimes = PlayerDataManager.Instance.totalDays / 7;
            //max stats / max total checkups
            float passingRate = (200 / 8) * checkupTimes;

            if (statsAverage >= passingRate)
            {
                dialogCounter = 3;
            }
            else
                dialogCounter = 4;
        }
        else if (dialogCounter == 3 || dialogCounter == 4)
        {
            dialogCounter = 5;
        }
        else if (dialogCounter == 5)
        {
            dialogCounter = 6;
            NextScene();
        }
        else
        {
            dialogCounter += 1;
        }
        
    }
    private void Start()
    {
        if (PlayerDataManager.Instance.gender == "male")
            playerAvatar.sprite = maleSprite;
        else if (PlayerDataManager.Instance.gender == "female")
            playerAvatar.sprite = femaleSprite;

        if(cutsceneParts[dialogCounter].isNpc)
            DialogManager.Instance.showDialog(cutsceneParts[dialogCounter].dialog,cutsceneParts[dialogCounter].character.characterName);
        else
            DialogManager.Instance.showDialog(cutsceneParts[dialogCounter].dialog, PlayerDataManager.Instance.playerName);
    }

    public void ShowDialog()
    {
        if (cutsceneParts[dialogCounter].isNpc)
            DialogManager.Instance.showDialog(cutsceneParts[dialogCounter].dialog, cutsceneParts[dialogCounter].character.characterName);
        else
            DialogManager.Instance.showDialog(cutsceneParts[dialogCounter].dialog, PlayerDataManager.Instance.playerName);
    }

    public void NextScene()
    {
        TimeManager.Instance.hour += 1;
        SceneInitiator.Instance.sceneName = "Hospital";
        SceneLoaderManager.OnSceneLoad("TownSquare");
    }
}
