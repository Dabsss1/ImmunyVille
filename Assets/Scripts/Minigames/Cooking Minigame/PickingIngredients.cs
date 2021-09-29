using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum pickingState {COUNTDOWN, PLAYING, DONE}
public class PickingIngredients : MonoBehaviour
{
    [SerializeField]
    GameObject cookingScene;
    [SerializeField]
    GameObject transition;

    [HideInInspector]
    //Store selected recipe name from picking
    public string selectedRecipe;

    pickingState state;

    [HideInInspector]
    //Main Recipe Object
    public Recipe mainRecipe;

    //ingredient scaler
    public int[] ingredientsSize = new int[2];

    //Text Informations
    public TextMeshProUGUI recipeInfoText;
    public TextMeshProUGUI ingredientInfoText;

    //timer
    public GameObject countdownGO;
    public TextMeshProUGUI countdownText;
    int countdown = 5;
    public TextMeshProUGUI timerText;
    float minutes = 2;
    float seconds = 00;
    [SerializeField]
    float timePenalty;

    //extra ingredients List (main recipe ingredients will be removed at start)
    public List<GameObject> extraIngredients = new List<GameObject>();

    //placeholders of ingredients
    public Transform[] placeholders = new Transform[2];

    //ingredient counter
    int ingredientCounter = 0;
    //current main ingredient
    GameObject currentMainIngredient;
    //current extra ingredients
    GameObject[] currentExtraIngredient = new GameObject[2];

    //extra ingredient counter
    int extraCounter = 0;

    //data
    int selectedIngredients=0;
    int mistakes=0;

    //touch inputs
    [SerializeField]
    LayerMask ingredientsLayer;
    [SerializeField]
    LayerMask trayLayer;
    [SerializeField]
    GameObject selectedIngredient; //selected ingredient
    Vector3 selectedIngredientOriginalPos;

    private void OnEnable()
    {
        InputTouch.OnFingerDown += CheckForIngredient;
        InputTouch.OnFingerMove += DragIngredient;
        InputTouch.OnFingerUp += ReleaseIngredient;
    }
        private void OnDisable()
    {
        InputTouch.OnFingerDown -= CheckForIngredient;
        InputTouch.OnFingerMove -= DragIngredient;
        InputTouch.OnFingerUp -= ReleaseIngredient;
    }

    void Start()
    {
        //set state to countdown
        state = pickingState.COUNTDOWN;

        //get selected main recipe from recipe manager
        mainRecipe = RecipeManager.Instance.getRecipe(selectedRecipe);

        //display recipe on panel
        SetRecipeInfoText();

        //remove main recipe ingredients from extra list
        RemoveMainRecipeIngredients();

        //shuffle extra ingredients
        Shuffle(extraIngredients);

        //shuffle main ingredients
        Shuffle(mainRecipe.recipeIngredients);

        //spawn 1st 3 ingredients
        SpawnIngredients();

        //start countdown
        StartCountdown();
    }

    private void Update()
    {
        if (state == pickingState.PLAYING)
        {
            if(minutes == 0 && seconds == 0)
            {
                state = pickingState.DONE;
                OpenCookingPanel();
                return;
            }

            seconds -= Time.deltaTime;
            if (seconds < 0)
            {
                if (minutes == 0)
                    seconds = 0;
                else
                {
                    minutes--;
                    seconds = 59;
                }
            }

            timerText.text = $"{minutes}:{seconds:00}";
        }
    }

    void SetRecipeInfoText()
    {
        //Display Recipe Name
        recipeInfoText.text = mainRecipe.recipeName;

        //Add space
        recipeInfoText.text += "\n";

        //Display Recipe Ingredients
        foreach (GameObject i in mainRecipe.recipeIngredients)
        {
            recipeInfoText.text += "\n" + i.name;
        }
    }

    void SpawnIngredients()
    {
        int rnd = UnityEngine.Random.Range(0, placeholders.Length);

        Vector3 scale = new Vector3();

        int extraIngredientCounter = 0;

        for (int i = 0; i < placeholders.Length; i++)
        {
            GameObject spawnedGO;

            if (selectedIngredients >= mainRecipe.recipeIngredients.Length)
            {
                OpenCookingPanel();
                return;
            }

            if (i != rnd)
            {
                if (extraCounter >= extraIngredients.Count)
                {
                    extraCounter = 0;
                }
                spawnedGO = Instantiate(extraIngredients[extraCounter], placeholders[i]);
                currentExtraIngredient[extraIngredientCounter] = spawnedGO;
                extraIngredientCounter++;
                extraCounter++;
            }
            else
            {
                spawnedGO = Instantiate(mainRecipe.recipeIngredients[ingredientCounter], placeholders[i]);
                currentMainIngredient = spawnedGO;
                ingredientCounter++;
            }

            spawnedGO.transform.localScale = new Vector3(ingredientsSize[0], ingredientsSize[1], 1);
        }
    }

    void CheckForIngredient(Vector3 fingerPosition)
    {
        if (state != pickingState.PLAYING)
            return;

        Collider2D ingredientCollider = Physics2D.OverlapPoint(fingerPosition, ingredientsLayer);
        if (ingredientCollider != null)
        {
            selectedIngredientOriginalPos = ingredientCollider.gameObject.transform.position;
            string ingredientInfo;
            ingredientInfo = ingredientCollider.GetComponent<IngredientController>().ingredient.ingredientName;

            ingredientInfo += "\n" + ingredientCollider.GetComponent<IngredientController>().ingredient.ingredientInfo;

            ingredientInfoText.text = ingredientInfo;

            selectedIngredient = ingredientCollider.gameObject;
        }
    }
    
    void DragIngredient(Vector3 fingerPosition)
    {
        fingerPosition.z = 0;
        selectedIngredient.transform.position = fingerPosition;
    }

    void ReleaseIngredient(Vector3 fingerPosition)
    {
        Collider2D trayCollider = Physics2D.OverlapPoint(fingerPosition, trayLayer);
        if (trayCollider != null) //if ingredient is dropped into the tray
        {
            if (currentMainIngredient == selectedIngredient)//if ingredient is in the recipe
            {
                //selected ingredients counter
                selectedIngredients++;

                //spawn new ingredients
                foreach (GameObject i in currentExtraIngredient)
                {
                    Destroy(i);
                }
                SpawnIngredients();
            }
            else //if ingredient is not in the recipe
            {
                //Return selected ingredient to plate
                selectedIngredient.transform.position = selectedIngredientOriginalPos;
                //mistakes counter
                mistakes++;
                //time penalty
                DeductTimePenalty();
            }
        }
        else //if not put into the tray
        {
            //Return selected ingredient to plate
            selectedIngredient.transform.position = selectedIngredientOriginalPos;
        }
        
        selectedIngredient = null;
    }

    void DeductTimePenalty()
    {
        if (seconds >= timePenalty)
            seconds -= timePenalty;
        else if (minutes == 0 && seconds < timePenalty)
        {
            minutes = 0;
            seconds = 0;
            timerText.text = $"{minutes}:{seconds:00}";
        }
        else
        {
            minutes--;
            float temp = timePenalty - seconds;
            seconds = 60 - temp;
        }
    }
    void RemoveMainRecipeIngredients()
    {
        foreach (GameObject i in mainRecipe.recipeIngredients)
        {
            extraIngredients.Remove(i);
        }
    }

    void OpenCookingPanel()
    {
        SendResults();
        StartCoroutine(OpenNextPanel());
    }

    IEnumerator OpenNextPanel()
    {
        transition.SetActive(false);
        transition.SetActive(true);
        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);
        cookingScene.SetActive(true);

        transition.GetComponent<Animator>().SetTrigger("Exit");
    }

    void StartCountdown()
    {
        StartCoroutine(CountdownTimer());
    }

    IEnumerator CountdownTimer()
    {
        while (countdown != 0)
        {
            yield return new WaitForSeconds(1f);
            countdown--;
            countdownText.text = countdown.ToString();
        }
        if (countdown == 0)
            countdownGO.SetActive(false);

        state = pickingState.PLAYING;
    }
    public void Shuffle(GameObject[] gameObject)
    {
        GameObject tempGameObject;

        for (int i = 0; i < gameObject.Length; i++)
        {
            int rnd = UnityEngine.Random.Range(0, gameObject.Length);
            tempGameObject = gameObject[rnd];
            gameObject[rnd] = gameObject[i];
            gameObject[i] = tempGameObject;
        }
    }

    public void Shuffle(List<GameObject> gameObject)
    {
        GameObject tempGameObject;

        for (int i = 0; i < gameObject.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(0, gameObject.Count);
            tempGameObject = gameObject[rnd];
            gameObject[rnd] = gameObject[i];
            gameObject[i] = tempGameObject;
        }
    }
    
    public void SendResults()
    {
        CookingResults.Instance.pickingTimeLeft = $"{minutes}:{seconds:00}";
        CookingResults.Instance.pickingIngredients = selectedIngredients.ToString() + "/" + mainRecipe.recipeIngredients.Length.ToString();
        CookingResults.Instance.mistakes = mistakes.ToString();
    }
}
