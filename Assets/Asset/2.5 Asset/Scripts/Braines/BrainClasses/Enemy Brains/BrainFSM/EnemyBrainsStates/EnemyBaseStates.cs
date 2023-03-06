using Brainintent;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class EnemyBrainState
{
    float _timeSinceStart;
    float _TTL;

    protected BaseEnemyCharacterBrain _enemyCharacterBrain;

    public EnemyBrainState(BaseEnemyCharacterBrain enemyCharacterBrain)
    {
        _enemyCharacterBrain = enemyCharacterBrain; 
    }

    public virtual void Enter() { }
    public virtual void Leave() { }

    public bool IsSelfTerminated()
    {
        _timeSinceStart += Time.deltaTime; 
        return _timeSinceStart > _TTL; 
    }
    public abstract Dictionary<ECharacterActionintentKey, float> SetIntent();
    public abstract void UpdateState(EnemyBrainFSM _fsm);  

    public virtual bool IsSameType(System.Type type) { return false;  }
}

public class BaseEnemyBrainIdleState : EnemyBrainState
{
    public BaseEnemyBrainIdleState(BaseEnemyCharacterBrain enemyCharacterBrain) : base(enemyCharacterBrain)
    {
    }

    public override Dictionary<ECharacterActionintentKey, float> SetIntent()
    {
        Dictionary<ECharacterActionintentKey, float> intent = new Dictionary<ECharacterActionintentKey, float>() ;
        intent.Add(ECharacterActionintentKey.Idle,1);
        return intent;
    }

    public override void UpdateState(EnemyBrainFSM _fsm)
    {
        
    }

    public override bool IsSameType(System.Type type) {
        if (type == typeof(BaseEnemyBrainIdleState))
            return true;
        return base.IsSameType(type); 
    }

}

public class BaseEnemyBrainPatrolState : EnemyBrainState
{
    protected int _pointsIndex;
    protected int _IsIncreasing; 

    protected List<Vector3> _patrollingPoints ;
    protected BasicCharacterController _basicCharacterController; 
    public BaseEnemyBrainPatrolState(BaseEnemyCharacterBrain enemyCharacterBrain , BasicCharacterController characterController, List<Vector3> points    ) :base(enemyCharacterBrain)
    {
        _basicCharacterController = characterController; 
        _IsIncreasing = 1;
        _pointsIndex = 0;

        _patrollingPoints = new List<Vector3>(); 
        for (int i = 0; i < points.Count ; i++ )
        {
            _patrollingPoints.Add(points[i]);
        }
    }

    public override Dictionary<ECharacterActionintentKey, float> SetIntent()
    {
        Dictionary<ECharacterActionintentKey, float> intent = new Dictionary<ECharacterActionintentKey, float>();
        Vector3 direction = GetMoveDirection(); 
        intent.Add(ECharacterActionintentKey.MovementX,direction.x);
        intent.Add(ECharacterActionintentKey.MovementZ,direction.z);
        return intent;
    }

    public override void UpdateState(EnemyBrainFSM _fsm)
    {
        if (_enemyCharacterBrain.GetFOVDetector().IsTargetDetected(_basicCharacterController.GetFaceingDirection() ))
        {
            _fsm.ChangeState(typeof(BaseEnemyBrainAleartState));
        } 
    }

    #region Special Methods 
    private Vector3 GetMoveDirection()
    {
        CharacterNavAgent _characterAgent = _enemyCharacterBrain.GetCharacterNavAgent(); 
        _characterAgent.SetDestination(_patrollingPoints[_pointsIndex] ) ;

        Vector3[] dest;
        bool hasPath;
        (dest,hasPath) = _characterAgent.GetCornerPath()  ;

        if (!hasPath)
            return Vector3.zero; 

        Vector3 direction = dest[1] - _enemyCharacterBrain.transform.position;

        if ((_patrollingPoints[_pointsIndex] - _enemyCharacterBrain.transform.position).magnitude <= 0.1f)
        {
            if (_pointsIndex == _patrollingPoints.Count - 1)
            {
                _IsIncreasing = -1;
            }
            else if (_pointsIndex == 0)
            {
                _IsIncreasing = 1;
            }

            _pointsIndex += _IsIncreasing * 1; 
        }
        return direction.normalized; 
    }
    #endregion

    public override bool IsSameType(System.Type type)
    {
        if (type == typeof(BaseEnemyBrainPatrolState))
            return true;
        return base.IsSameType(type);
    }
}

public class BaseEnemyBrainAleartState : EnemyBrainState
{
    protected GameObject _target;
    protected BasicCharacterController _basicCharacterController ;
    protected float _timeUndetectedTarget;
    protected float _timeBeforeLeave;
    protected float _attackRadius ; 
    public BaseEnemyBrainAleartState(BaseEnemyCharacterBrain enemyCharacterBrain, GameObject target , BasicCharacterController basicCharacterController,float timeBeforeLeave,float attackRadius) : base(enemyCharacterBrain)
    {
        this._target = target ;
        this._timeBeforeLeave = timeBeforeLeave ;
        this._attackRadius = attackRadius ;  
        _basicCharacterController = basicCharacterController ;
    }

    public override Dictionary<ECharacterActionintentKey, float> SetIntent()
    {
        Dictionary<ECharacterActionintentKey, float> intent = new Dictionary<ECharacterActionintentKey, float>();
        Vector3 direction = GetMoveDirection();
        intent.Add(ECharacterActionintentKey.MovementX, direction.x);
        intent.Add(ECharacterActionintentKey.MovementZ, direction.z);
        intent.Add(ECharacterActionintentKey.Aleart,1); 
        return intent;
    }

    public Vector3 GetMoveDirection()
    {
        CharacterNavAgent _characterAgent = _enemyCharacterBrain.GetCharacterNavAgent();
        _characterAgent.SetDestination(_target.transform.position) ;

        Vector3[] dest;
        bool hasPath;
        (dest, hasPath) = _characterAgent.GetCornerPath();

        if (!hasPath)
            return Vector3.zero;

        Vector3 direction = dest[1] - _enemyCharacterBrain.transform.position;

         
        return direction.normalized;
    }

    public override void UpdateState(EnemyBrainFSM _fsm)
    {
        if (!_enemyCharacterBrain.GetFOVDetector().IsTargetDetected(_basicCharacterController.GetFaceingDirection()))
        {
            _timeUndetectedTarget += 1 * Time.deltaTime;
            if (_timeUndetectedTarget >= _timeBeforeLeave)
            {
                _fsm.ChangeState(typeof(BaseEnemyBrainPatrolState));
            }
        }
        else
        {
            if (_attackRadius >= (_target.transform.position - _enemyCharacterBrain.transform.position).magnitude)
            {
                _fsm.ChangeState(typeof(BaseEnemyBrainAttackkState));
            }
            _timeUndetectedTarget = 0;
        }
    }

    public override bool IsSameType(System.Type type)
    {
        if (type == typeof(BaseEnemyBrainAleartState))
            return true;
        return base.IsSameType(type);
    }
}

public class BaseEnemyBrainAttackkState : EnemyBrainState
{
    public BaseEnemyBrainAttackkState(BaseEnemyCharacterBrain enemyCharacterBrain) : base(enemyCharacterBrain)
    {
    }

    public override Dictionary<ECharacterActionintentKey,float> SetIntent()
    {
        Dictionary<ECharacterActionintentKey, float> intent = new Dictionary<ECharacterActionintentKey, float>();
        intent.Add(ECharacterActionintentKey.Attack,1);
        return intent;
    }

    public override void UpdateState(EnemyBrainFSM _fsm)
    {
        _fsm.ChangeState(typeof(BaseEnemyBrainAleartState)); 
    }

    public override bool IsSameType(System.Type type)
    {
        if (type == typeof(BaseEnemyBrainAttackkState))
            return true;
        return base.IsSameType(type);
    }
}