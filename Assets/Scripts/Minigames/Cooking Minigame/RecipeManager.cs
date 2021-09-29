using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    [SerializeField]
    Recipe ChickenTinola = new Recipe("ChickenTinola");
    [SerializeField]
    Recipe PorkMenudo = new Recipe("PorkMenudo");
    [SerializeField]
    Recipe BeefNilaga = new Recipe("BeefNilaga");
    [SerializeField]
    Recipe FishSinigang = new Recipe("FishSinigang");
    [SerializeField]
    Recipe PorkPinakbet = new Recipe("PorkPinakbet");

    public static RecipeManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    public Recipe getRecipe(string recipeName)
    {
        switch (recipeName)
        {
            case "ChickenTinola":
                return ChickenTinola;
            case "PorkMenudo":
                return PorkMenudo;
            case "BeefNilaga":
                return BeefNilaga;
            case "FishSinigang":
                return FishSinigang;
            case "PorkPinakbet":
                return PorkPinakbet;
            default:
                Debug.LogError("Recipe Not Found");
                //returning pork pinakbet to avoid error
                return PorkPinakbet;
        }
    }
}
