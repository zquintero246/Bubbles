using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMicrophone : MonoBehaviour
{
    // Fuente de audio para reproducir el sonido capturado del micrófono (opcional).
    public AudioSource source;

    // Definen los valores mínimos y máximos de escala del objeto (posiblemente para animaciones visuales, pero no utilizados en este script).
    public Vector2 minScale;
    public Vector2 maxScale;

    // Referencia al script DetectorSonido, encargado de obtener la intensidad del sonido.
    public DetectorSonido detector;

    // Sensibilidad del detector de sonido, controla cuán reactivo es a la señal de entrada.
    public float loudnessSensibility = 320;

    // Umbral mínimo para aplicar la fuerza.
    public float threshold = 10f;

    void Update()
    {
        // Obtiene la intensidad del sonido desde el micrófono, ajustada por la sensibilidad.
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        
        // Si la intensidad del sonido supera el umbral, aplica una fuerza al objeto.
        if (loudness >= threshold)
        {
            float fuerza = 1f; // Magnitud de la fuerza aplicada.
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * fuerza, ForceMode2D.Impulse);
        }
        else
        {
            // Detiene el objeto si no hay sonido.
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}

