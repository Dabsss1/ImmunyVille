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

    public static Action<PlayerData> OnStartScene;

    private void Awake()
    {
        
    }

    void OnEnable()
    {
        OnStartScene += SpawnPlayer;
    }

    private void OnDisable()
    {
        OnStartScene -= SpawnPlayer;
    }

    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        GamePreferencesManager.OnLoadPrefs?.Invoke();
        
        foreach (GameObject i in spawnPoint)
        {
            if (i.name == previousScene)
            {
                Transform spawnPlace = i.GetComponent<Transform>();
                if (data.gender == "male")
                {
                    Instantiate(malePlayer,spawnPlace.position, Quaternion.identity);
                }
                else if (data.gender == "female")
                {
                    Instantiate(femalePlayer,spawnPlace);
                }
                else
                {
                    Debug.Log("Error Instatiating player");
                    Debug.Log("Spawning Female Player temporarily");
                    Instantiate(femalePlayer, spawnPlace.position, Quaternion.identity);
                }
                GameManagerScript.state = OpenWorldState.EXPLORE;
                return;
            }
            Debug.Log(i.name + "!=" +previousScene);
        }

        Debug.Log("SpawnPoint not found");
        Debug.Log("Error Instatiating player");
        Debug.Log("Spawning Female Player and default spawn temporarily");
        Instantiate(femalePlayer, new Vector3(.5f,0f), Quaternion.identity);

        GameManagerScript.state = OpenWorldState.EXPLORE;

    }
    void SpawnPlayer(PlayerData data)
    {
        
    }
}
