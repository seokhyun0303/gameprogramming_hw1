using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    // �浹�� �߻����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� "Bullet" �±׸� ������ �ִ��� Ȯ��
        if (other.CompareTag("obstacle"))
        {

            // �Ѿ˵� ����
            Destroy(gameObject);
        }
    }
}
