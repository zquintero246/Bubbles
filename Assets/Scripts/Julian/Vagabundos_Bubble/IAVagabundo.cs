using UnityEngine;
using System.Collections;

public class IAVagabundo : MonoBehaviour
{
    public Transform puntoA; 
    public Transform puntoB;  
    public float velocidad = 3f;
    public float fuerzaSalto = 5f;
    public float tiempoQuietoMin = 1f;  
    public float tiempoQuietoMax = 3f;  
    public float probabilidadSalto = 0.3f; 

    private Rigidbody2D rb;
    private bool moviendoHaciaB = true;
    private bool estaQuieto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Patrullar());
    }

    void Update()
    {
        if (!estaQuieto)
        {
            Mover();
        }
    }

    void Mover()
    {
        Vector2 destino = moviendoHaciaB ? puntoB.position : puntoA.position;
        transform.position = Vector2.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, destino) < 0.1f)
        {
            moviendoHaciaB = !moviendoHaciaB;
            StartCoroutine(EsperarYAccion());
        }
    }

    IEnumerator EsperarYAccion()
    {
        estaQuieto = true;
        float tiempoQuieto = Random.Range(tiempoQuietoMin, tiempoQuietoMax);
        yield return new WaitForSeconds(tiempoQuieto);

        if (Random.value < probabilidadSalto) 
        {
            Saltar();
        }

        estaQuieto = false;
    }

    void Saltar()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            Debug.Log("IA ha saltado!");
        }
    }

    private void OnDrawGizmos()
    {
        if (puntoA != null && puntoB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(puntoA.position, puntoB.position);
        }
    }
    IEnumerator Patrullar()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));
            if (Random.value < probabilidadSalto)
            {
                Saltar();
            }
        }
    }

}
