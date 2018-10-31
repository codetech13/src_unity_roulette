using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour {

	public float RotateSpeed = 5f;
	public float Radius = 0.1f;

	private Vector2 _centre;
	public float _angle;
    public GameHud hud;

	[SerializeField]private bool runOnlyOnce;
    private int luckyNumber;

	private void Start()
	{
		_centre = transform.position;
		Debug.Log ("a");
		RotateSpeed = RotateSpeed + Random.Range (0.5f, 1.3f);
	}

	float timer;

	void Update()
	{
        if (hud.gameState == GameState.DEFAULT) {
            return;
        }
        Debug.Log("YYYYYY");
		timer += Time.deltaTime;
		if (RotateSpeed > 0 && timer > 2f) {
			RotateSpeed -= Time.deltaTime;
			if (Radius > 2.05f) {
				Radius -= Time.deltaTime;
			}
			_angle += RotateSpeed * Time.deltaTime;

			var offset = new Vector2 (Mathf.Sin (_angle), Mathf.Cos (_angle)) * Radius;
			transform.position = _centre + offset;
		} else {
			_angle += RotateSpeed * Time.deltaTime;

			var offset = new Vector2 (Mathf.Sin (_angle), Mathf.Cos (_angle)) * Radius;
			transform.position = _centre + offset;
		}
		if (RotateSpeed <= 0 && !runOnlyOnce) {
			runOnlyOnce = true;
			StartCoroutine(checkClosestPoint ());
			RotateSpeed = 0;
		}

//		Debug.Log ("distance " + Vector3.Distance(transform.position, pointsList[0].transform.position));
	}

	//MAJ ASSEMBLY ERROR FOR COLLISION2D
	void OnCollisionEnter2D(Collision2D col){
		RotateSpeed = 0;
	}

	public List<ValuePoint> pointsList;

	IEnumerator checkClosestPoint(){
		yield return new WaitForSeconds (1);
		int temp = 0;
		for (int i = 0; i < pointsList.Count; i++) {
			if (i == 0)
				continue;
			
			if(Vector3.Distance(pointsList[i].transform.position, transform.position) < Vector3.Distance( pointsList[temp].transform.position, transform.position)){
				temp = i;
                luckyNumber = pointsList[i].serialNumber;
			}

			Debug.LogError ("point " + i + " temp is the closest point" + " distance " + 
				Vector3.Distance(pointsList[i].transform.position, transform.position) + "   cOunt ::: " +
                pointsList[i].serialNumber);
		}
        FinalizeResult(luckyNumber);

        GameButtonsHandler.instance.ClearLocalList();
        hud.SetText(luckyNumber, hud.betNo);
        hud.ActivateSpinBtn(hud.spinWheelBtn);
		DemoTimer.instance.resetTimer ();
		runOnlyOnce = false;
		hud.gameState = GameState.DEFAULT;
		Debug.Log ("point " + temp + " temp is the closest point" + "  Lucky Number  " + luckyNumber);
	 }
		
    private void FinalizeResult(int betNumber) {
		bool isWinner = false;
        GameButtonsHandler.instance.setFinalBetNumber(luckyNumber);
        List<int> localKey = new List<int>(GameButtonsHandler.instance.betDictionary.Keys);

		for (int i = 0; i < GameButtonsHandler.instance.betDictionary.Count; i++) {
			for (int j = 0; j < GameButtonsHandler.instance.betDictionary [localKey [i]].Count; j++) {
				if (betNumber == GameButtonsHandler.instance.betDictionary [localKey [i]] [j]) {
					GameButtonsHandler.instance.AddUserAmount (localKey [i]);
					Debug.Log ("add amount --> " + localKey[i]);
					isWinner = true;
				}
			} 
		}

		GameData.instance.postResult (luckyNumber, isWinner);

//        for (int i=0; i< localKey.Count; i++)
//        {
//            if (localKey[i] == betNumber)
//            {
//                for (int k = 0; k < GameButtonsHandler.instance.betDictionary[i].Count; k++)
//                {
//                    GameButtonsHandler.instance.AddUserAmount(GameButtonsHandler.instance.betDictionary[i][k]);
//                }
//            }
//            /*else
//            {
//
//                for (int k = 0; k < GameButtonsHandler.instance.betDictionary[i].Count; k++)
//                {
//                    GameButtonsHandler.instance.DeductUserAmount(GameButtonsHandler.instance.betDictionary[i][k]);
//                }
//
//            }*/
//
//        }
    }
}
