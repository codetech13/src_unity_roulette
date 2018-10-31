using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour {

	[SerializeField] string gameid;
	[SerializeField] string gametype;
	[SerializeField] string datetime;

	[SerializeField] string url = "http://funasiagame.16mb.com/v1/insertGameinfo";

	GameInfoData local;

	void Start(){
		PostRequest ();
	}

	void PostRequest()
	{

		local = new GameInfoData ();
		local.gameid = this.gameid;
		local.gametype = this.gametype;
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
	public class GameInfoData{

		public string gameid;
		public string gametype;
		public string datetime;

	}

}
