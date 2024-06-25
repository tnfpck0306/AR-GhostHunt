using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GamePaused : MonoBehaviour
{
    public static bool gameIsPaused = false; // ���� ���� ����
    public GameObject selectSkillUI; // ��ų ����â UI

    private void Update()
    {
        if (selectSkillUI.activeSelf != gameIsPaused)
            if(!gameIsPaused) // ��ų ����â�� �����ִµ� ������ �� �����ִٸ�
                Pause(); // ����
            
            else // ��ų ����â�� �������ִµ� ������ �����ִٸ�
                Resume(); // ����
    }

    private void Resume()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

}

