using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenuMusic : MonoBehaviour
{
    public AudioSource AudioSource; // grabbing player
    public AudioClip IntroMusic; // grabbing intro
    public AudioClip LoopMusic; // grabbing main song

    void Start()
    {
        StartCoroutine(PlayIntroThenLoop()); // for playing the short intro before looping the main song
    }

    private IEnumerator PlayIntroThenLoop()
    {
        AudioSource.clip = IntroMusic; // play intro
        AudioSource.loop = false; // don't loop
        AudioSource.Play(); // play

        yield return new WaitForSeconds(IntroMusic.length); // waitiing for intro to finish

        AudioSource.clip = LoopMusic; // play main loop
        AudioSource.loop = true; // loop
        AudioSource.Play(); // play
    }
}
