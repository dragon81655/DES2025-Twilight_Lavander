using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioSource AudioSource; // grabbing audio player

    // Footsteps

    public AudioClip[] Footsteps; // grabbing footsteps
    public float FSDelay = 0.3f; // delay so footsteps dont all play at the same time
    public bool Walking; // checking if moving
    private Coroutine walkCoroutine;

    // Ambience

    public AudioClip[] Ambience; // grabbing ambience sounds
    public float AmbDelay = 30f; // delay between sounds
    public float AmbTimer = 0f; // timer for ambience sounds

    // Parasite

    public AudioClip[] Parasite; // grabbing parasite sounds
    public float ParasiteDelay = 20f; // delay between sounds
    public float ParasiteTimer = 0f; // timer for parasite sounds
    public bool IsParasite; // checking if parasite

    // Inventory

    public AudioClip Collection; // grabbing collection sfx
    public AudioClip Equip; // grabbing equip sfx
    public AudioClip Drop; // grabbing drop sfx

    // Heartbeat

    public AudioClip Heartbeat; // grabbing heartbeat swap sfx

    // Door

    public AudioClip DoorOpen; // grabbing door open sfx 
    public AudioClip DoorClose; // grabbing door close sfx

    // Minigame

    public AudioClip MinigameWin; // grabbing minigame win sfx
    public AudioClip MinigameLose; // grabbing minigame lose sfx

    // Other Scripts

    public GameStateManager GameStateManager; // grabbing GSM
    public HiveMindController HiveMindController; // grabbing HMC

    // Misc

    void Start()
    {
        if (HiveMindController.IsHuman)
        {
            IsParasite = false;
        }
        else
        {
            IsParasite = true;
        }
    }
    void Update()
    {
        float switchTimerValueA = GameStateManager.instance.GetCurrentSwitchTimer(); // getting switch timer
        if (switchTimerValueA > 16 && switchTimerValueA < 17)
        {
            HeartbeatSFX(); // call switch sfx player
        }

        // Ambience Code

        AmbTimer += Time.deltaTime;
        if (AmbTimer >= AmbDelay)
        {
            PlayRandomAmbience();
            AmbTimer = 0f;
        }

        // Parasite Code

        ParasiteTimer += Time.deltaTime;
        if (ParasiteTimer >= ParasiteDelay)
        {
            ParasiteTimer = 0f;
            if (IsParasite)
            PlayRandomParasiteAmbience();
            ParasiteTimer = 0f;
        }

        // Footstep Code 

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || // check for movement
            Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!Walking)
            {
                Walking = true;
                walkCoroutine = StartCoroutine(WalkSounds()); // start walk sounds and set walking to true
            }
        }
        else
        {
            if (Walking)
            {
                Walking = false;
                if (walkCoroutine != null)
                {
                    StopCoroutine(walkCoroutine); // if not moving set walking to false and stop sounds
                }
            }
        }
    }

    private IEnumerator WalkSounds() // walking sounds continuation
    {
        while (Walking)
        {
            if (Footsteps.Length > 0)
            {
                AudioClip clip = Footsteps[Random.Range(0, Footsteps.Length)];
                AudioSource.PlayOneShot(clip);
            }
            yield return new WaitForSeconds(FSDelay);
        }
    }

    private void PlayRandomAmbience() // ambient sounds continuation
    {
        if (Ambience.Length == 0 || AudioSource == null)
            return;

        AudioClip clip = Ambience[Random.Range(0, Ambience.Length)];
        AudioSource.clip = clip;
        AudioSource.Play();
    }

    private void PlayRandomParasiteAmbience() // parasite sounds continuation
    {
        if (Parasite.Length == 0 || AudioSource == null)
            return;

        AudioClip clip = Parasite[Random.Range(0, Parasite.Length)];
        AudioSource.clip = clip;
        AudioSource.Play(); 
    }    

    public void CollectionSFX() // function for item collection sfx
    {
        AudioClip clip = Collection;
        AudioSource.Play();
    }

    public void HeartbeatSFX() // function for heartbeat switch sfx
    {
        float switchTimerValueA = GameStateManager.instance.GetCurrentSwitchTimer();
        AudioSource.clip = Heartbeat;
        AudioSource.Play();
    }

    public void DoorOpenSFX() // function for door open sfx, , called through Door Manager
    {
        AudioSource.clip = DoorOpen;
        AudioSource.Play();
    }

    public void DoorCloseSFX() // function for door close sfx, called through Door Manager
    {
        AudioSource.clip = DoorClose;
        AudioSource.Play();
    }

    public void MiniWinSFX() // function for win sfx, called through SkillCheckMinigame
    {
        AudioSource.clip = MinigameWin;
        AudioSource.Play();
    }

    public void MiniLoseSFX() // function for Lose sfx, called through SkillCheckMinigame
    {
        AudioSource.clip = MinigameLose;
        AudioSource.Play();
    }

    public void DropSFX() // function for drop sfx
    {
        AudioSource.clip = Drop;
        AudioSource.Play();
    }

    public void EquipSFX() // function for equip sfx
    {
        AudioSource.clip = Equip;
        AudioSource.Play();
    }
}
