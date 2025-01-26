using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodeBubble : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", 4f);
    }

    void Explode()
    {
        //Destroy(gameObject);
        BubbleController.instance.pool.ReturnBubble(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Explode();
        Transform parentTransform = collision.gameObject.transform;
        foreach (Transform child in parentTransform)
        {
            child.gameObject.SetActive(true);
        }
        collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.GetComponent<Rigidbody2D>().gravityScale = -0.9f;
        collision.GetComponent<ParticleSystem>().Play();
        collision.GetComponent<Collider2D>().enabled = false;
    }
}
