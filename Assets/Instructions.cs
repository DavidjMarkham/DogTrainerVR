using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public Graphics graphic;

    private bool fadingIn = false;
    private bool fadingOut = false;
    private Image image;

    private float secondsToShow = -1f;

    // Use this for initialization
    void Start () {
        image = GetComponentInChildren<Image>();
    }
	
	// Update is called once per frame
	void Update () {
        if(fadingIn)
        {            
            canvasGroup.alpha += 0.015f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, canvasGroup.alpha * .75f);
            if (canvasGroup.alpha >= 1.0f)
            {
                canvasGroup.alpha = 1.0f;
                image.color =  new Color(image.color.r,image.color.g, image.color.b, .75f);
                fadingIn = false;
            }
        }

        if (fadingOut)
        {
            canvasGroup.alpha -= 0.015f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, canvasGroup.alpha * .75f);
            if (canvasGroup.alpha <= 0.0f)
            {
                canvasGroup.alpha = 0.0f;
                image.color = new Color(image.color.r, image.color.g, image.color.b, 0.0f);
                fadingOut = false;
                gameObject.SetActive(false);
            }
        }

        if(secondsToShow > 0f)
        {
            secondsToShow -= Time.deltaTime;

            if (secondsToShow <= 0.0f)
            {
                secondsToShow = -1f;
                this.Hide();
            }
        }

    }

    public void FadeIn()
    {
        secondsToShow = -1f;
        gameObject.SetActive(true);
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        image = GetComponentInChildren<Image>();
        canvasGroup.alpha = 0.0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, canvasGroup.alpha);
        fadingIn = true;
        
    }

    public void FadeOut()
    {
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        image = GetComponentInChildren<Image>();
        fadingOut = true;        
    }

    public void Show(float inputSecondsToShow = -1f)
    {
        secondsToShow = inputSecondsToShow;
        gameObject.SetActive(true);
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        image = GetComponentInChildren<Image>();
        canvasGroup.alpha = 1.0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, canvasGroup.alpha*.75f);
    }

    public void Hide()
    {
        canvasGroup = transform.parent.gameObject.GetComponent<CanvasGroup>() as CanvasGroup;
        image = GetComponentInChildren<Image>();
        canvasGroup.alpha = 0.0f;
        image.color = new Color(image.color.r, image.color.g, image.color.b, canvasGroup.alpha);
        fadingOut = false;
        gameObject.SetActive(false);
    }
}
