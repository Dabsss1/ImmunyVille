using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using System;
public class InputTouch : MonoBehaviour
{
    public static Action<Vector3> OnFingerDown;
    public static Action<Vector3> OnFingerMove;
    public static Action<Vector3> OnFingerUp;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove += FingerMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp += FingerUp;
    }

    private void OnDisable()
    {
        TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= FingerDown;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerMove -= FingerMove;
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerUp -= FingerUp;
    }


    void FingerDown(Finger finger)
    {
        Vector3 fingerPosition = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        fingerPosition.z -= 10;

        OnFingerDown?.Invoke(fingerPosition);
    }

    void FingerMove(Finger finger)
    {
        Vector3 fingerPosition = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        fingerPosition.z -= 10;
        //Debug.Log("Drag:\n x: "+fingerPosition.x+"\ny: "+fingerPosition.y);
        OnFingerMove?.Invoke(fingerPosition);
    }

    void FingerUp(Finger finger)
    {
        Vector3 fingerPosition = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        fingerPosition.z -= 10;
        //Debug.Log("FingerUP:\n x: " + fingerPosition.x + "\ny: " + fingerPosition.y);
        OnFingerUp?.Invoke(fingerPosition);
    }
}
