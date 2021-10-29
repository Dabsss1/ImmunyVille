using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingIngredientBlueprint : MonoBehaviour
{
    public IngredientItem ingredient;

    // Start is called before the first frame update
    public void SetData(IngredientItem ingredient)
    {
        this.ingredient = ingredient;
        GetComponent<SpriteRenderer>().sprite = ingredient.icon;
    }
}
