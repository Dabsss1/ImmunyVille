using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;


public class SceneLoaderManager : MonoBehaviour
{
    public static Action<string> OnMinigameLoad,OnSceneLoad;
    public Animator sceneTransition;
        

    [SerializeField] IngredientsTips tips;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Slider loadingBar;
    [SerializeField] GameObject tapText;
    [SerializeField] TextMeshProUGUI informationText;
    [SerializeField] GameObject tapButton;

    [SerializeField] GameObject faderPanel;

    AsyncOperation operation;

    private void OnEnable()
    {
        OnSceneLoad += LoadNextScene;
        OnMinigameLoad += AsyncLoadScene;
    }

    private void OnDisable()
    {
        OnSceneLoad -= LoadNextScene;
        OnMinigameLoad -= AsyncLoadScene;
    }

    private void LoadNextScene(string sceneName)
    {
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
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        sceneTransition.SetTrigger("Start");
        UIManager.OnScreenLoad?.Invoke();

        yield return new WaitForSeconds(1f);

        loadingScreen.SetActive(true);

        informationText.text = tips.getTip;

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

    public void activateScene()
    {
        Debug.Log("Screen Tapped");
        operation.allowSceneActivation = true;
    }
}
