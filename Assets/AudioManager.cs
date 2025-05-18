using UnityEngine;

public class AudioManager : MonoBehaviour
{


    public static AudioManager instance;    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    public static object Instance { get; internal set; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        musicSource.loop = true; // Set the music source to loop
        PlayMusic("IntroSound");

    }


    public void PlayMusic(string name)
    {
        Sound sound = System.Array.Find(musicSounds, s => s.name == name);
        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }
    public void PlaySFX(string name)
    {
        Sound sound = System.Array.Find(sfxSounds, s => s.name == name);
        if (sound != null)
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause(); // Not Play(), so it continues from where it left off
    }

}
