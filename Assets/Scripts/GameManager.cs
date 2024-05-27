using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 점수와 게임오버 여부를 관리하는 게임 매니저
public class GameManager : MonoBehaviour
{
    // 싱글턴 접근용 프로퍼티
    public static GameManager instance
    {
        get
        {
            // if 싱글턴 변수에 아직 오브젝트가 할당되지 않았다면
            if(m_instance == null)
            {
                // 씬에서 GameManger 오브젝트 찾아서 할당
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
        // 씬에 싱글턴 오브젝트가 된 다른 GameManager 오브젝트가 있다면
        if(instance != this)
        {
            // 자신을 파괴
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 플레이어 캐릭터의 사망 이벤트 발생 시 게임오버
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    public void AddKill(int newKill)
    {
        if(!isGameOver)
        {
            kill += newKill;
            UIManager.instance.UpdateKillText(kill);
        }
    }

    public void EndGame()
    {
        isGameOver = true;

        UIManager.instance.SetActiveGameoverUI(true);
    }
}
