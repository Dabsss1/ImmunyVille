using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractButton : MonoBehaviour
{
    public static Action OnInteract;

    public void InteractPress()
    {
        OnInteract?.Invoke();
    }
}
