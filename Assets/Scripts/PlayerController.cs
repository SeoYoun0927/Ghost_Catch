using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public float speed = 8.0f;
    private float boostDuration = 5.0f; // 부스트 효과의 기본 지속 시간

    public GameObject boostEffectPrefab; // 부스트 효과의 파티클 프리팹
    private GameObject boostEffect; // 부스트 효과 파티클 객체

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

        // 새로운 이동 방향을 계산
        Vector3 moveDirection = new Vector3(xInput, 0f, zInput).normalized;

        if (moveDirection != Vector3.zero)
        {
            // 플레이어가 이동하는 방향으로 회전
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        // 이동 방향과 속도를 곱해서 새로운 속도를 계산
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
        // 이동 속도를 증가시킵니다.
        speed += boostAmount;

        // 부스트 효과 파티클을 생성합니다.
        if (boostEffectPrefab != null)
        {
            boostEffect = Instantiate(boostEffectPrefab, transform.position, Quaternion.identity);
            boostEffect.transform.SetParent(transform);
        }

        // 일정 시간이 지난 후에 이동 속도를 원래대로 되돌립니다.
        StartCoroutine(RestoreSpeedAfterDelay(boostDuration));
    }

    IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // 원래 이동 속도로 되돌립니다.
        speed = 12.0f;

        // 부스트 효과 파티클을 제거합니다.
        if (boostEffect != null)
        {
            Destroy(boostEffect);
        }
    }
}
