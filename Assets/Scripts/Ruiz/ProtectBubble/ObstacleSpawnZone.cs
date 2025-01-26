using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnZone : MonoBehaviour
{
    [SerializeField] List<GameObject> objectsToSpawn = new List<GameObject>();
    [SerializeField] GameObject bubbleTarget;

    //SpawnLimits
    [SerializeField] Vector2 maxHeightLimits = new Vector2(-12.5f, 12.5f);
    [SerializeField] Vector2 spawnHeightLimits = new Vector2(-5, 5);
    [SerializeField] bool spawnRight;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnObstacle());
    }

    IEnumerator spawnObstacle()
    {
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
        spawnRight = Random.Range(0, 2) == 0;
        float xPosition = spawnRight ? 11 : -11;
        float yPosition = Random.Range(spawnHeightLimits.x, spawnHeightLimits.y);

        yield return new WaitForSeconds(Random.Range(1f, 2.5f));
        StartCoroutine(spawnObstacle());
    }
}