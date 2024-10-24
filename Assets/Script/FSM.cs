using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FSM: MonoBehaviour
{
    public GameObject player; // 플레이어 타겟
    public float speed = 5f; // 적 이동 속도
    private float speedIncreaseRate = 0.5f; // 초당 속도 증가율
    private float maxSpeed = 15f;
    private Vector3 avoidDirection;

    private enum State { ChasingPlayer, AvoidingObstacle }
    private State currentState = State.ChasingPlayer; // 초기 상태는 플레이어 추적

    private float avoidTime = 1f; // 회피하는 시간
    private float avoidTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("플레이어 오브젝트를 찾을 수 없습니다. 'Player' 태그를 확인하세요.");
            }
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // 현재 상태에 따라 적의 행동 처리
        switch (currentState)
        {
            case State.ChasingPlayer:
                ChasePlayer();
                break;
            case State.AvoidingObstacle:
                AvoidObstacle();
                break;
        }

        // 속도 점진적으로 증가 (최대 속도 제한)
        speed = Mathf.Min(speed + speedIncreaseRate * Time.deltaTime, maxSpeed);
    }

    // 상태 변경 메서드
    private void ChangeState(State newState)
    {
        currentState = newState;

        switch (newState)
        {
            case State.ChasingPlayer:
                SetChasePlayerState();
                break;
            case State.AvoidingObstacle:
                SetAvoidObstacleState();
                break;
        }
    }

    // 플레이어 추적 상태 설정
    private void SetChasePlayerState()
    {
        
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // 부드럽게 회전
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }

    // 장애물 회피 상태 설정
    private void SetAvoidObstacleState()
    {
        avoidTimer = avoidTime; // 회피 시간 초기화
        float randomValue = Random.value;
        float rotationAngle = randomValue < 0.5f ? 90f : -90f; // 왼쪽 또는 오른쪽 회전

        // 회피 방향 설정
        transform.Rotate(0, rotationAngle, 0); // 적의 회전 각도를 변경
        avoidDirection = transform.forward; // 회전 후의 새로운 이동 방향 저장
    }

    // 장애물 회피 상태 행동
    private void AvoidObstacle()
    {
        avoidTimer -= Time.deltaTime;
        transform.Translate(avoidDirection * speed * Time.deltaTime, Space.World); // 새로운 회피 방향으로 이동

        
        if (avoidTimer <= 0)
        {
            ChangeState(State.ChasingPlayer);
        }
    }

    // 장애물과 충돌했을 때 상태 변경
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obstacle") && currentState == State.ChasingPlayer)
        {
            ChangeState(State.AvoidingObstacle);
        }
        else if (other.CompareTag("energy") && currentState == State.ChasingPlayer)
        {
            ChangeState(State.AvoidingObstacle);
        }
    }
}

