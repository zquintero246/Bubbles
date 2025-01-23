using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMicrophone : MonoBehaviour
{
    // Start is called before the first frame update
     public AudioSource source;
    public Vector2 minScale;
    public Vector2 maxScale;

    public DetectorSonido detector;

    public float loudnessSensibility = 100;
    public float threshold = 10f;
    void Update()
    {
        float loudness = detector.GetLoudnessFromMicrophone() * loudnessSensibility;
        if(loudness >= threshold){
            float fuerza = 1f; 
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * fuerza, ForceMode2D.Impulse);

        }

        
        

        // Aplicando la escala en 2D (manteniendo Z en 1 para evitar problemas en 2D)
       
    }
}
