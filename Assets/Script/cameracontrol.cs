using UnityEngine;

public class cameracontrol : MonoBehaviour
{
    public float moveSpeed = 500f; // 카메라 이동 속도 (10배 증가)
    public float verticalMoveSpeed = 20f; // 위아래 이동 속도
    public float lookSpeed = 4f; // 마우스 회전 속도
    public float verticalLookLimit = 80f; // 상하 회전 제한 각도

    private float rotationX = 0f; // 상하 회전 값 (마우스 Y축)
    private float rotationY = 0f; // 좌우 회전 값 (마우스 X축)

    void Update()
    {
        MoveCamera();

        // 마우스 좌클릭 중일 때만 회전
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // 0은 마우스 좌클릭을 의미
        {
            RotateCamera();
        }
        MoveCameraUpDown();
    }

    // 카메라 이동 함수
    void MoveCamera()
    {
        float moveX = Input.GetAxis("Horizontal") * moveSpeed; // A, D 또는 왼쪽/오른쪽 방향키
        float moveZ = Input.GetAxis("Vertical") * moveSpeed; // W, S 또는 위/아래 방향키

        // 카메라의 전방 및 좌우로 이동
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        transform.position += move;
    }

    // 카메라 회전 함수 (좌클릭 중일 때만 호출됨)
    void RotateCamera()
    {
        // 마우스 입력 값 받아오기
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed; // 좌우 회전 (마우스 X축)
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed; // 상하 회전 (마우스 Y축)

        // 상하 회전 값 갱신 및 제한
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -verticalLookLimit, verticalLookLimit); // 상하 회전 각도를 제한

        // 좌우 회전 값 갱신
        rotationY += mouseX;

        // 회전을 적용
        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    void MoveCameraUpDown()
    {
        if (Input.GetKey(KeyCode.Q)) // Q 키로 위로 이동
        {
            transform.position += Vector3.up * verticalMoveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E)) // E 키로 아래로 이동
        {
            transform.position += Vector3.down * verticalMoveSpeed * Time.deltaTime;
        }
    }
}
