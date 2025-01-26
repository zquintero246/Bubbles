using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Obstaculos : MonoBehaviour
{
    public GameObject[] partesPista;  // Referencia a las partes de la pista
    public GameObject[] obstaculos;   // Array de prefabs de obstáculos
    public Transform parentPista;     // Referencia al objeto Pista_Carrera
    public float tiempoEntreSpawn = 2.0f;  // Tiempo entre la aparición de obstáculos
    public int cantidadMaximaObstaculos = 10; // Límite de obstáculos activos
    public float radioDeSpawn = 2.0f;  // Radio mínimo entre obstáculos

    private List<GameObject> obstaculosActivos = new List<GameObject>();
    private float[] posicionesY = { 1.3f, 2.6f };  // Posiciones posibles en el eje Y

    private PistaCarrera pistaCarrera;

    void Start()
    {
        pistaCarrera = FindObjectOfType<PistaCarrera>();
        StartCoroutine(SpawnObstaculoAleatorio());
    }

    IEnumerator SpawnObstaculoAleatorio()
    {
        while (true)
        {
            yield return new WaitForSeconds(tiempoEntreSpawn);

            // Si no se ha alcanzado la cantidad máxima de obstáculos
            if (obstaculosActivos.Count < cantidadMaximaObstaculos)
            {
                int tramoJugador = pistaCarrera.ObtenerTramoJugador();

                // Obtener las partes de la pista que están más adelante del jugador
                List<GameObject> tramosDisponibles = new List<GameObject>();

                for (int i = 0; i < partesPista.Length; i++)
                {
                    Detector_tramo detector = partesPista[i].GetComponent<Detector_tramo>();
                    if (detector != null && detector.numeroTramo > tramoJugador)
                    {
                        tramosDisponibles.Add(partesPista[i]);
                    }
                }

                // Si hay tramos disponibles, instanciar obstáculo
                if (tramosDisponibles.Count > 0)
                {
                    GameObject tramoSeleccionado = tramosDisponibles[Random.Range(0, tramosDisponibles.Count)];
                    Vector3 spawnPosition = tramoSeleccionado.transform.position;

                    // Seleccionar aleatoriamente una de las posiciones posibles en el eje Y
                    spawnPosition.y = posicionesY[Random.Range(0, posicionesY.Length)];

                    // Ajustar la posición en Z para asegurarse de que esté en -1
                    spawnPosition.z = 1f;

                    // Verificar si la posición está libre dentro del radio
                    if (PosicionLibre(spawnPosition))
                    {
                        int indexObstaculo = Random.Range(0, obstaculos.Length);
                        GameObject nuevoObstaculo = Instantiate(obstaculos[indexObstaculo], spawnPosition, Quaternion.identity);

                        // Asignar la pista como padre del obstáculo
                        nuevoObstaculo.transform.SetParent(parentPista);

                        // Agregar el obstáculo a la lista de obstáculos activos
                        obstaculosActivos.Add(nuevoObstaculo);
                    }
                }
            }

            // Eliminar obstáculos fuera de pantalla
            LimpiarObstaculos();
        }
    }

    bool PosicionLibre(Vector3 spawnPosition)
    {
        foreach (GameObject obstaculo in obstaculosActivos)
        {
            if (obstaculo != null)
            {
                float distancia = Vector3.Distance(obstaculo.transform.position, spawnPosition);
                if (distancia < radioDeSpawn)
                {
                    return false;  // Hay otro obstáculo demasiado cerca
                }
            }
        }
        return true;  // La posición está libre
    }

    void LimpiarObstaculos()
    {
        for (int i = obstaculosActivos.Count - 1; i >= 0; i--)
        {
            if (obstaculosActivos[i] == null || obstaculosActivos[i].transform.position.y < -10f)
            {
                Destroy(obstaculosActivos[i]);
                obstaculosActivos.RemoveAt(i);
            }
        }
    }
}
