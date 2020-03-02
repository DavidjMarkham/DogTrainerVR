using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Lesson1 : Lesson {
    public AudioClip intro_1_audio;
    public AudioClip focus_1_audio;
    public AudioClip focus_2_audio;
    public AudioClip focus_3_audio;
    public AudioClip focus_4_audio;
    public AudioClip focus_5_audio;

    public Instructions introInstructions_1;
    public Instructions focusInstructions_1;
    public Instructions focusInstructions_2;

    private Vector3 playerPosition;

    private bool start_intro_1_audio_played = false;

    // Use this for initialization
    void Start () {
        courseManager.stopwatch.StartTimer();



        playerPosition = courseManager.player.transform.position;       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Play()
    {
        introInstructions_1.FadeIn();
        courseManager.Wait(3f, Start_intro_1_audio);
    }
    
    public void Start_intro_1_audio()
    {
        courseManager.PlayAudio(intro_1_audio, Intro_1_audio_callback);
    }

    public void Intro_1_audio_callback()
    {
        courseManager.Wait(.025f, Start_focus_1_audio);
    }
    
    public void Start_focus_1_audio()
    {
        introInstructions_1.FadeOut();
        courseManager.PlayAudio(focus_1_audio, Focus_1_callback);
    }

    public void Focus_1_callback()
    {
        courseManager.Wait(0.25f, Start_focus_2_audio);
    }

    public void Start_focus_2_audio()
    {        
        courseManager.PlayAudio(focus_2_audio, Focus_2_callback);
    }

    public void Focus_2_callback()
    {
        focusInstructions_1.FadeIn();
        // Wait for user to say "Spike"
        string[] waitKeywords = { "spike" };
        courseManager.speechRecognition.WaitForCommand(waitKeywords, Start_focus_3_audio);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Start_focus_3_audio);                
    }

    public void Start_focus_3_audio()
    {
        focusInstructions_1.Hide();
        courseManager.feedbackGood.Show(2f);        
        courseManager.dog.PlayComeCloser();
        courseManager.PlayAudio(focus_3_audio, Focus_3_callback);
    }

    public void Focus_3_callback()
    {
        courseManager.Wait(0.25f, Start_focus_4_audio);
    }

    public void Start_focus_4_audio()
    {        
        courseManager.PlayAudio(focus_4_audio, Focus_4_callback);
    }

    public void Focus_4_callback()
    {
        focusInstructions_2.FadeIn();
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Throw_treat);
        courseManager.inputController.WaitForTouchpadClick(Throw_treat);
    }

    public void Throw_treat()
    {
        focusInstructions_2.Hide();
        courseManager.feedbackGood.Show(1f);
        courseManager.dogTreat.throwTreat();        

        courseManager.Wait(0.75f, Eat_treat);
    }

    public void Eat_treat()
    {
        courseManager.dog.animationController.Play("FrenchieEat");

        courseManager.Wait(2.0f, Hide_treat);
    }

    public void Hide_treat()
    {
        courseManager.dogTreat.hideTreat();

        courseManager.dog.animationController.Play("FrenchieIdle");

        courseManager.PlayAudio(focus_5_audio, Focus_5_callback);
    }

    public void Focus_5_callback()
    {
        courseManager.LessonComplete();
    }
}
