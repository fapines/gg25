using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;
    
    [Header("Audio Clips")]
    public AudioClip menuAudioClip;
    public AudioClip gameAudioClip;
    public AudioClip tapButtonAudioClip;

    private bool isSoundMute;

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

    public void SetSoundClip(AudioClip clip)
    {
        soundSource.PlayOneShot(clip);
    }
    public void ToggleSound(bool isMute)
    {
        soundSource.mute = isMute;
    }
    
    public void SetMusicClip(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void ToggleMusic(bool isMute)
    {
        musicSource.mute = isMute;
    }
}
