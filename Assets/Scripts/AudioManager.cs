using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip Innesto;
    public AudioClip RotorMovement;
    public AudioClip TuboSemaforo;
    public AudioClip LoculoPieno;
    public AudioClip RotoreCompletato;

    public AudioClip MusicaMenu;
    public AudioClip StartNewLevel;
    public AudioClip LevelSelect;
    public AudioClip EndLevel;
    public List<AudioClip> MusicheSottofondo;

    public List<AudioClip> GUISounds;


    private AudioSource audioSource;


    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;

            Innesto.LoadAudioData();
            RotorMovement.LoadAudioData();
            TuboSemaforo.LoadAudioData();
            LoculoPieno.LoadAudioData();
            RotoreCompletato.LoadAudioData();
            LevelSelect.LoadAudioData();
            EndLevel.LoadAudioData();

            audioSource = this.GetComponent<AudioSource>();

        }

    }

    public void AudioFadeOut(float FadeTime)
    {
        FadeOut(FadeTime);
    }

    public void AudioFadeIn(float FadeTime)
    {
        FadeIn(FadeTime);
    }

    private IEnumerator FadeOut(float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    private IEnumerator FadeIn(float FadeTime)
    {
        float startVolume = 0.0f;

        while (audioSource.volume < 1)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
        audioSource.volume = 1.0f;
    }
}
