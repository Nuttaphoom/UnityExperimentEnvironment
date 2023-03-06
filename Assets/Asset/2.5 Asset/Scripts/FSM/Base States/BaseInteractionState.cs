using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseInteractionState : BaseCharacterState
{
    [SerializeField]
    private StateAnimationSO _staetAnimationSO;

    [SerializeField]
    private InteractionManager _interactionLogic;

    private Interaction _interactTarget; 
    public override void Do(BasicCharacterintent basicCharacterintent)
    {
        
    }

    public override void Enter()
    {
        _interactTarget  = _interactionLogic.GetPotentialInteraction();
        if (_interactTarget == null)
        {
            _fsm.ChangeState(typeof(BaseIdleState)); 
            return; 
        }  

        _TTL = _staetAnimationSO.DurationInSecond ;
        _timeSinceEnter = 0;
        if (_interactTarget.interactionType == InteractionType.Pickable)
        {
            _characterAnimatorHandler.PlayAnimation(_staetAnimationSO.AnimationName);
            _interactionLogic.Collect( gameObject); 
        }else if (_interactTarget.interactionType == InteractionType.NPC)
        {
            _characterAnimatorHandler.PlayAnimation(_staetAnimationSO.AnimationName);
            _interactionLogic.InteractWithNPC(_interactTarget.interactableObject.GetComponent<NPCStepControl>() );

        }
    }


    public override void UpdateFSMState(BasicCharacterintent basicCharacterintent)
    {
        if (basicCharacterintent.IsHolding(Brainintent.ECharacterActionintentKey.Hurt))
        {
            _fsm.ChangeState(typeof(BaseHurtState));
        }
        else if (SelfTerminated())
        {
            _fsm.ChangeState(typeof(BaseIdleState));
        }
    }
    public override void Exit()
    {

    }

    public override bool IsSameStateType(Type type)
    {
        if (type == typeof(BaseInteractionState))
        {
            return true;
        }
        return base.IsSameStateType(type);
    }

  
}