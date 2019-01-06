using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolderAnimation : MonoBehaviour {

	[SerializeField] List<GameObject> cards;

	[SerializeField] Transform  andar;
	[SerializeField] Transform bahar;
    [SerializeField] Transform initialTransform;
    [SerializeField] Transform parentTransform;

   public bool startAnimation = false;
	bool playNextCard = true;

	[SerializeField] float animationSpeed = 2f;

	GameObject currentCard;
    public List<GameObject> ALLCARD = new List<GameObject>();
    [SerializeField]private List<CardProperty> cardProperties = new List<CardProperty>();


    private void Awake()
    {
        ShuffleAllCards();
    }
    private void Start1()
    {
        ALLCARD.Clear();
        cardProperties.Clear();
        for (int i = 0; i < cards.Count; i++)
        {
            GameObject go = Instantiate(cards[i], parentTransform) as GameObject;
            //go.transform.position = initialTransform.position;
            ALLCARD.Add(go);
            cardProperties.Add(go.GetComponent<CardProperty>());
        }
    }
    public void ShuffleAllCards()
    {
        ShuffleCards.Shuffle(cards);
        for (int i = 0; i < ALLCARD.Count; i++)
        {
            Destroy(ALLCARD[i]);
            Destroy(cardProperties[i].gameObject);
        }
        currentIndex = 0;
        Invoke("Start1", 1.0f);
    }
    public bool stop = true;
    // Update is called once per frame
    void Update () {
        if (stop) {
            return;
        }
		if (startAnimation && currentIndex < cards.Count) {
			if (playNextCard) {
				playNextCard = false;
				currentIndex++;
//				newRoation = Random.Range (rotaionZ [0], rotaionZ [rotaionZ.Length-1]);
			}
		}

		if (currentIndex % 2 == 0) {
			playAnimation (ALLCARD [currentIndex], cardProperties[currentIndex], bahar, false);
		} else {
			playAnimation (ALLCARD [currentIndex], cardProperties[currentIndex], andar, true);
		}
	}

	[SerializeField] int currentIndex = 0;
    //	[SerializeField] int newRoation = 0;

    //	int[] rotaionZ = new int[13] {10,15,20,25,30,35,40,47,50,55,67,75,85};
    private bool isCardMAtched = false;
	void playAnimation(GameObject go, CardProperty cardProperty, Transform target, bool isAndar){
		playNextCard = false;
//		Quaternion rotaion = go.transform.rotation;

		go.transform.position = Vector3.Lerp (go.transform.position, target.position, Time.deltaTime * 0.9f * animationSpeed);

//		Debug.Log ("playing");
		if(Vector3.Distance(go.transform.position, target.position) < 0.5f){
            //			rotaion.z = newRoation;
            //			go.transform.rotation = rotaion;
            if (cardProperty.card == FunCardGameUI.instance.selectedCard)
            {
                Debug.Log("<color=green>Found The Card</color>" + cardProperty.card + "    " + FunCardGameUI.instance.selectedCard + "   is andar  " + isAndar, go);
           
                if (isAndar == FunCardGameUI.instance.isAndar)
                {
                    FunCardGameUI.instance.Result(true, cardProperty.card, cardProperty.cardType);
                    Debug.Log("<color=yellow> USER WON</color>");
                }
                else
                {

                    FunCardGameUI.instance.Result(false, cardProperty.card, cardProperty.cardType);
                }
                startAnimation = false;
                stop = true;
            }
            playNextCard = true;
		}


	}
}
