using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController Instance;

    public AudioSource source;
    public AudioClip bgMusic;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void StartBackgroundMusic()
    {
        source.clip = bgMusic;
        source.loop = true;
        source.Play();
    }
}
