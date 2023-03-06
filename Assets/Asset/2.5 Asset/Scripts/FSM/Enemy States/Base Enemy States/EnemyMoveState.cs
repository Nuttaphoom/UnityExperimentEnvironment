using UnityEditor;
using UnityEngine;

public class EnemyMoveState : BaseMoveState
{
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        base.Do(basicCharacterintent); 
    }

    public override bool IsSameStateType(System.Type type)
    {
        if (type == typeof(EnemyMoveState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }
}