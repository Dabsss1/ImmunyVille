using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Saving : MonoBehaviour
{
    public static Saving Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void QuickSave()
    {
        PlayerData.minute = TimeManager.minute;
        PlayerData.hour = TimeManager.hour;
        PlayerData.day = TimeManager.day;

        PlayerData.position[0] = Player.Instance.transform.position.x;
        PlayerData.position[1] = Player.Instance.transform.position.y;
        PlayerData.position[2] = Player.Instance.transform.position.z;

        PlayerData.isQuickSave = true;

        Debug.Log("Saved: " + SceneManager.GetActiveScene().name);
        PlayerData.scene = SceneManager.GetActiveScene().name;

        SaveSystem.SavePlayer();
    }
}
