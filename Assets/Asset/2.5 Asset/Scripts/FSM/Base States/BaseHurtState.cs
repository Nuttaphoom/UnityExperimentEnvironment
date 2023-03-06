using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HurtHandler))]
public class BaseHurtState : BaseCharacterState
{

    [SerializeField]
    protected HurtAnimationSO _hurtAnimationSO; 
 


    public override void Do(BasicCharacterintent basicCharacterintent)
    {
     
    }

    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        if (SelfTerminated())
        {
            _fsm.ChangeState(typeof(BaseIdleState));
        }
    }

    public override void Enter()
    {
        _TTL = _hurtAnimationSO.DurationInSecond;
        _timeSinceEnter = 0; 
        _characterAnimatorHandler.PlayAnimation(_hurtAnimationSO.AnimationName); 
    }

    public override void Exit()
    {

    }

    public override bool IsSameStateType(Type type)
    {
        if (type == typeof(BaseHurtState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }

 
}