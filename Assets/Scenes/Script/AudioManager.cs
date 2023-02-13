using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public GameObject[] audioObj;
    public Sound[] sounds;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }    
    }

    private void Start() {
        audioObj = GameObject.FindGameObjectsWithTag("Audio");

        if(audioObj.Length > 1){
            Destroy(this.gameObject);
        }

        InvokeRepeating("PlayBGM",1f,187f);
    }

    public void PlayBGM(){
        Play("BGM");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void ChangeVolume(string name, float value){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.volume = value;
    }
}