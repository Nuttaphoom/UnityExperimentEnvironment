using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EnemySttate ;

 


namespace EnemySttate {
    public class IdleState : State
    {
        public static List<AttackAnimationData> EnemyAttackAnimationDatas = new List<AttackAnimationData>();
        private EnemyMovement em;

        public IdleState(FSM fsm) : base(fsm) {
            _nextStates.Add((int)EnemyStateController.EEnemyState.Attack);
            _nextStates.Add((int)EnemyStateController.EEnemyState.Hit);
            EnemyAttackAnimationDatas.Add(new AttackAnimationData(AnimatorHash.PunchTrigerHashNumber, 1.0f, 0.0f));
        }
 

        

        public override void StateEnter()
        {
             em = m_fsm.GetStateOwner().GetComponent<EnemyMovement>();
        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {
            if (em.GetCurSpeed() != 0.0f)
            {
                float del = em.GetCurSpeed() > 0.0f ? (  -1.5f * Time.deltaTime) : (  1.5f * Time.deltaTime);
                em.SetCurSpeed(em.GetCurSpeed()  + del);
                if (Mathf.Abs(em.GetCurSpeed() - 0.0f) < 0.1f)
                {
                    em.SetCurSpeed(0);
                }
            }

            m_fsm.GetStateOwner().GetComponent<Enemy>().AnimatorSetFloat(AnimatorHash.RunSpeedFloatHashNumber, m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetCurSpeed());

        }
    }

    public class HitState : State
    {
 
        public HitState(FSM fsm) : base(fsm) { }

        public override void StateEnter()
        {
            _nextStates.Add((int)EnemyStateController.EEnemyState.Idle);
            SetLifeSpan(1.0f);
        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {
 
            if (_lifeSpan <= 0)
            {
                m_fsm.GetStateOwner().GetComponent<Enemy>().GetStateController().SetState(EnemyStateController.EEnemyState.Idle);
            }
            _lifeSpan -= 1 * Time.deltaTime;
        }
    }

    public class AttackState : State
    {
        private bool foundPlayer = false;
        private bool attacked = false; 
        public AttackState(FSM fsm) : base(fsm) { }
        private EnemyMovement em ;

        public override void StateEnter()
        {
            _nextStates.Add((int)EnemyStateController.EEnemyState.Idle);
            _nextStates.Add((int)EnemyStateController.EEnemyState.Hit);
            _nextStates.Add((int)EnemyStateController.EEnemyState.Retreat);

            foundPlayer = false;
            attacked = false;
            SetLifeSpan(1);

            m_fsm.GetStateOwner().GetComponent<Enemy>().TurnParticle(true); 
        }

        public override void StateExit()
        {
            m_fsm.GetStateOwner().GetComponent<Enemy>().TurnParticle(false);
        }

        public override void StateUpdate()
        {
            if (foundPlayer)
            {
               if (! attacked) AttackPlayer();
               else
                {
                    if (_lifeSpan <= 0)
                    {
                        m_fsm.GetStateOwner().GetComponent<Enemy>().GetStateController().SetState(EnemyStateController.EEnemyState.Retreat);
                    }

                    _lifeSpan -= 1 * Time.deltaTime;

                    m_fsm.GetStateOwner().GetComponent<EnemyMovement>().SetCurSpeed(m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetCurSpeed() - 2 * Time.deltaTime);

                    if (m_fsm.GetStateOwner().GetComponent<Enemy>().GetAnimator().GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f &&
                        ! m_fsm.GetStateOwner().GetComponent<Enemy>().GetAnimator().IsInTransition(0))
                    {
                        m_fsm.GetStateOwner().GetComponent<Enemy>().ChangeFSMState(EnemyStateController.EEnemyState.Retreat); 
                    }
                }
            }
            else
            {
                GoToPlayer();
            }

            m_fsm.GetStateOwner().GetComponent<Enemy>().AnimatorSetFloat(AnimatorHash.RunSpeedFloatHashNumber, m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetCurSpeed()) ; 


        }

        void AttackPlayer()
        {
            attacked = true;
            m_fsm.GetStateOwner().GetComponent<Enemy>().AnimatorTriger(AnimatorHash.PunchTrigerHashNumber);
        }

        private void GoToPlayer()
        {
            em = m_fsm.GetStateOwner().GetComponent<EnemyMovement>();


            Transform playerTransform = em.GetPlayerTransform();
            Transform selfTransform = m_fsm.GetStateOwner().transform;
            CharacterController cc = em.GetCharacterController();
            Vector3 lookAtVector = (playerTransform.position - selfTransform.position);
            cc.Move(lookAtVector.normalized * Time.deltaTime * em.GetSpeed());
            m_fsm.GetStateOwner().GetComponent<EnemyMovement>().SetCurSpeed(m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetCurSpeed()  +  2 * Time.deltaTime ); 

            if ((playerTransform.position - selfTransform.position).magnitude < 1)
            {
                foundPlayer = true; 
            }
        }
    }

    public class RetreatState : State
    {
 
        public RetreatState(FSM fsm) : base(fsm)
        {

        }

        Transform _selfTransform;
        Transform _playerTransform;

        private bool Retreat()
        {
            EnemyMovement em = m_fsm.GetStateOwner().GetComponent<EnemyMovement>();

            CharacterController cc = em.GetCharacterController();

            if (!_selfTransform) _selfTransform = m_fsm.GetStateOwner().transform;
            if (!_playerTransform) _playerTransform = m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetPlayerTransform();

            Vector3 lookAtVector = (_playerTransform.position - _selfTransform.position);
            if (lookAtVector.magnitude < 5.0f)
            {
                cc.Move(-1 * lookAtVector.normalized * Time.deltaTime * em.GetSpeed() / 3);
                m_fsm.GetStateOwner().GetComponent<EnemyMovement>().SetCurSpeed(m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetCurSpeed() - 2 * Time.deltaTime);
            }
            else
            {
                 m_fsm.GetStateOwner().GetComponent<EnemyMovement>().SetCurSpeed(m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetCurSpeed() + 2 * Time.deltaTime);
                return false;
            }

            return true; 
        }

        public override void StateEnter()
        {
            _playerTransform = m_fsm.GetStateOwner().GetComponent<EnemyMovement>().GetPlayerTransform(); 
            _selfTransform = m_fsm.GetStateOwner().transform;  
        }

        public override void StateExit()
        {
           
        }

        public override void StateUpdate()
        {
            if (! Retreat())
            {
                m_fsm.GetStateOwner().GetComponent<Enemy>().ChangeFSMState(EnemyStateController.EEnemyState.Idle);

            }


        }
    }

}
public class EnemyStateController  
{
    public enum EEnemyState
    {
        Idle,
        Attack,
        Hit,
        Retreat
    }
    FSM m_fsm = null ;
    EEnemyState enemyState; 
    // Start is called before the first frame update
    public EnemyStateController(GameObject obj)
    {
        m_fsm = new FSM(obj);
        m_fsm.AddState((int)EEnemyState.Idle, new IdleState(m_fsm));
        m_fsm.AddState((int)EEnemyState.Hit, new HitState(m_fsm));
        m_fsm.AddState((int)EEnemyState.Attack, new AttackState(m_fsm));
        m_fsm.AddState((int)EEnemyState.Retreat, new RetreatState(m_fsm));
        SetState((int)EEnemyState.Idle) ;
     }

    public void SetState(EEnemyState es)
    {
        enemyState = es; 
        m_fsm.GetCurrentState()?.StateExit();
        m_fsm.SetCurrentState((int)es);
        m_fsm.GetCurrentState().StateEnter(); 
    }
    // Update is called once per frame
    public void StateUpdate()
    {
        m_fsm.GetCurrentState().StateUpdate(); 
    }

    #region GETTER 
    public EEnemyState GetCurrentEnemyState()
    {
        return enemyState; 
    }
    #endregion
}
