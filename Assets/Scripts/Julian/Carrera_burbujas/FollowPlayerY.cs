using UnityEngine;

public class FollowPlayerFixedY : MonoBehaviour
{
    public Transform objetivo;  // Referencia al jugador u objeto a seguir
    public float yFijo = 5f;     // Posición fija en el eje Y
    public float suavizado = 5f; // Velocidad de seguimiento

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Solo interpola en los ejes X y Z, manteniendo el Y fijo
            Vector3 posicionDeseada = new Vector3(objetivo.position.x, yFijo, objetivo.position.z - 2);
            Vector3 posicionSuavizada = new Vector3(
                Mathf.Lerp(transform.position.x, posicionDeseada.x, Time.deltaTime * suavizado),
                yFijo,  // Mantiene la altura fija
                Mathf.Lerp(transform.position.z, posicionDeseada.z, Time.deltaTime * suavizado)
            );

            transform.position = posicionSuavizada;
        }
    }
}
