using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Audios[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
    }

    public void PlayMusic(string name)
    {
        Audios audio = Array.Find(musicSounds, x => x.name == name);

        if (audio == null)
            Debug.Log("Audio source not found!");
        else
        {
            musicSource.clip = audio.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Audios audio = Array.Find(sfxSounds, x => x.name == name);

        if (audio == null)
            Debug.Log("Audio source not found!");
        else
        {
            sfxSource.PlayOneShot(audio.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}


