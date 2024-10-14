using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public BoxCollider moveArea;      // ������ �� �ִ� ���� (BoxCollider)
    public float speed = 4f;          // ���� �̵� �ӵ�
    private Vector3 moveDirection;    // ���� �̵� ����
    private float moveDuration;       // �̵��� �ð�
    private float moveStartTime;      // �̵��� ���۵� �ð�

    void Start()
    {
        GameObject areaObject = GameObject.Find("spawnarea");
        if (areaObject != null)
        {
            moveArea = areaObject.GetComponent<BoxCollider>();
        }
        SetRandomDirectionAndDuration(); // �ʱ� �̵� ���� �� �ð� ����
    }

    void Update()
    {
        MoveEnemy(); // �� �̵�
    }

    void MoveEnemy()
    {
        // �̵� �ð��� �������� ���ο� ���� ����
        if (Time.time - moveStartTime >= moveDuration)
        {
            SetRandomDirectionAndDuration();
        }

        // ���� �̵� ������ ��� ��� ������ �ٽ� ����
        if (!IsWithinMoveArea())
        {
            SetRandomDirectionAndDuration();
        }

        // ���� �������� �� �̵�
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // ������ ����(90�� ����) �� �̵� �ð��� �����ϴ� �Լ�
    void SetRandomDirectionAndDuration()
    {
        // ������ 90�� ���� ȸ�� ���� (0��, 90��, 180��, 270��)
        int randomAngle = Random.Range(0, 4) * 90;

        // �̵� ���� ���� (randomAngle�� �������� ȸ���� ����)
        moveDirection = new Vector3(Mathf.Cos(randomAngle * Mathf.Deg2Rad), 0, Mathf.Sin(randomAngle * Mathf.Deg2Rad));

        // ���� �̵� ���⿡ ���� �ٶ󺸴� ���� ����
        transform.rotation = Quaternion.LookRotation(moveDirection);

        // �̵� �ð� ���� (1�ʿ��� 3�� ����)
        moveDuration = Random.Range(1f, 3f);
        moveStartTime = Time.time;
    }

    // Collider ���ο� �ִ��� Ȯ���ϴ� �Լ�
    bool IsWithinMoveArea()
    {
        Vector3 enemyPosition = transform.position;
        Bounds areaBounds = moveArea.bounds;

        // ���� ��ġ�� Collider ���� ���� �ִ��� Ȯ��
        if (enemyPosition.x < areaBounds.min.x || enemyPosition.x > areaBounds.max.x ||
            enemyPosition.z < areaBounds.min.z || enemyPosition.z > areaBounds.max.z)
        {
            return false;
        }

        return true;
    }
}
