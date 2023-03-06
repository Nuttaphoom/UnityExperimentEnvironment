using System;
using UnityEditor;
using UnityEngine;

public class HeroKnightNormalAttack1State : BaseAttackState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        if (!_isDealingDamage && _attackAnimationSO.IsInsideInterval(1, _timeSinceEnter))
        {
            _attackHandler.RegisterAttack(_attackAnimationSO.GetIntervalDuration(1));
            _isDealingDamage = true;
        }

    }

    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        if (SelfTerminated())
        {
            _fsm.ChangeState(typeof(HeroKnightIdleState));
        }

        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Hurt))
        {
            _fsm.ChangeState(typeof(BaseHurtState));
        }
    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("hero knight enter attack state***********************************");
        _characterAnimatorHandler.PlayAnimation(_attackAnimationSO.AnimationName);
    }

    public override void Exit()
    {

    }

    public override bool IsSameStateType(Type type)
    {
        if (type == typeof(HeroKnightNormalAttack1State))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }
}