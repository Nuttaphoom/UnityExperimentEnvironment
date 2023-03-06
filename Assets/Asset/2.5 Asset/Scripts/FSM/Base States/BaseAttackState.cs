using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public abstract class BaseAttackState : BaseCharacterState
{
    [Header("Attack & Animation Properties for Attack State")]
    [SerializeField]
    protected AttackAnimationSO _attackAnimationSO;

    protected AttackHandler _attackHandler; 
 
    protected bool _isDealingDamage; 

 
    public override void Enter()
    {
        _TTL = _attackAnimationSO.DurationInSecond   ;
        _timeSinceEnter = 0   ;  
        _isDealingDamage = false;
        _attackHandler = GetComponent<AttackHandler>(); 
    }


    public override bool IsSameStateType(System.Type type)
    {
        if (type == typeof(BaseAttackState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }

 
}