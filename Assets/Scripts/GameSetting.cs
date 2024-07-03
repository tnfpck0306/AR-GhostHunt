using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// ���� ����â (�����, ȿ���� ����)
public class GameSetting : MonoBehaviour
{
    public Slider bgmVolumeSlider; // BGM �Ҹ� ���� �����̴�
    public AudioSource bgmAudioSource; // BGM
    public Text bgmValueText; // BGM �ҷ� ǥ�� �ؽ�Ʈ

    public Slider effectVolumeSlider; // ȿ���� �Ҹ� ���� �����̴�
    public AudioSource effectAudioSource; // ȿ����
    public Text effectValueText; // ȿ���� �ҷ� ǥ�� �ؽ�Ʈ

    private void Awake()
    {
        // �Ҹ� �������� �����̴��� ǥ��
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
