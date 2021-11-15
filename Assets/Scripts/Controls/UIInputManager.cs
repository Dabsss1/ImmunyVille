using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIInputManager : MonoBehaviour
{
    public static Action<string> OnDpadUp, OnDpadDown, OnDpadLeft, OnDpadRight, OnDpadCancelled;

    PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Up.performed += ctx => OnDpadUp?.Invoke("Up");
        controls.Player.Up.canceled += ctx => OnDpadCancelled?.Invoke("Cancelled");

        controls.Player.Down.performed += ctx => OnDpadDown?.Invoke("Down");
        controls.Player.Down.canceled += ctx => OnDpadCancelled?.Invoke("Cancelled");


        controls.Player.Left.performed += ctx => OnDpadLeft?.Invoke("Left");
        controls.Player.Left.canceled += ctx => OnDpadCancelled?.Invoke("Cancelled");


        controls.Player.Right.performed += ctx => OnDpadRight?.Invoke("Right");
        controls.Player.Right.canceled += ctx => OnDpadCancelled?.Invoke("Cancelled");
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
    private void Update()
    {
        
    }
}
