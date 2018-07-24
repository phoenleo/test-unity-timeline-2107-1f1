using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class TestPlayableTrack : PlayableAsset 
{
	public TestPlayableBehaviour template = new TestPlayableBehaviour();
	
	public override Playable CreatePlayable (PlayableGraph graph, GameObject owner) {
		return ScriptPlayable<TestPlayableBehaviour>.Create(graph, template);  
	}
}

