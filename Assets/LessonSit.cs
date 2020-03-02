using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LessonSit : Lesson
{
    public AudioClip sit_1_audio;
    public AudioClip sit_2_audio;
    public AudioClip sit_3_audio;
    public AudioClip sit_4_audio;
    public AudioClip sit_5_audio;
    public AudioClip sit_6_audio;
    public AudioClip sit_7_audio;
    public AudioClip sit_8_audio;
    public AudioClip sit_9_audio;
    public AudioClip sit_10_audio;
    public AudioClip sit_11_audio;
    public AudioClip sit_12_audio;
    public AudioClip sit_13_audio;
    
    public Instructions sitInstructions_1;
    public Instructions sitInstructions_2;
    public Instructions sitInstructions_3;
    public Instructions sitInstructions_4;
    public Instructions sitInstructions_5;
    public Instructions sitInstructions_6;

    private Vector3 playerPosition;

    private int timedTries = 0;

    // Use this for initialization
    void Start()
    {
        playerPosition = courseManager.player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Play()
    {
        sitInstructions_1.FadeIn();
        courseManager.PlayAudio(sit_1_audio, this.Sit_1_audio_callback);
    }

    public void Sit_1_audio_callback()
    {
        sitInstructions_1.FadeOut();
        sitInstructions_2.FadeIn();

        // Wait for user to say "Sit"
        string[] waitKeywords = { "sit", "so it" };
        courseManager.speechRecognition.WaitForCommand(waitKeywords, Start_sit_2_audio);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Start_sit_2_audio);
    }

    public void Start_sit_2_audio()
    {
        sitInstructions_2.Hide();
        courseManager.feedbackGood.Show(2f);
        courseManager.PlayAudio(sit_2_audio, Sit_2_audio_callback);
    }

    public void Sit_2_audio_callback()
    {
        sitInstructions_3.FadeIn();
        /*
        courseManager.dogTreat.moveTreatAboveHead();
        courseManager.Wait(2.0f, Move_dog_treat);*/
        courseManager.inputController.WaitForTouchpadClick(Held_button);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Held_button);
    }

    public void Held_button()
    {
        sitInstructions_3.Hide();
        sitInstructions_4.FadeIn();
        courseManager.dogTreat.WaitForHighPosition(Moved_dog_treat);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Moved_dog_treat);
    }



    public void Moved_dog_treat()
    {
        sitInstructions_4.Hide();
        courseManager.feedbackGood.Show(2f);
        courseManager.dog.animationController.SetFloat("Sit", 2.0f);
        courseManager.Wait(2.0f, Start_sit_3_audio);
    }

    public void Start_sit_3_audio()
    {
        courseManager.dogTreat.hideTreat();
        courseManager.PlayAudio(sit_3_audio, Sit_3_audio_callback);
    }

    public void Sit_3_audio_callback()
    {
        courseManager.Wait(.025f, Start_sit_4_audio);
    }

    public void Start_sit_4_audio()
    {
        courseManager.PlayAudio(sit_4_audio, Sit_4_audio_callback);
    }

    public void Sit_4_audio_callback()
    {
        sitInstructions_5.FadeIn();
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Throw_treat);
        courseManager.inputController.WaitForTouchpadClick(Throw_treat);
    }

    public void Throw_treat()
    {
        sitInstructions_5.Hide();
        courseManager.feedbackGood.Show(2f);
        courseManager.dogTreat.throwTreat();

        courseManager.Wait(0.75f, Eat_treat);
    }

    public void Eat_treat()
    {
        courseManager.dog.animationController.Play("FrenchieEat");

        courseManager.dog.animationController.SetFloat("Sit", 0.0f);

        courseManager.Wait(2.0f, Hide_treat);
    }

    public void Hide_treat()
    {
        courseManager.dogTreat.hideTreat();

        courseManager.dog.animationController.Play("FrenchieIdle");

        courseManager.PlayAudio(sit_5_audio, Sit_5_audio_callback);
    }

    public void Sit_5_audio_callback()
    {
        courseManager.Wait(.025f, Start_sit_6_audio);
    }

    public void Start_sit_6_audio()
    {
        courseManager.PlayAudio(sit_6_audio, Start_Timed_Sit);
    }

    public void Start_Timed_Sit()
    {
        timedTries++;
        sitInstructions_6.FadeIn();
        courseManager.stopwatch.ResetTimer();
        courseManager.stopwatch.Show();
        // Wait for user to say "Sit"
        string[] waitKeywords = { "sit", "so it" };
        courseManager.speechRecognition.WaitForCommand(waitKeywords, Move_treat);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Move_treat);
    }

    public void Move_treat()
    {
        courseManager.stopwatch.StartTimer();
        courseManager.dogTreat.WaitForHighPosition(Wait_Move_dog_treat);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Wait_Move_dog_treat);
        /*courseManager.dogTreat.moveTreatAboveHead();
        courseManager.Wait(2.0f, Wait_Move_dog_treat);*/
    }

    public void Wait_Move_dog_treat()
    {
        sitInstructions_6.Hide();
        sitInstructions_5.Show();
        courseManager.dog.animationController.SetFloat("Sit", 2.0f);
        courseManager.inputController.WaitForKeyboardInput(KeyCode.Space, Throw_treat_2);
        courseManager.inputController.WaitForTouchpadClick(Throw_treat_2);
    }

    public void Throw_treat_2()
    {
        courseManager.stopwatch.StopTimer();
        sitInstructions_5.Hide();
        courseManager.dogTreat.throwTreat();

        courseManager.Wait(0.75f, Eat_treat_2);
    }

    public void Eat_treat_2()
    {
        courseManager.dog.animationController.Play("FrenchieEat");

        courseManager.dog.animationController.SetFloat("Sit", 0.0f);

        courseManager.Wait(2.0f, Hide_treat_2);
    }

    public void Hide_treat_2()
    {
        int secondsElapsed = (int)(courseManager.stopwatch.GetTime() % 60);

        if(secondsElapsed<=10.0f)
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

        courseManager.PlayAudio(sit_7_audio, Sit_7_audio_callback);
    }

    public void Sit_7_audio_callback()
    {
        if(timedTries==1)
        {
            courseManager.PlayAudio(sit_9_audio, Play_lesson_over_audio);
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

        courseManager.PlayAudio(sit_13_audio, Sit_13_audio_callback);
    }

    public void Sit_13_audio_callback()
    {
        courseManager.LessonComplete();
    }

    public void tooSlow()
    {
        courseManager.dogTreat.hideTreat();

        courseManager.dog.animationController.Play("FrenchieIdle");

        courseManager.feedbackBad.Show(4f);

        courseManager.PlayAudio(sit_8_audio, Sit_8_audio_callback);
    }

    public void Sit_8_audio_callback()
    {
        courseManager.stopwatch.ResetTimer();
        courseManager.stopwatch.Hide();

        if(timedTries>=3)
        {
            Play_lesson_over_audio();
        }
        else if(timedTries==1)
        {
            courseManager.PlayAudio(sit_9_audio, Sit_9_audio_callback);
        }
        else
        {
            courseManager.PlayAudio(sit_10_audio, Start_Timed_Sit);
        }        
    }

    public void Sit_9_audio_callback()
    {
        courseManager.PlayAudio(sit_10_audio, Start_Timed_Sit);
    }

}
