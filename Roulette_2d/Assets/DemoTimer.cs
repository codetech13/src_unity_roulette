using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DemoTimer : MonoBehaviour {
	public float timerSec = 60;
	public static DemoTimer instance;
	public bool stopTimer = true;
	public Text timerText;

	void Awake (){
		instance = this;
	}

	
	// Update is called once per frame
	void Update () {
		if (!stopTimer && timerSec > 0) {
			timerSec = timerSec - Time.deltaTime;
//			Debug.Log ("yyyy");
		}

		if(timerSec <=0 && !stopTimer){
			timerSec = 0;
			stopTimer = true;
			GameHud.instance.HideImage ();
		}
		timerText.text = timerSec.ToString ("F0");
		
	}

	public void resetTimer(){
		timerSec = 10;
		stopTimer = false;
	}
}
