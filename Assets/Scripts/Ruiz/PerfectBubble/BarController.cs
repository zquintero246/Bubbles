using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BarController : MonoBehaviour
{
    [SerializeField] float minPos;
    [SerializeField] float maxPos;
    [SerializeField] float currentPos;
    [SerializeField] int currentDirection = 1; //Valores de 1 o -1
    [SerializeField] float currentSpeed = 0.1f;
    [SerializeField] bool stop = false;
    [SerializeField] bool changeDirection = false;
    [SerializeField] float stopSpeed;
    [SerializeField] float waitTime;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StopBar");
    }

    private void FixedUpdate()
    {
        if (currentPos <= minPos || currentPos >= maxPos)
        {
            currentDirection *= -1;
        }

        if (!stop)
        {
            currentPos += currentDirection * currentSpeed;
            transform.position = stop ? new Vector2(transform.position.x, transform.position.y) : new Vector2(transform.position.x, currentPos);
        }
    }

    IEnumerator StopBar()
    {
        stop = true;
        stopSpeed = Random.Range(1f, 2f);
        changeDirection = Random.Range(0, 2) == 0;
        currentDirection = changeDirection ? currentDirection *= -1 : currentDirection;
        yield return new WaitForSeconds(stopSpeed);
        stop = false;
        waitTime = Random.Range(5f, 8f);
        yield return new WaitForSeconds(waitTime);
        StartCoroutine("StopBar");
    }
}
