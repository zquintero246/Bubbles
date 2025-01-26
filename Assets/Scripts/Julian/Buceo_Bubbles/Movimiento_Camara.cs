using UnityEngine;
using System.Collections;

public class CameraAscender : MonoBehaviour
{
    public float velocidadAscenso = 2f;  // Velocidad de subida de la cámara
    public float alturaObjetivo = 10f;   // Altura máxima a la que debe llegar la cámara
    public float retrasoInicio = 2f;     // Retraso en segundos antes de iniciar la subida

    private bool haAlcanzadoAltura = false;
    private bool iniciarAscenso = false;

    void Start()
    {
        // Inicia la subida después de 'retrasoInicio' segundos
        Invoke("ComenzarAscenso", retrasoInicio);
    }

    void Update()
    {
        if (iniciarAscenso && !haAlcanzadoAltura)
        {
            // Mueve la cámara hacia arriba de forma gradual
            transform.position = Vector3.MoveTowards(
                transform.position, 
                new Vector3(transform.position.x, alturaObjetivo, transform.position.z), 
                velocidadAscenso * Time.deltaTime
            );

            // Verifica si se ha alcanzado la altura objetivo
            if (Mathf.Abs(transform.position.y - alturaObjetivo) < 0.1f)
            {
                haAlcanzadoAltura = true;
                iniciarAscenso = false;
                Debug.Log("Altura objetivo alcanzada.");
            }
        }
    }

    void ComenzarAscenso()
    {
        iniciarAscenso = true;
        haAlcanzadoAltura = false;
        Debug.Log("Ascenso iniciado después de " + retrasoInicio + " segundos.");
    }
}
