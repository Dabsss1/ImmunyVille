using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : MonoBehaviour
{
    public GameObject malePlayer;
    public GameObject femalePlayer;

    public GameObject[] spawnPoint;

    public static string previousScene { get; set; }

    public static Action OnStartScene;


    void OnEnable()
    {
        OnStartScene += SpawnPlayer;
    }

    private void OnDisable()
    {
        OnStartScene -= SpawnPlayer;
    }

    private void Awake()
    {
        SaveSystem.LoadPlayer();
    }

    private void Start()
    {

        GamePreferencesManager.OnLoadPrefs?.Invoke();

        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        foreach (GameObject i in spawnPoint)
        {
            if (i.name == previousScene)
            {
                Transform spawnPlace = i.GetComponent<Transform>();

                spawnCharacter(spawnPlace.position);

                GameManagerScript.state = OpenWorldState.EXPLORE;
                return;
            }
        }

        Debug.Log("SpawnPoint not found");
        Debug.Log("Spawning default spawn temporarily");

        spawnCharacter(new Vector3(.5f, 0f));

        GameManagerScript.state = OpenWorldState.EXPLORE;
    }

    void spawnCharacter(Vector3 spawnPlace)
    {
        if (PlayerData.gender == "male")
        {
            Instantiate(malePlayer, spawnPlace, Quaternion.identity);
        }
        else if (PlayerData.gender == "female")
        {
            Instantiate(femalePlayer, spawnPlace, Quaternion.identity);
        }
        else
        {
            Debug.Log("Error Instatiating player");
            Debug.Log("Spawning Female Player temporarily");
            Instantiate(femalePlayer, spawnPlace, Quaternion.identity);
        }
    }
}
