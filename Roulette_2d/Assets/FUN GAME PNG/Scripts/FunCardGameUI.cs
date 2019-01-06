using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class FunCardGameUI : MonoBehaviour
{
    public static FunCardGameUI instance;
    public CardHolderAnimation cardAnim;
    private bool isChipSelected = false;
    private bool isBetDouble = false;
    private List<string> history = new List<string>();
    public int totalAmount = 20000;
    public int totalAmountOnBet = 0;
    public int winningAmount = 0;
    public float time = 60f;
    public bool isTimerComplete = false;
    public bool isBetWon = false;
    private bool result = false;
    public Image selectedCardImage;
    private Sprite selectedCardSprite;
    private bool isdeal = false;
    private float timeCopy;


    [Header("Texts")]
    public Text balanceText;
    public Text totalBetTexts;
    public Text winningText;
    public Text timerText;
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
        timeCopy = time;
        StartCoroutine(StartMyCoroutine());
    }


    private GameObject chipGO = null;
    private Image chipImage = null;
    private Sprite chipSprite;
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
        chipGO = EventSystem.current.currentSelectedGameObject;
        Debug.Log(chipGO.name, chipGO);
        chipImage = chipGO.GetComponent<Image>();
        chipSprite = chipImage.sprite;
        SetTexts();
    }
    private GameObject go = null;
    private Image goImage = null;


    public void DoubleBetAmounBtn()
    {
        if (!isdeal)
        {
            Debug.Log("Please select bet number or set amount");
            return;

        }
        if (totalAmount > totalAmountOnBet)
        {
            Debug.Log("Bets are double Now");
            totalAmount = totalAmount - totalAmountOnBet;
            totalAmountOnBet = 2 * totalAmountOnBet;
            isBetDouble = true;
            SetTexts();
        }

    }

    public void ClearButtonClick()
    {
        isChipSelected = false;
        isCardSelected = false;
        totalAmount = totalAmount + totalAmountOnBet;
        totalAmountOnBet = 0;
        isdeal = false;
        selectedCard = "";
        if(goImage != null)
        goImage.gameObject.SetActive(false);
        isAndar = false;
        SetTexts();
        if (result)
        {
            selectedCardImage.gameObject.SetActive(false);
            cardAnim.ShuffleAllCards();
            cardAnim.startAnimation = false;
            time = timeCopy;
            StartCoroutine(StartMyCoroutine());
        }

    }

    public void ResetValues()
    {
        isChipSelected = false;
        isCardSelected = false;
        totalAmountOnBet = 0;
    }

    public void SetTexts()
    {
        balanceText.text = totalAmount.ToString();
        totalBetTexts.text = totalAmountOnBet.ToString();
        winningText.text = winningAmount.ToString();
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

    public void DealButtonClick()
    {
        if (!isCardSelected || string.IsNullOrEmpty(andarOrBahar))
        {
            Debug.Log("NO DEAL");
            return;
        }
        isdeal = true;

    }
    public string selectedCard = "";
    public bool isCardSelected = false;
    public void SelectCard(string card)
    {
        if (!isChipSelected)
        {
            Debug.LogError("Chip isn't selected");
            return;
        }
        selectedCard = card;
        if (!isCardSelected)
        {
            isCardSelected = true;
        }


        if (go == null)
        {
            go = EventSystem.current.currentSelectedGameObject;
            Debug.Log(go.name, go);
            goImage = go.transform.GetChild(0).GetComponent<Image>();
            Debug.Log("goImage        " + goImage.name, goImage.gameObject);
            goImage.gameObject.SetActive(true);
            goImage.sprite = chipSprite;
        }
        else
        {
            goImage.gameObject.SetActive(false);

            go = EventSystem.current.currentSelectedGameObject;
            goImage = go.transform.GetChild(0).GetComponent<Image>();
            goImage.gameObject.SetActive(true);
            goImage.sprite = chipSprite;
        }
        selectedCardSprite = go.GetComponent<Image>().sprite;
    }

    public bool isAndar = false;
    private string andarOrBahar = "";
    public void AndarOrBahar(string name)
    {
        if (name == "ANDAR")
        {
            isAndar = true;
        }
        else
        {
            isAndar = false;
        }
        andarOrBahar = name;
    }

    private IEnumerator StartMyCoroutine()
    {
        isTimerComplete = false;
        cardAnim.startAnimation = false;
        selectedCardImage.gameObject.SetActive(false);
        while (time > 0){
            time -= Time.deltaTime;
            timerText.text = time.ToString("F0");
            yield return null;
        }
        timerText.text = "";
        if (isdeal)
        {
            // RepeatCoroutine();
            isTimerComplete = true;
            cardAnim.startAnimation = true;
            cardAnim.stop = false;
            selectedCardImage.gameObject.SetActive(true);
            selectedCardImage.sprite = selectedCardSprite;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            RepeatCoroutine();
        }
    }

    private void RepeatCoroutine()
    {
        StopCoroutine(StartMyCoroutine());
        time = timeCopy;
        StartCoroutine(StartMyCoroutine());
    }

    public void Result(bool isWon, string card, string cardType)
    {
        if (isWon)
        {
            isBetWon = isWon;
            winningAmount = 2 * totalAmountOnBet;
            totalAmount = totalAmount + 2 * totalAmountOnBet;
        }

        if (history.Count == historyText.Length)
        {
            history.RemoveAt(0);
        }
        result = true;
        SetTexts();
        ResetValues();
        history.Add(card + cardType);
        ShowHistory();
    }
}
