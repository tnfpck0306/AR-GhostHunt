using System.Collections;
using System.Collections.Generic;
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

    // ���ӿ��� ó��
    public void EndGame()
    {
        isGameOver = true;

        UIManager.instance.SetActiveGameoverUI(true);
    }
}
