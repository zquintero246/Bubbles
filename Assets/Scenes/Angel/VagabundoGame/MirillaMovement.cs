using UnityEngine;

public class MirillaController : MonoBehaviour
{
    public float acceleration = 10f;  // Aceleración de la mirilla
    public float maxSpeed = 5f;       // Velocidad máxima
    public float friction = 5f;       // Fricción para el deslizamiento
    public float cooldownRate = 1f;
    private Vector2 velocity;         // Velocidad de la mirilla
    private float CooldownTiro = 0;
    private bool EnemigoEnMira = false;

    // Variables de puntuación
    private int scorePosible = 150;   // Puntuación inicial
    private int scoreDecay = 10;      // Cantidad a disminuir cada ciclo

    // Referencias a los objetos de la escena
    public GameObject transformacion;
    public GameObject correr;
    public GameObject winManager;  // Objeto WinManager que se activará

    void Start()
    {
        ActualizarDificultad();
        InvokeRepeating("ReducirScore", 1f, 1f); // Disminuir la puntuación posible cada segundo
    }

    void Update()
    {
        // Capturar entrada del teclado
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        bool inputSpace = Input.GetKeyDown(KeyCode.Space);  // Detectar solo la primera pulsación

        // Aplicar aceleración según la dirección de entrada
        Vector2 inputDirection = new Vector2(inputX, inputY).normalized;

        if (inputDirection.magnitude > 0)
        {
            velocity += inputDirection * acceleration * Time.deltaTime;
            velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            // Aplicar fricción cuando no se presiona ninguna tecla
            velocity = Vector2.Lerp(velocity, Vector2.zero, friction * Time.deltaTime);
        }

        // Mover la mirilla
        transform.position += (Vector3)velocity * Time.deltaTime;

        // Disminuir el cooldown basado en el tiempo
        if (CooldownTiro > 0)
        {
            CooldownTiro -= cooldownRate * Time.deltaTime;
            if (CooldownTiro < 0)
            {
                CooldownTiro = 0;
            }
            Debug.Log("Cooldown restante: " + CooldownTiro);
        }

        // Detectar la entrada del disparo si el cooldown está en 0
        if (CooldownTiro <= 0 && inputSpace)
        {
            if (EnemigoEnMira)
            {
                Debug.Log("Le diste al enemigo");
                //ScoreMan.instance.AddScore(scorePosible);

                // Activar transformacion y desactivar correr
                
                    transformacion.SetActive(true);
                    correr.SetActive(false);
                

                // Activar el WinManager después de 2 segundos
                
                
                    Invoke(nameof(ActivarWinManager), 2f);
                
            }
            else
            {
                Debug.Log("Jajapendejo");
            }

            CooldownTiro = 10;  // Resetear el cooldown después de disparar
        }

        // Actualizar la fricción en función de la dificultad
        if (DifficultyManager.instance != null)
        {
            friction = 3f - DifficultyManager.instance.dificultadGlobal;
        }
    }

    void ActualizarDificultad()
    {
        if (DifficultyManager.instance != null)
        {
            friction = 3f - DifficultyManager.instance.dificultadGlobal;
            Debug.Log("Dificultad aplicada a Carrera Burbuja: " + DifficultyManager.instance.dificultadGlobal);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EnemigoEnMira = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemigo"))
        {
            EnemigoEnMira = false;
            Debug.Log("Enemigo salió de la mira");
        }
    }

    // Método para reducir la puntuación posible con el tiempo
    void ReducirScore()
    {
        if (scorePosible > 0)
        {
            scorePosible -= scoreDecay;
            Debug.Log("Puntuación posible reducida: " + scorePosible);  
        }
        else
        {
            Debug.Log("Perdiste");
            CancelInvoke("ReducirScore"); // Detener la reducción de puntuación
        }
    }

    // Método para activar el WinManager
    void ActivarWinManager()
    {
        winManager.SetActive(true);
        Debug.Log("WinManager activado después de 2 segundos.");
    }
}