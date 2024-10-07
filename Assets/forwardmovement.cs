using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardmovement : MonoBehaviour
{
    public float speed;
    public float lifetime = 5f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� ��("Target") �±׸� ������ �ִ��� Ȯ��
        if (other.CompareTag("Target"))
        {
            // ���� �浹���� �� �Ѿ� ����
            Destroy(gameObject);
        }
    }
}
