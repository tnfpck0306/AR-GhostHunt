using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 주기적으로 유령 생성 및 사망 상호작용
public class GhostSpawner : MonoBehaviour
{
    public Ghost ghostPrefab; // 유령 프리팹

    public GhostData ghostData; // 사용할 유령 데이터
    public Transform[] spawnPoints; // 유령을 소환할 위치

    private List<Ghost> ghosts = new List<Ghost>(); // 생성된 유령을 담을 리스트

    private void Update()
    {
        // 메인메뉴 화면에서는 생성하지 않음
        if (UIManager.instance.startMenu)
        {
            return;
        }

        // 게임오버 상태에서는 생성하지 않음
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
        // Spawn Points에서 랜덤으로 유령을 생성
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 유령 생성
        Ghost ghost = Instantiate(ghostPrefab, spawnPoint.position, spawnPoint.rotation);

        // 유령 능력치 부여 및 리스트에 추가
        ghost.Setup(ghostData);
        ghosts.Add(ghost);

        // 유령 사망시 -> 리스트에서 제거, 유령파괴, 킬 수 상승
        ghost.onDeath += () => ghosts.Remove(ghost);
        ghost.onDeath += () => Destroy(ghost.gameObject, 0.2f);
        ghost.onDeath += () => GameManager.instance.AddKill(1);
    }

}
