using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;   // 스폰할 오브젝트
    public BoxCollider spawnArea;      // 스폰 영역 (Box Collider 사용)

    public int spawnCount = 5;        // 스폰할 오브젝트 수

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        // Box Collider의 범위 내에서 랜덤한 위치를 계산
        Vector3 randomPosition = GetRandomPositionInCollider();

        // 오브젝트 생성
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPositionInCollider()
    {
        // 스폰 영역의 중심을 기준으로 Box Collider의 크기를 가져와 범위 내 랜덤 위치 계산
        Vector3 boundsMin = spawnArea.bounds.min;
        Vector3 boundsMax = spawnArea.bounds.max;

        // 각 축의 랜덤 값 계산
        float randomX = Random.Range(boundsMin.x, boundsMax.x);
        float randomY = Random.Range(boundsMin.y, boundsMax.y);
        float randomZ = Random.Range(boundsMin.z, boundsMax.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}
