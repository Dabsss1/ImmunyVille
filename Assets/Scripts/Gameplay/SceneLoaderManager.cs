using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


public class SceneLoaderManager : MonoBehaviour
{
    public static Action<string,PlayerData> OnSceneLoad;
    public Animator sceneTransition;

    public string prevScene,currentScene;
    
    private void OnEnable()
    {
        OnSceneLoad += LoadNextScene;
    }

    private void OnDisable()
    {
        OnSceneLoad -= LoadNextScene;
    }

    private void LoadNextScene(string sceneName,PlayerData data)
    {
        SaveSystem.SavePlayer(data);
        StartCoroutine(LoadScene(sceneName));
    }

    IEnumerator LoadScene(string sceneName)
    {
        sceneTransition.SetTrigger("Start");
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(sceneName);
    }

    private void Start()
    {
    }
}
