using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerfectZoneController : MonoBehaviour
{
    public DetectorSonido detector;

    public float loudnesSensivity;
    public float threshold;
    public float forcePulse;

    public string[] sceneNames;

    [SerializeField] bool IsInside = false;

    // Time Variables
    [SerializeField] float bubbleInterval;
    [SerializeField] float totalTime;
    [SerializeField] float currentInTime = 0; 

    private float scoreTimer = 40f; 
    private bool scoreAdded = false; 

    Rigidbody2D _rb;

    public GameObject bubblePrefab;
    public GameObject bubbleSpawner;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine("GenerateBubbles");
        StartCoroutine("ScoreTimer");  
    }

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
        }
        else
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
        yield return new WaitForSeconds(bubbleInterval / Mathf.Max(1f, currentInTime)); 
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

    // Corrutina que se ejecuta después de 40 segundos para agregar la puntuación
    private IEnumerator ScoreTimer()
    {
        yield return new WaitForSeconds(scoreTimer);

        if (!scoreAdded) // Asegurarse de que solo se ejecute una vez
        {
            int scoreToAdd = Mathf.RoundToInt(totalTime * 10);
            if (ScoreMan.instance != null)
            {
                ScoreMan.instance.AddScore(scoreToAdd);
                Debug.Log("Puntuación añadida: " + scoreToAdd);
            }
            else
            {
                Debug.LogError("ScoreMan instance no encontrada.");
            }
            scoreAdded = true;  // Evitar que se repita la suma de puntos
        }

        // Aprovecho este mismo segmento de codigo para crear el timer de cambio de escena

        // Verificar si totalTime es mayor o igual a 15 antes de cambiar de escena
        if (totalTime >= 15)
        {
            if (sceneNames.Length > 0)
            {
                int randomIndex = Random.Range(0, sceneNames.Length);
                string nextScene = sceneNames[randomIndex];
                Debug.Log("Cambiando a la escena: " + nextScene);
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                Debug.LogError("No hay escenas disponibles para cambiar.");
            }
        }
        else
        {
            Debug.Log("No se cumple la condición para cambiar de escena (totalTime < 15).");
        }

    }
}