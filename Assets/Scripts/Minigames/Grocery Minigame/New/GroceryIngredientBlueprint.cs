using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryIngredientBlueprint : MonoBehaviour
{
    public IngredientItem ingredientItem;

    public void SetData(IngredientItem item)
    {
        ingredientItem = item;

        GetComponent<SpriteRenderer>().sprite = ingredientItem.icon;

        gameObject.AddComponent<PolygonCollider2D>();

        if (!ingredientItem.ExtraItem)
            IngredientsManager.Instance.ingredientNames.Add(ingredientItem.ingredientName);
    }

    // When an ingredient is touched
    public void touched(GameObject cart, float speed)
    {
        //call coroutine to move the ingredient
        StartCoroutine(moveToCart(cart, speed));

        IngredientsManager.Instance.ingredientNames.Remove(ingredientItem.ingredientName);

        IngredientsManager.Instance.SetPoolText();
    }

    //move the ingredient to cart
    IEnumerator moveToCart(GameObject cart, float speed)
    {
        //cart position
        Vector2 cartPosition = new Vector2(cart.transform.position.x, cart.transform.position.y);
        //ingredient position
        Vector2 ingredientPos = new Vector2(transform.position.x, transform.position.y);

        //while ingredient is not in the same position of cart
        while ((cartPosition - ingredientPos).sqrMagnitude > Mathf.Epsilon)
        {
            //move the ingredient to the cart
            transform.position = Vector3.MoveTowards(transform.position, cartPosition, speed * Time.deltaTime);
            yield return null;
        }
        //set ingredient position the same as cart position
        transform.position = cartPosition;
    }
}
