using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopwatchTimer : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Graphics graphic;

    private bool fadingIn = false;
    private bool fadingOut = false;

    private float secondsToShow = -1f;

    private float curTime = 0f;
    private bool keepingTime = true;
    public Text text;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (fadingIn)
        {
            canvasGroup.alpha += 0.005f;
            if (canvasGroup.alpha >= 1.0f)
            {
                canvasGroup.alpha = 1.0f;
                fadingIn = false;
            }
        }

        if (fadingOut)
        {
            canvasGroup.alpha -= 0.005f;
            if (canvasGroup.alpha <= 0.0f)
            {
                canvasGroup.alpha = 0.0f;
                fadingOut = false;
                gameObject.SetActive(false);
            }
        }

        if (secondsToShow > 0f)
        {
            secondsToShow -= Time.deltaTime;

            if (secondsToShow <= 0.0f)
            {
                secondsToShow = -1f;
                this.Hide();
            }
        }

        if(keepingTime)
        {
            curTime += Time.deltaTime;

            string minutes = Mathf.Floor(curTime / 60).ToString("00");
            string seconds = (curTime % 60).ToString("00");
            string hundredthOfSec = Mathf.Floor((curTime*100) %100).ToString("00");

            text.text = seconds + ":" + hundredthOfSec;
        }
    }

    public void FadeIn()
    {
        secondsToShow = -1f;
        gameObject.SetActive(true);
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        canvasGroup.alpha = 0.0f;
        fadingIn = true;

    }

    public void FadeOut()
    {
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        fadingOut = true;
    }

    public void Show(float inputSecondsToShow = -1f)
    {
        secondsToShow = inputSecondsToShow;
        gameObject.SetActive(true);
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        canvasGroup.alpha = 1.0f;
        text.text = "00:00";
    }

    public void Hide()
    {
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        canvasGroup.alpha = 0.0f;
        fadingOut = false;
        gameObject.SetActive(false);
    }

    public void StartTimer()
    {
        keepingTime = true;
    }

    public void StopTimer()
    {
        keepingTime = false;
    }

    public void ResetTimer()
    {
        curTime = 0f;
        keepingTime = false;
    }

    public float GetTime()
    {
        return curTime;
    }
}

