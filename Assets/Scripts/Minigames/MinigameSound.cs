using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSound : MonoBehaviour
{
    public void PlayMinigameSound()
    {
        AudioManager.Instance.PlaySfx("Minigame");
    }
}
