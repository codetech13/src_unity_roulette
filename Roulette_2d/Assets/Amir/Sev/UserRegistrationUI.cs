using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Pathfinding.Serialization.JsonFx;
using System.Text;
using System;

public class UserRegistrationUI : MonoBehaviour {

	[SerializeField] string usertype = "1";
	[SerializeField] string gsm_token = "sdfsdfsdyytyty23242342342sdfsd";
	[SerializeField] string url = "http://funasiagame.16mb.com/v1/Registration";

	[SerializeField] InputField playerName;
	[SerializeField] InputField email;
	[SerializeField] InputField address;
	[SerializeField] InputField userID;
	[SerializeField] InputField mobileNumber;
	[SerializeField] InputField password;
	[SerializeField] InputField day;
	[SerializeField] InputField month;
	[SerializeField] InputField year;

	[SerializeField] Text errorText;

	public string message;

	void Start(){
		
	}

	public void submit(){
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			PostRequest ();
		} else {
			Debug.LogError ("INTERNET NOT AVAILABLE");
		}
	}

	RegistrationData local;

	void PostRequest()
	{

		local = new RegistrationData ();
		local.playername = playerName.text;
		local.dob = day.text + "/" + month.text + "/" + year.text;
		local.email = email.text;
		local.address = address.text;
		local.username = userID.text;
		local.password = password.text;
		local.mobile = mobileNumber.text;
		local.usertype = usertype;
		local.gsm_token = gsm_token;

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

		responseData = new response ();
		responseData = JsonReader.Deserialize<response> (www.text);
		Debug.Log ("data " + responseData.data.address);

		//load next scene if successful
		message = responseData.message;
//		SplashScript.instance.showMessage ();
		if (message.Contains ("Successfully")) {
			LogInScript.instance.RegisteredSuccessfully ();
		} else if (message.Contains ("already registered")) {
			Debug.LogError ("ALREADY REGISTERED");
		} else {
			Debug.LogError ("USER NOT REGISTERED");
		}


		//set local data in gamedata
		GameData.instance.localData = responseData.data;
	}

	public response responseData;

	public void OnValidateDay(){
		if (int.Parse (day.text) > 31) {
			errorText.text = "Enter a valid date...";
			day.text = null;
		} else {
			errorText.text = "";
		}
	}

	public void OnValidateMonth(){
		if (int.Parse (month.text) > 12) {
			errorText.text = "Enter a valid month...";
			month.text = null;
		} else {
			if (int.Parse (month.text) == 2) {
				if (int.Parse (day.text) > 29) {
					errorText.text = "Enter a valid date...";
				} else {
					errorText.text = "";
				}
			} else {
				errorText.text = "";
			}
		}
	}

	public void OnValidateYear(){
		if (DateTime.Today.Year - int.Parse (year.text) < 15) {
			errorText.text = "Bring your dad you are just a child...";
			year.text = null;
		} else {
			errorText.text = "";
		}
	}


	#region HELPER CLASSES
	[System.Serializable]
	public class RegistrationData{

		public string playername;
		public string dob;
		public string email;
		public string address;
		public string username;
		public string password;
		public string mobile;
		public string usertype;
		public string gsm_token;

	}

	[System.Serializable]
	public class response{
		public string status_code;
		public string message;
		public data data;
	}

	#endregion
}

[System.Serializable]
public class data{
	public string uid;
	public string playername;
	public string username;
	public string dob;
	public string address;
	public string mobile;
	public string email;
	public string gsm_token;
	public string usertype;
	public string userstatus;
	public string cdate;
	public CurrencyDetail[] CurrencyDetail;
	public string message;
	public string status;
}

[System.Serializable]
public class CurrencyDetail{
	public string currencyName;
	public string currentAmount;
	public string currentStatus;
}