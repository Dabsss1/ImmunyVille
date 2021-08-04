using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System;
public class InputTouch : MonoBehaviour
{
    public static Action<Vector3> OnFingerDown;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
    }

    private void OnDisable()
    {
        
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
    }


    void FingerDown(Finger finger)
    {
        Vector3 fingerPosition = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        fingerPosition.z -= 10;

        OnFingerDown?.Invoke(fingerPosition);
    }
}
