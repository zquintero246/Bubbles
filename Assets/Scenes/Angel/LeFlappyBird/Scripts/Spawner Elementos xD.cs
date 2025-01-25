using UnityEngine;

public class Obstaculo : MonoBehaviour
{
    public float velocidadMin = 1f; // Velocidad mínima para el movimiento vertical
    public float velocidadMax = 6f; // Velocidad máxima para el movimiento vertical
    private float velocidad; // Velocidad de movimiento vertical aleatoria

    public float velocidadLateral = 3f;
    public float limiteSuperior = 8f; // Límite superior del movimiento
    public float limiteInferior = -8f; // Límite inferior del movimiento
    public GameObject obstaculoPrefab; // Prefab del obstáculo para clonarlo
    public float tiempoEntreClones = 3f; // Tiempo en segundos entre clonaciones

    private bool subiendo = true;
    private float temporizador;
    private bool moviendo = true;
    private Rigidbody2D rb;

    void Start()
    {
        temporizador = tiempoEntreClones; // Inicializa el temporizador
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("El objeto no tiene un Rigidbody2D adjunto.");
        }

        // Asignar una velocidad aleatoria dentro del rango definido
        velocidad = Random.Range(velocidadMin, velocidadMax);

        ActualizarDificultad();
    }

    void Update()
    {
        if (DifficultyManager.instance != null)
    {
        velocidadMax = 6 + DifficultyManager.instance.dificultadGlobal;
    }

    if (moviendo)
    {
        Mover();
        TemporizadorDeClonacion();
        transform.position += Vector3.right * Time.deltaTime * velocidadLateral;
    }
        
    }

    void Mover()
    {
        // Movimiento hacia arriba y abajo entre los límites
        if (subiendo)
        {
            transform.position += Vector3.up * velocidad * Time.deltaTime;
            if (transform.position.y >= limiteSuperior)
            {
                subiendo = false;
            }
        }
        else
        {
            transform.position += Vector3.down * velocidad * Time.deltaTime;
            if (transform.position.y <= limiteInferior)
            {
                subiendo = true;
            }
        }
    }

    void TemporizadorDeClonacion()
    {
        temporizador -= Time.deltaTime;
        if (temporizador <= 0f)
        {
            ClonarObstaculo();
            FreezeMovimiento();  // Congelar movimiento después de clonar
            moviendo = false;    // Detener movimiento en Update
        }
    }

    void ClonarObstaculo()
    {
        // Instanciar el clon en la posición actual
        GameObject clon = Instantiate(obstaculoPrefab, transform.position, transform.rotation);

        // Asegurar que el clon es Kinematic
        Rigidbody2D clonRb = clon.GetComponent<Rigidbody2D>();
        if (clonRb != null)
        {
            clonRb.bodyType = RigidbodyType2D.Kinematic;
        }
        else
        {
            Debug.LogWarning("El clon no tiene un Rigidbody2D adjunto.");
        }
    }

    void FreezeMovimiento()
    {
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            Debug.Log("Movimiento congelado en X e Y.");
        }
    }

    void ActualizarDificultad()
{
    if (DifficultyManager.instance != null)
    {
        velocidadMax = 6 + DifficultyManager.instance.dificultadGlobal;
        Debug.Log("Velocidad actualizada: " + velocidadMax);
    }
}
}