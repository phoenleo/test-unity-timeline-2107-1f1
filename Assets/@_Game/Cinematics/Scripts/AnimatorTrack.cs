using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

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
