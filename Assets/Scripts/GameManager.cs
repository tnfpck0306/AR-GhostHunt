using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

// 점수(킬 수)와 게임오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }

            return m_instance;
        }
    }

    private static GameManager m_instance;
    public SetSkillManager setSkillManager;

    public int kill = 0; // 게임의 킬 수

    private bool isChoice = false; // 스킬 선택 여부
    public bool isGameOver { get; private set; } // 게임오버 여부

    string highScoreKey = "HighScore"; // PlayerPrefs에 사용할 최고기록 키

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 플레이어 캐릭터의 사망 이벤트 발생 시 게임오버
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    private void Update()
    {
        // 스킬 선택을 아직 안함 & 10킬 마다
        if(kill > 0 && kill % 10 == 0 && !isChoice )
        {
            // 스킬 선택창 UI 활성화
            setSkillManager.SetActiveSkillUI();
            isChoice = true;
        }

        // 스킬을 선택 & 1킬 후
        if (kill % 10 == 1 && isChoice)
        {
            isChoice = false;
        }
    }

    // 킬 수를 추가하고 UI 생신
    public void AddKill(int newKill)
    {
        if(!isGameOver)
        {
            kill += newKill;
            UIManager.instance.UpdateKillText(kill);
        }
    }

    // PlayerPrefs에서 최고 점수 불러오기
    public int Get_HighScore()
    {
        int highScore = PlayerPrefs.GetInt(highScoreKey);
        return highScore;
    }

    // PlayerPrefs에 최고 점수 갱신
    public void Set_HightScore(int cur_score)
    {
        if (cur_score > Get_HighScore())
        {
            PlayerPrefs.SetInt(highScoreKey, cur_score);
        }
    }

    // 게임오버 처리
    public void EndGame()
    {
        isGameOver = true;
        Set_HightScore(kill);

        UIManager.instance.SetActiveGameoverUI(true);
    }
}
