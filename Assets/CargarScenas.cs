using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargarScenas : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject canvascores;
    public string[] sceneNames;

    void OnEnable(){

        canvascores.SetActive(true);
        LoadRandomScene();

        



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
