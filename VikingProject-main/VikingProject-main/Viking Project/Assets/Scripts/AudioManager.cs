using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;

    void Awake()
    {
        if (musicSource == null)
            musicSource = GetComponent<AudioSource>();
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource != null)
            musicSource.volume = volume;
    }

    public float GetMusicVolume()
    {
        return musicSource != null ? musicSource.volume : 1f;
    }
}
