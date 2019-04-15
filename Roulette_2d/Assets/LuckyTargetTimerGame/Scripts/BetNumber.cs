using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetNumber : MonoBehaviour {
    public int betNumber;
    public Text betAmountText;
    private Button betNumberButton;
    [SerializeField] private int betAmount = 0;
    public LuckyTargetTimerUI luckyTargetTimerUI;

    private void Awake()
    {
        betNumberButton = GetComponent<Button>();
    }

    private void Start()
    {
        luckyTargetTimerUI = LuckyTargetTimerUI.instance;
        betNumberButton.onClick.AddListener(OnBetNumberButtonListener);
    }


    private void OnBetNumberButtonListener()
    {
        OnBetNumberButtonClick(betNumber);
    }

    public void OnBetNumberButtonClick(int betNumber)
    {
        if (!luckyTargetTimerUI.isChipSelected) {
            return;
        }

        if (betAmount == 0)
        {
            if (luckyTargetTimerUI.currentBetAmount <= luckyTargetTimerUI.totalAmount)
            {
                
            }
            else
            {
                Debug.LogError("NOT ENOUGH AMOUNT");
                return;
            }
            Debug.Log("HERE 0");
        }
        else
        {
            if ((betAmount + luckyTargetTimerUI.currentBetAmount) <= luckyTargetTimerUI.totalAmount)
            {
               
            }
            else
            {
                Debug.LogError("NOT ENOUGH AMOUNT");
                return;
            }

            Debug.Log("HERE !0");
        }

        betAmount = betAmount + luckyTargetTimerUI.currentBetAmount;
        luckyTargetTimerUI.totalAmount = luckyTargetTimerUI.totalAmount - luckyTargetTimerUI.currentBetAmount;
        luckyTargetTimerUI.SetTotalAmountOnBet(luckyTargetTimerUI.currentBetAmount);
       // luckyTargetTimerUI.totalAmount = luckyTargetTimerUI.totalAmount - betAmount;
        betAmountText.text = betAmount.ToString();
        if (!luckyTargetTimerUI.betNumberList.Contains(betNumber))
        {
            luckyTargetTimerUI.betNumberList.Add(betNumber);
        }


        if (!luckyTargetTimerUI.betNumberANDAmountDict.ContainsKey(betNumber))
        {
            luckyTargetTimerUI.betNumberANDAmountDict.Add(betNumber, betAmount);
        }
        else
        {
            luckyTargetTimerUI.betNumberANDAmountDict[betNumber] = betAmount;
        }
    }

    public void OnCLear()
    {
        betAmount = 0;
        betAmountText.text = "";
    }
}
