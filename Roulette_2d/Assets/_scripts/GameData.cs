using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameData : MonoBehaviour {

	public static GameData instance;

	public data localData;

	public List<int> betNumbers;
	public int totalAmountOnBets;

//	[SerializeField] GameResult gameResult;

	void Awake(){
		instance = this;

		DontDestroyOnLoad (this);
	}

	public void postResult(int luckyNumber, bool iswinner){

		string numbers = string.Join(", ", betNumbers.Select(i => i.ToString()).ToArray());

		GameResult.instance.postGameResult (localData.uid, localData.uid, "1000", totalAmountOnBets.ToString (), numbers, luckyNumber.ToString() ,iswinner ? 1.ToString() : 2.ToString());
		Debug.LogError ("list numbers " +  betNumbers.ToString ());
	}

    public void PostFungameResult(string card, int totalAmountOnbet, string selectedCard, bool isUserWinner)
    {
        GameResult.instance.postGameResult("FUN GAME", localData.uid, "1000", totalAmountOnbet.ToString(), selectedCard, card, isUserWinner.ToString());
    }
    public void PostLuckyGameResult(string winningNumber, int totalAmountOnbet, string betNumbers, bool isUserWinner)
    {
        GameResult.instance.postGameResult("LUCKY GAME", localData.uid, "1000", totalAmountOnbet.ToString(), betNumbers, winningNumber, isUserWinner.ToString());
    }
}
