using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public static BubbleController instance;

    [SerializeField] float speed;
    [SerializeField] float shootSpeed;

    [SerializeField] GameObject direction;
    [SerializeField] GameObject directionArrow;
    [SerializeField] GameObject shooterBubble;

    private Vector3 finalTarget;

    [SerializeField] public BubblePool pool;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    void FixedUpdate()
    {
        transform.position += Vector3.up * speed * Time.fixedDeltaTime;
    }
}
