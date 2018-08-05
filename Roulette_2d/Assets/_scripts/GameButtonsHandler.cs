using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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

    public Text userBets;
    public Text finalBets;

    private void Awake()
    {
        instance = this;
    }

    public void ChipsAmountButton(int chipAmount)
    {
        if (chipAmount > userTotalAmount) {
            return;
        }
        this.chipAmount = chipAmount;
        SetUserTotalAmountonBet(this.chipAmount);
       // DeductUserAmount(this.chipAmount);
    }


    private List<int> localList = new List<int>();
    private int tempStoreBetnumberList = 0;
    public void SetBetNumberButton(int betNumber)
    {
        if (chipAmount <= 0) {
            return;
        }


    
        this.betNumber = betNumber;
        betNumbersList.Add(this.betNumber);
        localList.Add(this.chipAmount);
        betDictionary.Add(this.betNumber, localList);
        DeductUserAmount(this.chipAmount);

        if (betDictionary.Count > 0)
        {
            List<int> localKey = new List<int>(betDictionary.Keys); // if double click on same bet number then remove the bet
            for (int i = 0; i < localKey.Count; i++)
            {
                if (localKey[i] == betNumber)
                {
                    for (int k = 0; k < betDictionary[i].Count; k++)
                    {
                        AddUserAmount(2* betDictionary[i][k]);
                        localList = localList.Distinct().ToList();
                        localList.Remove(betDictionary[i][k]);
                        betNumbersList.Remove(localKey[i]);
                        betDictionary.Remove(betNumber);
                        SetUserBetNumbers();
                        break;
                    }
                }
            }
        }


    }

    public void MakeDoubleAmount()
    {
        if (!isBetDouble && userTotalAmount > 2 * chipAmount)
        {
            chipAmount = 2 * chipAmount;
            DeductUserAmount(chipAmount);
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

}

    public void ClearLocalList()
    {

        localList.Clear();
    }

    public void SetUserBetNumbers()
    {
        for (int i=0; i<betNumbersList.Count; i++)
        {
            userBets.text = userBets.text + " " + betNumbersList[i].ToString();
        }
    }

    public void setFinalBetNumber(int number)
    {
        betResultNumberList.Add(number);
        for (int i = 0; i < betResultNumberList.Count; i++)
        {
            finalBets.text = finalBets.text + " " + betResultNumberList[i].ToString();
        }

    }

    public void DeductUserAmount(int amount) {
        userTotalAmount = userTotalAmount - amount;
    }
    public void AddUserAmount(int amount)
    {
        userTotalAmount = userTotalAmount + amount;
    }

    public void SetUserTotalAmountonBet(int amount)
    {

        userBetAmountList.Add(amount);
        for (int i = 0; i < userBetAmountList.Count; i++) {
            userTotalAmountOnBet = userTotalAmountOnBet + userBetAmountList[i];
        }
    }
}
