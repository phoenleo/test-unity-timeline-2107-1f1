using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
[System.Serializable]
public class AnimatorPlayableBehaviour : PlayableBehaviour
{
	private Animator animator;
	public string animationName;

	private bool isAnimationStarted;

	public override void ProcessFrame (Playable playable, FrameData info, object playerData) {
		if (!isAnimationStarted && info.weight > 0) {
			if (animator == null) animator = playerData as Animator;
			if (!animator.isActiveAndEnabled) return;

			animator.Play(Animator.StringToHash(animationName));
			isAnimationStarted = true;
		}
	}

	public override void OnBehaviourPause (Playable playable, FrameData info) {
		if (isAnimationStarted) isAnimationStarted = false;
	}
}
