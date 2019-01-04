using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderAnimation : MonoBehaviour {

	[SerializeField] List<GameObject> cards;

	[SerializeField] Transform  andar;
	[SerializeField] Transform bahar;

	[SerializeField] bool startAnimation = true;
	bool playNextCard = true;

	[SerializeField] float animationSpeed = 2f;

	GameObject currentCard;

	// Update is called once per frame
	void Update () {
		if (startAnimation) {
			if (playNextCard) {
				playNextCard = false;
				currentIndex++;
//				newRoation = Random.Range (rotaionZ [0], rotaionZ [rotaionZ.Length-1]);
			}
		}

		if (currentIndex % 2 == 0) {
			playAnimation (cards [currentIndex], bahar);
		} else {
			playAnimation (cards [currentIndex], andar);
		}
	}

	[SerializeField] int currentIndex = 0;
//	[SerializeField] int newRoation = 0;

//	int[] rotaionZ = new int[13] {10,15,20,25,30,35,40,47,50,55,67,75,85};

	void playAnimation(GameObject go, Transform target){
		playNextCard = false;
//		Quaternion rotaion = go.transform.rotation;

		go.transform.position = Vector3.Lerp (go.transform.position, target.position, Time.deltaTime * 0.9f * animationSpeed);
		Debug.Log ("playing");
		if(Vector3.Distance(go.transform.position, target.position) < 0.5f){
//			rotaion.z = newRoation;
//			go.transform.rotation = rotaion;
			playNextCard = true;
		}
	}
}
