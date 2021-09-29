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
        //SaveSystem.LoadPlayer();
    }

    private void Start()
    {
        //previousScene = Player.Instance.currentScene;
        //GamePreferencesManager.OnLoadPrefs?.Invoke();

        if (Player.Instance == null)
            previousScene = "PlayerLot";
        else
            previousScene = Player.Instance.lastScene;

        Debug.Log("Previous Scene: " + previousScene);

        GameManagerScript.sceneState = SceneState.OPENWORLD;

        if (previousScene=="MainMenu")
        {
            LoadQuickSave();
            return;
        }
        SpawnPlayer();
    }
    void SpawnPlayer()
    {
        if (Player.Instance != null && !Player.Instance.gameObject.activeSelf)
        {
            Player.Instance.gameObject.SetActive(true);
        }

        if (Player.Instance == null)
        {
            foreach (GameObject i in spawnPoint)
            {
                if (i.name == previousScene)
                {
                    Transform spawnPlace = i.GetComponent<Transform>();

                    spawnCharacter(spawnPlace.position);

                    GameManagerScript.state = OpenWorldState.EXPLORE;
                    Player.Instance.isSpawned = true;

                    return;
                }
            }

            Debug.Log("SpawnPoint not found\n" +
                "Spawning default spawn temporarily");

            spawnCharacter(new Vector3(.5f, 0f));
        }
        else
        {
            foreach (GameObject i in spawnPoint)
            {
                if (i.name == previousScene)
                {
                    Transform spawnPlace = i.GetComponent<Transform>();

                    Player.Instance.transform.position = spawnPlace.position;

                    GameManagerScript.state = OpenWorldState.EXPLORE;
                    return;
                }
            }
        }

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
            Debug.Log("Gender not specified\n" +
                "Spawning Female Player temporarily");
            Instantiate(femalePlayer, spawnPlace, Quaternion.identity);
        }
    }

    void LoadQuickSave()
    {
        Vector3 spawnPosition = new Vector3(PlayerData.position[0], PlayerData.position[1], PlayerData.position[2]);
        spawnCharacter(spawnPosition);
        GameManagerScript.state = OpenWorldState.EXPLORE;
    }
}
