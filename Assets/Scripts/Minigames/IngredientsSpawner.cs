using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IngredientsSpawner : MonoBehaviour
{
    //placeholders of ingredients
    [SerializeField] Transform[] placeHolder;
    //ingredient prefabs
    [SerializeField] GameObject[] ingredientPrefab;
    //temporary gameobject for shuffle
    private GameObject tempGameObject;

    //ingredients name to pass to supermarket manager script
    public string[] ingredientsName = new string[22];

    private void Start()
    {
        int counter=0;
        Shuffle(ingredientPrefab);

        int ingredientCounter = 0;

        foreach (GameObject i in ingredientPrefab)
        {
            Instantiate(i,placeHolder[counter]);
            if (i.GetComponent<IngredientController>() != null)
            {
                ingredientsName[ingredientCounter] = i.name+"        ";
                ingredientCounter++;
            }
            counter++;
        }

        SupermarketManager.setIngredientsName?.Invoke(ingredientsName);
    }

    //Shuffle ingredients
    public void Shuffle(GameObject[] gameObject)
    {
        for (int i = 0; i < gameObject.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, gameObject.Length);
            tempGameObject = gameObject[rnd];
            gameObject[rnd] = gameObject[i];
            gameObject[i] = tempGameObject;
        }
    }

}
