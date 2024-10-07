using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public BoxCollider moveArea;      // 움직일 수 있는 영역 (BoxCollider)
    public float speed = 4f;          // 적의 이동 속도
    private Vector3 moveDirection;    // 현재 이동 방향
    private float moveDuration;       // 이동할 시간
    private float moveStartTime;      // 이동이 시작된 시간

    void Start()
    {
        GameObject areaObject = GameObject.Find("spawnarea");
        if (areaObject != null)
        {
            moveArea = areaObject.GetComponent<BoxCollider>();
        }
        SetRandomDirectionAndDuration(); // 초기 이동 방향 및 시간 설정
    }

    void Update()
    {
        MoveEnemy(); // 적 이동
    }

    void MoveEnemy()
    {
        // 이동 시간이 끝났으면 새로운 방향 설정
        if (Time.time - moveStartTime >= moveDuration)
        {
            SetRandomDirectionAndDuration();
        }

        // 적이 이동 영역을 벗어날 경우 방향을 다시 설정
        if (!IsWithinMoveArea())
        {
            SetRandomDirectionAndDuration();
        }

        // 현재 방향으로 적 이동
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // 랜덤한 방향(90도 단위) 및 이동 시간을 설정하는 함수
    void SetRandomDirectionAndDuration()
    {
        // 랜덤한 90도 단위 회전 선택 (0도, 90도, 180도, 270도)
        int randomAngle = Random.Range(0, 4) * 90;

        // 이동 방향 설정 (randomAngle을 기준으로 회전된 방향)
        moveDirection = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        // 적의 이동 방향에 따라 바라보는 방향 설정
        transform.rotation = Quaternion.LookRotation(moveDirection);

        // 이동 시간 설정 (1초에서 3초 사이)
        moveDuration = Random.Range(1f, 3f);
        moveStartTime = Time.time;
    }

    // Collider 내부에 있는지 확인하는 함수
    bool IsWithinMoveArea()
    {
        Vector3 enemyPosition = transform.position;
        Bounds areaBounds = moveArea.bounds;

        // 현재 위치가 Collider 범위 내에 있는지 확인
        if (enemyPosition.x < areaBounds.min.x || enemyPosition.x > areaBounds.max.x ||
            enemyPosition.z < areaBounds.min.z || enemyPosition.z > areaBounds.max.z)
        {
            return false;
        }

        return true;
    }
}
