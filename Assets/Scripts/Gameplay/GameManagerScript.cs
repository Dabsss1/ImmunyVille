using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OpenWorldState { EXPLORE, DIALOG, SETTINGS, SCENECHANGING}
public class GameManagerScript : MonoBehaviour
{
    public static OpenWorldState state;
}
