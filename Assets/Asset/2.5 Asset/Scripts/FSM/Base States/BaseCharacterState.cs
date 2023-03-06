using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BasicCharacterController))]
[RequireComponent(typeof(CharacterAnimatorHandler))]
[RequireComponent(typeof(BaseCharacterFSM) ) ]
[RequireComponent(typeof(BasicCharacterBrain))]
public abstract class BaseCharacterState : MonoBehaviour
{
    protected BaseCharacterFSM _fsm;
    protected BasicCharacterController _basicCharacterController;
    protected CharacterAnimatorHandler _characterAnimatorHandler;
    protected BasicCharacterBrain _brain; 

    //For self terminated state 
    protected float _timeSinceEnter;
    protected float _TTL;
    protected bool SelfTerminated()
    {
        _timeSinceEnter  += Time.deltaTime ; 
        return _timeSinceEnter >= _TTL;
    }
     

    public virtual void Init(BaseCharacterFSM fsm)
    {
        _fsm = fsm ;

        _brain = GetComponent<BasicCharacterBrain>();  
        
        if (!TryGetComponent(out _basicCharacterController))
            throw new System.Exception("Missing BasicCharacterController"); 
        if (! TryGetComponent(out _characterAnimatorHandler))
            throw new System.Exception("Missing CharacterAnimatorHandler");


    }

    //UpdateFSMState is called before Do in BaseCharacterFSM
    public abstract void UpdateFSMState(BasicCharacterintent basicCharacterintent);

    public virtual bool IsSameStateType(System.Type type) { 
        return false;
    }  

    public abstract void Enter();
    public abstract void Do(BasicCharacterintent basicCharacterintent);

    public abstract void Exit();

 

}