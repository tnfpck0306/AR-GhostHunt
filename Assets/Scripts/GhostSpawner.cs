using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// �ֱ������� ���� ���� �� ��� ��ȣ�ۿ�
public class GhostSpawner : MonoBehaviour
{
    public Ghost ghostPrefab; // ���� ������

    public GhostData ghostData; // ����� ���� ������
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

        if(ghosts.Count <= 1)
        {
            CreateGhost();
        }
    }

    private void CreateGhost()
    {
        // Spawn Points���� �������� ������ ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ���� ����
        Ghost ghost = Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);

        // ���� �ɷ�ġ �ο� �� ����Ʈ�� �߰�
        ghost.Setup(ghostData);
        ghosts.Add(ghost);

        // ���� ����� -> ����Ʈ���� ����, �����ı�, ų �� ���
        ghost.onDeath += () => ghosts.Remove(ghost);
        ghost.onDeath += () => Destroy(ghost.gameObject, 0.2f);
        ghost.onDeath += () => GameManager.instance.AddKill(1);
    }

}
