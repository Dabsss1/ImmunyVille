using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] string backgroundMusic;

    private void Start()
    {
        AudioManager.PlaySound?.Invoke(backgroundMusic);
    }

    private void OnDisable()
    {
        AudioManager.StopSound?.Invoke(backgroundMusic);
    }
}
