using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroKnightCharacterBrain : BaseEnemyCharacterBrain
{

    protected override void SetUpFSM()
    {
        List<EnemyBrainState> _validStates = new List<EnemyBrainState>();
        enemyBrainFSM = new EnemyBrainFSM();
        _validStates.Add(new BaseEnemyBrainAttackkState(this));
        _validStates.Add(new BaseEnemyBrainIdleState(this));
        _validStates.Add(new BaseEnemyBrainPatrolState(this, _basicCharacterController,_patrollingPoints));
        _validStates.Add(new BaseEnemyBrainAleartState(this,GameObject.FindGameObjectWithTag("PlayerCharacter"), _basicCharacterController, 3.0f,2.5f) );
        enemyBrainFSM.Init(_validStates, typeof(BaseEnemyBrainPatrolState)); 
    }
    protected override void ManipulateBrain()
    {
        Dictionary<Brainintent.ECharacterActionintentKey,float> intents = enemyBrainFSM.SetIntent();  
        foreach (Brainintent.ECharacterActionintentKey intent in intents.Keys)
        {
            Setintent(intent, intents[intent]) ;  
        }
    }

    protected override void UpdateState()
    {
        enemyBrainFSM.UpdateState();
    }

    protected override void SetUpBrain()
    {

    }
}