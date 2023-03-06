using System;
using UnityEditor;
using UnityEngine;

public class PlayerNormalAttack1State : BaseAttackState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        if (! _isDealingDamage && _attackAnimationSO.IsInsideInterval(1, _timeSinceEnter))
        {
             _attackHandler.RegisterAttack(_attackAnimationSO.GetIntervalDuration(1)) ;
            _isDealingDamage = true;
        }
       
    }
    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Hurt))
        {
            _fsm.ChangeState(typeof(BaseHurtState));
        }
        else if (SelfTerminated())
        {
            _fsm.ChangeState(typeof(PlayerIdleState));
        }

        else if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Attack))
        {
            if (_attackAnimationSO.IsInsideInterval(0, _timeSinceEnter))
                _fsm.ChangeState(typeof(PlayerNormalAttack2State));
        }
    }

    public override void Enter() 
    {
        base.Enter();
        _characterAnimatorHandler.PlayAnimation(_attackAnimationSO.AnimationName); 
        
        
    }

    public override void Exit()
    {

    }

    public override bool IsSameStateType(Type type)
    {
        if (type == typeof(PlayerNormalAttack1State))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }


}