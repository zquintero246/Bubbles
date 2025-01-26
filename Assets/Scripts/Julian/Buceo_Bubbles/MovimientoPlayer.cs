using UnityEngine;

public class MovimientoPlayer : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del personaje

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        movement.y = Input.GetAxisRaw("Vertical");   
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * speed;
    }
}
