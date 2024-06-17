using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 주기적으로 유령 생성 및 사망 상호작용
public class GhostSpawner : MonoBehaviour
{
    // 유령 프리팹 & 유령 데이터
    // 0. 일반 유령 1. 갈색 유령
    public Ghost[] ghostPrefab;
    public GhostData[] ghostData;

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

        /*
         * 레벨 디자인
         * 기본적으로 유령(체력100, 공격력 20, 속도 0.5)이 한마리씩 등장
         * 5킬마다 갈색 유령(체력150, 공격력 40, 속도 0.3) 추가 등장
         * 10킬을 기준으로 유령이 한마리씩 추가 등장 [10-> 2마리, 20-> 3마리]
        */
        if (ghosts.Count < (GameManager.instance.kill / 10) + 1)
        {
            CreateGhost(0);

            if (GameManager.instance.kill % 5 == 0 && GameManager.instance.kill != 0)
            {
                CreateGhost(1);
            }
        }
    }

    private void CreateGhost(int num)
    {
        // Spawn Points에서 랜덤으로 유령을 생성
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 유령 생성
        Ghost ghost = Instantiate(ghostPrefab[num], spawnPoint.position, spawnPoint.rotation);

        // 유령 능력치 부여 및 리스트에 추가
        ghost.Setup(ghostData[num]);
        ghosts.Add(ghost);

        // 유령 사망시 -> 리스트에서 제거, 유령파괴, 킬 수 상승
        ghost.onDeath += () => ghosts.Remove(ghost);
        ghost.onDeath += () => Destroy(ghost.gameObject, 0.2f);
        ghost.onDeath += () => GameManager.instance.AddKill(1);

        // 유령이 플레이어와 충돌시 -> 리스트에서 제거, 유령파괴, 충돌효과
        ghost.onCollison += () => ghosts.Remove(ghost);
        ghost.onCollison += () => Destroy(ghost.gameObject);
        ghost.onCollison += () => UIManager.instance.CollisionEffect();
    }

}
