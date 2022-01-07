using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class IngredientsManager : MonoBehaviour
{
    //placeholders of ingredients
    [SerializeField] Transform[] placeHolder;
    //ingredient items
    public IngredientItem[] ingredientItems;
    //ingredient blueprint
    [SerializeField] GroceryIngredientBlueprint ingredientBlueprint;

    [HideInInspector]
    //ingredient names
    public List<string> ingredientNames = new List<string>();
    [Header("UI")]
    [SerializeField] public GameObject ingredientPoolPanel;
    //text field
    [SerializeField] public Text ingredientPoolText;

    public static IngredientsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //Shuffle(ingredientItems);

        SpawnIngredients();

        SetPoolText();
    }

    //spawn ingredients
    void SpawnIngredients()
    {
        for(int i=0; i<ingredientItems.Length; i++)
        {
            GroceryIngredientBlueprint itemBlueprint = Instantiate(ingredientBlueprint,placeHolder[i]);
            itemBlueprint.SetData(ingredientItems[i]);
        }
    }

    public void SetPoolText()
    {
        string text = "";

        for(int i=0; i < ingredientNames.Count; i++)
        {
            text += ingredientNames[i] + "  ||  ";
        }

        ingredientPoolText.text = text;

        if (string.IsNullOrEmpty(text))
        {
            SupermarketManager.Instance.state = GameState.SUCCESS;
            ingredientPoolPanel.SetActive(false);
            GroceryResults.Instance.ShowResults();
        }
    }

    //Shuffle ingredients
    public void Shuffle(IngredientItem[] gameObject)
    {
        IngredientItem tempGameObject;

        for (int i = 0; i < gameObject.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, gameObject.Length);
            tempGameObject = gameObject[rnd];
            gameObject[rnd] = gameObject[i];
            gameObject[i] = tempGameObject;
        }
    }

}
