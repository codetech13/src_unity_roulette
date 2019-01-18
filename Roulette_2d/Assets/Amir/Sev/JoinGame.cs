using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinGame : MonoBehaviour {

	[SerializeField] string gameid = "4234234242";
	[SerializeField] string uid = "222";
	[SerializeField] string joinamount = "2000";
	[SerializeField] string datetime = "1284242";

	[SerializeField] string url = "http://funasiagame.16mb.com/v1/JoinGame";

	void Start(){
//		PostRequest ();
	}

	JoinGameData local;

	public void JoinGameRequest(string gameID, string UID, string joinAmount, string dataTime){
		local = new JoinGameData ();
		local.gameid = gameID;
//		local.uid = UID;
		local.uid = GameData.instance.localData.uid;
		local.joinamount = joinAmount;
        local.datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");


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

		local = new JoinGameData ();
		local.gameid = this.gameid;
		local.uid = this.uid;
		local.joinamount = this.joinamount;
		local.datetime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

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
	public class JoinGameData{

		public string gameid;
		public string uid;
		public string joinamount;
		public string datetime;

	}
}
