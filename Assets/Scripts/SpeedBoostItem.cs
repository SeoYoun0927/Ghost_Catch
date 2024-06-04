using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour
{
    public float speedBoostAmount = 5f; // 이동 속도 증가량
    public GameObject particlePrefab; // 스폰 이펙트 프리팹

    void Start()
    {
        // 아이템이 생성될 때 파티클 생성
        if (particlePrefab != null)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            // 파티클이 자식 오브젝트가 되도록 설정
            particle.transform.parent = transform;
            // 파티클이 파괴되지 않도록 설정
            DontDestroyOnLoad(particle);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.ApplySpeedBoost(speedBoostAmount);
            }
            Destroy(gameObject); // 아이템을 파괴합니다.
        }
    }
}
