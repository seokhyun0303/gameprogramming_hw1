using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooting : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject shoot;

    public float redfireDelay = 0.1f;
    public float bluefireDelay = 1f;  // �Ѿ� �߻� ������ (���յ� ������)

    private float redlastFireTime = 0f; // ������ �Ѿ� �߻� �ð�
    private float bluelastFireTime = 0f; 

    void Update()
    {
        if (redlastFireTime > redfireDelay && Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(red, shoot.transform.position, shoot.transform.rotation);

            // ������ �߻� �ð��� ������Ʈ
            redlastFireTime = 0;
        }

        // ���콺 ������ ��ư���� Blue bullet �߻�
        if (bluelastFireTime > bluefireDelay && Input.GetKey(KeyCode.Mouse1))
        {
            // �������� Blue bullet �߻�
            Instantiate(blue, shoot.transform.position, shoot.transform.rotation);

            // ���������� 15�� ȸ���� �������� Blue bullet �߻�
            Quaternion rightRotation = Quaternion.Euler(0, 15, 0) * shoot.transform.rotation;
            Instantiate(blue, shoot.transform.position, rightRotation);

            // �������� 15�� ȸ���� �������� Blue bullet �߻�
            Quaternion leftRotation = Quaternion.Euler(0, -15, 0) * shoot.transform.rotation;
            Instantiate(blue, shoot.transform.position, leftRotation);

            // ������ �߻� �ð��� ������Ʈ
            bluelastFireTime = 0;
        }
        redlastFireTime += Time.deltaTime;
        bluelastFireTime += Time.deltaTime;
    }
}
