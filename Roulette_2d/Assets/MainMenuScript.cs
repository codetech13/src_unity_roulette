using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour {

	public GameObject myPanel;
	public GameObject singlePlayerPanel;
	public GameObject multiPlayerPanel;
	public GameObject modePanel;

	public void onCLickSinglePlayer(){
		singlePlayerPanel.SetActive (true);
		myPanel.SetActive (false);
	}

	public void onCLickMultiPlayer(){
		multiPlayerPanel.SetActive (true);
		myPanel.SetActive (false);
	}

	public void onClickMode(){
		modePanel.SetActive (true);
		myPanel.SetActive (false);
	}
}
