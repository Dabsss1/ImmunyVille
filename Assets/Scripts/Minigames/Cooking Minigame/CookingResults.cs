using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CookingResults : MonoBehaviour
{
    //Data
    //part1
    public string recipeName;
    //part2
    public string pickingTimeLeft;
    public string mistakes;
    public string pickingIngredients;
    //part3
    public string cookingQuality;

    [SerializeField]
    TextMeshProUGUI recipeNameText;
    [SerializeField]
    TextMeshProUGUI pickingIngredientsResult;
    [SerializeField]
    TextMeshProUGUI cookingPartResult;
    [SerializeField]
    GameObject resultsPanel;

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
        pickingIngredientsResult.text = "TIME LEFT\n" +
            "   "+pickingTimeLeft+"\n"+
            "INGREDIENTS\n" +
            "   "+pickingIngredients+"\n"+
            "MISTAKES\n" +
            "   "+mistakes;

        cookingPartResult.text = "QUALITY\n\n" + cookingQuality;
    }
    
}
