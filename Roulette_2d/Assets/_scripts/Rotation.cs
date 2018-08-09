using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {

	// Use this for initialization
	public GameHud hud;

	bool shouldStop = false;

	bool runOnlyOnce = false;

	void Start(){
		
	}

	// Update is called once per frame
	void Update () {
		if (hud.gameState == GameState.DEFAULT) {
			Debug.Log ("here");
			return;
		} else {
			if (!runOnlyOnce) {
				runOnlyOnce = true;
				StartCoroutine (stop ());
			}
		}

		if (!shouldStop) {
			Debug.Log ("roatet");
			transform.Rotate (0, 0, 2, Space.Self);
		}
	}

	IEnumerator stop(){
		yield return new WaitForSeconds (3);
		shouldStop = true;
	}
}
