using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
using System;

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
//		userTotalAmount = GameData.instance.localData.CurrencyDetail [0].currentAmount;

		Int32.TryParse (GameData.instance.localData.CurrencyDetail [0].currentAmount, out userTotalAmount);
		balanceText.text = userTotalAmount.ToString();
	}


	public int totalAmtOnBets = 0;

	#region UI
	void RefreshUI(int reward){
		
		balanceText.text = userTotalAmount.ToString();
		userWiningText.text = reward.ToString ();
	}
	#endregion

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

	[SerializeField] List<int> CurrentBetsList = new List<int> ();

	void selectedBetNumber(int value){
		int tempChipAmount = chipAmount;
		if(GameHud.instance.gameState == GameState.RUNNING){
			Debug.LogError ("TABLE IS RUNNING CAN'T BET NOW");
			return;
		}

		if (chipAmount <= 0 || chipAmount > userTotalAmount) {
			return;
		}

		if (isBetDouble && chipAmount * 2 > userTotalAmount) {
			return;
		}

		if (!betNumberData.ContainsKey (value)) {
			if (!isBetDouble) {
				betNumberData.Add (value, chipAmount);
			} else {
				betNumberData.Add (value, chipAmount * 2);
			}
		} else {
			if (!isBetDouble) {
				betNumberData [value] = betNumberData [value] + chipAmount;
			} else {
				betNumberData [value] = (betNumberData [value] + chipAmount)*2;
			}
		}

		if (!CurrentBetsList.Contains (value)) {
			CurrentBetsList.Add (value);
		}

		if (isBetDouble && userTotalAmount > 2 * chipAmount) {
			chipAmount = 2 * chipAmount;
			DeductUserAmount (chipAmount);
		} else {
			DeductUserAmount (chipAmount);
		}
		chipAmount = tempChipAmount;
		Debug.LogError ("betN " + value + " amt " + betNumberData [value]);

		userBets.text = "";
		for (int i = 0; i < CurrentBetsList.Count; i++) {
			userBets.text += CurrentBetsList [i];
		}
			

		balanceText.text = " " + userTotalAmount.ToString();

		betUI ();
	}

	void betUI(){
		int temp = 0;
		totaleBet.text = " ";
		for (int i = 0; i < CurrentBetsList.Count; i++) {
			temp += betNumberData [CurrentBetsList[i]];
		}

		totaleBet.text = temp.ToString();
	}

	void deleteSelectedBetNumber(int value){
		if(betNumberData.ContainsKey(value)){
			if (betNumberData [value] > 0) {
				betNumberData [value] = betNumberData [value] - chipAmount;
				AddUserAmount (chipAmount);
			}

			if (betNumberData [value] < 0) {
				betNumberData [value] = 0;
			}
		}

		userBets.text = "";
		for (int i = 0; i < CurrentBetsList.Count; i++) {
			userBets.text += " " + CurrentBetsList [i];
		}

		int temp = 0;
		totaleBet.text = "";
		for (int i = 0; i < CurrentBetsList.Count; i++) {
			temp += betNumberData [CurrentBetsList[i]];
		}
		totaleBet.text = temp.ToString();

		balanceText.text = userTotalAmount.ToString();
	}
	#endregion

	[SerializeField] int[] rList_0_36;
	[SerializeField] int[] rList_112;
	[SerializeField] int[] rList_212;
	[SerializeField] int[] rList_312;
	[SerializeField] int[] rList_118;
	[SerializeField] int[] rList_100;
	[SerializeField] int[] rList_200;
	[SerializeField] int[] rList_300;
	[SerializeField] int[] rList_400;
	[SerializeField] int[] rList_1936;
	[SerializeField] int[] rList_2111;
	[SerializeField] int[] rList_2211;
	[SerializeField] int[] rList_2311;

	#region FINALIZE BET RESULT AND REWARD

	public void finalizeReward(int luckyNumber){
		int netAMT = 0;
		int finalReward = 0;

		finalBets.text = "Lucky Number : " + luckyNumber;

		if(betNumberData.ContainsKey(luckyNumber)){
			betNumberData.TryGetValue (luckyNumber, out netAMT);
			finalReward = finalReward + netAMT * 36;
			Debug.LogError ("default netAMT " + netAMT + " finalReward " + finalReward);
			netAMT = 0;
		}

		if (CurrentBetsList.Contains (112)) {

			for (int i = 0; i < rList_112.Length; i++) {
				if (luckyNumber == rList_112 [i]) {
					betNumberData.TryGetValue (112, out netAMT);
					finalReward = finalReward +netAMT * 3;
					Debug.LogError ("112 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		}  
		if (CurrentBetsList.Contains (212)) {

			for (int i = 0; i < rList_212.Length; i++) {
				if (luckyNumber == rList_212 [i]) {
					betNumberData.TryGetValue (212, out netAMT);
					finalReward = finalReward + netAMT * 3;
					Debug.LogError ("212 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		}
		if (CurrentBetsList.Contains (312)) {
			for (int i = 0; i < rList_312.Length; i++) {
				if (luckyNumber == rList_312 [i]) {
					betNumberData.TryGetValue (312, out netAMT);
					finalReward = finalReward + netAMT * 3;
					Debug.LogError ("312 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		} 
		if (CurrentBetsList.Contains (118)) {
			for (int i = 0; i < rList_118.Length; i++) {
				if (luckyNumber == rList_118 [i]) {
					betNumberData.TryGetValue (118, out netAMT);
					finalReward =  finalReward + netAMT * 2;
					Debug.LogError ("118 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		}
		if (CurrentBetsList.Contains (100)) {
			for (int i = 0; i < rList_100.Length; i++) {
				if (luckyNumber == rList_100 [i]) {
					betNumberData.TryGetValue (100, out netAMT);
					finalReward = finalReward + netAMT * 2;
					Debug.LogError ("100 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		}  
		if (CurrentBetsList.Contains (200)) {

			for (int i = 0; i < rList_200.Length; i++) {
				if (luckyNumber == rList_200 [i]) {
					betNumberData.TryGetValue (200, out netAMT);
					finalReward = finalReward + netAMT * 2;
					Debug.LogError ("200 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		}  
		if (CurrentBetsList.Contains (300)) {
			for (int i = 0; i < rList_300.Length; i++) {
				if (luckyNumber == rList_300 [i]) {
					betNumberData.TryGetValue (300, out netAMT);
					finalReward = finalReward + netAMT * 2;
					Debug.LogError ("300 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		} 
		if (CurrentBetsList.Contains (400)) {
			for (int i = 0; i < rList_400.Length; i++) {
				if (luckyNumber == rList_400 [i]) {
					betNumberData.TryGetValue (400, out netAMT);
					finalReward = finalReward + netAMT * 2;
					Debug.LogError ("400 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		} 
		if (CurrentBetsList.Contains (1936)) {
			for (int i = 0; i < rList_1936.Length; i++) {
				if (luckyNumber == rList_1936 [i]) {
					betNumberData.TryGetValue (1936, out netAMT);
					finalReward = finalReward + netAMT * 2;
					Debug.LogError ("1936 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		} 
		if (CurrentBetsList.Contains (2111)) {
			for (int i = 0; i < rList_2111.Length; i++) {
				betNumberData.TryGetValue (2111, out netAMT);
				if (luckyNumber == rList_2111 [i]) {
					finalReward = finalReward + netAMT * 3;
					Debug.LogError ("2111 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		} 
		if (CurrentBetsList.Contains (2211)) {

			for (int i = 0; i < rList_2211.Length; i++) {
				if (luckyNumber == rList_2211 [i]) {
					betNumberData.TryGetValue (2211, out netAMT);
					finalReward = finalReward + netAMT * 3;
					Debug.LogError ("2211 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		} 
		if (CurrentBetsList.Contains (2311)) {
			for (int i = 0; i < rList_2311.Length; i++) {
				if (luckyNumber == rList_2311 [i]) {
					betNumberData.TryGetValue (2311, out netAMT);
					finalReward = finalReward + netAMT * 3;
					Debug.LogError ("2311 netAMT " + netAMT + " finalReward " + finalReward);
				}
			}
			netAMT = 0;
		}

		Debug.LogError (" finalReward " + finalReward);
		RefreshUI (finalReward);
		CurrentBetsList.Clear ();
		betNumberData.Clear ();
		ClearBet ();
	}

	#endregion

	bool isDeletable;

	public void deleteToggle(GameObject btn){
		if (!isDeletable) {
			isDeletable = true;
			toggleUI (btn, true);
		} else {
			isDeletable = false;
			toggleUI (btn, false);
		}
	}


	public List<int> local;
	bool hasSelected;

	System.Action action;

	public void buttonClick (GameObject go){
		go.transform.GetChild (0).gameObject.SetActive (true);
		Debug.Log ("clicked");
	}


	void toggleUI(GameObject btn, bool toggleState){
		if (toggleState) {
			btn.GetComponent<Image> ().color = Color.black;
		} else {
			btn.GetComponent<Image> ().color = Color.white;
		}
	}

	#region BET NUMBER BUTTONS
	public void betNumberButton(int value){
		if (!isDeletable) {
			selectedBetNumber (value);
		} else {
			deleteSelectedBetNumber (value);
		}
		return;







		//WASTE CODE HERE
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

	public void MakeDoubleAmount(GameObject btn)
	{

		if (!isBetDouble) {
			isBetDouble = true;	
			toggleUI (btn, true);
		} else {
			isBetDouble = false;
			toggleUI (btn, false);
		}
	}


	public void ClearBet()
	{
		CurrentBetsList.Clear ();
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
		userWiningText.text = "";
		balanceText.text = GameData.instance.localData.CurrencyDetail [0].currentAmount;
		finalBets.text = "";
		Debug.LogError ("clear bet");

		ClearLocalList ();

		local.Clear ();
		temp.Clear ();
	}










    private List<int> localList = new List<int>();
    private int tempStoreBetnumberList = 0;



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
