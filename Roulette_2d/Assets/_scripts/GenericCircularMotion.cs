using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericCircularMotion : MonoBehaviour {

	public float RotateSpeed = 5f;
	public float Radius = 0.1f;

	private Vector2 _centre;
	public float _angle;
	public GameHud hud;

	[SerializeField]private bool runOnlyOnce;

	float timer;

	private void Start()
	{
		_centre = transform.position;
		Debug.Log ("a");
		RotateSpeed = RotateSpeed + Random.Range (0.5f, 1.3f);
	}

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
			RotateSpeed = 0;
		}

		//		Debug.Log ("distance " + Vector3.Distance(transform.position, pointsList[0].transform.position));
	}
}
