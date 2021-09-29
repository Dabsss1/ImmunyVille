using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipePicking : MonoBehaviour
{
    [SerializeField]
    GameObject pickingIngredientsPanel;

    [SerializeField]
    GameObject transition;

    public void RecipeSelected(string recipeName)
    {
        OpenPickingIngredientsPanel(recipeName);
    }

    void OpenPickingIngredientsPanel(string recipeName)
    {
        StartCoroutine(OpenNextPanel(recipeName));
    }

    IEnumerator OpenNextPanel(string recipeName)
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
        pickingIngredientsPanel.SetActive(true);
        pickingIngredientsPanel.GetComponent<PickingIngredients>().selectedRecipe = recipeName;
        CookingResults.Instance.recipeName = recipeName;

        transition.GetComponent<Animator>().SetTrigger("Exit");
    }
}
