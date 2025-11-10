using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Music")]
    public AudioClip backgroundMusic;
    [Range(0,1)] public float musicVolume = 0.6f;

    [Header("SFX")]
    public AudioClip bounceSfx;
    public AudioClip winClapSfx;
    public AudioClip loseSfx;
    [Range(0,1)] public float sfxVolume = 1f;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.playOnAwake = false;
            musicSource.volume = musicVolume;

            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.playOnAwake = false;
            sfxSource.volume = sfxVolume;

            if (backgroundMusic != null)
                musicSource.Play();
        }
        else Destroy(gameObject);
    }

    public void PlayBounce()
    {
        if (bounceSfx != null) sfxSource.PlayOneShot(bounceSfx, sfxVolume);
    }

    public void PlayWin()
    {
        if (winClapSfx != null) sfxSource.PlayOneShot(winClapSfx, sfxVolume);
    }

    public void PlayLose()
    {
        if (loseSfx != null) sfxSource.PlayOneShot(loseSfx, sfxVolume);
    }

    public void PlaySfx(AudioClip clip)
    {
        if (clip != null) sfxSource.PlayOneShot(clip, sfxVolume);
    }
}