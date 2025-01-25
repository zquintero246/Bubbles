using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Para TextMeshPro

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Referencia al componente de texto

    void Start()
    {
        if (ScoreMan.instance == null)
        {
            Debug.LogError("ScoreMan no encontrado en la escena.");
            return;
        }
        
        UpdateScoreUI();  // Actualiza la UI al inicio
    }

    void Update()
    {
        UpdateScoreUI();  // Actualiza el texto en cada frame (puedes optimizarlo usando eventos)
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + ScoreMan.instance.score.ToString();
    }
}