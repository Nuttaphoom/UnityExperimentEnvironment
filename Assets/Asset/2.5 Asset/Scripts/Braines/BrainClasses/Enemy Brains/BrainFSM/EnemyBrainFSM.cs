using Brainintent;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyBrainFSM  
{
    EnemyBrainState _currentState;
    private bool _setUp = false;

    private List<EnemyBrainState> _validStates;
    public void Init(List<EnemyBrainState> states, System.Type type = null)
    {
        if (_setUp)
            return;

        _validStates = states;
         ChangeState(type == null ? typeof(BaseEnemyBrainIdleState) : type);
        _setUp = true;
    }

    public void ChangeState(System.Type type) 
    {
        for (int i = 0; i  < _validStates.Count; i++)
        {
            if ( _validStates[i].IsSameType(type))
            {
                if (_currentState != null)
                    _currentState.Leave(); 
                _currentState = _validStates[i];
                _currentState.Enter();
                return; 
            }
        }
    }

    public Dictionary<Brainintent.ECharacterActionintentKey,float> SetIntent()
    {
        return _currentState.SetIntent(); 
    }

    public void UpdateState()
    {
        _currentState.UpdateState(this); 
    }
}

