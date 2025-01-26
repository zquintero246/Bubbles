using UnityEngine;

public class ButtonNext : MonoBehaviour
{
    public void ActivateUICOSO()
    {
        // Buscar el objeto con el tag "UICOSO"
        GameObject objetoInactivo = GameObject.FindWithTag("UICOSO");

        if (objetoInactivo != null)
        {
            objetoInactivo.SetActive(true);
            Debug.Log("Objeto con tag 'UICOSO' ha sido activado.");
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'UICOSO' en la escena.");
        }
    }
}
