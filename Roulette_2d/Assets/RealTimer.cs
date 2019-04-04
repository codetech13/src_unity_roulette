using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RealTimer : MonoBehaviour {

    public static RealTimer instance;
    public Text timerText;
    public float timer = 30;
    private Coroutine timerCoroutine = null;
    public bool buttonsInterctable = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        timerText.text = "";
        StartTimer();
    }

    public void StartTimer()
    {
        buttonsInterctable = true;
        timerCoroutine = StartCoroutine(StartTimerCoroutine());
    }

    private IEnumerator StartTimerCoroutine()
    {
        float temp = timer;
        while (temp >= 0)
        {
            temp -= Time.deltaTime;
            timerText.text = temp.ToString("F0");
            if (temp < 10)
            {
                buttonsInterctable = false;
            }
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        timerText.text = "0";

        yield return new WaitForSeconds(1f);
        SpinWheel.instance.SpinTheWheel();
        RestartTimer();
        Debug.Log("TIMER IS COMPLETE");
    }

    public void KillCoroutine()
    {
        StopCoroutine(timerCoroutine);
    }


    private void RestartTimer()
    {
        KillCoroutine();
        StartTimer();
    }


}
