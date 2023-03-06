using UnityEditor;
using UnityEngine;

public class HeroKnightMoveState : EnemyMoveState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        base.Do(basicCharacterintent);

    }

    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        base.UpdateFSMState(basicCharacterintent);
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Attack))
        {
            _fsm.ChangeState(typeof(HeroKnightNormalAttack1State));
        }
    }
}