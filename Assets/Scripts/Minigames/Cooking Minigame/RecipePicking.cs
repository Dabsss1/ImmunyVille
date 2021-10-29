using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipePicking : MonoBehaviour
{
    [SerializeField]
    GameObject pickingIngredientsPanel;

    [SerializeField]
    GameObject transition;

    public void RecipeSelected(CookingRecipeItem recipe)
    {
        OpenPickingIngredientsPanel(recipe);
    }

    void OpenPickingIngredientsPanel(CookingRecipeItem recipe)
    {
        StartCoroutine(OpenNextPanel(recipe));
    }

    IEnumerator OpenNextPanel(CookingRecipeItem recipe)
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);


        gameObject.SetActive(false);
        pickingIngredientsPanel.SetActive(true);
        PickingIngredients.Instance.recipe = recipe;
        CookingResults.Instance.recipeName = recipe.recipeName;

        transition.GetComponent<Animator>().SetTrigger("Exit");
    }
}
