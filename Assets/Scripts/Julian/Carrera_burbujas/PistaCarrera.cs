using UnityEngine;

public class PistaCarrera : MonoBehaviour
{
    private int tramoActualJugador = 0;

    public void ActualizarTramoJugador(int nuevoTramo)
    {
        if (nuevoTramo > tramoActualJugador)
        {
            tramoActualJugador = nuevoTramo;
            Debug.Log("Jugador ahora en tramo: " + tramoActualJugador);
        }
    }

    public int ObtenerTramoJugador()
    {
        return tramoActualJugador;
    }
}
