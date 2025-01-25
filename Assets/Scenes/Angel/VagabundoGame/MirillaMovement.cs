using UnityEngine;

public class MirillaController : MonoBehaviour
{
    public float acceleration = 10f;  // Aceleración de la mirilla
    public float maxSpeed = 5f;       // Velocidad máxima
    public float friction = 5f;       // Fricción para el deslizamiento
    public float cooldownRate= 1f;
    private Vector2 velocity;         // Velocidad de la mirilla
    public float CooldownTiro = 0;
    private bool EnemigoEnMira = false;

    void Update()
    {
        // Capturar entrada del teclado
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        bool inputSpace = Input.GetKey(KeyCode.Space);

        // Aplicar aceleración según la dirección de entrada
        Vector2 inputDirection = new Vector2(inputX, inputY).normalized;

        if (inputDirection.magnitude > 0)
        {
            velocity += inputDirection * acceleration * Time.deltaTime;
            velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        }
        else
        {
            // Aplicar fricción cuando no se presiona ninguna tecla
            velocity = Vector2.Lerp(velocity, Vector2.zero, friction * Time.deltaTime);
        }

        // Mover la mirilla
        transform.position += (Vector3)velocity * Time.deltaTime;
        if (CooldownTiro > 0)
        {
            // Disminuir el cooldown basado en el tiempo
            CooldownTiro -= cooldownRate * Time.deltaTime;

            // Asegurar que no baje de 0
            if (CooldownTiro < 0)
            {
                CooldownTiro = 0;
            }

            Debug.Log("Cooldown restante: " + CooldownTiro);
        if(CooldownTiro == 0 && inputSpace == true){
            Debug.Log("Bang");
            CooldownTiro = 10;
        }

        


    }
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Enemigo")){
            EnemigoEnMira = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Enemigo")){
            EnemigoEnMira = false;
            Debug.Log("Enemigo salio de mira");
        }
    }

    
}