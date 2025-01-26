using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Configuración del Spawner")]
    public GameObject objetoPrefab;  // Prefab del objeto a spawnear
    public Transform limiteInferiorIzquierdo;  // Objeto delimitador inferior izquierdo
    public Transform limiteSuperiorDerecho;    // Objeto delimitador superior derecho

    [Header("Parámetros del Spawner")]
    public int maxObjetos = 10;  // Número máximo de objetos permitidos en la zona
    public float intervaloSpawn = 2f;  // Tiempo entre cada spawn

    private int objetosActuales = 0;  // Contador de objetos actuales en escena

    void Start()
    {
        if (limiteInferiorIzquierdo == null || limiteSuperiorDerecho == null)
        {
            Debug.LogError("Los límites de spawn no están asignados.");
            return;
        }

        InvokeRepeating(nameof(SpawnObjeto), 0f, intervaloSpawn);
    }

    void SpawnObjeto()
    {
        if (objetosActuales < maxObjetos)
        {
            Vector3 spawnPosition = ObtenerPosicionAleatoria();
            Instantiate(objetoPrefab, spawnPosition, Quaternion.identity);
            objetosActuales++;
        }
    }

    Vector3 ObtenerPosicionAleatoria()
    {
        float posX = Random.Range(limiteInferiorIzquierdo.position.x, limiteSuperiorDerecho.position.x);
        float posY = Random.Range(limiteInferiorIzquierdo.position.y, limiteSuperiorDerecho.position.y);
        return new Vector3(posX, posY, 0f);
    }

    void OnDrawGizmos()
    {
        if (limiteInferiorIzquierdo != null && limiteSuperiorDerecho != null)
        {
            Gizmos.color = Color.green;
            Vector3 center = (limiteInferiorIzquierdo.position + limiteSuperiorDerecho.position) / 2;
            Vector3 size = new Vector3(
                Mathf.Abs(limiteSuperiorDerecho.position.x - limiteInferiorIzquierdo.position.x),
                Mathf.Abs(limiteSuperiorDerecho.position.y - limiteInferiorIzquierdo.position.y),
                1);

            Gizmos.DrawWireCube(center, size);
        }
    }
}
