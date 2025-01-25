using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    private DifficultyManager dificultad;  // Variable para almacenar la referencia

    void Start()
    {
        GameObject difficultyManager = GameObject.Find("DifficultyManager");

        if (difficultyManager != null)
        {
            dificultad = difficultyManager.GetComponent<DifficultyManager>();

            if (dificultad != null)
            {
                Debug.Log("Dificultad actual: " + dificultad.contadorDificultad);
            }
            else
            {
                Debug.LogError("No se encontró el componente DifficultyManagerScript en el GameObject.");
            }
        }
        else
        {
            Debug.LogError("No se encontró el GameObject DifficultyManager.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (dificultad != null)
            {
                Debug.Log("Dificultad al ganar: " + dificultad.dificultadGlobal);
                dificultad.contadorDificultad = dificultad.contadorDificultad+1;

            }

            SceneManager.LoadScene("Carrera_burbujas");
        }
    }
}
