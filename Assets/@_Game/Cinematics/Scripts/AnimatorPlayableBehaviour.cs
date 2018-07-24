using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/*
|------------------------
|   Custom Playable Behaviour
|------------------------
|
|	Used to manipulate animator using timeline
|	Put logic code when clip is played in this PlayableBehaviour scripts
|	
*/
[System.Serializable]
public class AnimatorPlayableBehaviour : PlayableBehaviour
{
	// Serialized fields for designers
	public string animationName;
	
	// Cached references
	private Animator animator;

	// Flag
	private bool isAnimationStarted;

	// This method will be called every frame when timeline is played
	public override void ProcessFrame (Playable playable, FrameData info, object playerData) 
	{
		// Check if animation was started and currently playing
		if (!isAnimationStarted && info.weight > 0) 
		{	
			// Get animator reference to from player data object
			// Player data object is instance from TrackBinding Attribute
			if (animator == null) animator = playerData as Animator;
			
			// Check if current game object is active
			// Will throw warning if we access animator while the game object is disabled
			if (!animator.isActiveAndEnabled) return;

			// Play animator using Hash to improve performance
			animator.Play(Animator.StringToHash(animationName));
			
			// Boolean flag to indicate that the animation is started
			isAnimationStarted = true;
		}
	}

	// This method will be called when the timeline found no clip
	public override void OnBehaviourPause (Playable playable, FrameData info) {
		if (isAnimationStarted) isAnimationStarted = false;
	}
}
