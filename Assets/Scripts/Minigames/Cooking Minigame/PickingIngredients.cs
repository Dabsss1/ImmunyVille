using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum pickingState {COUNTDOWN, PLAYING, DONE}
public class PickingIngredients : MonoBehaviour
{
    [Header("Next Scene")]
    [SerializeField]
    GameObject cookingScene;
    [SerializeField]
    GameObject transition;

    pickingState state;

    [HideInInspector]
    public CookingRecipeItem recipe;

    [Header("UI")]
    //Text Informations
    public TextMeshProUGUI recipeInfoText;
    public TextMeshProUGUI ingredientInfoText;

    [Header("Timer")]
    //timer
    public GameObject countdownGO;
    public TextMeshProUGUI countdownText;
    int countdown = 5;
    public TextMeshProUGUI timerText;
    float minutes = 2;
    float seconds = 00;
    [SerializeField]
    float timePenalty;

    [Header("Ingredients")]
    //extra ingredients List (main recipe ingredients will be removed at start)
    [SerializeField] List<IngredientItem> extraIngredients = new List<IngredientItem>();
    List<IngredientItem> recipeIngredientsCopy;

    [SerializeField] CookingIngredientBlueprint ingredientBluePrint;

    //placeholders of ingredients
    public Transform[] placeholders = new Transform[2];

    //ingredient counter
    int ingredientCounter = 0;
    //current main ingredient
    [SerializeField]
    CookingIngredientBlueprint currentMainIngredient;
    //current extra ingredients
    CookingIngredientBlueprint[] currentExtraIngredient = new CookingIngredientBlueprint[2];

    //extra ingredient counter
    int extraCounter = 0;

    //touch inputs
    [SerializeField]
    LayerMask ingredientsLayer;
    [SerializeField]
    LayerMask trayLayer;
    [SerializeField]
    CookingIngredientBlueprint selectedIngredient; //selected ingredient
    Vector3 selectedIngredientOriginalPos;

    //data
    int selectedIngredients = 0;
    int mistakes = 0;

    public static PickingIngredients Instance { get; private set; }
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

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        //set state to countdown
        state = pickingState.COUNTDOWN;

        //display recipe on panel
        SetRecipeInfoText();

        //get a copy of ingredients list
        recipeIngredientsCopy = recipe.ingredients;

        //remove main recipe ingredients from extra list
        RemoveMainRecipeIngredients();

        //shuffle extra ingredients
        Shuffle(extraIngredients);

        //shuffle main ingredients
        Shuffle(recipeIngredientsCopy);

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
        recipeInfoText.text = recipe.recipeName;

        //Add space
        recipeInfoText.text += "\n";

        //Display Recipe Ingredients
        for (int i=0; i<recipe.ingredients.Count; i++)
        {
            recipeInfoText.text += "\n" + recipe.ingredients[i].ingredientName;
        }
        
    }
    void RemoveMainRecipeIngredients()
    {
        foreach (IngredientItem i in recipe.ingredients)
        {
            extraIngredients.Remove(i);
        }
    }
    void SpawnIngredients()
    {
        int rnd = UnityEngine.Random.Range(0, placeholders.Length);

        int extraIngredientCounter = 0;

        for (int i = 0; i < placeholders.Length; i++)
        {
            CookingIngredientBlueprint spawnedIngredient;

            if (selectedIngredients >= recipeIngredientsCopy.Count)
            {
                OpenCookingPanel();
                return;
            }

            spawnedIngredient = Instantiate(ingredientBluePrint, placeholders[i]);

            if (i != rnd)
            {
                if (extraCounter >= extraIngredients.Count)
                {
                    extraCounter = 0;
                }
                spawnedIngredient.SetData(extraIngredients[extraCounter]);
                currentExtraIngredient[extraIngredientCounter] = spawnedIngredient;
                extraIngredientCounter++;
                extraCounter++;
            }
            else
            {
                spawnedIngredient.SetData(recipeIngredientsCopy[ingredientCounter]);
                currentMainIngredient = spawnedIngredient;
                ingredientCounter++;
            }

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

            ingredientInfoText.text = ingredientCollider.GetComponent<CookingIngredientBlueprint>().ingredient.ingredientName;

            ingredientInfoText.text += "\n" + ingredientCollider.GetComponent<CookingIngredientBlueprint>().ingredient.ingredientDescription;

            selectedIngredient = ingredientCollider.GetComponent<CookingIngredientBlueprint>();
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
                foreach (CookingIngredientBlueprint i in currentExtraIngredient)
                {
                    Destroy(i.gameObject);
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

            AudioManager.Instance.PlaySfx("Minigame");
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

    public void Shuffle(List<IngredientItem> item)
    {
        IngredientItem temp;

        for (int i = 0; i < item.Count; i++)
        {
            int rnd = UnityEngine.Random.Range(0, item.Count);
            temp = item[rnd];
            item[rnd] = item[i];
            item[i] = temp;
        }
    }
    
    public void SendResults()
    {
        CookingResults.Instance.pickingTimeLeft = $"{minutes}:{seconds:00}";
        CookingResults.Instance.pickingIngredients = selectedIngredients.ToString() + "/" + recipeIngredientsCopy.Count.ToString();
        CookingResults.Instance.mistakes = mistakes;

        if (selectedIngredients == recipeIngredientsCopy.Count)
            CookingResults.Instance.perfectIngredients = true;
        else
            CookingResults.Instance.perfectIngredients = false;
    }
}
