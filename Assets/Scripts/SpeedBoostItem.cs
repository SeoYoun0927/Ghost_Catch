using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoostItem : MonoBehaviour
{
    public float speedBoostAmount = 5f; // �̵� �ӵ� ������
    public GameObject particlePrefab; // ���� ����Ʈ ������

    void Start()
    {
        // �������� ������ �� ��ƼŬ ����
        if (particlePrefab != null)
        {
            GameObject particle = Instantiate(particlePrefab, transform.position, Quaternion.identity);
            // ��ƼŬ�� �ڽ� ������Ʈ�� �ǵ��� ����
            particle.transform.parent = transform;
            // ��ƼŬ�� �ı����� �ʵ��� ����
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
            Destroy(gameObject); // �������� �ı��մϴ�.
        }
    }
}
