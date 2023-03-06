using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class AnimatorHash
{
    public static int AirKickTrigerHashNumber = Animator.StringToHash("AirKick");
    public static int HighKickTrigerHashNumber = Animator.StringToHash("HighKick");
    public static int FlipKickTrigerHashNumber = Animator.StringToHash("FlipKick");

    public static int HitHeadTrigerHashNumber = Animator.StringToHash("HeadHit");

    public static int RunSpeedFloatHashNumber = Animator.StringToHash("RunSpeed");

    public static int PunchTrigerHashNumber = Animator.StringToHash("Punch");

    public static int CounterTrigerHashNumber = Animator.StringToHash("Counter");


}

public class AttackAnimationData
{
    public AttackAnimationData(int t, float l, float ls) { TrigerHashNumber = t; LifeSpanTime = l; LerpSpeed = ls; }
    public int TrigerHashNumber;
    public float LifeSpanTime;
    public float LerpSpeed;
}

public abstract class State 
{
    protected List<int> _nextStates = new List<int>() ;
    protected FSM m_fsm ;
    protected float _lifeSpan = Mathf.Infinity ;
    protected bool _inited = false; 

    public abstract void StateUpdate();
    public abstract void StateEnter() ;
    public abstract void StateExit()  ; 

    public State(FSM fsm) 
    {
        m_fsm = fsm;
        
    }

    public void SetLifeSpan(float f)
    {
        _lifeSpan = f; 
    }
    public bool IsValidDestinationState(int key)
    {
        return _nextStates.Contains(key) ;
    }
}

public class FSM : MonoBehaviour
{
    private Dictionary<int, State> states ;
    private State current ;
    private GameObject _stateOwner ; 

    public FSM(GameObject owner)
    {
        this._stateOwner = owner;
        states = new Dictionary<int, State>(); 
    }

    public void AddState(int key, State state)
    {
        states.Add(key, state);  
    }

    #region Getter 
    public State GetCurrentState()
    {
        return current;
    }

    public GameObject GetStateOwner()
    {
        return _stateOwner; 
    }
    #endregion

    public bool SetCurrentState(int key)
    {
        if (current == null|| current.IsValidDestinationState(key))
        {
            current = states[key];
            return true; 
        }

        return false; 
    }

}
