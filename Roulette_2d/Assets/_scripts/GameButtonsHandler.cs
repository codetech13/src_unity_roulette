using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class GameButtonsHandler : MonoBehaviour
{
    public static GameButtonsHandler instance;
    public int chipAmount;
    public int betNumber;
    public bool isBetDouble;
    public bool setPreviousAmount;
    public List<int> betNumbersList;
    public List<int> betResultNumberList;
    public int userTotalAmount = 10000;
    public int userTotalAmountOnBet;
    public List<int> userBetAmountList;
    public Dictionary<int , List<int>> betDictionary;
    public bool isBetSelected = false;

	public List<GameObject> allButtons = new List<GameObject>();

    public Text userBets;
    public Text finalBets;
	public Text totaleBet;
	public Text balanceText;
	public Text userWiningText;

	public GameObject parentGO;
	public GameObject mainMenuGO;

    private void Awake()
    {
        instance = this;
    }

	private void Start(){
	    local = new List<int> ();
		betDictionary = new Dictionary<int, List<int>> ();
	}


	public int totalAmtOnBets = 0;

	#region BET AMMOUNT BUTTONS
	int currentBetAmt;
	public void betAMTButton(int value){

		if (chipAmount > userTotalAmount) {
			return;
		}
		currentBetAmt = value;
		chipAmount = currentBetAmt;
		local.Clear ();
	}
	#endregion

	#region New Bet Number logic
	public Dictionary<int , int> betNumberData = new Dictionary<int, int>();
	void selectedBetNumber(int value){
		if(GameHud.instance.gameState == GameState.RUNNING){
			Debug.LogError ("TABLE IS RUNNING CAN'T BET NOW");
			return;
		}

		if (chipAmount <= 0) {
			return;
		}

		if (!betNumberData.ContainsKey (value)) {
			betNumberData.Add (value, chipAmount);
		} else {
			betNumberData [value] = betNumberData [value] + chipAmount;
		}

		Debug.LogError ("betN " + value + " amt " + betNumberData [value]);
	}
	#endregion

	#region FINALIZE BET RESULT AND REWARD

	public void finalizeReward(int luckyNumber){
		int netAMT = 0;
		int finalReward = 0;
		if(betNumberData.ContainsKey(luckyNumber)){
			betNumberData.TryGetValue (luckyNumber, out netAMT);
			finalReward = netAMT * 36;
			Debug.LogError ("netAMT " + netAMT + " finalReward " + finalReward);
		}
	}

	#endregion

	public List<int> local;
	bool hasSelected;

	System.Action action;

	public void buttonClick (int value, Object go){
	}

	public void buttonClick (GameObject go){
		go.transform.GetChild (0).gameObject.SetActive (true);
		Debug.Log ("clicked");
	}

	#region BET NUMBER BUTTONS
	public void betNumberButton(int value){
		selectedBetNumber (value);
		return;

		if(GameHud.instance.gameState == GameState.RUNNING){
			Debug.LogError ("TABLE IS RUNNING CAN'T BET NOW");
			return;
		}

		if (chipAmount <= 0) {
			return;
		}

		hasSelected = false;

		if (betDictionary.ContainsKey (currentBetAmt)) {
			Debug.Log ("contains key");
			local = betDictionary [currentBetAmt];
		} else {
			Debug.Log ("not\t contains key");
			local = null;
		}

		if (local != null) {
			for (int i = 0; i < local.Count; i++) {
				if (local [i] == value) {
					hasSelected = true;
					local.Remove (value);
					//					clickedButton.GetComponentInChildren<Image> ().enabled = true;
					allButtons.Remove(EventSystem.current.currentSelectedGameObject.transform.GetChild (0).gameObject);
					EventSystem.current.currentSelectedGameObject.transform.GetChild (0).gameObject.SetActive (false);
					userTotalAmount = userTotalAmount + currentBetAmt;
					totalAmtOnBets = totalAmtOnBets - currentBetAmt;
				}
			}
		}



		if (!hasSelected) {
			Debug.Log ("value " + value);
			if (local == null) {
				local = new List<int> ();
			}

			local.Add (value);
			//			clickedButton.GetComponentInChildren<Image> ().enabled = false;
			EventSystem.current.currentSelectedGameObject.transform.GetChild (0).gameObject.SetActive (true);
			allButtons.Add (EventSystem.current.currentSelectedGameObject.transform.GetChild (0).gameObject);
			userTotalAmount = userTotalAmount - currentBetAmt;
			totalAmtOnBets = totalAmtOnBets + currentBetAmt;
		}

		if (betDictionary.ContainsKey (currentBetAmt)) {
			betDictionary [currentBetAmt] = local;
		} else {
			betDictionary.Add (currentBetAmt, local);
		}

		balanceText.text = userTotalAmount.ToString ();
		totaleBet.text = totalAmtOnBets.ToString ();

		SetUserBetNumbers ();
	}
	#endregion

    public void ChipsAmountButton(int chipAmount)
    {
        if (chipAmount > userTotalAmount) {
            return;
        }
        this.chipAmount = chipAmount;
        SetUserTotalAmountonBet(this.chipAmount);
		List<int> amountKeys = new List<int>(betDictionary.Keys);

		foreach(int key in amountKeys) {
			if (!betDictionary.ContainsKey (key)) {
				//add
				betDictionary.Add (key, new List<int> ());
			} else {
				betDictionary [key].Add (this.betNumber);
			}
		}
		betDictionary.Add (this.chipAmount, null);
       // DeductUserAmount(this.chipAmount);
    }


    private List<int> localList = new List<int>();
    private int tempStoreBetnumberList = 0;
    public void SetBetNumberButton(int betNumb)
    {
//        if (chipAmount <= 0) {
//            return;
//        }
//
//		if (betDictionary.Count > 0)
//		{
//			List<int> localKey = new List<int>(betDictionary.Keys); // if double click on same bet number then remove the bet
//			for (int i = 0; i < localKey.Count; i++)
//			{
//				Debug.Log ("Local String " + localKey[i].ToString());
//				if (localKey[i] == betNumb)
//				{
//					Debug.Log ("   :::: Local String " + localKey[i].ToString() + "   " + betNumb);
//					for (int k = 0; k < betDictionary[betNumb].Count; k++)
//					{
//						AddUserAmount( betDictionary[i][k]);
//						localList = localList.Distinct().ToList();
//						localList.Remove(betDictionary[i][k]);
//						betNumbersList.Remove(localKey[i]);
//						betDictionary.Remove(betNumb);
//						SetUserBetNumbers();
//						break;
//					}
//				}
//			}
//		}
//        this.betNumber = betNumb;
//		Debug.LogError (this.betNumber);
//		betNumbersList.Add(this.betNumber);
//		Debug.LogError ("COUNT -- " + betNumbersList.Count);
//		betNumbersList = betNumbersList.Distinct().ToList();
//		Debug.LogError ("COUNTTTT -- " + betNumbersList.Count);
//		localList.Add(this.betNumber);
//		localList = localList.Distinct().ToList();
//
//		List<int> keys = new List<int>(betDictionary.Keys);
//		foreach(int key in keys) {
//			if (!betDictionary.ContainsKey (key)) {
//				//add
//				betDictionary.Add (key, new List<int> ());
//			} else {
//				betDictionary [key].Add (this.betNumber);
//			}
//		}
//        DeductUserAmount(this.chipAmount);

    }

    public void MakeDoubleAmount()
    {
//		if (!isBetDouble && userTotalAmount > 2 * chipAmount && userBetAmountList.Count > 0)
//        {
//            chipAmount = 2 * chipAmount;
//            DeductUserAmount(chipAmount);
//            isBetDouble = true;
//        }

		if(!isBetDouble){
			isBetDouble = true;	
		}
    }

    public void MakePreviousAmount()
    {
        if (chipAmount == 0) {
            return;
        }
        if (!setPreviousAmount && isBetDouble)
        {
            chipAmount = chipAmount/2;
            AddUserAmount(chipAmount);
            setPreviousAmount = true;
        }

    }

    public void ClearBet()
    {
	    chipAmount = 0;
	    betNumber = 0;
	    isBetDouble = false;
	    setPreviousAmount = false;
	    betNumbersList.Clear();
	    userBetAmountList.Clear();
	    betResultNumberList.Clear();
		userBets.text = "";
		totaleBet.text = "";
		betDictionary.Clear ();
		clearSelectedButton ();

		Debug.LogError ("clear bet");

		ClearLocalList ();

		local.Clear ();
		temp.Clear ();
	}

	void clearSelectedButton(){
		for (int i = 0; i < allButtons.Count; i++) {
			allButtons [i].SetActive (false);
		}
	}

    public void ClearLocalList()
    {
		betDictionary.Clear ();
        localList.Clear();
		clearSelectedButton ();
    }

	List<int> dictKeys =  new List<int>();
	public List<int> temp = new List<int>();
    public void SetUserBetNumbers()
    {
		userBets.text = "";
//		temp.Clear();
		if (betDictionary == null) {
			return;
		}

		dictKeys = new List<int> (betDictionary.Keys);

		for (int i=0; i<betDictionary.Count; i++)
        {
			for (int j = 0; j < betDictionary[dictKeys[i]].Count; j++) {
//				betDictionary[dictKeys[i]] = betDictionary [dictKeys [i]].Distinct ().ToList ();
				 temp.Add(betDictionary[dictKeys[i]][j]);
//				userBets.text = userBets.text + " " +  betDictionary[dictKeys[i]][j].ToString();
			}

        }

		temp = temp.Distinct ().ToList ();

		for (int i = 0; i < temp.Count; i++) {
			
			userBets.text =  userBets.text + " " + temp [i].ToString ();
		}
//		userBets.text
    }

    public void setFinalBetNumber(int number)
    {
		betResultNumberList.Add(number);
		betResultNumberList = betResultNumberList.Distinct().ToList();
        for (int i = 0; i < betResultNumberList.Count; i++)
        {
            finalBets.text = finalBets.text + " " + betResultNumberList[i].ToString();
        }

    }

    public void DeductUserAmount(int amount) {
        userTotalAmount = userTotalAmount - amount;
    }
	int loclAmount =0;

    public void AddUserAmount(int amount)
    {
		loclAmount = loclAmount + amount;
		userWiningText.text = loclAmount.ToString ();
        userTotalAmount = userTotalAmount + amount;
    }

    public void SetUserTotalAmountonBet(int amount)
    {

        userBetAmountList.Add(amount);
        for (int i = 0; i < userBetAmountList.Count; i++) {
            userTotalAmountOnBet = userTotalAmountOnBet + userBetAmountList[i];
        }
    }

	public void showGameTable(){
		mainMenuGO.SetActive (false);
		parentGO.SetActive (true);
		DemoTimer.instance.resetTimer ();

	}

	public void hideGameTable(){
		mainMenuGO.SetActive (true);
		parentGO.SetActive (false);
		GameHud.instance.gameState = GameState.DEFAULT;
		DemoTimer.instance.stopTimer = true;
	}
}
