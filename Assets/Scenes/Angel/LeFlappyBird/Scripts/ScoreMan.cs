using UnityEngine;

public class ScoreMan : MonoBehaviour
{
    public static ScoreMan instance;  // Instancia Singleton

    public int score = 0;  // Variable para almacenar la puntuación

    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);  // Hace que el objeto persista entre escenas
        }
        else
        {
            Destroy(gameObject);  // Elimina cualquier instancia duplicada
        }
    }

    // Método para aumentar la puntuación
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log("Puntuación actual: " + score);
    }

    // Método para reiniciar la puntuación (opcional)
    public void ResetScore()
    {
        score = 0;
    }
}