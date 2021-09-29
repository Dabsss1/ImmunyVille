using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public string recipeName;
    public GameObject[] recipeIngredients;

    public Recipe(string recipeName)
    {
        this.recipeName = recipeName;
    }
}
