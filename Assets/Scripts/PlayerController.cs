using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 8.0f;
    private float boostDuration = 5.0f; // �ν�Ʈ ȿ���� �⺻ ���� �ð�

    public GameObject boostEffectPrefab; // �ν�Ʈ ȿ���� ��ƼŬ ������
    private GameObject boostEffect; // �ν�Ʈ ȿ�� ��ƼŬ ��ü

    private GameManager gameManager;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        // ���ο� �̵� ������ ���
        Vector3 moveDirection = new Vector3(xInput, 0f, zInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // �÷��̾ �̵��ϴ� �������� ȸ��
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // �̵� ����� �ӵ��� ���ؼ� ���ο� �ӵ��� ���
        Vector3 newVelocity = moveDirection * speed;
        playerRigidbody.velocity = newVelocity;
    }

    public void TakeDamage()
    {
        if (gameManager != null)
        {
            gameManager.PlayerHit();
        }
    }

    public void Heal(int amount)
    {
        if (gameManager != null)
        {
            gameManager.PlayerHeal(amount);
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);

        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    public void ApplySpeedBoost(float boostAmount)
    {
        // �̵� �ӵ��� ������ŵ�ϴ�.
        speed += boostAmount;

        // �ν�Ʈ ȿ�� ��ƼŬ�� �����մϴ�.
        if (boostEffectPrefab != null)
        {
            boostEffect = Instantiate(boostEffectPrefab, transform.position, Quaternion.identity);
            boostEffect.transform.SetParent(transform);
        }

        // ���� �ð��� ���� �Ŀ� �̵� �ӵ��� ������� �ǵ����ϴ�.
        StartCoroutine(RestoreSpeedAfterDelay(boostDuration));
    }

    IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // ���� �̵� �ӵ��� �ǵ����ϴ�.
        speed = 12.0f;

        // �ν�Ʈ ȿ�� ��ƼŬ�� �����մϴ�.
        if (boostEffect != null)
        {
            Destroy(boostEffect);
        }
    }
}
