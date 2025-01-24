using UnityEngine;

public class CambioCarril : MonoBehaviour
{
    // Posiciones predefinidas en el eje Y
    private float[] posiciones = { 2.6f, 1.3f };

    // Índice para saber en qué posición estamos (0 = 2.6, 1 = 1.3)
    private int posicionActual = 0;

    void Start()
    {
        // Establece la posición inicial del objeto en la primera posición (2.6)
        transform.position = new Vector3(transform.position.x, posiciones[posicionActual], transform.position.z);
    }

    void Update()
    {
        CambioDeCarril();
    }

    void CambioDeCarril()
    {
        // Si se presiona la flecha derecha, cambiar a la posición más baja (1.3)
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            posicionActual = 1;  // Posición 1 corresponde a 1.3 en el arreglo
            MoverObjeto();
        }

        // Si se presiona la flecha izquierda, cambiar a la posición más alta (2.6)
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            posicionActual = 0;  // Posición 0 corresponde a 2.6 en el arreglo
            MoverObjeto();
        }
    }

    void MoverObjeto()
    {
        // Aplica la nueva posición en el eje Y, manteniendo los otros valores
        transform.position = new Vector3(transform.position.x, posiciones[posicionActual], transform.position.z);
    }
}
