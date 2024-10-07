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
        // 충돌한 오브젝트가 적("Target") 태그를 가지고 있는지 확인
        if (other.CompareTag("Target"))
        {
            // 적과 충돌했을 때 총알 제거
            Destroy(gameObject);
        }
    }
}
