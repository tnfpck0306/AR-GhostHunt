using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class GamePaused : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject selectSkillUI;

    private void Update()
    {
        if (selectSkillUI.activeSelf != gameIsPaused)
            if(!gameIsPaused)
                Pause();
            else
                Resume();
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

