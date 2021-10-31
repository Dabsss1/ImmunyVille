using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public string gender = "";
    public string playerName = "";

    public string savedScene = "";
    public float[] position = { 0,0,0};

    public static PlayerDataManager Instance { get; private set; }

    // Update is called once per frame
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void ResetData()
    {
        gender = "";
        playerName = "";
        savedScene = "";
        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
    }

}
