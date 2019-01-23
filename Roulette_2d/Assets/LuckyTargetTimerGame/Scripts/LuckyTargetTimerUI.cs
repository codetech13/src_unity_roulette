using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LuckyTargetTimerUI : MonoBehaviour {
    public static LuckyTargetTimerUI instance;
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
    public Color newColor;
    public Color oldColor;


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
    }
    private GameObject go = null;
    public void SelectBetNumber(int betNumber)
    {
        if (!isChipSelected)
        {
            return;
        }
        isBetSlected = true;
        selectedBetNumber = betNumber;
        if (go == null) {
            go = EventSystem.current.currentSelectedGameObject;
            Image newBtn = go.GetComponent<Image>();
            newBtn.color = newColor;

            Debug.Log("XOXOXOXO");
        }
        else
        {
            Debug.Log("HJHJHJHJ");
            Image button = go.GetComponent<Image>();
            button.color = oldColor;


            go = EventSystem.current.currentSelectedGameObject;
            Image newBtn = go.GetComponent<Image>();
            newBtn.color = newColor;

        }

    }

    public void DoubleBetAmounBtn()
    {
        if (!isBetSlected || !isChipSelected)
        {
            Debug.Log("Please select bet number or set amount");
            return;

        }
        if (totalAmount > totalAmountOnBet)
        {
            Debug.Log("Bets are double Now");
            totalAmount = totalAmount - totalAmountOnBet;
            totalAmountOnBet = 2 * totalAmountOnBet;
            SetTexts();
        }

    }

    public void ClearButtonClick()
    {
        isChipSelected = false;
        isBetSlected = false;
        totalAmount = totalAmount + totalAmountOnBet;
        totalAmountOnBet = 0;
        SetTexts();

    }

    public void ResetValues()
    {
        isChipSelected = false;
        isBetSlected = false;
        totalAmountOnBet = 0;
    }

    public void SetTexts()
    {
        balanceText.text = totalAmount.ToString();
        totalBetTexts.text = totalAmountOnBet.ToString();
        winningText.text = winningAmount.ToString();
    }

    public void Result(int amount , int betNumberRslt)
    {
        if (selectedBetNumber == betNumberRslt)
        {
            isBetWon = true;
        }
        if (isBetWon)
        {
            totalAmount = totalAmount + amount;
            winningAmount = amount;
        }
        else
        {
            winningAmount = 0;
        }
        winNumber = betNumberRslt;
        if(GameData.instance != null)
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
            for (int i=0; i < history.Count; i++)
            {
                historyText[i].text = history[i].ToString();
            }
        }

    }

    public void BackToMainScene()
    {
        SceneManager.LoadScene("GameScene");
    }

}
