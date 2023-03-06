using UnityEditor;
using UnityEngine;

public class HeroKnightIdleState : EnemyIdleState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("enemy idle state********");
    }
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        base.Do(basicCharacterintent); 

        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Attack))
        {
            _fsm.ChangeState(typeof(HeroKnightNormalAttack1State));
        }
    }

    public override bool IsSameStateType(System.Type type)
    {
        if (type == typeof(HeroKnightIdleState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }
}