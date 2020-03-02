using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CourseManager : MonoBehaviour {
    public GameObject player;
    public Lesson[] lessons;
    public DogAI dog;
    public DogTreat dogTreat;
    public InputController inputController;
    public SpeechRecognition speechRecognition;
    public StopwatchTimer stopwatch;

    public Instructions feedbackGood;
    public Instructions feedbackBad;
    public Instructions congrats;

    private int curLesson = 0;

    // Use this for initialization
    void Start () {
        speechRecognition = FindObjectOfType<SpeechRecognition>() as SpeechRecognition;
        lessons[curLesson].Play();
    }
	
	// Update is called once per frame
	void Update () {
		if(speechRecognition == null)
        {
            speechRecognition = FindObjectOfType<SpeechRecognition>() as SpeechRecognition;
        }
	}

    public void PlayAudio(AudioClip clip, System.Action callback)
    {
        AudioSource.PlayClipAtPoint(clip, this.player.transform.position);

        float duration = clip.length;
        StartCoroutine(this.StartMethod(duration,callback));
    }

    private IEnumerator StartMethod(float duration, System.Action callback)
    {
        yield return new WaitForSeconds(duration);        
        callback();
    }

    public void Wait(float duration, System.Action callback)
    {
        StartCoroutine(this.StartMethod(duration, callback));
    }

    public void LessonComplete()
    {
        curLesson++;
        if(curLesson<lessons.Length)
        {
            lessons[curLesson].Play();
        }
        else
        {
            this.Wait(4.0f, Show_congrats); 
        }
    }    

    public void Show_congrats()
    {
        congrats.FadeIn();
    }
}
