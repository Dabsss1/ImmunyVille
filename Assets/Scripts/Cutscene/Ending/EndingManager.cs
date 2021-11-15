using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndingManager : MonoBehaviour
{
    public Sprite maleSprite;
    public Sprite femaleSprite;
    public SpriteRenderer playerAvatar;

    public static Action OnFinishDialog;

    public int dialogCounter = 0;

    [SerializeField] List<CutscenePart> cutsceneParts = new List<CutscenePart>();

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
        dialogCounter += 1;

        if(dialogCounter >= cutsceneParts.Count)
            SceneLoaderManager.OnSceneLoad("Credits");
        else
            ShowDialog();
    }

    void Start()
    {
        if (PlayerDataManager.Instance.gender == "male")
            playerAvatar.sprite = maleSprite;
        else if (PlayerDataManager.Instance.gender == "female")
            playerAvatar.sprite = femaleSprite;

        if (cutsceneParts[dialogCounter].isNpc)
            DialogManager.Instance.showDialog(cutsceneParts[dialogCounter].dialog, cutsceneParts[dialogCounter].character.characterName);
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
}
