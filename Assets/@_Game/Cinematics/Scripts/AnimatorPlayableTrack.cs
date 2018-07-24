using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class AnimatorPlayableTrack : PlayableAsset
{
	public AnimatorPlayableBehaviour template = new AnimatorPlayableBehaviour();
	
	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
		return ScriptPlayable<AnimatorPlayableBehaviour>.Create(graph, template);
	}
	
}
