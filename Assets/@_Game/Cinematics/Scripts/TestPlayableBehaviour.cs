using System;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class TestPlayableBehaviour : PlayableBehaviour
{
	public string message;

	private bool isClipEverPlayed = false;
	private PlayableDirector director;

	public override void OnPlayableCreate (Playable playable) {
		director = playable.GetGraph().GetResolver() as PlayableDirector;
	}

	public override void ProcessFrame (Playable playable, FrameData info, object playerData) {
		Debug.Log("message");
		if (!isClipEverPlayed && info.weight > 0f) isClipEverPlayed = true;
	}

	public override void OnBehaviourPause (Playable playable, FrameData info) {
		Debug.Log("pause");
		if (isClipEverPlayed) {
			SceneController.Instance.PausePlayableDirector(director);
		}

		isClipEverPlayed = false;
	}	
}
