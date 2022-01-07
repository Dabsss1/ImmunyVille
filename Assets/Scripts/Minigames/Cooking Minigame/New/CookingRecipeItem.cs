using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Recipe/Create new Recipe")]
[System.Serializable]
public class CookingRecipeItem : ScriptableObject
{
    public string recipeName;

    //public List<IngredientItem> ingredients;

    public List<RecipeIngredient> ingredients;
}

[System.Serializable]
public class RecipeIngredient
{
    public IngredientItem ingredient;
    public string measurement;
}