using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float moveSpeed = 3.0f;

    void Update()
    {
        // �÷��̾ ���� �̵�
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
            // �÷��̾�� ������ �÷��̾��� ü�� ����
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.TakeDamage();
            }
            // ���� �ı�
            Destroy(gameObject);
        }
    }
}
