using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    // 충돌이 발생했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Bullet" 태그를 가지고 있는지 확인
        if (other.CompareTag("Bullet"))
        {
            // 적 오브젝트(자기 자신)를 제거
            Destroy(gameObject);

            // 총알도 제거
            Destroy(other.gameObject);
        }
    }
}
