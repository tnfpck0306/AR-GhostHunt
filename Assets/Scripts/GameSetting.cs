using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// ���� ����â (�����, ȿ���� ����)
public class GameSetting : MonoBehaviour
{
    public Slider[] bgmVolumeSlider; // BGM �Ҹ� ���� �����̴�
    public AudioSource bgmAudioSource; // BGM
    public Text[] bgmValueText; // BGM �ҷ� ǥ�� �ؽ�Ʈ

    public Slider[] effectVolumeSlider; // ȿ���� �Ҹ� ���� �����̴�
    public AudioSource effectAudioSource; // ȿ����
    public Text[] effectValueText; // ȿ���� �ҷ� ǥ�� �ؽ�Ʈ

    private void Awake()
    {
        for (int i = 0; i < bgmVolumeSlider.Length; i++)
        {
            // �Ҹ� �������� �����̴��� ǥ��
            bgmVolumeSlider[i].value = bgmAudioSource.volume;
            effectVolumeSlider[i].value = effectAudioSource.volume;

            // �����̴� ���� ���� �ؽ�Ʈ ǥ��
            bgmValueText[i].text = (Math.Truncate(bgmVolumeSlider[i].value * 100f)).ToString();
            effectValueText[i].text = (Math.Truncate(effectVolumeSlider[i].value * 100f)).ToString();
        }
    }

    public void VolumeValueChange()
    {
        for (int i = 0;i < bgmVolumeSlider.Length; i++)
        {
            // �Ҹ� �������� �����̴��� ǥ��
            bgmVolumeSlider[i].value = bgmAudioSource.volume;
            effectVolumeSlider[i].value = effectAudioSource.volume;

            // �����̴� ���� ���� �ؽ�Ʈ ǥ��
            bgmValueText[i].text = (Math.Truncate(bgmVolumeSlider[i].value * 100f)).ToString();
            effectValueText[i].text = (Math.Truncate(effectVolumeSlider[i].value * 100f)).ToString();
        }
    }
}
