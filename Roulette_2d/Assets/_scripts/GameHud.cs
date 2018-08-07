using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHud : MonoBehaviour {
	public static GameHud instance;
    public GameState gameState;
    public GameObject spinWheelBtn;
    public Text number;
    public Text betNumber;
    public int betNo;
    public GameObject mainSPinButton;
    public GameObject mainImage;

	public InputField userName;
	public InputField passsword;
	public GameObject logInSCreen;

    private void Awake()
    {
		instance = this;
        gameState = GameState.DEFAULT;
        mainSPinButton.SetActive(false);
        if (spinWheelBtn.activeSelf)
        {
            spinWheelBtn.SetActive(false);
        }
    }

    private void Start()
    {
        this.betNumber.text = "PLEASE SELECT BET NUMBER";
    }

    public void SetText(int luckyNumber, int betNO) {
        if(luckyNumber == betNo)
        {
            number.text = " <color=green>YOU WIN</color>   Number is :: " + luckyNumber.ToString();

        }
        else
        {
            number.text = " <color=red>YOU LOST</color>   Number is :: " + luckyNumber.ToString();

        }
    }

    public void spinWheel()
    {
        SceneManager.LoadScene(0);
    }

    public void ActivateSpinBtn(GameObject go)
    {
        if (!go.activeSelf)
        {
            go.SetActive(true);
        }

    }

    public void DeactivateSpinBtn(GameObject go)
    {
        if (!go.activeSelf)
        {
            go.SetActive(true);
        }

    }

    public void SetBetNumber(int betNumber)
    {
        this.betNo = betNumber;
        this.betNumber.text = "BET NUMBER : " + this.betNo.ToString();
        mainSPinButton.SetActive(true);

    }

    public void HideImage()
    {
        mainImage.SetActive(false);
        gameState = GameState.RUNNING;
        mainSPinButton.SetActive(false);
    }

	public void LogInButton(){
		if (userName.text == "021001574" && passsword.text == "111111") {
			logInSCreen.SetActive (false);
		}
	}

}
[System.Serializable]
public enum GameState
{
    RUNNING,
    NOT_RUNNNG,
    DEFAULT
}
