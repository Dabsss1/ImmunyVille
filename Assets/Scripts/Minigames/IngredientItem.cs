using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "ingredient/create new ingredient item")]
[System.Serializable]   
public class IngredientItem : ScriptableObject
{
    public bool ExtraItem = false;

    public Sprite icon;
    public string ingredientName;
    public string ingredientDescription;
}
