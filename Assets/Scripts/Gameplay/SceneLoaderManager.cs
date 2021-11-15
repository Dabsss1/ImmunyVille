using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class SceneLoaderManager : MonoBehaviour
{
    public static Action<string> OnMinigameLoad,OnSceneLoad,OnSleepLoad;
    public Animator sceneTransition;

    
    [Header("LoadingScreen")]
    [SerializeField] List<IngredientItem> ingredientTips;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBar;
    [SerializeField] TextMeshProUGUI informationText;
    [SerializeField] Image informationIcon;
    [SerializeField] GameObject tapButton;
    [SerializeField] GameObject tapText;

    [SerializeField] GameObject faderPanel;

    AsyncOperation operation;

    private void OnEnable()
    {
        OnSceneLoad += LoadNextScene;
        OnMinigameLoad += AsyncLoadScene;
        OnSleepLoad += LoadSleep;
    }

    private void OnDisable()
    {
        OnSceneLoad -= LoadNextScene;
        OnMinigameLoad -= AsyncLoadScene;
        OnSleepLoad -= LoadSleep;
    }

    private void LoadNextScene(string sceneName)
    {
        PlayerSceneInformation.Instance.previousScene = SceneInitiator.Instance.sceneName;
        StartCoroutine(LoadScene(sceneName));

    }

    void LoadSleep(string sceneName)
    {
        PlayerSceneInformation.Instance.previousScene = "Bedroom";
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        sceneTransition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

    void AsyncLoadScene (string sceneName)
    {
        PlayerSceneInformation.Instance.previousScene = SceneInitiator.Instance.sceneName;
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        sceneTransition.SetTrigger("Start");
        UIManager.OnScreenLoad?.Invoke();

        yield return new WaitForSeconds(1f);

        loadingScreen.SetActive(true);

        int rnd = UnityEngine.Random.Range(0, ingredientTips.Count);

        informationText.text = ingredientTips[rnd].ingredientDescription;
        informationIcon.sprite = ingredientTips[rnd].icon;

        yield return new WaitForSeconds(1f);

        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;
        
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingBar.value = progress;
            tapText.SetActive(true);
            tapButton.SetActive(true);
            yield return null;
        }

        
    }

    //Start scene after loading screen screen is tapped
    public void activateScene()
    {
        Debug.Log("Screen Tapped");
        operation.allowSceneActivation = true;
    }
}
