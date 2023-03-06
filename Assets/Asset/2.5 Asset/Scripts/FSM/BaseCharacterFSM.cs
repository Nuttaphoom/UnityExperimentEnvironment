using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterFSM : MonoBehaviour
{
    private BaseCharacterState _currentState ;
    private bool _setUp = false;

    private List<BaseCharacterState> _validStates ; 
    private void Start ()
    {
        if (_setUp)
            return; 

       GetComponent<BasicCharacterBrain>().GetCharacterIntents().OnActionRequest += Do ;

        _validStates = new List<BaseCharacterState>();
       foreach (BaseCharacterState state in GetComponents<BaseCharacterState>())
        {
            state.Init(this); 
            _validStates.Add(state); 
        }
       ChangeState(typeof(BaseIdleState)); 
       _setUp = true; 
    }

    public void Enter()
    {
        if (!_setUp)
            return; 
        _currentState.Enter(); 
    }

    public void Exit()
    {
        if (!_setUp)
            return;
        _currentState.Exit();
    }

    public void Do(BasicCharacterintent basicCharacterintent)
    {
        if (!_setUp)
            return;

        _currentState.UpdateFSMState(basicCharacterintent);
        _currentState.Do(basicCharacterintent);

    }

    public void ChangeState(System.Type type)
    {
        if (_currentState != null)
            _currentState.Exit() ;
        foreach (BaseCharacterState s in _validStates)
        {
            if (s.IsSameStateType(type))
            {
                _currentState = s ;
                _currentState.Enter();
                return; 
            }
        }
        foreach (BaseCharacterState s in _validStates)
        {
            if (s.IsSameStateType(type))
            {
                _currentState = s;
                _currentState.Enter();
                return;
            }
        }
        throw new System.Exception("Invalid state destination in " + gameObject.name + " trying to access " + type ); 

    }

    public   System.Type GetCurrentStateType()
    {
        if (_currentState == null)
        {
            return typeof(BaseIdleState).GetType(); 
        }
        return _currentState.GetType() ; 
    }


}

