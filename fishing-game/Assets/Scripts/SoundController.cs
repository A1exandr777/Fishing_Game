using System;
using System.Globalization;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambienceSource;
    
    public AudioClip bgMusic;

    public AudioClip owl;
    
    public AudioClip crickets;

    private float nextPlayTime;

    private bool isDay;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);

        Events.DayStarted += OnDayStart;
        Events.NightStarted += OnNightStart;
    }

    public void Update()
    {
        if (!isDay)
        {
            if (Time.time > nextPlayTime)
            {
                sfxSource.clip = owl;
                sfxSource.Play();

                nextPlayTime = Time.time + Random.Range(5f, 15f);
            }
        }
    }

    // public void StartBackgroundMusic()
    // {
    //     musicSource.clip = bgMusic;
    //     musicSource.loop = true;
    //     musicSource.Play();
    // }

    public void OnDayStart()
    {
        ambienceSource.Stop();
        
        musicSource.clip = bgMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
    
    public void OnNightStart()
    {
        musicSource.Stop();
        
        ambienceSource.clip = crickets;
        ambienceSource.Play();
    }

    public void UpdateAmbiance(bool daytime)
    {
        isDay = daytime;
    }
}
