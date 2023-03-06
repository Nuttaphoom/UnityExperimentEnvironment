using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables; 

using TMPro;

[TrackClipType(typeof(BrainCustomClip))]
[TrackBindingType(typeof(Transform))]
 public class BrainCustomTrack : PlayableTrack
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<BrainCustomMixer>.Create(graph, inputCount);
    }



}
