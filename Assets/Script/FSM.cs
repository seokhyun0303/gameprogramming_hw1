using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FSM: MonoBehaviour
{
    public GameObject player; // �÷��̾� Ÿ��
    public float speed = 5f; // �� �̵� �ӵ�
    private float speedIncreaseRate = 0.5f; // �ʴ� �ӵ� ������
    private float maxSpeed = 15f;
    private Vector3 avoidDirection;

    private enum State { ChasingPlayer, AvoidingObstacle }
    private State currentState = State.ChasingPlayer; // �ʱ� ���´� �÷��̾� ����

    private float avoidTime = 1f; // ȸ���ϴ� �ð�
    private float avoidTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("�÷��̾� ������Ʈ�� ã�� �� �����ϴ�. 'Player' �±׸� Ȯ���ϼ���.");
            }
        }
    }

    void Update()
    {
        if (player == null)
            return;

        // ���� ���¿� ���� ���� �ൿ ó��
        switch (currentState)
        {
            case State.ChasingPlayer:
                ChasePlayer();
                break;
            case State.AvoidingObstacle:
                AvoidObstacle();
                break;
        }

        // �ӵ� ���������� ���� (�ִ� �ӵ� ����)
        speed = Mathf.Min(speed + speedIncreaseRate * Time.deltaTime, maxSpeed);
    }

    // ���� ���� �޼���
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

    // �÷��̾� ���� ���� ����
    private void SetChasePlayerState()
    {
        
    }

    private void ChasePlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // �ε巴�� ȸ��
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }

    // ��ֹ� ȸ�� ���� ����
    private void SetAvoidObstacleState()
    {
        avoidTimer = avoidTime; // ȸ�� �ð� �ʱ�ȭ
        float randomValue = Random.value;
        float rotationAngle = randomValue < 0.5f ? 90f : -90f; // ���� �Ǵ� ������ ȸ��

        // ȸ�� ���� ����
        transform.Rotate(0, rotationAngle, 0); // ���� ȸ�� ������ ����
        avoidDirection = transform.forward; // ȸ�� ���� ���ο� �̵� ���� ����
    }

    // ��ֹ� ȸ�� ���� �ൿ
    private void AvoidObstacle()
    {
        avoidTimer -= Time.deltaTime;
        transform.Translate(avoidDirection * speed * Time.deltaTime, Space.World); // ���ο� ȸ�� �������� �̵�

        
        if (avoidTimer <= 0)
        {
            ChangeState(State.ChasingPlayer);
        }
    }

    // ��ֹ��� �浹���� �� ���� ����
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

