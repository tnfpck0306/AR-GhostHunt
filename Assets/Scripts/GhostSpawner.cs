using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// �ֱ������� ���� ���� �� ��� ��ȣ�ۿ�
public class GhostSpawner : MonoBehaviour
{
    // ���� ������ & ���� ������
    // 0. �Ϲ� ���� 1. ���� ����
    public Ghost[] ghostPrefab;
    public GhostData[] ghostData;

    public Transform[] spawnPoints; // ������ ��ȯ�� ��ġ

    private List<Ghost> ghosts = new List<Ghost>(); // ������ ������ ���� ����Ʈ

    private void Update()
    {
        // ���θ޴� ȭ�鿡���� �������� ����
        if (UIManager.instance.startMenu)
        {
            return;
        }

        // ���ӿ��� ���¿����� �������� ����
        if (GameManager.instance != null && GameManager.instance.isGameOver)
        {
            return;
        }

        /*
         * ���� ������
         * �⺻������ ����(ü��100, ���ݷ� 20, �ӵ� 0.5)�� �Ѹ����� ����
         * 5ų���� ���� ����(ü��150, ���ݷ� 40, �ӵ� 0.3) �߰� ����
         * 10ų�� �������� ������ �Ѹ����� �߰� ���� [10-> 2����, 20-> 3����]
         * 20ų ���� 3ų���� ������(ü�� 50, ���ݷ� 20, �ӵ� 0.5, ��ũ ȿ��) ����
        */
        int killCount = GameManager.instance.kill; // ų ��

        if (ghosts.Count < (killCount / 10) + 1)
        {
            CreateGhost(0);

            if (killCount > 0 && killCount % 5 == 0)
            {
                CreateGhost(1);
            }

            if(killCount > 20 && (killCount - 20) % 3 == 0)
            {
                CreateGhost(2);
            }
        }
    }

    private void CreateGhost(int num)
    {
        // Spawn Points���� �������� ������ ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ���� ����
        Ghost ghost = Instantiate(ghostPrefab[num], spawnPoint.position, spawnPoint.rotation);

        // ���� �ɷ�ġ �ο� �� ����Ʈ�� �߰�
        ghost.Setup(ghostData[num]);
        ghosts.Add(ghost);

        // ���� ����� -> ����Ʈ���� ����, �����ı�, ų �� ���
        ghost.onDeath += () => ghosts.Remove(ghost);
        ghost.onDeath += () => Destroy(ghost.gameObject, 0.2f);
        ghost.onDeath += () => GameManager.instance.AddKill(1);

        // ������ �÷��̾�� �浹�� -> ����Ʈ���� ����, �����ı�, �浹ȿ��
        ghost.onCollison += () => ghosts.Remove(ghost);
        ghost.onCollison += () => Destroy(ghost.gameObject);
        ghost.onCollison += () => UIManager.instance.CollisionEffect();
    }

}
