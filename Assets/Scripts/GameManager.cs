using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ���ӿ��� ���θ� �����ϴ� ���� �Ŵ���
public class GameManager : MonoBehaviour
{
    // �̱��� ���ٿ� ������Ƽ
    public static GameManager instance
    {
        get
        {
            // if �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if(m_instance == null)
            {
                // ������ GameManger ������Ʈ ã�Ƽ� �Ҵ�
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
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if(instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // �÷��̾� ĳ������ ��� �̺�Ʈ �߻� �� ���ӿ���
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
