using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    public float moveSpeed = 500f; // ī�޶� �̵� �ӵ� (10�� ����)
    public float verticalMoveSpeed = 20f; // ���Ʒ� �̵� �ӵ�
    public float lookSpeed = 4f; // ���콺 ȸ�� �ӵ�
    public float verticalLookLimit = 80f; // ���� ȸ�� ���� ����

    private float rotationX = 0f; // ���� ȸ�� �� (���콺 Y��)
    private float rotationY = 0f; // �¿� ȸ�� �� (���콺 X��)

    void Update()
    {
        MoveCamera();

        // ���콺 ��Ŭ�� ���� ���� ȸ��
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // 0�� ���콺 ��Ŭ���� �ǹ�
        {
            RotateCamera();
        }
        MoveCameraUpDown();
    }

    // ī�޶� �̵� �Լ�
    void MoveCamera()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed; // A, D �Ǵ� ����/������ ����Ű
        float moveZ = Input.GetAxis("Vertical") * moveSpeed; // W, S �Ǵ� ��/�Ʒ� ����Ű

        // ī�޶��� ���� �� �¿�� �̵�
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;
    }

    // ī�޶� ȸ�� �Լ� (��Ŭ�� ���� ���� ȣ���)
    void RotateCamera()
    {
        // ���콺 �Է� �� �޾ƿ���
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed; // �¿� ȸ�� (���콺 X��)
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed; // ���� ȸ�� (���콺 Y��)

        // ���� ȸ�� �� ���� �� ����
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit); // ���� ȸ�� ������ ����

        // �¿� ȸ�� �� ����
        rotationY += mouseX;

        // ȸ���� ����
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    void MoveCameraUpDown()
    {
        if (Input.GetKey(KeyCode.Q)) // Q Ű�� ���� �̵�
        {
            transform.position += Vector3.up * verticalMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E)) // E Ű�� �Ʒ��� �̵�
        {
            transform.position += Vector3.down * verticalMoveSpeed * Time.deltaTime;
        }
    }
}
