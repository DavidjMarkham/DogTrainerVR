using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LessonDown : Lesson {
    public AudioClip down_1_audio;
    public AudioClip down_2_audio;
    public AudioClip down_3_audio;
    public AudioClip down_4_audio;
    public AudioClip down_6_audio;
    public AudioClip down_7_audio;
    public AudioClip down_8_audio;
    public AudioClip down_9_audio;
    public AudioClip down_10_audio;
    public AudioClip down_11_audio;

    public Instructions downInstructions_1;
    public Instructions downInstructions_2;
    public Instructions downInstructions_3;
    public Instructions downInstructions_4;
    public Instructions downInstructions_5;
    public Instructions downInstructions_6;

    private Vector3 playerPosition;
    private int timedTries = 0;

    // Use this for initialization
    void Start () {        
        playerPosition = courseManager.player.transform.position;        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Play()
    {
        downInstructions_1.FadeIn();
        courseManager.PlayAudio(down_1_audio, this.down_1_audio_callback);
    }

    public void down_1_audio_callback()
    {
        downInstructions_1.FadeOut();
        downInstructions_2.FadeIn();

        // Wait for user to say "Down"
        string[] waitKeywords = { "down"};
        courseManager.speechRecognition.WaitForCommand(waitKeywords, Start_down_2_audio);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Start_down_2_audio);
    }
    
    public void Start_down_2_audio()
    {
        downInstructions_2.Hide();
        courseManager.feedbackGood.Show(2f);
        courseManager.PlayAudio(down_2_audio, down_2_audio_callback);
    }

    public void down_2_audio_callback()
    {
        downInstructions_3.FadeIn();
        /*
        courseManager.dogTreat.moveTreatAboveHead();
        courseManager.Wait(2.0f, Move_dog_treat);*/
        courseManager.inputController.WaitForTouchpadClick(this.Held_button);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, this.Held_button);
    }

    public void Held_button()
    {
        downInstructions_3.Hide();
        downInstructions_4.FadeIn();
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, this.Moved_dog_treat);
        courseManager.dogTreat.WaitForLowPosition(Moved_dog_treat);        
    }

        

    public void Moved_dog_treat()
    {
        downInstructions_4.Hide();
        courseManager.feedbackGood.Show(2f);
        courseManager.dog.animationController.SetFloat("Down", 2.0f);
        courseManager.Wait(2.0f, Start_down_3_audio);
    }

    public void Start_down_3_audio()
    {        
        courseManager.dogTreat.hideTreat();
        courseManager.PlayAudio(down_3_audio, down_3_audio_callback);
    }

    public void down_3_audio_callback()
    {
        courseManager.Wait(.025f, Start_down_4_audio);
    }

    public void Start_down_4_audio()
    {
        courseManager.PlayAudio(down_4_audio, down_4_audio_callback);
    }
        
    public void down_4_audio_callback()
    {
        downInstructions_5.FadeIn();
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Throw_treat);
        courseManager.inputController.WaitForTouchpadClick(Throw_treat);
    }

    public void Throw_treat()
    {
        downInstructions_5.Hide();
        courseManager.feedbackGood.Show(2f);
        courseManager.dogTreat.throwTreat();

        courseManager.Wait(0.75f, Eat_treat);
    }

    public void Eat_treat()
    {
        courseManager.dog.animationController.Play("FrenchieEat");

        courseManager.dog.animationController.SetFloat("Down", 0.0f);

        courseManager.Wait(2.0f, Hide_treat);
    }

    public void Hide_treat()
    {
        courseManager.dogTreat.hideTreat();

        courseManager.dog.animationController.Play("FrenchieIdle");

        courseManager.PlayAudio(down_6_audio, Start_Timed_Down);
    }
        
    public void Start_Timed_Down()
    {
        timedTries++;
        downInstructions_6.FadeIn();
        courseManager.stopwatch.ResetTimer();
        courseManager.stopwatch.Show();
        // Wait for user to say "Down"
        string[] waitKeywords = { "down" };
        courseManager.speechRecognition.WaitForCommand(waitKeywords, Move_treat);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Move_treat);
    }

    public void Move_treat()
    {
        courseManager.stopwatch.StartTimer();
        courseManager.dogTreat.WaitForLowPosition(Wait_Move_dog_treat);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Wait_Move_dog_treat);
        /*courseManager.dogTreat.moveTreatAboveHead();
        courseManager.Wait(2.0f, Wait_Move_dog_treat);*/
    }

    public void Wait_Move_dog_treat()
    {
        downInstructions_6.Hide();
        downInstructions_5.Show();
        courseManager.dog.animationController.SetFloat("Down", 2.0f);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Throw_treat_2);
        courseManager.inputController.WaitForTouchpadClick(Throw_treat_2);
    }

    public void Throw_treat_2()
    {
        courseManager.stopwatch.StopTimer();
        downInstructions_5.Hide();
        courseManager.dogTreat.throwTreat();

        courseManager.Wait(0.75f, Eat_treat_2);
    }

    public void Eat_treat_2()
    {
        courseManager.dog.animationController.Play("FrenchieEat");

        courseManager.dog.animationController.SetFloat("Down", 0.0f);

        courseManager.Wait(2.0f, Hide_treat_2);
    }

    public void Hide_treat_2()
    {
        int secondsElapsed = (int)(courseManager.stopwatch.GetTime() % 60);

        if (secondsElapsed <= 10.0f)
        {
            fastEnough();
        }
        else
        {
            tooSlow();
        }
    }

    public void fastEnough()
    {
        courseManager.dogTreat.hideTreat();

        courseManager.dog.animationController.Play("FrenchieIdle");

        courseManager.feedbackGood.Show(2f);

        courseManager.PlayAudio(down_7_audio, Down_7_audio_callback);
    }

    public void Down_7_audio_callback()
    {
        if (timedTries == 1)
        {
            courseManager.PlayAudio(down_9_audio, Play_lesson_over_audio);
        }
        else
        {
            Play_lesson_over_audio();
        }
    }

    public void Play_lesson_over_audio()
    {
        courseManager.stopwatch.ResetTimer();
        courseManager.stopwatch.Hide();

        courseManager.PlayAudio(down_11_audio, Down_11_audio_callback);
    }

    public void Down_11_audio_callback()
    {
        courseManager.LessonComplete();
    }

    public void tooSlow()
    {
        courseManager.dogTreat.hideTreat();

        courseManager.dog.animationController.Play("FrenchieIdle");

        courseManager.feedbackBad.Show(4f);

        courseManager.PlayAudio(down_8_audio, Down_8_audio_callback);
    }

    public void Down_8_audio_callback()
    {
        courseManager.stopwatch.ResetTimer();
        courseManager.stopwatch.Hide();

        if (timedTries >= 3)
        {
            Play_lesson_over_audio();
        }
        else if (timedTries == 1)
        {
            courseManager.PlayAudio(down_9_audio, Down_9_audio_callback);
        }
        else
        {
            courseManager.PlayAudio(down_10_audio, Start_Timed_Down);
        }
    }

    public void Down_9_audio_callback()
    {
        courseManager.PlayAudio(down_10_audio, Start_Timed_Down);
    }


}
