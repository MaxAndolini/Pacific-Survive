using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        foreach (var sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }

        Play("MainTheme");
    }

    public void Play(string audioName)
    {
        foreach (var sound in sounds)
            if (sound.name == audioName)
                sound.source.Play();
    }

    public void Stop(string audioName)
    {
        foreach (var sound in sounds)
            if (sound.name == audioName)
                sound.source.Stop();
    }

    public void Mute(bool status)
    {
        AudioListener.pause = status;
    }
}