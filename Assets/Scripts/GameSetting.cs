using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// 게임 설정창 (배경음, 효과음 제어)
public class GameSetting : MonoBehaviour
{
    public Slider bgmVolumeSlider; // BGM 소리 조절 슬라이더
    public AudioSource bgmAudioSource; // BGM
    public Text bgmValueText; // BGM 불륨 표시 텍스트

    public Slider effectVolumeSlider; // 효과음 소리 조절 슬라이더
    public AudioSource effectAudioSource; // 효과음
    public Text effectValueText; // 효과음 불륨 표시 텍스트

    private void Awake()
    {
        // 소리 볼륨값을 슬라이더에 표시
        bgmVolumeSlider.value = bgmAudioSource.volume;
        effectVolumeSlider.value = effectAudioSource.volume;

        bgmValueText.text = (Math.Truncate(bgmVolumeSlider.value * 100f)).ToString();
        effectValueText.text = (Math.Truncate(effectVolumeSlider.value * 100f)).ToString();
    }

    public void VolumeValueChange()
    {
        bgmValueText.text = (Math.Truncate(bgmVolumeSlider.value * 100f)).ToString();
        effectValueText.text = (Math.Truncate(effectVolumeSlider.value * 100f)).ToString();
    }
}
