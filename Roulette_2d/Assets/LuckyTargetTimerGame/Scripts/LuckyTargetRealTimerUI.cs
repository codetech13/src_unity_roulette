using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LuckyTargetRealTimerUI : MonoBehaviour {
    public static LuckyTargetRealTimerUI instance;
    private bool isChipSelected = false;
    private bool isBetSlected = false;
    private bool isBetDouble = false;
    private List<int> history = new List<int>();
    public int totalAmount = 20000;
    public int totalAmountOnBet = 0;
    public int selectedBetNumber;
    public int winningAmount = 0;
    public int winNumber;

    public bool isBetWon = false;


    [Header("Texts")]
    public Text balanceText;
    public Text totalBetTexts;
    public Text winningText;
    public Text[] historyText;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        history.Clear();
        ResetValues();
        totalAmount = int.Parse(GameData.instance.localData.CurrencyDetail[1].currentAmount);
        SetTexts();
    }

    public void OnSpinButtonClick()
    {
        if (isBetSlected && isChipSelected)
        {
            SpinWheel.instance.SpinTheWheel();
        }
    }

    public void OnChipButtonCick(int chipAmount)
    {
        /*
        if (totalAmount > chipAmount && !isChipSelected)
        {
            isChipSelected = true;
            totalAmount = totalAmount - chipAmount;
            totalAmountOnBet = totalAmountOnBet + chipAmount;
        }

        if (isChipSelected)
        {
            if (totalAmount > chipAmount)
            {
                totalAmount = totalAmount + totalAmountOnBet;
                totalAmountOnBet = chipAmount;
                totalAmount = totalAmount - chipAmount;
            }

        }

        SetTexts();
        */

        if (!RealTimer.instance.buttonsInterctable || isdeal)
        {
            Debug.Log("TIMER IS LESS THAN 10 SECS");
            return;
        }
        currentBetAmount = chipAmount;
        isChipSelected = true;
    }


    int currentBetAmount = 0;
    private GameObject go = null;
    Text tempText;
    public void SelectBetNumber(int betNumber)
    {
        if (!isChipSelected || isdeal)
        {
            return;
        }

        if (!RealTimer.instance.buttonsInterctable)
        {
            return;
        }

        if (totalAmount < currentBetAmount)
        {
            Debug.LogError("BET AMOUNT  > CURRENT AMOUNT");
            return;
        }

        if (go == null)
        {
            go = EventSystem.current.currentSelectedGameObject;


            totalAmountOnBet = totalAmountOnBet + currentBetAmount;
            totalAmount = totalAmount - currentBetAmount;

            // tempText.gameObject.SetActive(false);
            tempText = go.transform.GetChild(2).GetComponent<Text>();

            tempText.gameObject.SetActive(true);
            tempText.text = "";
            tempText.text = totalAmountOnBet.ToString();


            Debug.Log("XOXOXOXO");
        }
        else
        {
            Debug.Log("HJHJHJHJ");

            go = EventSystem.current.currentSelectedGameObject;



            totalAmountOnBet = totalAmountOnBet + currentBetAmount;
            totalAmount = totalAmount - currentBetAmount;

            //   if (isBetSlected)
            // {
            tempText.gameObject.SetActive(false);
            tempText = go.transform.GetChild(2).GetComponent<Text>();
            tempText.gameObject.SetActive(true);
            tempText.text = "";
            tempText.text = totalAmountOnBet.ToString();
            //  }

        }

        SetTexts();

        isBetSlected = true;
        selectedBetNumber = betNumber;

    }

    public void DoubleBetAmounBtn()
    {
        if (!isBetSlected || !isChipSelected || isdeal)
        {
            Debug.Log("Please select bet number or set amount");
            return;

        }
        if (!RealTimer.instance.buttonsInterctable)
        {
            Debug.Log("TIMER IS LESS THAN 10 SECS");
            return;
        }
        if (totalAmount > 2 * totalAmountOnBet)
        {
            Debug.Log("Bets are double Now");
            totalAmountOnBet = 2 * totalAmountOnBet;
            totalAmount = totalAmount - totalAmountOnBet;

            tempText.gameObject.SetActive(true);
            tempText.text = "";
            tempText.text = totalAmountOnBet.ToString();
            SetTexts();
        }

    }

    public void ClearButtonClick()
    {
        if (!RealTimer.instance.buttonsInterctable || isdeal)
        {
            Debug.Log("TIMER IS LESS THAN 10 SECS");
            return;
        }
        isChipSelected = false;
        isBetSlected = false;
        totalAmount = totalAmount + totalAmountOnBet;
        totalAmountOnBet = 0;
        currentBetAmount = 0;
        SetTexts();

    }

    public void ResetValues()
    {
        isChipSelected = false;
        isBetSlected = false;
        isdeal = false;
        currentBetAmount = 0;
        totalAmountOnBet = 0;
    }

    public void SetTexts()
    {
        balanceText.text = totalAmount.ToString();
        totalBetTexts.text = totalAmountOnBet.ToString();
        winningText.text = winningAmount.ToString();
    }

    public void Result(int amount, int betNumberRslt)
    {
        if (selectedBetNumber == betNumberRslt)
        {
            isBetWon = true;
        }
        if (isdeal) { 
        if (isBetWon)
        {
            totalAmount = totalAmount + amount;
            winningAmount = amount;
        }
        else
        {
            winningAmount = 0;
        }
    }else{
            totalAmount = totalAmount + totalAmountOnBet;
    }
        winNumber = betNumberRslt;
        if (GameData.instance != null)
            GameData.instance.PostLuckyGameResult(winNumber.ToString(), totalAmountOnBet, selectedBetNumber.ToString(), isBetWon);
        SetTexts();
        ResetValues();
        if (history.Count == historyText.Length)
        {
            history.RemoveAt(0);
        }
        history.Add(betNumberRslt);
        ShowHistory();

    }

    public void ShowHistory()
    {
        if (history.Count <= historyText.Length)
        {
            for (int i = 0; i < history.Count; i++)
            {
                historyText[i].text = history[i].ToString();
            }
        }

    }

    public void BackToMainScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    private bool isdeal = false;
    public void DealButton()
    {
        isdeal = true;


    }

}

