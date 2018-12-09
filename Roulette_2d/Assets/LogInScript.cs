using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding.Serialization.JsonFx;

public class LogInScript : MonoBehaviour {

	[SerializeField] string email = "";
	[SerializeField] string password = "";
	[SerializeField] string mobile = "";
	string gsm_token = "sdfsdfsdfsdfs323242342342sdfsd";

	string url = "http://funasiagame.16mb.com/v1/getLogin";

	[SerializeField] InputField mail;
	[SerializeField] InputField pass;
	[SerializeField] InputField m_number;

	[SerializeField] GameObject registrationPanel;

	public static LogInScript instance;
    public Toggle rememberMe;

	void Awake(){
		instance = this;
	}

	void Start(){

        rememberMe.onValueChanged.AddListener(delegate {
            ToggleValueChanged(rememberMe);
        });

        if (PlayerPrefs.HasKey("Save_Email") && PlayerPrefs.HasKey("Save_Pass") && PlayerPrefs.HasKey ("Save_Mobile"))
        {
            mail.text = PlayerPrefs.GetString("Save_Email");
            pass.text = PlayerPrefs.GetString("Save_Pass");
            m_number.text = PlayerPrefs.GetString("Save_Mobile");
        }
    }

    private void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            if (!string.IsNullOrEmpty(mail.text) && !string.IsNullOrEmpty(pass.text) && !string.IsNullOrEmpty(m_number.text))
            {
                PlayerPrefs.SetString("Save_Email", mail.text);
                PlayerPrefs.SetString("Save_Pass", pass.text);
                PlayerPrefs.SetString("Save_Mobile", m_number.text);
            }
        }
        Debug.Log("Change " + change.isOn);

    }


    LogInNow logInDetails;

	public void LogInRequest(){
		
		logInDetails = new LogInNow ();
		logInDetails.email = mail.text;
		logInDetails.gsm_token = this.gsm_token;
		logInDetails.mobile = m_number.text;
		logInDetails.password = pass.text;

		string dataString = JsonUtility.ToJson (logInDetails);

		Debug.Log (dataString);
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Authorization", "sadas21321");

		// convert json string to byte
		var formData = System.Text.Encoding.UTF8.GetBytes(dataString);

		www = new WWW(this.url, formData, postHeader);
		StartCoroutine(WaitForRequest(www));
	}

	public UserRegistrationUI.response responseData;
	string message;
	IEnumerator WaitForRequest(WWW www){
		yield return www;
		Debug.Log (www.text);
		if (www.error != null) {
			Debug.Log (www.error.ToString ());
		} else {
			Debug.Log (www.text);
		}

		responseData = new UserRegistrationUI.response ();
		responseData = JsonReader.Deserialize<UserRegistrationUI.response> (www.text);
		Debug.Log ("data " + responseData.data.address);

		//load next scene if successful
		message = responseData.message;
//		SplashScript.instance.showMessage ();
		if (message.Contains ("Data Found")) {
//			LogInScript.instance.RegisteredSuccessfully ();
			GameHud.instance.logInSuccessfully();
		} else if (message.Contains ("already registered")) {
			Debug.LogError ("ALREADY REGISTERED");
		} else {
			Debug.LogError ("USER NOT REGISTERED");
		}


		//set local data in gamedata
		GameData.instance.localData = responseData.data;
	}

	public void showRegistrationPanel(){
		registrationPanel.SetActive (true);
	}

	public void RegisteredSuccessfully(){
		registrationPanel.SetActive (false);
	}

	[System.Serializable]
	public class LogInNow {
		public string email;
		public string password;
		public string mobile;
		public string gsm_token;
	}
}
