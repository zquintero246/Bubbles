using UnityEngine;

public class FollowPlayerFixedY : MonoBehaviour
{
    public Transform objetivo;  // Referencia al jugador u objeto a seguir
    public float yFijo = 5f;     // Posición fija en el eje Y
    public float suavizado = 5f; // Velocidad de seguimiento

    private float offsetX;       // Almacenar el offset en X

    void Start()
    {
        if (objetivo != null)
        {
            // Calcula el offset inicial en X basado en la posición inicial de la cámara respecto al objetivo
            offsetX = transform.position.x - objetivo.position.x;
        }
    }

    void LateUpdate()
    {
        if (objetivo != null)
        {
            // Mantener el offset en X y ajustar la posición en Z suavemente
            Vector3 posicionDeseada = new Vector3(objetivo.position.x + offsetX, yFijo, objetivo.position.z - 2);
            Vector3 posicionSuavizada = new Vector3(
                Mathf.Lerp(transform.position.x, posicionDeseada.x, Time.deltaTime * suavizado),
                yFijo,  // Mantiene la altura fija en Y
                Mathf.Lerp(transform.position.z, posicionDeseada.z, Time.deltaTime * suavizado)
            );

            transform.position = posicionSuavizada;
        }
    }
}