using Brainintent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseEnemyCharacterBrain : BasicCharacterBrain
{
    //Properties for Subclass of Enemy Brain
    protected GameObject _playerCharacter;
    protected EnemyBrainFSM enemyBrainFSM;
    protected List<Vector3> _patrollingPoints;


    [SerializeField]
    protected PatrolLogic _patrolLogic;
    [SerializeField]
    protected CharacterNavAgent _characterNavAgent;
    [SerializeField]
    private FieldOfViewDetector _fovDetector;


    protected void Awake()
    {
        base.Awake(); 
        //Setup "basic" needs for character brain
        _patrollingPoints = _patrolLogic.GetPatrolPoints();
        _playerCharacter = GameObject.FindGameObjectWithTag("PlayerCharacter");

        //Setup Brain 
        SetUpBrain(); 
        //Setup States 
        SetUpFSM(); 
        
    }

    private void Update()
    {
        this.ManipulateBrain();
        this.CombineOutsideAndInsideIntent();
        this.RequestAction();       
        //Add UpdateState for enemy brain 
        this.UpdateState();
        this.ResetIntents();
    }

    public CharacterNavAgent GetCharacterNavAgent()
    {
        return _characterNavAgent; 
    }

    public FieldOfViewDetector GetFOVDetector()
    {
        return _fovDetector; 
    }

    protected abstract void SetUpBrain(); 
    protected abstract void SetUpFSM();

    protected abstract void UpdateState(); 

}

