using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectZoneController : MonoBehaviour
{
    public DetectorSonido detector;

    public float loudnesSensivity;
    public float threshold;
    public float forcePulse;

    [SerializeField] bool IsInside = false;

    //Time Variables
    [SerializeField] float bubbleInterval;
    [SerializeField] float totalTime; //Tiempo para la puntuación final
    [SerializeField] float currentInTime = 0; //Tiempo para la aparición de las burbujas

    Rigidbody2D _rb;

    public GameObject bubblePrefab;
    public GameObject bubbleSpawner;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine("GenerateBubbles");
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < -3.45f)
        {
            transform.position = new Vector2(transform.position.x, -3.45f);
            _rb.velocity = Vector2.zero;
        }

        if (transform.position.y > 3.45f)
        {
            transform.position = new Vector2(transform.position.x, 3.45f);
            _rb.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnesSensivity;

        if (loudness >= threshold)
        {
            _rb.AddForce(Vector2.up * forcePulse, ForceMode2D.Impulse);
        } else
        {
            _rb.velocity = Vector2.Lerp(_rb.velocity, Vector2.zero, Time.deltaTime * 4.0f);
        }
    }
    private IEnumerator GenerateBubbles()
    {
        Debug.Log("Me activé");
        if (IsInside)
        {
            Debug.Log("Estoy generando burbujas");
            GameObject bubble = Instantiate(bubblePrefab, bubbleSpawner.transform.position, Quaternion.identity);

            bubble.transform.localScale = new Vector3(0.1f + currentInTime / 10f, 0.1f + currentInTime / 10f, 0.1f + currentInTime / 10f);
        }
        yield return new WaitForSeconds(bubbleInterval / Mathf.Max(1f, currentInTime)); // Genera más burbujas cuanto más tiempo pase

        StartCoroutine("GenerateBubbles");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IsInside = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        currentInTime += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsInside = false;
        totalTime += currentInTime;
        currentInTime = 0f;
    }
}
