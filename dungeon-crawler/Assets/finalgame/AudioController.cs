using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public Slider volSlider; //volume slider (needs to be set manually)

    public AudioSource MusicSource;
    public AudioSource EffectsSource;
    // Start is called before the first frame update

    void Start()
    {
        MusicSource.volume = 1f;
        MusicSource.Play();
    }

    void Update()
    {
        //Uncomment when pause menu is implemented:
        //EffectsSource.volume = volSlider.value;
        //AudioSource.volume = volSlider.value;
    }

    public void PlaySound(AudioClip AudioClip, float volOffset)
    {
        EffectsSource.PlayOneShot(AudioClip, 1 + volOffset);
    }

    public void PlayMusic(AudioClip AudioClip, float volOffset)
    {
        MusicSource.clip = AudioClip;
        MusicSource.Play();
    }
}
