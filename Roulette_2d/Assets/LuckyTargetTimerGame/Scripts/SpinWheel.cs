using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpinWheel : MonoBehaviour
{
    public static SpinWheel instance;
	public List<int> prize;
	public List<AnimationCurve> animationCurves;
    public CollideWithArrow arrowCollider;
	public bool spinning;	
	private float anglePerItem;	
	private int randomTime;
	private int itemNumber;

    private void Awake()
    {
        instance = this;
    }

    void Start(){
		spinning = false;
		anglePerItem = 360/prize.Count;		
	}
	

    public void SpinTheWheel()
    {
        if (spinning)
        {
            return;
        }
        arrowCollider.ResetRewardImages();

        //randomTime = Random.Range(1, 10);
        randomTime = 5;
        itemNumber = Random.Range(0, prize.Count);
        Debug.Log("item number is ::: " + itemNumber);
        float maxAngle = 360 * randomTime + (itemNumber * anglePerItem);

        StartCoroutine(SpinTheWheel(2 * randomTime, maxAngle));

    }
	
	IEnumerator SpinTheWheel (float time, float maxAngle)
	{
		spinning = true;
        arrowCollider.time = time;
		float timer = 0.0f;		
		float startAngle = transform.eulerAngles.z;		
		maxAngle = maxAngle - startAngle;
		
		int animationCurveNumber = Random.Range (0, animationCurves.Count);
		Debug.Log ("Animation Curve No. : " + animationCurveNumber + "time is "+ time);
		
		while (timer < time) {
		//to calculate rotation
			float angle = maxAngle * animationCurves [animationCurveNumber].Evaluate (timer / time) ;
			transform.eulerAngles = new Vector3 (0.0f, 0.0f, angle + startAngle);
			timer += Time.deltaTime;
			yield return 0;
		}
		
		transform.eulerAngles = new Vector3 (0.0f, 0.0f, maxAngle + startAngle);
		spinning = false;
        LuckyTargetTimerUI.instance.Result(prize[itemNumber], itemNumber);
		Debug.Log ("Prize: " + prize [itemNumber]);//use prize[itemNumnber] as per requirement
	}	
}
