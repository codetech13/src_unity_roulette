using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModePanelScript : MonoBehaviour {

	public GameObject mode1;
	public GameObject myPanel;

	public void onClickMode1(){
		mode1.SetActive (true);
		//myPanel.SetActive (false);
	}
}
