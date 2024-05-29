using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] items; // ������
    public Transform playerTransform; // �÷��̾��� ��ġ

    public float maxDistance = 3f; // �÷��̾� ���� ������ ������ �ִ� �Ÿ�
    private float distanceX; // X�� ���� �Ÿ�
    private float distanceZ; // Z�� ���� �Ÿ�

    public float timeBetSpawnMax = 10f; // ���� �ִ� �ð�
    public float timeBetSpawnMin = 5f; // ���� �ּ� �ð�
    private float timeInterval; // ���� �ֱ�

    private float lastSpawnTime; // ������ ���� �ð�

    private void Start()
    {
        // ���� ����
        timeInterval = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
        lastSpawnTime = 0;

        // ���� �Ÿ�
        distanceX = Random.Range(-maxDistance, maxDistance);
        distanceZ = Random.Range(-maxDistance, maxDistance);
    }

    private void Update()
    {
        // �ð��� �����ֱ⸦ �ѱ� && �÷��̾ ����
        if(Time.time >= timeInterval + lastSpawnTime && playerTransform != null)
        {
            // ������ ���� �ð� ����
            lastSpawnTime = Time.time;
            timeInterval = Random.Range(timeBetSpawnMin, timeBetSpawnMax);
            Spawn();
        }
    }

    private void Spawn()
    {
        // ���� ��ġ
        Vector3 spawnPosition = playerTransform.position;
        spawnPosition.x += distanceX;
        spawnPosition.y -= 1f;
        spawnPosition.z += distanceZ;

        // ������ ���� �� ����
        GameObject selectedItem = items[Random.Range(0, items.Length)];
        GameObject item = Instantiate(selectedItem, spawnPosition, Quaternion.identity);


        // ���� ��ġ ����
        distanceX = Random.Range(-maxDistance, maxDistance);
        distanceZ = Random.Range(-maxDistance, maxDistance);

        // �ð� �� ������ �ı�
        Destroy(item, 5f);
    }
}
