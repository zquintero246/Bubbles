using UnityEngine;

public class Obstaculo_Estatico : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MovimientoSonidoCarrera jugador = FindObjectOfType<MovimientoSonidoCarrera>();
            if (jugador != null)
            {
                jugador.DetenerJugador();  
                Debug.Log("¡Colisión con obstáculo estático! La pista se detendrá por 1 segundo.");
            }
            else
            {
                Debug.LogWarning("No se encontró el script MovimientoSonidoCarrera en la escena.");
            }

           gameObject.SetActive(false);  // Eliminar el obstáculo después de la colisión
        }
    }
}
