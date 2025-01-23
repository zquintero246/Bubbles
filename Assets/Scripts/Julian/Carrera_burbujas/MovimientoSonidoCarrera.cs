using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoSonidoCarrera : MonoBehaviour
{
    // Fuente de audio para reproducir el sonido capturado del micrófono (opcional).
    public AudioSource source;

    // Referencia al script DetectorSonido, encargado de obtener la intensidad del sonido.
    public DetectorSonido detector;

    // Sensibilidad del detector de sonido, controla cuán reactivo es a la señal de entrada.
    public float loudnessSensibility = 100;

    public float fuerza = 1f; // Magnitud de la fuerza aplicada.

    // Umbral mínimo para aplicar la fuerza.
    public float threshold = 10f;
    void Update()
    {
        // Obtiene la intensidad del sonido desde el micrófono, ajustada por la sensibilidad.
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // Si la intensidad del sonido supera el umbral, aplica una fuerza al objeto.
        if (loudness >= threshold)
        {
            rb.AddForce(Vector2.left * fuerza, ForceMode2D.Impulse);
        }
        else
        {
            // Desaceleración progresiva aplicando fricción manualmente
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * 2.0f);
        }
    }

}

