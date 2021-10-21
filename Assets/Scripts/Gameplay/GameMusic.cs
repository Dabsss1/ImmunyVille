using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    [SerializeField] string backgroundMusic;

    private void Start()
    {
        AudioManager.Instance.Play(backgroundMusic);
    }

    private void OnDisable()
    {
        AudioManager.Instance.Stop(backgroundMusic);
    }
}
