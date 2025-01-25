using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;
    public float floatAmplitude = 0.5f;
    public float floatFrequency = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        speed = Random.Range(2f, 4f);
        floatAmplitude = Random.Range(0.3f, 0.7f);
        floatFrequency = Random.Range(0.8f, 1.2f);
    }

    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        float floatOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(transform.position.x, initialPosition.y + floatOffset, transform.position.z);
    }
}
