﻿using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    public static AudioMaster instance;

    [SerializeField, EventRef] private string testSFX;
    [SerializeField] private EventInstance SFXVolumeTestEvent;

    [Range(0,1)] public float masterVolume = 1f;
    [Range(0,1)] public float musicVolume = 1f;
    [Range(0,1)] public float sfxVolume = 1f;
    private float oldSfxVolume = 1f;

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;
    private const string masterVolumePrefString = "Master_Volume";
    private const string musicVolumePrefString = "Music_Volume";
    private const string sfxVolumePrefString = "SFX_Volume";

    //public AudioManager audioManager;
    
    private void Awake()
    {
        Singleton();
        // Create an instance of the sound to be played
        SFXVolumeTestEvent = RuntimeManager.CreateInstance(testSFX);
    }

    private void Start()
    {
//        if (audioManager == null)
//        {
//            Debug.Log("No Audio Manager Available");
//            return;
//        }
//        
//        audioManager.gameObject.SetActive(true);
//        audioManager.gameObject.SetActive(false);

        // Establish the busses to be loaded and managed
        masterBus = RuntimeManager.GetBus("bus:/Main");
        musicBus = RuntimeManager.GetBus("bus:/Main/Music");
        sfxBus = RuntimeManager.GetBus("bus:/Main/SFX");  
        
        // Check player prefs, if there is a value for each take it, if not make it
        if (PlayerPrefs.HasKey(masterVolumePrefString)) 
            masterVolume = PlayerPrefs.GetFloat(masterVolumePrefString);
        
        if (PlayerPrefs.HasKey(musicVolumePrefString))
            musicVolume = PlayerPrefs.GetFloat(musicVolumePrefString);
        
        if (PlayerPrefs.HasKey(sfxVolumePrefString))
            sfxVolume = PlayerPrefs.GetFloat(sfxVolumePrefString);

        // reset the oldValue before testing anything
        oldSfxVolume = sfxVolume;
    }

    private void Update()
    {
        // Listen for changes in the audio menu
        masterBus.setVolume(masterVolume);
        PlayerPrefs.SetFloat(masterVolumePrefString, masterVolume);
        
        musicBus.setVolume(musicVolume);
        PlayerPrefs.SetFloat(musicVolumePrefString, musicVolume);
        
        SetSfxVolume(sfxVolume);
        
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Escape))
        {
            //audioManager.gameObject.SetActive(!audioManager.isActiveAndEnabled);
        }
    }

    public void SetMasterVolume(float value)
    {
        masterVolume = value;
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = value;
    }
    
    public void SetSfxVolume(float value)
    {
        sfxVolume = value;
        
        sfxBus.setVolume(sfxVolume);
        PlayerPrefs.SetFloat(sfxVolumePrefString, sfxVolume);
        
        SFXVolumeLevel(sfxVolume);
    } 

    public void SFXVolumeLevel(float newSFXVolume)
    {
        if (oldSfxVolume == sfxVolume) return;
        
        // Play a sound when we test the SFX Volume
        PLAYBACK_STATE playbackState;
        SFXVolumeTestEvent.getPlaybackState(out playbackState);
        if (playbackState != PLAYBACK_STATE.PLAYING)
        {
            SFXVolumeTestEvent.start();
        }

        oldSfxVolume = sfxVolume;
    }
    
    private void Singleton()
    {
        if (instance == null)
        {
            instance = this;
            //@TODO do we want to not destory on load?
        }
        else if (instance != this)
        {
            Debug.Log("not the instance " + name);
            Destroy(gameObject);
        }
    }
}