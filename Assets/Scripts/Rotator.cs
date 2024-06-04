using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float initialRotationSpeed = 60f;
    public float rotationAcceleration = 10f; // 회전 가속도

    private float currentRotationSpeed;

    void Start()
    {
        currentRotationSpeed = initialRotationSpeed;
    }

    void Update()
    {
        // 회전 속도를 시간에 따라 증가시킴
        currentRotationSpeed += rotationAcceleration * Time.deltaTime;
        transform.Rotate(0f, currentRotationSpeed * Time.deltaTime, 0f);
    }
}
