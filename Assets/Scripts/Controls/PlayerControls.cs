// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Controls/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""517ff0c0-e24b-4ee8-9b51-d8ace3f2dd4e"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""d6718dcf-f716-403b-bd6b-b4827d93613f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""b48059e6-5404-4d6c-8c24-76815a99ddf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Left"",
                    ""type"": ""Button"",
                    ""id"": ""31b7e65f-d109-4fa6-b396-39d27d2eeca4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Right"",
                    ""type"": ""Button"",
                    ""id"": ""7e1b9971-7a09-4fba-9d69-33b41e02b936"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cross"",
                    ""type"": ""Button"",
                    ""id"": ""74333bfe-0e53-48dd-81d7-babb2ca4e252"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Square"",
                    ""type"": ""Button"",
                    ""id"": ""005454f7-dcc6-4175-ab65-414b8c98f455"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Triangle"",
                    ""type"": ""Button"",
                    ""id"": ""9f2939f8-cfd7-41d9-98cb-f39e8608af1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Circle"",
                    ""type"": ""Button"",
                    ""id"": ""75547da0-66fd-4436-90a7-eadb206a6391"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""f0ddaec5-2fc1-4751-b321-18ac1b9cf26c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1a740c68-eed5-4d83-a891-d3222c54ad05"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""723181cb-c6ac-4050-a4e9-098348b5df14"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a79025fa-68a8-4594-be87-113cae23caec"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Left"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e067d368-aa9c-4dac-8b7b-6889a94600e9"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Right"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cd1d7bc0-3ddb-426b-9106-eee3ef900bf2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cross"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6705e35-4ffd-4345-9f16-68083261844e"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Square"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdc559d2-5d64-46e5-a96b-118b33302a77"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Triangle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d637faa8-3119-4cb6-a5ca-e9c6067d37a8"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Circle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9fc7f03d-f058-4800-bfba-c5780e04a731"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Up = m_Player.FindAction("Up", throwIfNotFound: true);
        m_Player_Down = m_Player.FindAction("Down", throwIfNotFound: true);
        m_Player_Left = m_Player.FindAction("Left", throwIfNotFound: true);
        m_Player_Right = m_Player.FindAction("Right", throwIfNotFound: true);
        m_Player_Cross = m_Player.FindAction("Cross", throwIfNotFound: true);
        m_Player_Square = m_Player.FindAction("Square", throwIfNotFound: true);
        m_Player_Triangle = m_Player.FindAction("Triangle", throwIfNotFound: true);
        m_Player_Circle = m_Player.FindAction("Circle", throwIfNotFound: true);
        m_Player_Start = m_Player.FindAction("Start", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Up;
    private readonly InputAction m_Player_Down;
    private readonly InputAction m_Player_Left;
    private readonly InputAction m_Player_Right;
    private readonly InputAction m_Player_Cross;
    private readonly InputAction m_Player_Square;
    private readonly InputAction m_Player_Triangle;
    private readonly InputAction m_Player_Circle;
    private readonly InputAction m_Player_Start;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_Player_Up;
        public InputAction @Down => m_Wrapper.m_Player_Down;
        public InputAction @Left => m_Wrapper.m_Player_Left;
        public InputAction @Right => m_Wrapper.m_Player_Right;
        public InputAction @Cross => m_Wrapper.m_Player_Cross;
        public InputAction @Square => m_Wrapper.m_Player_Square;
        public InputAction @Triangle => m_Wrapper.m_Player_Triangle;
        public InputAction @Circle => m_Wrapper.m_Player_Circle;
        public InputAction @Start => m_Wrapper.m_Player_Start;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Up.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Up.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Up.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUp;
                @Down.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Down.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDown;
                @Left.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Left.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Left.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLeft;
                @Right.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Right.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Right.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnRight;
                @Cross.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCross;
                @Cross.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCross;
                @Cross.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCross;
                @Square.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquare;
                @Square.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquare;
                @Square.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSquare;
                @Triangle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTriangle;
                @Triangle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTriangle;
                @Triangle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTriangle;
                @Circle.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCircle;
                @Circle.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCircle;
                @Circle.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCircle;
                @Start.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStart;
                @Start.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStart;
                @Start.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStart;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Up.started += instance.OnUp;
                @Up.performed += instance.OnUp;
                @Up.canceled += instance.OnUp;
                @Down.started += instance.OnDown;
                @Down.performed += instance.OnDown;
                @Down.canceled += instance.OnDown;
                @Left.started += instance.OnLeft;
                @Left.performed += instance.OnLeft;
                @Left.canceled += instance.OnLeft;
                @Right.started += instance.OnRight;
                @Right.performed += instance.OnRight;
                @Right.canceled += instance.OnRight;
                @Cross.started += instance.OnCross;
                @Cross.performed += instance.OnCross;
                @Cross.canceled += instance.OnCross;
                @Square.started += instance.OnSquare;
                @Square.performed += instance.OnSquare;
                @Square.canceled += instance.OnSquare;
                @Triangle.started += instance.OnTriangle;
                @Triangle.performed += instance.OnTriangle;
                @Triangle.canceled += instance.OnTriangle;
                @Circle.started += instance.OnCircle;
                @Circle.performed += instance.OnCircle;
                @Circle.canceled += instance.OnCircle;
                @Start.started += instance.OnStart;
                @Start.performed += instance.OnStart;
                @Start.canceled += instance.OnStart;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IPlayerActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
        void OnLeft(InputAction.CallbackContext context);
        void OnRight(InputAction.CallbackContext context);
        void OnCross(InputAction.CallbackContext context);
        void OnSquare(InputAction.CallbackContext context);
        void OnTriangle(InputAction.CallbackContext context);
        void OnCircle(InputAction.CallbackContext context);
        void OnStart(InputAction.CallbackContext context);
    }
}
