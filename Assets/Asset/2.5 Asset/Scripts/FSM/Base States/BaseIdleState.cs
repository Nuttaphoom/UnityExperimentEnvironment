using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseIdleState : BaseCharacterState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {



    }



    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Hurt))
        {
            _fsm.ChangeState(typeof(BaseHurtState));
        }
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.MovementX) || basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.MovementZ))
        {
            _fsm.ChangeState(typeof(BaseMoveState));
        }if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Interact))
        {
            _fsm.ChangeState(typeof(BaseInteractionState)); 
        }

    }
    public override void Enter()
    {
        _TTL = 0;
        _timeSinceEnter = 0; 
        _characterAnimatorHandler.PlayAnimation(Brainintent.ECharacterActionintentKey.Idle);

    }

    public override void Exit()
    {

    }

    public override bool IsSameStateType(Type type)
    {
        if (type == typeof(BaseIdleState) )
        {
            return true; 
        }
        return base.IsSameStateType(type);
    }
}