using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResult : MonoBehaviour {

	[SerializeField] string gameid = "4234234242";
	[SerializeField] string uid="222";
	[SerializeField] string joinamount = "2000";
	[SerializeField] string betAmount = "100";
	[SerializeField] string betNumbers = "1,2,3,4";
	[SerializeField] string winningNumber = "2";
	[SerializeField] string isUserWinner = "2";
	[SerializeField] string datetime = "1284242";

	[SerializeField] string url = "http://funasiagame.16mb.com/v1/JoinGameResult";

	public static GameResult instance;

	void Awake(){
		instance = this;
	}

	void Start(){
//		PostRequest ();
	}

	JoinGameResponseData local;

	public void postGameResult(string gameID, string UID, string joinAmount, string betAmount, string betNumbers, string wininngNumber, string isWinner){

		local = new JoinGameResponseData ();
//		local.gameid = gameID;
		local.gameid = this.gameid;
		local.uid = this.uid;
//		local.uid = UID;
		local.joinamount = this.joinamount;
		local.joinamount = joinAmount;
		local.betAmount = betAmount;
		local.betNumbers = betNumbers;
		local.winningNumber = wininngNumber;
		local.isUserWinner = isWinner;
		local.datetime = this.datetime;

		string dataString = JsonUtility.ToJson (local);

		Debug.Log (dataString);
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Authorization", "sadas21321");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes(dataString);

		www = new WWW(this.url, formData, postHeader);
		StartCoroutine(WaitForRequest(www));
	}

	void PostRequest()
	{

		local = new JoinGameResponseData ();
		local.gameid = this.gameid;
		local.uid = this.uid;
		local.joinamount = this.joinamount;
		local.betAmount = this.betAmount;
		local.betNumbers = this.betNumbers;
		local.winningNumber = this.winningNumber;
		local.isUserWinner = this.isUserWinner;
		local.datetime = this.datetime;

		string dataString = JsonUtility.ToJson (local);

		Debug.Log (dataString);
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Authorization", "sadas21321");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes(dataString);

		www = new WWW(this.url, formData, postHeader);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www){
		yield return www;
		Debug.Log (www.text);
		if (www.error != null) {
			Debug.Log (www.error.ToString ());
		} else {
			Debug.Log (www.text);
		}
	}

	[System.Serializable]
	public class JoinGameResponseData{

		public string gameid ;
		public string uid;
		public string joinamount ;
		public string betAmount ;
		public string betNumbers ;
		public string winningNumber;
		public string isUserWinner;
		public string datetime ;

	}
}
