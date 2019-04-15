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
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
	}

	public void postResult(int luckyNumber, bool iswinner){
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            string numbers = string.Join(", ", betNumbers.Select(i => i.ToString()).ToArray());

            GameResult.instance.postGameResult(localData.uid, localData.uid, "1000", totalAmountOnBets.ToString(), numbers, luckyNumber.ToString(), iswinner ? 1.ToString() : 2.ToString());
            // Debug.LogError("list numbers " + betNumbers.ToString());
        }
        else
        {
            Debug.LogError("INTERNET NOT AVAILABLE");
        }
    }

    public void PostFungameResult(string card, int totalAmountOnbet, string selectedCard, bool isUserWinner)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            GameResult.instance.postGameResult("FUN GAME", localData.uid, "1000", totalAmountOnbet.ToString(), selectedCard, card, isUserWinner.ToString());
        }
        else
        {
            Debug.LogError("INTERNET NOT AVAILABLE");
        }
    }
    public void PostLuckyGameResult(string winningNumber, int totalAmountOnbet, string betNumbers, bool isUserWinner)
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            GameResult.instance.postGameResult("LUCKY GAME", localData.uid, "1000", totalAmountOnbet.ToString(), betNumbers, winningNumber, isUserWinner.ToString());
        }
        else
        {
            Debug.LogError("INTERNET NOT AVAILABLE");
        }
    }
}
