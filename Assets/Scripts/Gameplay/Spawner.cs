using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : MonoBehaviour
{
    public GameObject malePlayer;
    public GameObject femalePlayer;

    public GameObject dog;

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
        foreach (GameObject i in spawnPoint)
        {
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
        if (PlayerDataManager.Instance.gender == "male")
        {
            Instantiate(malePlayer, spawnPlace, Quaternion.identity);
        }
        else if (PlayerDataManager.Instance.gender == "female")
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

    public void LoadSavePlayer()
    {
        Vector3 spawnPosition = new Vector3(PlayerDataManager.Instance.position[0], PlayerDataManager.Instance.position[1], PlayerDataManager.Instance.position[2]);
        spawnCharacter(spawnPosition);
        SpawnDog();
    }

   public void RepositionPlayer()
    {
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

    public void SpawnDog()
    {
        if (Tasks.Instance.taskSlots[7].done)
        {
            Instantiate(dog);
        }
    }
}
