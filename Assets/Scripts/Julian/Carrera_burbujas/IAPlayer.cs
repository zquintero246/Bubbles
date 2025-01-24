using System.Collections;
using UnityEngine;

public class IAPlayer : MonoBehaviour
{
    public float velocidadMovimiento = 1f;
    public float margenDeError = 0.2f;
    public Transform detectorObstaculos;
    public LayerMask capaObstaculos;
    public float distanciaDeteccion = 2.0f;
    public float tiempoDetencion = 1.0f;
    public float posicionCarril1 = -1.8f;
    public float posicionCarril2 = -3.2f;

    private Rigidbody2D rb;
    private bool enCarrilSuperior = true;
    private bool detenido = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = new Vector3(transform.position.x, posicionCarril1, transform.position.z);
    }

    void Update()
    {
        if (detenido)
            return;

        // Movimiento constante hacia adelante
        rb.velocity = new Vector2(velocidadMovimiento, 0);

        // Dibuja un raycast para depuración visual
        Debug.DrawRay(detectorObstaculos.position, Vector2.right * distanciaDeteccion, Color.red);

        // Detectar obstáculos
        RaycastHit2D hit = Physics2D.Raycast(detectorObstaculos.position, Vector2.right, distanciaDeteccion, capaObstaculos);
        if (hit.collider != null)
        {
            Debug.Log("¡Obstáculo detectado: " + hit.collider.gameObject.name + "!");
            DecidirMovimiento();
        }
    }

    void DecidirMovimiento()
    {
        float probabilidadError = Random.value;

        if (probabilidadError > margenDeError)
        {
            CambiarCarril();
        }
        else
        {
            StartCoroutine(DetenerPorObstaculo());
        }
    }

    void CambiarCarril()
    {
        if (enCarrilSuperior)
        {
            transform.position = new Vector3(transform.position.x, posicionCarril2, transform.position.z);
            enCarrilSuperior = false;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, posicionCarril1, transform.position.z);
            enCarrilSuperior = true;
        }

        Debug.Log("IA cambió de carril");
    }

    public void DetenerIA()
    {
        StartCoroutine(DetenerPorObstaculo());
    }

    IEnumerator DetenerPorObstaculo()
    {
        Debug.Log("¡IA detenida!");
        detenido = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(tiempoDetencion);
        detenido = false;
        Debug.Log("¡IA reanuda su movimiento!");
    }
}
