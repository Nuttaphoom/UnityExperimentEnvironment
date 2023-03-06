using UnityEditor;
using UnityEngine;

 

public class PlayerNormalAttack2State : BaseAttackState
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
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Hurt))
        {
            _fsm.ChangeState(typeof(BaseHurtState));
        }
        if (SelfTerminated())
        {
            _fsm.ChangeState(typeof(PlayerIdleState));
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

    public override bool IsSameStateType(System.Type type)
    {
        if (type == typeof(PlayerNormalAttack2State))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }


}