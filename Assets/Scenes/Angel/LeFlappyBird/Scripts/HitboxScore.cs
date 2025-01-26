using UnityEngine;

public class HitboxScore : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Verifica si la instancia de ScoreMan existe antes de acceder a ella
            if (ScoreMan.instance != null)
            {
                // Obtener la puntuación actual
                int currentScore = ScoreMan.instance.score;
                Debug.Log("Puntuación actual: " + currentScore);

                // Opcional: aumentar la puntuación al entrar en la hitbox
                ScoreMan.instance.AddScore(10);  // Aumenta la puntuación en 10
            }
            else
            {
                Debug.LogError("ScoreMan instance no está inicializada.");
            }
        }
    }
}

