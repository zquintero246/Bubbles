using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Starting : MonoBehaviour
{
    public string[] sceneNames;
    public GameObject scoreDisplayer;	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Return))
        {
             scoreDisplayer.SetActive(true);
             int randomIndex = Random.Range(0, sceneNames.Length);
            string nextScene = sceneNames[randomIndex];
            Debug.Log("Cambiando a la escena: " + nextScene);
            SceneManager.LoadScene(nextScene);

        }
    }
}
