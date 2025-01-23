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

    private List<GameObject> obstaculosActivos = new List<GameObject>();
    private float[] posicionesY = { 1.3f, 2.6f };  // Posiciones posibles en el eje Y

    void Start()
    {
        // Iniciar la generación de obstáculos periódicamente
        StartCoroutine(SpawnObstaculoAleatorio());
    }

    IEnumerator SpawnObstaculoAleatorio()
    {
        while (true)
        {
            // Espera antes de spawnear el siguiente obstáculo
            yield return new WaitForSeconds(tiempoEntreSpawn);

            // Si no se ha alcanzado la cantidad máxima de obstáculos
            if (obstaculosActivos.Count < cantidadMaximaObstaculos)
            {
                // Elegir una parte aleatoria de la pista
                int indexParte = Random.Range(0, partesPista.Length);
                Vector3 spawnPosition = partesPista[indexParte].transform.position;

                // Seleccionar aleatoriamente una de las posiciones posibles en el eje Y
                spawnPosition.y = posicionesY[Random.Range(0, posicionesY.Length)];

                // Ajustar la posición en Z para asegurarse de que esté en -1
                spawnPosition.z = -1f;

                // Elegir un obstáculo aleatorio
                int indexObstaculo = Random.Range(0, obstaculos.Length);
                GameObject nuevoObstaculo = Instantiate(obstaculos[indexObstaculo], spawnPosition, Quaternion.identity);

                // Asignar la pista como padre del obstáculo
                if (parentPista != null)
                {
                    nuevoObstaculo.transform.SetParent(parentPista);
                }
                else
                {
                    Debug.LogWarning("No se ha asignado la referencia a Pista_Carrera.");
                }

                // Agregar el obstáculo a la lista de obstáculos activos
                obstaculosActivos.Add(nuevoObstaculo);
            }

            // Eliminar obstáculos fuera de pantalla
            LimpiarObstaculos();
        }
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
