using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BrainCustomMixer : PlayableBehaviour
{
	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		Transform transform =  playerData as Transform;
		int inputCount = playable.GetInputCount(); //get the number of all clips on this track
		Vector3 final_destination = new Vector3() ; 

		for (int i =0; i < inputCount; i++ )
        {
			float inputWeight = playable.GetInputWeight(i);
			ScriptPlayable<BrainCustomBehavior> inputPlayable = (ScriptPlayable<BrainCustomBehavior>)playable.GetInput(i);
			BrainCustomBehavior behavior = inputPlayable.GetBehaviour();

			final_destination += behavior._destPos * inputWeight; 
		}

		final_destination /= inputCount;

		transform.position = final_destination; // = playerData as Transform;


	}
}
