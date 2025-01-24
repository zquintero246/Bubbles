using UnityEngine;

public class Meta : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el objeto que colisiona tiene la etiqueta "Player" o "IAPlayer"
        if (other.CompareTag("Player") || other.CompareTag("IAPlayer"))
        {
            // Obtener el Rigidbody2D y detener el movimiento
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // Detiene el movimiento
                rb.isKinematic = true;       // Evita que se mueva nuevamente
            }

            // Desactivar los scripts de movimiento si existen
            MonoBehaviour movimiento = other.GetComponent<MonoBehaviour>();
            if (movimiento != null)
            {
                movimiento.enabled = false;
            }

            Debug.Log(other.gameObject.name + " ha llegado a la meta y se ha detenido.");
        }
    }
}
