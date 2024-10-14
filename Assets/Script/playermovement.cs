using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    private float jumpForce = 30f; // 점프의 힘
    private bool isGrounded = true; // 점프 가능 상태 확인용
    private Rigidbody rb;

    // 이동 가능한지 여부를 관리하는 변수
    private bool canMoveForward = true;
    private bool canMoveBackward = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
        jump();
    }

    void move()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX * rotationspeed * Time.deltaTime, 0);
        // transform.Rotate(-mouseY * rotationspeed * Time.deltaTime, 0, 0);

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (canMoveForward)
                transform.Translate(0, 0, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (canMoveBackward)
                transform.Translate(0, 0, -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (canMoveLeft)
                transform.Translate(-speed * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (canMoveRight)
                transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * 20f);
        }
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // 충돌을 감지하여 해당 방향의 이동을 제한
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // 충돌 방향을 계산
            Vector3 contactNormal = collision.contacts[0].normal;

            // 전방(앞쪽)으로의 이동 제한
            if (contactNormal.z > 0.5f)
            {
                canMoveForward = false;
            }
            // 후방(뒤쪽)으로의 이동 제한
            if (contactNormal.z < -0.5f)
            {
                canMoveBackward = false;
            }
            // 좌측으로의 이동 제한
            if (contactNormal.x > 0.5f)
            {
                canMoveLeft = false;
            }
            // 우측으로의 이동 제한
            if (contactNormal.x < -0.5f)
            {
                canMoveRight = false;
            }
        }

        // 바닥에 닿았을 때 점프 가능 상태로 변경
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 충돌에서 벗어났을 때 다시 이동 가능하도록 설정
        if (collision.gameObject.CompareTag("obstacle"))
        {
            canMoveForward = true;
            canMoveBackward = true;
            canMoveLeft = true;
            canMoveRight = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Base와 충돌 시 (킬 카운트가 목표치에 도달했을 때 승리)
        if (other.CompareTag("base"))
        {
            if (GameManager.instance.killCount >= GameManager.instance.killGoal)
            {
                GameManager.instance.WinGame();
            }
        }

        // Enemy와 충돌 시 즉시 패배
        if (other.CompareTag("Target"))
        {
            GameManager.instance.LoseGame();
        }
    }
}
