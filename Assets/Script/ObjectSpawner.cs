using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;   // ������ ������Ʈ
    public BoxCollider spawnArea;      // ���� ���� (Box Collider ���)

    public int spawnCount = 5;        // ������ ������Ʈ ��
    public float spawnDelay = 2.5f;     // ���� ���� �ð� (��)

    void Start()
    {
        // 4�� �� ������ �����ϴ� �ڷ�ƾ ȣ��
        StartCoroutine(SpawnAfterDelay());
    }

    IEnumerator SpawnAfterDelay()
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(spawnDelay);

        // ������ ����ŭ ������Ʈ�� ����
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnRandomObject();
        }
    }

    void SpawnRandomObject()
    {
        // Box Collider�� ���� ������ ������ ��ġ�� ���
        Vector3 randomPosition = GetRandomPositionInCollider();

        // ������Ʈ ����
        Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
    }

    Vector3 GetRandomPositionInCollider()
    {
        // ���� ������ �߽��� �������� Box Collider�� ũ�⸦ ������ ���� �� ���� ��ġ ���
        Vector3 boundsMin = spawnArea.bounds.min;
        Vector3 boundsMax = spawnArea.bounds.max;

        // �� ���� ���� �� ���
        float randomX = Random.Range(boundsMin.x, boundsMax.x);
        float randomY = Random.Range(boundsMin.y, boundsMax.y);
        float randomZ = Random.Range(boundsMin.z, boundsMax.z);

        return new Vector3(randomX, randomY, randomZ);
    }
}

