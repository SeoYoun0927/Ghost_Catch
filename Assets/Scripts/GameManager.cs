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
    public GameObject heartItemPrefab; // ��Ʈ ������ ������
    public GameObject boostItemPrefab; // �ν�Ʈ ������ ������
    public GameObject monsterPrefab; // ���� ������
    public float monsterSpawnInterval = 20.0f; // ���� ��ȯ ����
    public float heartSpawnInterval = 10.0f; // ��Ʈ ���� ����
    public float boostSpawnInterval = 20.0f; // �ν�Ʈ ������ ���� ����
    public Vector3 spawnAreaMin; // ������ ���� ���� �ּҰ�
    public Vector3 spawnAreaMax; // ������ ���� ���� �ִ밪
    public GameObject gameoverImage;

    private float surviveTime;
    private bool isGameover;
    private int lives;
    private int heartItemCount; // ������ ��Ʈ ������ ��

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

        lives = Mathf.Min(lives + amount, 3); // �ִ� ���� ���� �ʰ����� �ʵ��� �մϴ�.
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

            // �ִ� 3���� ��Ʈ �����۸� ����
            if (heartItemCount < 3)
            {
                Vector3 spawnPosition = new Vector3(
                    Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                    Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                    Random.Range(spawnAreaMin.z, spawnAreaMax.z)
                );

                GameObject heartItem = Instantiate(heartItemPrefab, spawnPosition, Quaternion.identity);
                heartItemCount++;
                // ���� �ð� �Ŀ� �������� �ı��ǵ��� ����
                StartCoroutine(DestroyItemAfterDelay(heartItem, 5f));
            }
        }
    }

    IEnumerator SpawnBoostItems()
    {
        while (!isGameover)
        {
            yield return new WaitForSeconds(boostSpawnInterval);

            // �ν�Ʈ ������ ����
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

            // �÷��̾� �ֺ��� ���͸� ��ȯ�մϴ�.
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
