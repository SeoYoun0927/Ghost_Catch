using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    void Update()
    {
        // 플레이어를 향해 이동
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            transform.LookAt(player.transform);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어에게 닿으면 플레이어의 체력 감소
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage();
            }
            // 몬스터 파괴
            Destroy(gameObject);
        }
    }
}
