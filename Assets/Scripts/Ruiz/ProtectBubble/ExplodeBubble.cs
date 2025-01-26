using System.Collections;
using System.Collections.Generic;
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
}
