using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public int healAmount = 1;
    public GameObject particlePrefab; // ��ƼŬ ������

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
                playerController.Heal(healAmount);
            }
            Destroy(gameObject); // �������� �ı��մϴ�.
        }
    }
}
