using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{

    private float velocidad =5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    void Mover(){
        transform.position += Vector3.right * Time.deltaTime * velocidad;
    }
}
