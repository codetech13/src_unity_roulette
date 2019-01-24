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
    private List<string> cardhistory = new List<string>();
    private List<string> winnerhistory = new List<string>();
    public int totalAmount = 20000;
    public int totalAmountOnBet = 0;
    public int winningAmount = 0;
    public float time = 60f;
    public bool isTimerComplete = false;
    public bool isBetWon = false;
    public bool isPlayerFirst = false;
    private bool result = false;
   // public Image selectedCardImage;
    private Sprite selectedCardSprite;
    private bool isdeal = false;
    private float timeCopy;


    [Header("Texts")]
    public Text balanceText;
    public Text totalBetTexts;
    public Text winningText;
    public Text timerText;
    public Text[] historyText;
    public Text[] cardhistoryText;
    public Text[] winnerhistoryText;
    public Toggle playerToggle;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        playerToggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(playerToggle);
        });
        history.Clear();
        cardhistory.Clear();
        winnerhistory.Clear();
        ResetValues();
        SetTexts();
        timeCopy = time;
       // StartCoroutine(StartMyCoroutine());
    }
    private void ToggleValueChanged(Toggle change)
    {
        if (change.isOn)
        {
            Debug.Log("Change is on   " + change.isOn);
        }
        isPlayerFirst = change.isOn;
    }

    private GameObject chipGO = null;
    private Image chipImage = null;
    private Sprite chipSprite;
    int currentBetAmount = 0;
    public void OnChipButtonCick(int chipAmount)
    {
        currentBetAmount = chipAmount;

        /* if (totalAmount > chipAmount && !isChipSelected)
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
        */
        chipGO = EventSystem.current.currentSelectedGameObject;
        Debug.Log(chipGO.name, chipGO);
        chipImage = chipGO.GetComponent<Image>();
        chipSprite = chipImage.sprite;
        isChipSelected = true;
        //SetTexts();
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
           // selectedCardImage.gameObject.SetActive(false);
            cardAnim.ShuffleAllCards();
            cardAnim.startAnimation = false;
            time = timeCopy;
           // StartCoroutine(StartMyCoroutine());
        }

    }

    public void ResetValues()
    {
        isChipSelected = false;
        isCardSelected = false;
        totalAmountOnBet = 0;
        selectedCard = "";
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


        if (cardhistory.Count <= cardhistoryText.Length)
        {
            for (int i = 0; i < cardhistory.Count; i++)
            {
                cardhistoryText[i].text = cardhistory[i].ToString();
            }
        }


        if (winnerhistory.Count <= winnerhistoryText.Length)
        {
            for (int i = 0; i < winnerhistory.Count; i++)
            {
                winnerhistoryText[i].text = winnerhistory[i].ToString();
            }
        }

    }

    public void BackToMainScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void DealButtonClick()
    {
        if (!isCardSelected /*|| string.IsNullOrEmpty(andarOrBahar)*/)
        {
            Debug.Log("NO DEAL");
            return;
        }
        isdeal = true;

        cardAnim.SwapTransform(isPlayerFirst);
        cardAnim.startAnimation = true;
        cardAnim.stop = false;
        //totalAmountOnBet = tempNO;
        //totalAmount = totalAmount - totalAmountOnBet;
        SetTexts();

    }
    [SerializeField] private int tempNO = 0;
    public string selectedCard = "";
    public bool isCardSelected = false;
    public void SelectCard(string card)
    {
        if (!isChipSelected)
        {
            Debug.LogError("Chip isn't selected");
            return;
        }

        if (totalAmount < currentBetAmount) {
            Debug.LogError("BET AMOUNT  > CURRENT AMOUNT");
            return;
        }

        /* tempValue++;
         if (isCardSelected) {
             tempNO = tempValue * totalAmountOnBet;
             if (totalAmount > tempNO)
             {

                 // totalAmount = totalAmount - totalAmountOnBet;
             }
             else
             {
                 totalAmountOnBet = tempValue * totalAmountOnBet;
             }
         }*/

   
    



        if (go == null)
        {
            go = EventSystem.current.currentSelectedGameObject;
          //  Debug.Log(go.name, go);
            goImage = go.transform.GetChild(0).GetComponent<Image>();
           // Debug.Log("goImage        " + goImage.name, goImage.gameObject);
            goImage.gameObject.SetActive(true);
            goImage.sprite = chipSprite;
            //   Debug.Log("BBBB");
            totalAmountOnBet = totalAmountOnBet + currentBetAmount;
            totalAmount = totalAmount - currentBetAmount;
        }
        else
        {
            goImage.gameObject.SetActive(false);

            go = EventSystem.current.currentSelectedGameObject;
            goImage = go.transform.GetChild(0).GetComponent<Image>();
            goImage.gameObject.SetActive(true);

            totalAmountOnBet = totalAmountOnBet + currentBetAmount;
            totalAmount = totalAmount - currentBetAmount;

             if (isCardSelected)
             {
                 Text tempText = goImage.gameObject.transform.GetChild(0).GetComponent<Text>();
                 tempText.gameObject.SetActive(true);
                 tempText.text = "";
                 tempText.text = totalAmountOnBet.ToString();
             }

            goImage.sprite = chipSprite;
            Debug.Log("CCCC");
        }
        selectedCardSprite = go.GetComponent<Image>().sprite;
        SetTexts();
        if (!isCardSelected)
        {
            //tempNO = 0;
            selectedCard = card;
            isCardSelected = true;
            tap++;
        }
    }
    int tap = 0;
    public bool isAndar = false;
    private string andarOrBahar = "";
    [SerializeField]private int tempValue = 0; //for clicking many times on same button
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

    #region For TIMER GAME
    private IEnumerator StartMyCoroutine()
    {
        isTimerComplete = false;
        cardAnim.startAnimation = false;
        //selectedCardImage.gameObject.SetActive(false);
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
            cardAnim.SwapTransform(isPlayerFirst);
            cardAnim.startAnimation = true;
            cardAnim.stop = false;
            //selectedCardImage.gameObject.SetActive(true);
            //selectedCardImage.sprite = selectedCardSprite;
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
#endregion

    public void Result(bool isWon, string card, string cardType)
    {
        if (isWon)
        {
            isBetWon = isWon;
            winningAmount = 2 * totalAmountOnBet;
            totalAmount = totalAmount + 2 * totalAmountOnBet;
        }
        //Post Result
        //GameData.instance.PostFungameResult(card, totalAmountOnBet, selectedCard, isWon);
        if (history.Count == historyText.Length)
        {
            history.RemoveAt(0);
        }
        if (cardhistory.Count == cardhistoryText.Length)
        {
            cardhistory.RemoveAt(0);
        }
        if (winnerhistory.Count == winnerhistoryText.Length)
        {
            winnerhistory.RemoveAt(0);
        }
        result = true;
        history.Add(selectedCard);
        SetTexts();
        ResetValues();
        cardhistory.Add(card + cardType);
        if (isWon) {
            winnerhistory.Add("Player");
        }
        else
        {
            winnerhistory.Add("Dealer");

        }
        ShowHistory();
        cardAnim.ResetTransform();
    }
}
