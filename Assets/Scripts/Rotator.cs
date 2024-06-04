using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float initialRotationSpeed = 60f;
    public float rotationAcceleration = 10f; // ȸ�� ���ӵ�

    private float currentRotationSpeed;

    void Start()
    {
        currentRotationSpeed = initialRotationSpeed;
    }

    void Update()
    {
        // ȸ�� �ӵ��� �ð��� ���� ������Ŵ
        currentRotationSpeed += rotationAcceleration * Time.deltaTime;
        transform.Rotate(0f, currentRotationSpeed * Time.deltaTime, 0f);
    }
}
