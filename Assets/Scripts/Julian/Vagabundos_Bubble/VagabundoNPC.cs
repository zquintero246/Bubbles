using UnityEngine;
using System.Collections;

public class NPCPatrol3D : MonoBehaviour
{
    public Transform boundary1;  // Primer objeto delimitador (esquina inferior)
    public Transform boundary2;  // Segundo objeto delimitador (esquina superior)
    public float speed = 3.0f;   // Velocidad del NPC
    public float waitTime = 1.0f; // Reducimos el tiempo de espera
    public float minMoveTime = 3.0f; // Tiempo mínimo de movimiento antes de detenerse

    private Vector3 targetPosition;
    private bool isMoving = true;

    void Start()
    {
        ChooseNewTarget();
        StartCoroutine(MoveRoutine());
    }

    void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)  // Aumentar tolerancia para mejorar el flujo
        {
            StartCoroutine(WaitBeforeMoving());
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        isMoving = false;
        yield return new WaitForSeconds(waitTime);
        ChooseNewTarget();
        isMoving = true;
    }

    void ChooseNewTarget()
    {
        float minX = Mathf.Min(boundary1.position.x, boundary2.position.x);
        float maxX = Mathf.Max(boundary1.position.x, boundary2.position.x);
        float minY = Mathf.Min(boundary1.position.y, boundary2.position.y);
        float maxY = Mathf.Max(boundary1.position.y, boundary2.position.y);
        float minZ = Mathf.Min(boundary1.position.z, boundary2.position.z);
        float maxZ = Mathf.Max(boundary1.position.z, boundary2.position.z);

        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);  // Movimiento en el eje Y (sube y baja)
        float randomZ = Random.Range(minZ, maxZ);

        targetPosition = new Vector3(randomX, randomY, randomZ);
    }

    void OnDrawGizmos()
    {
        if (boundary1 != null && boundary2 != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(
                (boundary1.position + boundary2.position) / 2, 
                new Vector3(
                    Mathf.Abs(boundary1.position.x - boundary2.position.x),
                    Mathf.Abs(boundary1.position.y - boundary2.position.y),
                    Mathf.Abs(boundary1.position.z - boundary2.position.z)
                )
            );
        }
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            isMoving = true;
            yield return new WaitForSeconds(minMoveTime);  // Se mueve un tiempo mínimo antes de detenerse
            yield return new WaitUntil(() => Vector3.Distance(transform.position, targetPosition) < 0.5f);
            isMoving = false;
            yield return new WaitForSeconds(waitTime);
            ChooseNewTarget();
        }
    }
}