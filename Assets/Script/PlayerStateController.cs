using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 






namespace PlayerState
{

    public class IdleState : State
    {
        public static List<AttackAnimationData> PlayerAttackAnimationDatas = new List<AttackAnimationData>();
        
        public IdleState(FSM fsm) : base(fsm) {
            PlayerAttackAnimationDatas.Clear();
            PlayerAttackAnimationDatas.Add(new AttackAnimationData(AnimatorHash.AirKickTrigerHashNumber, 1.0f,3.0f));
            PlayerAttackAnimationDatas.Add(new AttackAnimationData(AnimatorHash.FlipKickTrigerHashNumber, 1.2f,1.4f * 1.5f));

            _nextStates.Add((int)PlayerStateController.EPlayerState.Attack);
            _nextStates.Add((int)PlayerStateController.EPlayerState.Counter);
            _nextStates.Add((int)PlayerStateController.EPlayerState.Hit) ;
        }

        PlayerController pc;
        float _velocityX, _velocityZ;

        private void RandomAttack(Enemy target)
        {
            int randomAttackType = Random.Range(0, PlayerAttackAnimationDatas.Count) ;
            pc.GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Attack);
            pc.GetPlayerStateController().GetFSM().GetCurrentState().SetLifeSpan(PlayerAttackAnimationDatas[randomAttackType].LifeSpanTime) ;
            pc.GetCombatController().Attack(target, PlayerAttackAnimationDatas[randomAttackType].TrigerHashNumber, PlayerAttackAnimationDatas[randomAttackType].LerpSpeed)  ;
        }

        private void MouseHandler()
        {
            if (! pc) pc = m_fsm.GetStateOwner().GetComponent<PlayerController>();

            if (Input.GetMouseButtonDown(0))
            {
                Enemy target;

                if (pc.GetPlayerDetection().GetDetectedEnemy(out target))
                {
                    RandomAttack(target); 

                }
            }
        }

        private void InputHandler()
        {
    #region Controller 
            if (!pc)  pc = m_fsm.GetStateOwner().GetComponent<PlayerController>();
            Vector3 forward = Camera.main.gameObject.transform.forward;
            Vector3 right = Camera.main.gameObject.transform.right;

            forward.y = 0;
            right.y = 0;

            Vector3 movingDirection = forward * _velocityZ + right * _velocityX;

            _velocityX = Input.GetAxis("Horizontal");
            _velocityZ = Input.GetAxis("Vertical");

            if (Mathf.Abs(_velocityZ) > 0 || Mathf.Abs(_velocityX) > 0)
            {
                Quaternion lookAt = Quaternion.LookRotation(movingDirection);
                Quaternion lookAtRotationOnly_Y = Quaternion.Euler(pc.transform.rotation.eulerAngles.x, lookAt.eulerAngles.y, pc.transform.rotation.eulerAngles.z);

                m_fsm.GetStateOwner().transform.rotation = Quaternion.Slerp(m_fsm.GetStateOwner().transform.rotation, lookAtRotationOnly_Y, 0.05f);
            }
            pc.GetCharacterController().Move(movingDirection * Time.deltaTime * pc.GetSpeed());

            pc.AnimatorSetFloat(AnimatorHash.RunSpeedFloatHashNumber, new Vector2(_velocityX, _velocityZ).sqrMagnitude) ;
            #endregion

    #region Counter 
            if (Input.GetKeyDown(KeyCode.E))
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Counter); 
             
    #endregion
        }
        public override void StateEnter()
        {
            

        }

        public override void StateExit()
        {

        }

        public override void StateUpdate()
        {
            InputHandler();
            MouseHandler();
        }
    }

    public class AttackState : State
    {
        private float _countDownTime = 0.0f;

        public AttackState(FSM fsm) : base(fsm) {
            _inited = false; 
            _nextStates.Add((int) PlayerStateController.EPlayerState.Idle) ;
            _nextStates.Add((int)PlayerStateController.EPlayerState.Counter);

        }

        public override void StateEnter()
        {
            if (_inited)
                return;

            _inited = true;
        }

        public override void StateExit()
        {
            _inited = false; 
        }

        
        public override void StateUpdate()
        {
            #region Counter 
            if (Input.GetKeyDown(KeyCode.E) && _lifeSpan < 0.25f)
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Counter);

            #endregion

            if (_lifeSpan <= 0)
            {
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetCombatController().StopLerping();
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Idle); 
            }

            _lifeSpan -= 1 * Time.deltaTime; 
 

        }
    }

    public class HitState : State
    {
 
        public HitState(FSM fsm) : base(fsm)
        {
            _nextStates.Add((int)PlayerStateController.EPlayerState.Idle);
        }

        public override void StateEnter()
        {
            m_fsm.GetStateOwner().GetComponent<PlayerController>().AnimatorTriger(AnimatorHash.HitHeadTrigerHashNumber); 
        }

        public override void StateExit()
        {

        }


        public override void StateUpdate()
        {
            if (_lifeSpan <= 0)
            {
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Idle);
            }
            _lifeSpan -= 1 * Time.deltaTime;
        }
    }

    public class CounterState : State
    {
        Enemy enemyToCounter = null;  
        public CounterState(FSM fsm) : base(fsm)
        {
            _nextStates.Add((int)PlayerStateController.EPlayerState.Attack);
        }

        public override void StateEnter()
        {
            if (SetEnemyToCounter() )
            {
                Debug.Log("Set enemy successfully"); 
                Counter(); 
            }else
            {
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Idle); 
            }
        }

        public override void StateExit()
        {

        }


        public override void StateUpdate()
        {
            if (_lifeSpan <= 0)
            {
                m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Idle);
            }
            _lifeSpan -= 1 * Time.deltaTime;
        }

        void Counter()
        {
            m_fsm.GetStateOwner().GetComponent<PlayerCombbatController>().Attack(enemyToCounter, AnimatorHash.CounterTrigerHashNumber, 3.0f);
            m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().SetState(PlayerStateController.EPlayerState.Attack);
            m_fsm.GetStateOwner().GetComponent<PlayerController>().GetPlayerStateController().GetFSM().GetCurrentState().SetLifeSpan(1.15f);

        }

        bool SetEnemyToCounter()
        {
            return m_fsm.GetStateOwner().GetComponent<PlayerCombbatController>().SetEnemyToCounter(out enemyToCounter) ;
        }
    }
}
    

public class PlayerStateController  
{
    private FSM m_fsm = null;

    public enum EPlayerState
    {
        Idle,
        Attack,
        Hit,
        Counter 
    }

    // Start is called before the first frame update
    public PlayerStateController(GameObject obj)
    {
        m_fsm = new FSM(obj);
        m_fsm.AddState((int)EPlayerState.Idle, new PlayerState.IdleState(m_fsm));
        m_fsm.AddState((int)EPlayerState.Attack, new PlayerState.AttackState(m_fsm));
        m_fsm.AddState((int)EPlayerState.Hit, new PlayerState.HitState(m_fsm));
        m_fsm.AddState((int)EPlayerState.Counter, new PlayerState.CounterState(m_fsm));
        m_fsm.SetCurrentState((int)EPlayerState.Idle);
    }

    public void SetState(EPlayerState es)
    {
        if (m_fsm.GetCurrentState().IsValidDestinationState((int)es))
        {
            m_fsm.GetCurrentState()?.StateExit();
            m_fsm.SetCurrentState((int)es);
            m_fsm.GetCurrentState().StateEnter();
        }
    }

    public FSM GetFSM()
    {
        return m_fsm; 
    }
    // Update is called once per frame
    public void StateUpdate()
    {
        m_fsm.GetCurrentState().StateUpdate(); 
    }
}
