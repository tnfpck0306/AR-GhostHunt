using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GamePaused : MonoBehaviour
{
    public static bool gameIsPaused = false; // 게임 멈춤 여부
    public GameObject selectSkillUI; // 스킬 선택창 UI

    private void Update()
    {
        if (selectSkillUI.activeSelf != gameIsPaused)
            if(!gameIsPaused) // 스킬 선택창이 켜져있는데 게임이 안 멈춰있다면
                Pause(); // 멈춤
            
            else // 스킬 선택창이 안켜져있는데 게임이 멈춰있다면
                Resume(); // 진행
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

