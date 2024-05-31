using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // 싱글턴 접근용 프로퍼티
    public static UIManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }

            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글턴이 할당될 변수

    public Text ammoText; // 탄알 표시용 텍스트
    public Text killText; // 킬 수 표시용 텍스트
    public GameObject gameoverUI; // 게임오버 시 활성화할 UI

    public GameObject mainMenuUI; // 메인메뉴 UI
    public bool startMenu = true;

    // 탄알 텍스트 갱신
    public void UpdateAmmoText(int magAmmo, int remainAmmo)
    {
        ammoText.text = magAmmo + " / " + remainAmmo;
    }

    // 킬 수 텍스트 갱신
    public void UpdateKillText(int newKill)
    {
        killText.text = "Kill : " + newKill;
    }
    
    // 게임오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    // 메인메뉴 UI 비활성화
    public void GameStart()
    {
        startMenu = false;
        mainMenuUI.SetActive(startMenu);
    }

    // 게임 재시작
    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
