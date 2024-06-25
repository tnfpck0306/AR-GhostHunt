using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Text highScoreText; // 최고 점수 표시용 텍스트

    public GameObject collisonEffect; // 충돌시 효과 UI
    public GameObject gameoverUI; // 게임오버 시 활성화할 UI
    public GameObject mainMenuUI; // 메인메뉴 UI
    public GameObject selectSkillUI; // 스킬 선택창 UI

    public int[] skill = new int[10];

    public bool startMenu = true;

    public void Start()
    {
        for(int index = 0; index < skill.Length; index++)
        {
            skill[index] = index;
        }
    }

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

    // 스킬 선택창 활성화
    public void SetActiveSkillUI()
    {
        int[] skillIndex = new int[3]; // 3개의 스킬 선택지
        int[] sampleSkill = skill; // 
        
        selectSkillUI.SetActive(true);

        for(int index = 0; index < skillIndex.Length; index++)
        {
            int rand = Random.Range(0, sampleSkill.Length);
            skillIndex[index] = sampleSkill[rand];
            sampleSkill = sampleSkill.Where(val => val != skillIndex[index]).ToArray();

        }
    }

    // 충돌 효과의 코루틴
    public void CollisionEffect()
    {
        StartCoroutine(CollisonEffect_Coroutine());
    }

    // 충돌시 짧은 시간 UI 활성화
    public IEnumerator CollisonEffect_Coroutine()
    {
        collisonEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        collisonEffect.SetActive(false);

    }
    
    // 게임오버 UI 활성화
    public void SetActiveGameoverUI(bool active)
    {
        int highScore = GameManager.instance.Get_HighScore();
        print(highScore);
        highScoreText.text = "High Score : " + highScore;

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
