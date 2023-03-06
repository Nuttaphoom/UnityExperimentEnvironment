using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events; 

[CreateAssetMenu(fileName = "InputReader", menuName =  "ScriptableObject/System/InputReader")]
public class InputReader : DescriptionBaseSO, GameplayInput.IGameplayActions, GameplayInput.IMenusActions, GameplayInput.IDialoguesActions
{
    //Gameplay 
    public event UnityAction AttackEvent = delegate { } ;
    public event UnityAction<Vector2> MoveEvent = delegate { } ;
    public event UnityAction InteractEvent = delegate { };
    public event UnityAction TabPressEvent = delegate { };

    //Menu
    public event UnityAction TabPressedMenuEvent = delegate { };

    //Dialogue 
    public event UnityAction DialogueAdvanceEvent = delegate { };

    private GameplayInput _input = null;

    private void OnEnable()
    {

        if (_input == null)
        {
            _input = new GameplayInput(); 
            _input.Gameplay.SetCallbacks(this);
            _input.Menus.SetCallbacks(this);
            _input.Dialogues.SetCallbacks(this); 
        }

        _input.Gameplay.Enable();

    }
    //Set Active 
    public void SetMenuActive()
    {
        _input.Gameplay.Disable() ;
        _input.Menus.Enable(); 
        _input.Dialogues.Disable();

    }

    public void SetGameplayActive()
    {
        _input.Gameplay.Enable();
        _input.Menus.Disable();
        _input.Dialogues.Disable(); 
    }

    public void SetDialogueActive()
    {
        _input.Gameplay.Disable();
        _input.Menus.Disable();
        _input.Dialogues.Enable();
    }

    //Gameplay callback from input  

    public void OnAttack(InputAction.CallbackContext context)
    {
        AttackEvent?.Invoke();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>()); 
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        InteractEvent?.Invoke();
    }

    public void OnTabPress(InputAction.CallbackContext context)
    {
        TabPressEvent?.Invoke();
    }

    //Menu callback from input
    public void OnTabPressed(InputAction.CallbackContext context)
    {
        TabPressedMenuEvent?.Invoke(); 
    }

    //Dialogue callback from input 
    public void OnAdvanceDialogue(InputAction.CallbackContext context)
    {
        DialogueAdvanceEvent?.Invoke();
    }
}
