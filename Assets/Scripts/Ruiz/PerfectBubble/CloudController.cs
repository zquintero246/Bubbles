using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 1f;
    public bool moveToLeft;
    public float boundaryLeft = -20f;
    public float boundaryRight = 20f; 

    void Start()
    {
        moveToLeft = Random.Range(0, 2) == 0;
    }

    void Update()
    {
        float direction = moveToLeft ? -1f : 1f;

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (moveToLeft && transform.position.x < boundaryLeft)
        {
            moveToLeft = !moveToLeft;
        }
        else if (!moveToLeft && transform.position.x > boundaryRight)
        {
            moveToLeft = !moveToLeft;
        }
    }
}
