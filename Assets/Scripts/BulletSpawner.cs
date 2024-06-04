using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float spawnRateMin = 0.5f;
    public float initialSpawnRateMax = 30f;
    public float minSpawnRateMax = 1f; // 최대로 낮출 수 있는 spawnRateMax 값

    private Transform target;
    private float spawnRateMax;
    private float spawnRate;
    private float timeAfterSpawn;
    void Start()
    {
        timeAfterSpawn = 0f;
        spawnRateMax = initialSpawnRateMax;
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        target = FindObjectOfType<PlayerController>().transform;
    }


    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if (timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0f;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.transform.LookAt(target);

            // 시간이 지날수록 spawnRateMax를 줄임
            spawnRateMax = Mathf.Max(minSpawnRateMax, initialSpawnRateMax - Time.time * 0.5f);

            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
