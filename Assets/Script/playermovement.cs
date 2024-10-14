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

    // �̵� �������� ���θ� �����ϴ� ����
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

    // �浹�� �����Ͽ� �ش� ������ �̵��� ����
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            // �浹 ������ ���
            Vector3 contactNormal = collision.contacts[0].normal;

            // ����(����)������ �̵� ����
            if (contactNormal.z > 0.5f)
            {
                canMoveForward = false;
            }
            // �Ĺ�(����)������ �̵� ����
            if (contactNormal.z < -0.5f)
            {
                canMoveBackward = false;
            }
            // ���������� �̵� ����
            if (contactNormal.x > 0.5f)
            {
                canMoveLeft = false;
            }
            // ���������� �̵� ����
            if (contactNormal.x < -0.5f)
            {
                canMoveRight = false;
            }
        }

        // �ٴڿ� ����� �� ���� ���� ���·� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �浹���� ����� �� �ٽ� �̵� �����ϵ��� ����
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
        // Base�� �浹 �� (ų ī��Ʈ�� ��ǥġ�� �������� �� �¸�)
        if (other.CompareTag("base"))
        {
            if (GameManager.instance.killCount >= GameManager.instance.killGoal)
            {
                GameManager.instance.WinGame();
            }
        }

        // Enemy�� �浹 �� ��� �й�
        if (other.CompareTag("Target"))
        {
            GameManager.instance.LoseGame();
        }
    }
}
