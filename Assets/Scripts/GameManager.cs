using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameoverText;
    public Text timeText;
    public Text recordText;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;
    public GameObject heartItemPrefab; // 하트 아이템 프리팹
    public GameObject boostItemPrefab; // 부스트 아이템 프리팹
    public GameObject monsterPrefab; // 몬스터 프리팹
    public float monsterSpawnInterval = 20.0f; // 몬스터 소환 간격
    public float heartSpawnInterval = 10.0f; // 하트 생성 간격
    public float boostSpawnInterval = 20.0f; // 부스트 아이템 생성 간격
    public Vector3 spawnAreaMin; // 아이템 생성 영역 최소값
    public Vector3 spawnAreaMax; // 아이템 생성 영역 최대값
    public GameObject gameoverImage;

    private float surviveTime;
    private bool isGameover;
    private int lives;
    private int heartItemCount; // 생성된 하트 아이템 수

    void Start()
    {
        surviveTime = 0;
        isGameover = false;
        gameoverImage.SetActive(false);
        lives = 3;
        heartItemCount = 0;
        UpdateHeartDisplay();
        StartCoroutine(SpawnHeartItems());
        StartCoroutine(SpawnBoostItems());
        StartCoroutine(SpawnMonsters());
    }

    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Survive: " + (int)surviveTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    public void PlayerHit()
    {
        if (isGameover)
            return;

        lives--;
        UpdateHeartDisplay();

        if (lives <= 0)
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.Die();
            }
        }
    }

    public void PlayerHeal(int amount)
    {
        if (isGameover)
            return;

        lives = Mathf.Min(lives + amount, 3); // 최대 생명 수를 초과하지 않도록 합니다.
        UpdateHeartDisplay();
    }

    void UpdateHeartDisplay()
    {
        heart1.SetActive(lives >= 1);
        heart2.SetActive(lives >= 2);
        heart3.SetActive(lives >= 3);
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);
        gameoverImage.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime)
        {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        recordText.text = "Best Survive: " + (int)bestTime;
    }
    IEnumerator DestroyItemAfterDelay(GameObject item, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (item != null)
        {
            Destroy(item);
        }
    }

    IEnumerator SpawnHeartItems()
    {
        while (!isGameover)
        {
            yield return new WaitForSeconds(heartSpawnInterval);

            // 최대 3개의 하트 아이템만 생성
            if (heartItemCount < 3)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                );

                GameObject heartItem = Instantiate(heartItemPrefab, spawnPosition, Quaternion.identity);
                heartItemCount++;
                // 일정 시간 후에 아이템이 파괴되도록 설정
                StartCoroutine(DestroyItemAfterDelay(heartItem, 5f));
            }
        }
    }

    IEnumerator SpawnBoostItems()
    {
        while (!isGameover)
        {
            yield return new WaitForSeconds(boostSpawnInterval);

            // 부스트 아이템 생성
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            GameObject boostitem= Instantiate(boostItemPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyItemAfterDelay(boostitem, 5f));

        }
    }
    IEnumerator SpawnMonsters()
    {
        while (!isGameover)
        {
            yield return new WaitForSeconds(monsterSpawnInterval);

            // 플레이어 주변에 몬스터를 소환합니다.
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );
            GameObject spawnMoster=Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(DestroyItemAfterDelay(spawnMoster, 20f));
        }
    }
}
