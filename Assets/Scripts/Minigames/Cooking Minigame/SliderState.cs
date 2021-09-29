using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderState : MonoBehaviour
{
    [HideInInspector]
    public string cookQuality = "";

    public static SliderState Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GreenArea")
        {
            cookQuality = "Gourmet";
        }
        else if (collision.gameObject.name == "YellowArea")
        {
            cookQuality = "Good";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "YellowArea")
        {
            cookQuality = "Edible";
        }
    }
}
