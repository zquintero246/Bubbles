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

    [SerializeField] float spawnForce;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("spawnObstacle");
    }

    IEnumerator spawnObstacle()
    {
        yield return new WaitForSeconds(Random.Range(1f, 2.5f));

        if (maxHeightLimits.x < bubbleTarget.transform.position.y && bubbleTarget.transform.position.y < maxHeightLimits.y)
        {
            GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Count)];
            spawnRight = Random.Range(0, 2) == 0;
            float xPosition = spawnRight ? 11 : -11;
            float yPosition = Random.Range(spawnHeightLimits.x, spawnHeightLimits.y);

            Vector3 spawnPosition = new Vector3(xPosition, yPosition + bubbleTarget.transform.position.y, 0);
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            spawnedObject.transform.localScale *= spawnRight ? new Vector2(transform.localScale.x, transform.localScale.y) : new Vector2(transform.localScale.x * -1, transform.localScale.y);

            if (bubbleTarget != null)
            {
                Vector3 direction = new Vector3(bubbleTarget.transform.position.x - spawnedObject.transform.position.x, bubbleTarget.transform.position.y - spawnedObject.transform.position.y + 2.5f, bubbleTarget.transform.position.z - spawnedObject.transform.position.z).normalized;

                Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(direction * spawnForce, ForceMode2D.Impulse);
                }
            }
        }

        StartCoroutine(spawnObstacle());
    }
}