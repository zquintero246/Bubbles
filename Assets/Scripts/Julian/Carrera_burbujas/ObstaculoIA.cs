using System.Collections;
using UnityEngine;

public class ObstaculoIA : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("IAPlayer"))
        {
            IAPlayer iaPlayer = other.GetComponent<IAPlayer>();
            if (iaPlayer != null)
            {
                iaPlayer.DetenerIA();  // Llamar a la función para detener el movimiento
                Debug.Log("¡La IA ha sido detenida por un obstáculo!");
            }

            Destroy(gameObject);  // Destruir el obstáculo después de la colisión
        }
    }
}
