using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooking : MonoBehaviour
{
    [SerializeField]
    Slider cookingSlider;

    [SerializeField]
    float sliderSpeed;
    [SerializeField]
    float sliderMaxValue;
    [SerializeField]
    float sliderMinValue;

    bool increasing=true;
    bool cooking=true;

    [SerializeField]
    GameObject doneText;
    [SerializeField]
    GameObject cookingResultsPanel;



    // Update is called once per frame
    void Update()
    {
        if(cooking)
            MoveSlider();
    }

    void MoveSlider()
    {
        if (increasing)
            cookingSlider.value += Time.deltaTime * sliderSpeed;
        else
            cookingSlider.value -= Time.deltaTime * sliderSpeed;

        if (cookingSlider.value >= sliderMaxValue)
            increasing = false;
        else if (cookingSlider.value <= sliderMinValue)
            increasing = true;
    }

    public void CookConfirm()
    {
        cooking = false;
        
        StartCoroutine(ShowCookingResults());
    }

    IEnumerator ShowCookingResults()
    {
        doneText.SetActive(true);
        CookingResults.Instance.cookingQuality = SliderState.Instance.cookQuality;
        yield return new WaitForSeconds(1f);
        CookingResults.Instance.ShowResults();
    }
}
