using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� "Bullet" �±׸� ������ �ִ��� Ȯ��
        if (other.CompareTag("Bullet"))
        {
            // �� ������Ʈ(�ڱ� �ڽ�)�� ����
            Destroy(gameObject);

            // �Ѿ˵� ����
            Destroy(other.gameObject);
        }
    }
}
