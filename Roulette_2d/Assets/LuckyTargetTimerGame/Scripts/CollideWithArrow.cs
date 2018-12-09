using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollideWithArrow : MonoBehaviour {
    [SerializeField]private string temp = "";
    public Image[] rewardSprites;
    [SerializeField] private Image currentImage;
    [SerializeField] private Image nextImage;
    [SerializeField] private Image previousImage;
    public RectTransform nextPosition;
    public RectTransform mainPosition;
    public RectTransform previousPosition;
    [SerializeField] private int currentIndex;
    public float time = 0;

    private void Start1()
    {
        currentIndex = 0;
        currentImage = rewardSprites[currentIndex];
        nextImage = rewardSprites[currentIndex + 1];
        for (int i = 0; i < rewardSprites.Length; i++)
        {
            if (rewardSprites[i] == nextImage || rewardSprites[i] == currentImage)
            {

            }
            else
            {
                rewardSprites[i].transform.position = previousPosition.transform.position;
            }
        }
        previousImage = null;
    }

    public void ResetRewardImages()
    {
        Start1();
    }

    private void Update()
    {
        if (!SpinWheel.instance.spinning) {
            return;
        }
        time -= Time.deltaTime;
        if (time < 0.1f)
        {
            if (currentImage != null && nextImage != null)
            {
                float current2Main = Vector3.Distance(currentImage.transform.position, mainPosition.transform.position);
                float next2Main = Vector3.Distance(nextImage.transform.position, mainPosition.transform.position);

                if (current2Main < next2Main)
                {
                    currentImage.transform.position = mainPosition.transform.position;
                    nextImage.transform.position = previousPosition.transform.position;
                    nextImage = null;
                }
                else
                {
                    nextImage.transform.position = mainPosition.transform.position;
                    currentImage.transform.position = nextPosition.transform.position;
                    currentImage = null;
                }
            }
        }

        if (time > 0.5f) { 
            if (currentImage != null && currentImage.transform.position.x <= nextPosition.transform.position.x)
        {
            currentImage.transform.position += transform.right * Time.deltaTime * 90f;
        }
        else
        {
            currentImage = null;
        }

        if (nextImage != null && nextImage.transform.position.x <= mainPosition.transform.position.x)
        {
            nextImage.transform.position += transform.right * Time.deltaTime * 90f;
        }
        else
        {
            currentImage = nextImage;
            nextImage = null;
        }

        if (nextImage == null && currentImage != null) {
            currentIndex++;
            if (currentIndex + 1 == rewardSprites.Length)
            {
                currentIndex = 0;
            }
            nextImage = rewardSprites[currentIndex + 1];
            nextImage.transform.position = previousPosition.transform.position;
        }
    }

        /* if(nextImage.transform.position.x <= mainPosition.transform.position.x){
             currentImage.transform.position += transform.right * Time.deltaTime * 10f;

         }*/


    }
}
