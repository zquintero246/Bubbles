using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win2 : MonoBehaviour
{
    private DifficultyManager dificultad;  // Variable para almacenar la referencia
    public string[] sceneNames;
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

            LoadRandomScene();
        }
    }

    public void LoadRandomScene()
    {
        if (sceneNames.Length == 0)
        {
            Debug.LogError("No hay escenas asignadas en la lista de nombres de escenas.");
            return;
        }

        // Seleccionar una escena aleatoria
        int randomIndex = Random.Range(0, sceneNames.Length);
        string selectedScene = sceneNames[randomIndex];

        // Cargar la escena seleccionada
        SceneManager.LoadScene(selectedScene);
    }
}
