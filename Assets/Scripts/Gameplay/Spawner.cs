using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Spawner : MonoBehaviour
{
    public GameObject malePlayer;
    public GameObject femalePlayer;

    public List<GameObject> spawnPoint;

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
        if (data.gender == "male")
        {
            Instantiate(malePlayer);
        }
        else if (data.gender == "female")
        {
            Instantiate(femalePlayer);
        }
    }
    void SpawnPlayer(PlayerData data)
    {
        
    }
}
