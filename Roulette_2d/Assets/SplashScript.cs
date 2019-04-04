using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScript : MonoBehaviour {

	// Use this for initialization

	public static SplashScript instance;

	[SerializeField] GameObject regForm;

	[SerializeField] Text messageText;

	[SerializeField] Image mainSplashImage;
	[SerializeField] Sprite nextSplashImage;


	UserRegistrationUI ureg;

	void Awake () {
		instance = this;
//		ureg.gameObject.SetActive (false);
	}

	void Start(){
//		StartCoroutine (showRegistrationForm ());
//		RegisteredSuccessfullyLoadNextScene ();
		StartCoroutine ("changeSplashImage");
	}

	public void showMessage(){
		messageText.text = ureg.message;
	}

	IEnumerator showRegistrationForm(){
		yield return new WaitForSeconds (1);
		regForm.SetActive (true);
		ureg = regForm.GetComponent<UserRegistrationUI>();
	}

	public void RegisteredSuccessfullyLoadNextScene(){
		regForm.SetActive(false);
		StartCoroutine (loadNextScene ());
	}

	IEnumerator loadNextScene(){
		yield return null;
		//put initialization hold here

		//SceneManager.LoadScene ("GameScene");
        SceneManager.LoadScene("MainMenu");
    }


	IEnumerator changeSplashImage(){
		yield return new WaitForSeconds (2f);
		mainSplashImage.sprite = nextSplashImage;

		yield return new WaitForSeconds (1.5f);
		RegisteredSuccessfullyLoadNextScene ();
	}
}
