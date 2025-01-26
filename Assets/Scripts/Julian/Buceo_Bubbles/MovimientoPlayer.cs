using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento del personaje
    public float tiempoInicial = 20f;  // Tiempo inicial del juego en segundos
    public float tiempoExtraPorObjetivo = 5f;  // Tiempo extra otorgado al recoger un objetivo
    public float tiempoMaximo = 10f;  // Tiempo máximo permitido en el temporizador
    public Text temporizadorTexto;  // Referencia al UI para mostrar el tiempo restante

    private Rigidbody2D rb;
    private Vector2 movement;
    private float tiempoRestante;
    private bool juegoTerminado = false;

    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        tiempoRestante = tiempoInicial;
        ActualizarTemporizador();
    }

    void Update()
    {
        if (juegoTerminado) return;

        // Movimiento del jugador
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Reducir el tiempo restante
        tiempoRestante -= Time.deltaTime;

        // Asegurar que el tiempo no sobrepase el límite máximo ni sea negativo
        tiempoRestante = Mathf.Clamp(tiempoRestante, 0, tiempoMaximo);

        ActualizarTemporizador();

        if (tiempoRestante <= 0)
        {
            FinDelJuego(false);
        }

        // Actualizar estado del Animator
        ActualizarAnimacion();
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Objetivo"))
        {
            tiempoRestante += tiempoExtraPorObjetivo;  // Sumar tiempo adicional

            // Limitar el tiempo al máximo definido
            tiempoRestante = Mathf.Clamp(tiempoRestante, 0, tiempoMaximo);

            Destroy(other.gameObject);
            Debug.Log("¡Objetivo recogido! Tiempo añadido: " + tiempoExtraPorObjetivo);
        }

        // Verificar si el jugador ha alcanzado la meta
        if (other.CompareTag("Meta"))
        {
            FinDelJuego(true);
        }
    }

    void ActualizarTemporizador()
    {
        temporizadorTexto.text = "Tiempo: " + Mathf.Ceil(tiempoRestante).ToString();
    }

    void FinDelJuego(bool victoria)
    {
        juegoTerminado = true;
        rb.velocity = Vector2.zero; // Detener el movimiento del jugador

        if (victoria)
        {
            Debug.Log("¡Felicidades! Has alcanzado la meta.");
        }
        else
        {
            Debug.Log("Tiempo agotado. ¡Fin del juego!");
        }

        // Reiniciar el juego después de 2 segundos
        Invoke("ReiniciarJuego", 2f);
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ActualizarAnimacion()
    {
        if (movement.magnitude > 0)
        {
            animator.SetBool("Nadando", true);
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Nadando", false);
            animator.SetBool("Idle", true);
        }
    }
}
