using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum gymState { COUNTDOWN, PLAYING, DONE}
public class GymManager : MonoBehaviour
{
    [SerializeField]
    Slider barSlider;

    [SerializeField]
    float sliderSpeed;

    [SerializeField]
    Sprite highlightedIcon;
    [SerializeField]
    Sprite normalIcon;

    [SerializeField]
    Sprite avatar1;
    [SerializeField]
    Sprite avatar2;
    [SerializeField]
    Sprite avatar3;

    [SerializeField]
    float goodRepValue;
    [SerializeField]
    float perfectRepValue;

    [SerializeField]
    Image barSliderImage;
    [SerializeField]
    Image avatarImage;

    [SerializeField]
    int reps;
    int remainingReps;
    int goodReps = 0;
    int perfectReps = 0;
    int badReps = 0;

    [SerializeField]
    TextMeshProUGUI infoText;

    private void Start()
    {
        remainingReps = reps;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfoText();
        MoveSlider();
        AnimateAvatar();
    }

    void MoveSlider()
    {
        barSlider.value += Time.deltaTime * sliderSpeed;

        if (barSlider.value >= 100)
        {
            barSlider.value = 0;
            badReps++;
            remainingReps--;
            UpdateInfoText();
        } 

        if (barSlider.value >= 85)
            barSliderImage.sprite = highlightedIcon;
        else
            barSliderImage.sprite = normalIcon;
    }

    void AnimateAvatar()
    {
        if (barSlider.value >= 30 && barSlider.value <= 84)
        {
            avatarImage.sprite = avatar2;
        }
        else if (barSlider.value >= 85)
        {
            avatarImage.sprite = avatar3;
        }
        else
        {
            avatarImage.sprite = avatar1;
        }
    }

    public void ConfirmRep()
    {
        if (barSlider.value <= 10)
            return;
        if (barSlider.value < goodRepValue)
            badReps++;
        else if (barSlider.value >= goodRepValue && barSlider.value < perfectRepValue)
            goodReps++;
        else if (barSlider.value >= perfectRepValue)
            perfectReps++;
        barSlider.value = 0;
        remainingReps--;
        UpdateInfoText();
    }

    public void UpdateInfoText()
    {
        infoText.text = "" + 
            "Remaining Reps: " + remainingReps + "\n" + 
            "Perfect Reps: "+ perfectReps +"\n" +
            "Good Reps: "+ goodReps + "\n" +
            "Bad Repds: "+ badReps;
    }
}
