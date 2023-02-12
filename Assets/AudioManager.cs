using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public static AudioManager instance;


    // Start is called before the first frame update
    void Awake() {
        // singleton pattern
        // there's only one audio manager and it lives between scenes
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);



        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start() {
        Play("Theme");
    }

    // if you want to play a sound from another script, call this function
    // pass in the name as it's defined on the audio manager script
    // ex: FindObjectOfType<AudioManager>().Play("name");
    public void Play(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " does not exist.");
            return;
        }
        s.source.Play();
    }
}
