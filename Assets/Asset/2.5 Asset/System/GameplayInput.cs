//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Asset/2.5 Asset/System/GameplayInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @GameplayInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameplayInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameplayInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""cdea5863-460a-4f4c-be71-3a459275712b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b6aa515a-4308-41ce-8872-0a9eceaab20a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""40fa733c-8ea4-46f1-9e1c-a9bc20e3eeab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""bc08392e-5b8d-40cb-a061-0bb5066c659d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TabPress"",
                    ""type"": ""Button"",
                    ""id"": ""c1b67e59-4c73-4864-9772-c9e4d19eaee0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""f98e31e4-f742-4668-b098-705839963055"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""160ad60a-7366-47bb-90b7-bf3bfbb6d5cd"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bf0f4292-a2e1-4503-9df6-6253e20fe3f4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f9d687f0-0c6c-44a0-b848-2baa52ff7a1b"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""075e4ac8-df04-471d-9660-4071bb618ac9"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bbe101e0-9ef4-4685-a69c-98c5b9606724"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42bf8697-8f41-404b-8b26-6df484a1141e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c1d50d2-5b7a-4509-9649-fe49221b6334"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TabPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menus"",
            ""id"": ""dc9d29cb-43c3-48f1-b637-3b485d14b4fa"",
            ""actions"": [
                {
                    ""name"": ""TabPressed"",
                    ""type"": ""Button"",
                    ""id"": ""1b1e3d26-d50e-4221-9289-0c82d5467167"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec37a4f1-86e0-4a6a-9b8f-a3cb4ec79b8b"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TabPressed"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialogues"",
            ""id"": ""aecc707e-eb56-4509-9f93-5c99d8ea167c"",
            ""actions"": [
                {
                    ""name"": ""AdvanceDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""5058ce97-7efc-4a41-9e6b-79f03ec58efb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b8bc87aa-6add-49b1-b49f-105843441d9b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Attack = m_Gameplay.FindAction("Attack", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        m_Gameplay_TabPress = m_Gameplay.FindAction("TabPress", throwIfNotFound: true);
        // Menus
        m_Menus = asset.FindActionMap("Menus", throwIfNotFound: true);
        m_Menus_TabPressed = m_Menus.FindAction("TabPressed", throwIfNotFound: true);
        // Dialogues
        m_Dialogues = asset.FindActionMap("Dialogues", throwIfNotFound: true);
        m_Dialogues_AdvanceDialogue = m_Dialogues.FindAction("AdvanceDialogue", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Attack;
    private readonly InputAction m_Gameplay_Interact;
    private readonly InputAction m_Gameplay_TabPress;
    public struct GameplayActions
    {
        private @GameplayInput m_Wrapper;
        public GameplayActions(@GameplayInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Attack => m_Wrapper.m_Gameplay_Attack;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputAction @TabPress => m_Wrapper.m_Gameplay_TabPress;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Attack.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttack;
                @Interact.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteract;
                @TabPress.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTabPress;
                @TabPress.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTabPress;
                @TabPress.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTabPress;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @TabPress.started += instance.OnTabPress;
                @TabPress.performed += instance.OnTabPress;
                @TabPress.canceled += instance.OnTabPress;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Menus
    private readonly InputActionMap m_Menus;
    private IMenusActions m_MenusActionsCallbackInterface;
    private readonly InputAction m_Menus_TabPressed;
    public struct MenusActions
    {
        private @GameplayInput m_Wrapper;
        public MenusActions(@GameplayInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @TabPressed => m_Wrapper.m_Menus_TabPressed;
        public InputActionMap Get() { return m_Wrapper.m_Menus; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenusActions set) { return set.Get(); }
        public void SetCallbacks(IMenusActions instance)
        {
            if (m_Wrapper.m_MenusActionsCallbackInterface != null)
            {
                @TabPressed.started -= m_Wrapper.m_MenusActionsCallbackInterface.OnTabPressed;
                @TabPressed.performed -= m_Wrapper.m_MenusActionsCallbackInterface.OnTabPressed;
                @TabPressed.canceled -= m_Wrapper.m_MenusActionsCallbackInterface.OnTabPressed;
            }
            m_Wrapper.m_MenusActionsCallbackInterface = instance;
            if (instance != null)
            {
                @TabPressed.started += instance.OnTabPressed;
                @TabPressed.performed += instance.OnTabPressed;
                @TabPressed.canceled += instance.OnTabPressed;
            }
        }
    }
    public MenusActions @Menus => new MenusActions(this);

    // Dialogues
    private readonly InputActionMap m_Dialogues;
    private IDialoguesActions m_DialoguesActionsCallbackInterface;
    private readonly InputAction m_Dialogues_AdvanceDialogue;
    public struct DialoguesActions
    {
        private @GameplayInput m_Wrapper;
        public DialoguesActions(@GameplayInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @AdvanceDialogue => m_Wrapper.m_Dialogues_AdvanceDialogue;
        public InputActionMap Get() { return m_Wrapper.m_Dialogues; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialoguesActions set) { return set.Get(); }
        public void SetCallbacks(IDialoguesActions instance)
        {
            if (m_Wrapper.m_DialoguesActionsCallbackInterface != null)
            {
                @AdvanceDialogue.started -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnAdvanceDialogue;
                @AdvanceDialogue.performed -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnAdvanceDialogue;
                @AdvanceDialogue.canceled -= m_Wrapper.m_DialoguesActionsCallbackInterface.OnAdvanceDialogue;
            }
            m_Wrapper.m_DialoguesActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AdvanceDialogue.started += instance.OnAdvanceDialogue;
                @AdvanceDialogue.performed += instance.OnAdvanceDialogue;
                @AdvanceDialogue.canceled += instance.OnAdvanceDialogue;
            }
        }
    }
    public DialoguesActions @Dialogues => new DialoguesActions(this);
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnTabPress(InputAction.CallbackContext context);
    }
    public interface IMenusActions
    {
        void OnTabPressed(InputAction.CallbackContext context);
    }
    public interface IDialoguesActions
    {
        void OnAdvanceDialogue(InputAction.CallbackContext context);
    }
}
