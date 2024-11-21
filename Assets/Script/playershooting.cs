using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playershooting : MonoBehaviour
{
    public GameObject red;
    public GameObject blue;
    public GameObject shoot;

    public AudioClip redFireSound; // Red 총알 발사 효과음
    public AudioClip blueFireSound; // Blue 총알 발사 효과음
    private AudioSource audioSource; // AudioSource 컴포넌트


    public float redfireDelay = 0.1f;
    public float bluefireDelay = 1f;  // 총알 발사 딜레이 (통합된 딜레이)

    private float redlastFireTime = 0f; // 마지막 총알 발사 시간
    private float bluelastFireTime = 0f;

    void Start()
    {
        // AudioSource 컴포넌트 가져오기 (게임 오브젝트에 추가 필요)
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (redlastFireTime > redfireDelay && Input.GetKey(KeyCode.Mouse0))
        {
            Instantiate(red, shoot.transform.position, shoot.transform.rotation);
            audioSource.PlayOneShot(redFireSound);

            // 마지막 발사 시간을 업데이트
            redlastFireTime = 0;
        }

        // 마우스 오른쪽 버튼으로 Blue bullet 발사
        if (bluelastFireTime > bluefireDelay && Input.GetKey(KeyCode.Mouse1))
        {
            // 정면으로 Blue bullet 발사
            Instantiate(blue, shoot.transform.position, shoot.transform.rotation);

            // 오른쪽으로 15도 회전된 방향으로 Blue bullet 발사
            Quaternion rightRotation = Quaternion.Euler(0, 15, 0) * shoot.transform.rotation;
            Instantiate(blue, shoot.transform.position, rightRotation);

            // 왼쪽으로 15도 회전된 방향으로 Blue bullet 발사
            Quaternion leftRotation = Quaternion.Euler(0, -15, 0) * shoot.transform.rotation;
            Instantiate(blue, shoot.transform.position, leftRotation);
            audioSource.PlayOneShot(blueFireSound);

            // 마지막 발사 시간을 업데이트
            bluelastFireTime = 0;
        }
        redlastFireTime += Time.deltaTime;
        bluelastFireTime += Time.deltaTime;
    }
}
