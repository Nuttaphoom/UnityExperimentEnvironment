using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseMoveState : BaseCharacterState
{ 
    
    public override void Enter()
    {
        _timeSinceEnter = 0; 
    }
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.MovementX) ||
            basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.MovementZ))
        {
            float x = basicCharacterintent.GetIntentActionVlue(Brainintent.ECharacterActionintentKey.MovementX);
            float z = basicCharacterintent.GetIntentActionVlue(Brainintent.ECharacterActionintentKey.MovementZ);
            _basicCharacterController.MoveAlongAxis(new Vector3(x, 0, z));
            _characterAnimatorHandler.PlayAnimation(Brainintent.ECharacterActionintentKey.MovementX);

            _characterAnimatorHandler.SetVisualForward2DDirection(x);
            _basicCharacterController.SetCharacterForward2DDirection(x); 
        }else
        {
            _fsm.ChangeState(typeof(BaseIdleState));

        }

    }

    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Hurt))
        {
            _fsm.ChangeState(typeof(BaseHurtState));
            return; 
        }

        else if (! basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.MovementX) &&
           ! basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.MovementZ))
        {
            _fsm.ChangeState(typeof(BaseIdleState));
            return; 
        }

    }

    public override bool IsSameStateType(Type type)
    {
        if (type == typeof(BaseMoveState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }

    public override void Exit()
    {

    }

  
}