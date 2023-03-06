using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using System;

[Serializable]
public class BrainCustomClip : PlayableAsset
{

    [SerializeField] public BrainCustomBehavior _targetBrain;
    [SerializeField] public Vector3 _targetPosition; 

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<BrainCustomBehavior> playable = ScriptPlayable<BrainCustomBehavior>.Create(graph, _targetBrain);

        BrainCustomBehavior _behavior = playable.GetBehaviour();
        _behavior._destPos = _targetPosition; 
        return playable ; 
    }
}
