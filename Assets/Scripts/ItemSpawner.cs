using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; // 아이템
    public Transform playerTransform; // 플레이어의 위치

    public float maxDistance = 3f; // 플레이어 기준 아이템 생성될 최대 거리
    private float distanceX; // X축 생성 거리
    private float distanceZ; // Z축 생성 거리

    public float timeBetSpawnMax = 10f; // 생성 최대 시간
    public float timeBetSpawnMin = 5f; // 생성 최소 시간
    private float timeInterval; // 생성 주기

    private float lastSpawnTime; // 마지막 생성 시간

    private void Start()
    {
        // 생성 간격
        timeInterval = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;

        // 생성 거리
        distanceX = Random.Range(-maxDistance, maxDistance);
        distanceZ = Random.Range(-maxDistance, maxDistance);
    }

    private void Update()
    {
        // 시간이 생성주기를 넘김 && 플레이어가 존재
        if(Time.time >= timeInterval + lastSpawnTime && playerTransform != null)
        {
            // 마지막 생성 시간 갱신
            lastSpawnTime = Time.time;
            timeInterval = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        // 생성 위치
        Vector3 spawnPosition = playerTransform.position;
        spawnPosition.x += distanceX;
        spawnPosition.y -= 1f;
        spawnPosition.z += distanceZ;

        // 아이템 선택 후 생성
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);


        // 생성 위치 갱신
        distanceX = Random.Range(-maxDistance, maxDistance);
        distanceZ = Random.Range(-maxDistance, maxDistance);

        // 시간 후 아이템 파괴
        Destroy(item, 5f);
    }
}
