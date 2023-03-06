using UnityEditor;
using UnityEngine;

public class EnemyIdleState : BaseIdleState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        base.Do(basicCharacterintent);
    }



    public override bool IsSameStateType(System.Type type)
    {
        if (type == typeof(EnemyIdleState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }
}