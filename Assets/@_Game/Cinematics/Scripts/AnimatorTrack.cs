using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;


/*
|-------------------
|   Custom Track   
|-------------------
|
|	- Track Clip Type define what track clip that can be instantiated in this track
|	- Track Binding Type definge what MonoObject that can be bound to this track
|		- Bound object can be manipulated within PlayableBehaviour defined within the track clip
|
*/
[TrackClipType(typeof(AnimatorPlayableTrack))]
[TrackBindingType(typeof(Animator))]
public class AnimatorTrack : TrackAsset 
{
	public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
	{
		foreach (var c in GetClips())
		{
			//Clips are renamed after the actionType of the clip itself
			var clip = (AnimatorPlayableTrack)c.asset;
			c.displayName = clip.template.animationName.ToString();
		}

		return ScriptPlayable<AnimatorPlayableBehaviour>.Create (graph, inputCount);
	}
}
