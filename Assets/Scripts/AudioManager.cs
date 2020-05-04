using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    private float movementTimer = 0f;
    private bool playMovementLoop = false;
    private Sound movementSound;


    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake() {

        if (instance == null) instance = this;
        else 
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }


    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }

    public void PlayOneShot (string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return; 
        s.source.PlayOneShot(s.clip, s.volume);
    }

    public void PlayMovementLoop (string name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return; 

        movementTimer = s.source.clip.length;
        movementSound = s;
        playMovementLoop = true;

    }

    public void Stop (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Stop();
    }

    public void StopMovementLoop()
    {
        movementSound.source.Stop();
        playMovementLoop = false;
    }

    public bool IsAudioSourcePlaying (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return false;
        return s.source.isPlaying;
    }

    public void PlayButtonToggle() {
        Sound s = Array.Find(sounds, sound => sound.name == "ButtonPress");
        if (s == null) return; 
        s.source.PlayOneShot(s.clip, s.volume);
    }

    private void Update() {
        if (playMovementLoop)
        {
            if (movementTimer == movementSound.source.clip.length) {
            movementSound.source.PlayOneShot(movementSound.clip, movementSound.volume);
            }

            movementTimer -= 1f * Time.deltaTime;
            if (movementTimer <= 0f) movementTimer = movementSound.source.clip.length;
        }        
    }

}
    
