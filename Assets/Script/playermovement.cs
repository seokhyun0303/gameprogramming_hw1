using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    private float jumpForce = 30f; // ������ ��
    private bool isGrounded = true; // ���� ���� ���� Ȯ�ο�
    private Rigidbody rb;
    private Animator animator;


    // �̵� �������� ���θ� �����ϴ� ����
    private bool canMoveForward = true;
    private bool canMoveBackward = true;
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    private bool isBouncing = false; // ƨ�� ������ ���¸� �����ϴ� ����
    private float bounceCooldown = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ƨ�� ������ ������ �̵� ����
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
            Vector3 collisionNormal = collision.contacts[0].normal; // �浹�� ������ ���� ����
            Vector3 bounceDirection = collisionNormal;


            rb.velocity = Vector3.zero; // ���� �ӵ��� �ʱ�ȭ�ؼ� ���� �� ����
            rb.AddForce(bounceDirection * 50f, ForceMode.Impulse);

            // ƨ�� ������ ���� �̵� �Ұ�
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

    // ���� �ð� ���� �̵� ���� �ڷ�ƾ
    IEnumerator DisableMovementAfterBounce()
    {
        isBouncing = true; // ƨ�� ������ ���� �̵� �Ұ�
        yield return new WaitForSeconds(bounceCooldown); // 1�� ��� (���ϴ� �ð����� ���� ����)
        isBouncing = false; // �ٽ� �̵� ����
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
        // ���� �浹�ϸ� ���� ���� ó��
        if (other.CompareTag("Target"))
        {
            GameManager.instance.LoseGame(); // ���� �浹 �� ��� ���� ���� ó��
            return; // ƨ�� ������ ������ �������� �ʵ��� �ٷ� ��ȯ
        }

        if (other.CompareTag("base") && GameManager.instance.killCount >=5)
        {
            GameManager.instance.WinGame(); // ���� �浹 �� ��� ���� ���� ó��
            return; // ƨ�� ������ ������ �������� �ʵ��� �ٷ� ��ȯ
        }
    }
}
