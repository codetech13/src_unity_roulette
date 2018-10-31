using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefrences : MonoBehaviour {

	public static Prefrences p;

	private int coins;

	void Awake(){
		p = this;

//		addCoins (800);
//		ResetCoins ();
	}
		
	void ResetCoins(){
		PlayerPrefs.DeleteKey("tOTALcOins");
	}

	public int getTotalCoins(){
		return PlayerPrefs.GetInt ("tOTALcOins");
	}

	public void useCoins(int value){
		int temp;
		temp = PlayerPrefs.GetInt ("tOTALcOins") - value;
		if (temp < 0) {
			temp = 0;
		}
		PlayerPrefs.SetInt ("tOTALcOins", temp);
	}

	public int addCoins(int value){
		int temp;
		temp = PlayerPrefs.GetInt ("tOTALcOins") + value;
		PlayerPrefs.SetInt ("tOTALcOins", temp);
		return temp;
	}

}
