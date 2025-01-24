using UnityEngine;

public class Detector_tramo : MonoBehaviour
{
    public int numeroTramo; 
    private PistaCarrera pistaCarrera;

    void Start()
    {
        pistaCarrera = FindObjectOfType<PistaCarrera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pistaCarrera.ActualizarTramoJugador(numeroTramo);
        }
    }
}
