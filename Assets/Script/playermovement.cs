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
    private Animator animator;


    // 이동 가능한지 여부를 관리하는 변수
    private bool canMoveForward = true;
    private bool canMoveBackward = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private bool isBouncing = false; // 튕겨 나가는 상태를 관리하는 변수
    private float bounceCooldown = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 튕겨 나가는 동안은 이동 제한
        if (!isBouncing)
        {
            move();
        }
        jump();
    }

    void move()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationspeed * Time.deltaTime, 0);

        bool isFront = false;
        bool isBack = false;
        bool isRight = false;
        bool isLeft = false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (canMoveForward)
            {
                transform.Translate(0, 0, speed * Time.deltaTime);
                isFront = true;
            }
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (canMoveBackward)
            {
                transform.Translate(0, 0, -speed * Time.deltaTime);
                isBack = true;
            }
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (canMoveLeft)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0);
                isLeft = true;
            }
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (canMoveRight)
            {
                transform.Translate(speed * Time.deltaTime, 0, 0);
                isRight = true;
            }
        }
        if (isGrounded)
        {
            animator.SetBool("isFront", isFront);
            animator.SetBool("isBack", isBack);
            animator.SetBool("isLeft", isLeft);
            animator.SetBool("isRight", isRight);
        }
        else
        {
            animator.SetBool("isFront", false);
            animator.SetBool("isBack", false);
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("energy"))
        {
            Vector3 collisionNormal = collision.contacts[0].normal; // 충돌한 지점의 법선 벡터
            Vector3 bounceDirection = collisionNormal;


            rb.velocity = Vector3.zero; // 기존 속도를 초기화해서 이전 힘 제거
            rb.AddForce(bounceDirection * 50f, ForceMode.Impulse);

            // 튕겨 나가는 동안 이동 불가
            StartCoroutine(DisableMovementAfterBounce());
        }

        if (collision.gameObject.CompareTag("obstacle"))
        {
            Vector3 contactNormal = collision.contacts[0].normal;

            if (contactNormal.z > 0.5f)
            {
                canMoveForward = false;
            }
            if (contactNormal.z < -0.5f)
            {
                canMoveBackward = false;
            }
            if (contactNormal.x > 0.5f)
            {
                canMoveLeft = false;
            }
            if (contactNormal.x < -0.5f)
            {
                canMoveRight = false;
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // 일정 시간 동안 이동 제한 코루틴
    IEnumerator DisableMovementAfterBounce()
    {
        isBouncing = true; // 튕겨 나가는 동안 이동 불가
        yield return new WaitForSeconds(bounceCooldown); // 1초 대기 (원하는 시간으로 조정 가능)
        isBouncing = false; // 다시 이동 가능
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            canMoveForward = true;
            canMoveBackward = true;
            canMoveLeft = true;
            canMoveRight = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 적과 충돌하면 게임 오버 처리
        if (other.CompareTag("Target"))
        {
            GameManager.instance.LoseGame(); // 적과 충돌 시 즉시 게임 오버 처리
            return; // 튕겨 나가는 로직을 실행하지 않도록 바로 반환
        }

        if (other.CompareTag("base") && GameManager.instance.killCount >=5)
        {
            GameManager.instance.WinGame(); // 적과 충돌 시 즉시 게임 오버 처리
            return; // 튕겨 나가는 로직을 실행하지 않도록 바로 반환
        }
    }
}
