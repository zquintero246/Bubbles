using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo_Estatico : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
    }

    // Detección de colisión con el jugador
    void OnTriggerEnter2D(Collider2D other)
    {

        Debug.Log("Colisión con: " + other.name);

        
        if (other.CompareTag("Player"))
        {
            // Reducir velocidad del jugador o aplicar penalización
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity *= 0.5f;  // Reduce la velocidad a la mitad
                Debug.Log("¡Colisión con obstáculo estático! Velocidad reducida.");
            }

            Destroy(gameObject);
        }
    }
}
