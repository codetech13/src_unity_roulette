using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine (loadNextScene ());
	}
	
	IEnumerator loadNextScene(){
		yield return new WaitForSeconds (0.5f);
		//put initialization hold here

		SceneManager.LoadScene ("GameScene");
	}

}
