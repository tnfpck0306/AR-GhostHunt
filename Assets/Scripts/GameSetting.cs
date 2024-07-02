using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public Slider bgmVolumeSlider;
    public AudioSource bgmAudioSource;

    void Awake()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        bgmVolumeSlider.value = bgmAudioSource.volume;
    }

}
