using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public static BubbleController instance;

    [SerializeField] float speed;
    [SerializeField] float shootSpeed;
    [SerializeField] int health = 3;
    [SerializeField] GameObject Wincoso;
    [SerializeField] GameObject direction;
    [SerializeField] GameObject directionArrow;
    [SerializeField] GameObject shooterBubble;
    [SerializeField] Transform cameraTransform;
    [SerializeField] Transform bubbleTransform;

    private Vector3 finalTarget;

    [SerializeField] public BubblePool pool;
    [SerializeField] Animator animator;
    [SerializeField] RectTransform heartUI;
    [SerializeField] int spriteSize = 64;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Time.timeScale = 0f;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float radianAngle = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        float angle = (180 / Mathf.PI) * radianAngle - 90;
        direction.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            //GameObject shootedBubble = Instantiate(shooterBubble, directionArrow.transform.position, direction.transform.localRotation);
            GameObject shootedBubble = pool.SpawnBubble(directionArrow.transform.position, direction.transform.localRotation);
            finalTarget = (mousePosition - transform.position).normalized;
            shootedBubble.GetComponent<Rigidbody2D>().AddForce(finalTarget * shootSpeed, ForceMode2D.Impulse);
        }

        if (transform.position.y >= 82.5f && cameraTransform.parent == bubbleTransform)
        {
            detachCamera();

            Invoke(nameof(ActivarWinManager), 3f);
        }
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * speed * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            health -= 1;
            animator.Play("bubbleHurt");
            heartUI.sizeDelta = new Vector2(health * spriteSize, spriteSize);
        }
    }

    public void detachCamera()
    {
        cameraTransform.SetParent(null);
    }

void ActivarWinManager()
    {
        Wincoso.SetActive(true);
        Debug.Log("WinManager activado después de 2 segundos.");
    }
}
