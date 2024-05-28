using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수와 게임오버 여부를 관리하는 게임 매니저
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

    private int kill = 0;
    public bool isGameOver { get; private set; }

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

    // 킬 수를 추가하고 UI 생신
    public void AddKill(int newKill)
    {
        if(!isGameOver)
        {
            kill += newKill;
            UIManager.instance.UpdateKillText(kill);
        }
    }

    // 게임오버 처리
    public void EndGame()
    {
        isGameOver = true;

        UIManager.instance.SetActiveGameoverUI(true);
    }
}
