using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
	public static SceneController Instance;

	public PlayableDirector cinematicsControllerPrefab;
	public GameObject decorationObjectPrefab;
	
	private Action onUpdateAction;
	private PlayableDirector currentlyPausedPlayableDirector;
	
	
	void Awake () {
		// HACK: Quick and dirty Singleton implementation
		if (Instance != null) Destroy(this);
		if (Instance == null) Instance = this;
	}

	void Start () {
		Init();
	}

	void Update () {
		if (onUpdateAction != null) onUpdateAction();
	}

	
	/*
	|-------------------
	|   Public APIs   
	|-------------------
	*/
	public void PausePlayableDirector (PlayableDirector playableDirector) {
		playableDirector.Pause();
		currentlyPausedPlayableDirector = playableDirector;
		onUpdateAction = WaitForSpaceInputToResume;
	}

	
	/*
	|-------------------
	|   Logic   
	|-------------------
	*/
	void Init () {
		// Dynamic object instatiation process
		var decorationObject = Instantiate(decorationObjectPrefab);
		var cinematicsController = Instantiate(cinematicsControllerPrefab);

		// Binding dynamically instatiated object to instantiated timeline (playable director) instance
		// Notes:
		//	- in this case playable asset is bound in prefab asset, so we don't need to instantiate timeline asset
		var timelineAsset = (TimelineAsset) cinematicsController.playableAsset;
		var outputs = timelineAsset.outputs.ToArray();
		for (int i = 0; i < outputs.Count(); i++) {
			var track = (TrackAsset) outputs[i].sourceObject;
			var trackOutputs = track.outputs.ToArray();
			Debug.Log(track.name);	
			Debug.Log(trackOutputs[0].GetType().Name);	// Always return PlayableBinding, must manually try to cast to child classes
			SetBinding(cinematicsController, track, decorationObject);
		}
		
		cinematicsController.Play();
	}

	
	// Set binding based on track name
	// - need better binding logic for multiple bindSource variant
	void SetBinding (PlayableDirector cinematicsController, TrackAsset trackAsset, GameObject bindSource) {
		switch (trackAsset.name) {
			case "Activation Track":
				cinematicsController.SetGenericBinding(trackAsset, bindSource);
				break;
			case "Animator Track":
			case "Animation Track":
				cinematicsController.SetGenericBinding(trackAsset, bindSource.GetComponent<Animator>());
				break;
		}
	}
	
	void ResumeCurrentlyPausedPlayableDirector () {
		if (currentlyPausedPlayableDirector != null) {
			currentlyPausedPlayableDirector.Resume();
		}

		onUpdateAction = null;
		currentlyPausedPlayableDirector = null;
	}
	
	/*
	|-------------------
	|   Delegates   
	|-------------------
	*/
	void WaitForSpaceInputToResume () {
		if (Input.GetKeyUp(KeyCode.Space)) {
			Debug.Log("Space");
			ResumeCurrentlyPausedPlayableDirector();
		}
	}
	
	
	
}
