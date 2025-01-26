using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [Header("Cloud Prefabs")]
    public GameObject[] cloudPrefabs; // Array de prefabs de nubes

    [Header("Spawn Settings")]
    public int cloudCount = 10; // Número total de nubes a generar
    public Vector2 leftSpawnRange = new Vector2(-10f, 10f); // Rango de spawn a la izquierda
    public Vector2 rightSpawnRange = new Vector2(10f, 20f); // Rango de spawn a la derecha
    public Vector2 heightRange = new Vector2(-5f, 5f); // Rango de altura para spawn

    [Header("Opacity Settings")]
    public float minOpacity = 0.5f; // Opacidad mínima
    public float maxOpacity = 1f; // Opacidad máxima

    [Header("Size Settings")]
    public Vector2 sizeRange = new Vector2(0.5f, 1.5f); // Rango para el tamaño de las nubes

    private void Start()
    {
        SpawnClouds();
    }

    void SpawnClouds()
    {
        for (int i = 0; i < cloudCount; i++)
        {
            // Selecciona un prefab de nube al azar
            GameObject cloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

            // Decide si la nube se generará a la izquierda o a la derecha
            bool spawnOnLeft = Random.Range(0, 2) == 0;

            // Calcula la posición de la nube
            float xPosition = spawnOnLeft
                ? Random.Range(leftSpawnRange.x, leftSpawnRange.y)
                : Random.Range(rightSpawnRange.x, rightSpawnRange.y);
            float yPosition = Random.Range(heightRange.x, heightRange.y);
            Vector3 spawnPosition = new Vector3(xPosition, yPosition, 0);

            // Instancia la nube
            GameObject cloud = Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);

            // Cambia la opacidad del color de la nube
            SpriteRenderer sr = cloud.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color cloudColor = sr.color;
                cloudColor.a = Random.Range(minOpacity, maxOpacity);
                sr.color = cloudColor;
            }

            // Ajusta el tamaño de la nube de manera aleatoria
            float randomSize = Random.Range(sizeRange.x, sizeRange.y);
            cloud.transform.localScale = new Vector3(randomSize, randomSize, 1);

            // Opcional: puedes hacer que las nubes sean hijas del spawner para organizarlas mejor
            cloud.transform.parent = transform;
        }
    }
}
