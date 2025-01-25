using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;

    public float dificultadGlobal = 0f;  // Nivel de dificultad global (1 = normal, 2 = difícil, etc.)
    public float contadorDificultad = 0f;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Hacer que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update(){

        if(contadorDificultad == 2f){
            dificultadGlobal = 1;
        }else if(contadorDificultad == 4f){
            dificultadGlobal = 2;
        }else if(contadorDificultad == 6f){
            dificultadGlobal = 3;
        }else if(contadorDificultad >6f){
            dificultadGlobal = 3;
            contadorDificultad = 6;
        }

    }

    void Start()
    {
        AplicarDificultad();  // Aplicar dificultad al iniciar la escena
        SceneManager.sceneLoaded += OnSceneLoaded;  // Suscribirse al cambio de escenas
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AplicarDificultad();  // Aplicar cambios de dificultad al cargar una nueva escena
    }

    void AplicarDificultad()
    {
        string escenaActual = SceneManager.GetActiveScene().name;

        switch (escenaActual)
        {
            case "FlappyBird":
                AjustarDificultadFlappyBird();
                break;

            case "CarreraBurbuja":
                AjustarDificultadCarreraBurbuja();
                break;

            default:
                Debug.Log("Escena no reconocida para ajuste de dificultad.");
                break;
        }
    }

    void AjustarDificultadFlappyBird()
    {
        // Buscar el objeto que contiene el script del juego Flappy Bird
        Obstaculo flappyBird = FindObjectOfType<Obstaculo>();
        if (flappyBird != null)
        {
            flappyBird.velocidadMax = 6+dificultadGlobal;
            Debug.Log("Dificultad aplicada a Flappy Bird: " + dificultadGlobal);
        }
    }

    void AjustarDificultadCarreraBurbuja()
    {
        // Buscar el objeto que contiene el script de Carrera de Burbujas
        IAPlayer carrera = FindObjectOfType<IAPlayer>();
        if (carrera != null)
        {
            
            carrera.margenDeError = 0.5f-(dificultadGlobal*0.1f);
            Debug.Log("Dificultad aplicada a Carrera Burbuja: " + dificultadGlobal);
        }
    }
    

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Desuscribirse para evitar problemas
    }
}
