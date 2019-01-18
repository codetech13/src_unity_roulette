using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModePanelScript : MonoBehaviour {

	public GameObject mode1;
	public GameObject myPanel;

	[SerializeField] JoinGame joinGame;

	[SerializeField] string gameidMode1 = "4234234242";
	[SerializeField] string uidMode1 = "222";
	[SerializeField] string joinamountMode1 = "2000";
	[SerializeField] string datetimeMode1 = "1284242";

	public void onClickMode1(){
		mode1.SetActive (true);
		myPanel.SetActive (false);
		DemoTimer.instance.stopTimer = false;
		DemoTimer.instance.timerSec = 10f;

        datetimeMode1 = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        //send join game request
        joinGame.JoinGameRequest(gameidMode1,uidMode1,joinamountMode1,datetimeMode1);

	}

    public void OnClickMode3()
    {
        datetimeMode1 = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        joinGame.JoinGameRequest("LUCKY TARGET", uidMode1, joinamountMode1, datetimeMode1);
        SceneManager.LoadScene("LuckyTargetTimer");
    }

    public void OnClickMode4()
    {

        datetimeMode1 = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
        joinGame.JoinGameRequest("FUN GAME", uidMode1, joinamountMode1, datetimeMode1);
        SceneManager.LoadScene("FunCardGame");
    }
}
