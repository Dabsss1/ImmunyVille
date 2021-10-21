using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : MonoBehaviour
{
    public GameObject malePlayer;
    public GameObject femalePlayer;

    public GameObject[] spawnPoint;

    public static Spawner Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        Instance = this;
    }

    public void SpawnPlayer()
    {
        Debug.Log("called");
        foreach (GameObject i in spawnPoint)
        {
            Debug.Log("called1");

            if (i.name == PlayerSceneInformation.Instance.previousScene)
            {
                //get spawn point coordinates
                Transform spawnPlace = i.GetComponent<Transform>();

                //spawn player on coordinates
                spawnCharacter(spawnPlace.position);

                return;
            }
        }
        
        Debug.Log("SpawnPoint not found\n" +
            "Spawning character from "+spawnPoint[0].name+" temporarily.");
        Transform defaultSpawnPlace = spawnPoint[0].GetComponent<Transform>();
        spawnCharacter(defaultSpawnPlace.position);
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
            Debug.Log("Gender not detected\n" +
                "Spawning Female Player temporarily");
            Instantiate(femalePlayer, spawnPlace, Quaternion.identity);
        }
    }

    public void LoadQuickSave()
    {
        Vector3 spawnPosition = new Vector3(PlayerData.position[0], PlayerData.position[1], PlayerData.position[2]);
        spawnCharacter(spawnPosition);
    }

   public void RepositionPlayer()
    {
        Debug.Log("repositioning from "+ PlayerSceneInformation.Instance.previousScene);
        foreach (GameObject i in spawnPoint)
        {
            if (i.name == PlayerSceneInformation.Instance.previousScene)
            {
                Transform spawnPlace = i.GetComponent<Transform>();

                Player.Instance.transform.position = spawnPlace.position;

                return;
            }
        }
    }
}
