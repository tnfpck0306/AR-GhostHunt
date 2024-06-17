using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// ������ ���ӿ��� ���θ� �����ϴ� ���� �Ŵ���
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

    public int kill = 0; // ������ ų ��
    public bool isGameOver { get; private set; } // ���ӿ��� ����

    string highScoreKey = "HighScore"; // PlayerPrefs�� ����� �ְ��� Ű

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // �÷��̾� ĳ������ ��� �̺�Ʈ �߻� �� ���ӿ���
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    // ų ���� �߰��ϰ� UI ����
    public void AddKill(int newKill)
    {
        if(!isGameOver)
        {
            kill += newKill;
            UIManager.instance.UpdateKillText(kill);
        }
    }

    // PlayerPrefs���� �ְ� ���� �ҷ�����
    public int Get_HighScore()
    {
        int highScore = PlayerPrefs.GetInt(highScoreKey);
        return highScore;
    }

    // PlayerPrefs�� �ְ� ���� ����
    public void Set_HightScore(int cur_score)
    {
        if (cur_score > Get_HighScore())
        {
            PlayerPrefs.SetInt(highScoreKey, cur_score);
        }
    }

    // ���ӿ��� ó��
    public void EndGame()
    {
        isGameOver = true;
        Set_HightScore(kill);

        UIManager.instance.SetActiveGameoverUI(true);
    }
}
