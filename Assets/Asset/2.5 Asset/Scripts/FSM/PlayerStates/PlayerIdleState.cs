using UnityEditor;
using UnityEngine;

public class PlayerIdleState : BaseIdleState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        //Concentional use of Idle Update state  
        base.Do(basicCharacterintent);

        
    }

    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        base.UpdateFSMState(basicCharacterintent);
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Attack))
        {
            _fsm.ChangeState(typeof(PlayerNormalAttack1State));
        }

    }
    public override void Enter()
    {
        _characterAnimatorHandler.PlayAnimation(Brainintent.ECharacterActionintentKey.Idle);
    }

    public override void Exit()
    {

    }
    public override bool IsSameStateType(System.Type type)
    {
        if (type == typeof(PlayerIdleState) )
        {
            return true;
        }
        return base.IsSameStateType(type);
    }
}