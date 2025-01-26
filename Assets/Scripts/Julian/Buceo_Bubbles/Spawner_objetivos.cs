using UnityEngine;

public class SpawnerObjetivos : MonoBehaviour
{
    public GameObject objetivoPrefab;  // Prefab del objetivo a generar
    public int cantidadObjetivos = 10;  // Número de objetivos a spawnear
    public Vector2 limiteInferior;  // Coordenadas mínimas (X, Y) del área de spawn
    public Vector2 limiteSuperior;  // Coordenadas máximas (X, Y) del área de spawn

    void Start()
    {
        SpawnObjetivos();
    }

    void SpawnObjetivos()
    {
        for (int i = 0; i < cantidadObjetivos; i++)
        {
            // Generar coordenadas aleatorias dentro de los límites definidos
            float randomX = Random.Range(limiteInferior.x, limiteSuperior.x);
            float randomY = Random.Range(limiteInferior.y, limiteSuperior.y);
            float fixedZ = -1f;  // Fijar la posición en Z a -1

            Vector3 spawnPosition = new Vector3(randomX, randomY, fixedZ);

            // Instanciar el objetivo en la posición generada
            Instantiate(objetivoPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Visualización del área de spawn en la vista de escena
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((limiteInferior.x + limiteSuperior.x) / 2, 
                                     (limiteInferior.y + limiteSuperior.y) / 2, 
                                     -1f);  // Asegurar Z = -1 para la visualización
        Vector3 size = new Vector3(limiteSuperior.x - limiteInferior.x, 
                                   limiteSuperior.y - limiteInferior.y, 
                                   0);
        Gizmos.DrawWireCube(center, size);
    }
}
