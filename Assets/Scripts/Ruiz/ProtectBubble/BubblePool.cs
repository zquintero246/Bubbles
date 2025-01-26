using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubblePool : MonoBehaviour
{
    [SerializeField] List<GameObject> poolList = new List<GameObject>();
    [SerializeField] GameObject objectToPool;
    [SerializeField] int totalBubbles;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < totalBubbles; i++)
        {
            poolList.Add(Instantiate(objectToPool));
            poolList[i].SetActive(false);
            poolList[i].transform.parent = transform;
        }
    }

    public GameObject SpawnBubble(Vector3 position, Quaternion rotation)
    {
        GameObject bubbleToReturn;

        if (poolList.Count > 0)
        {
            bubbleToReturn = poolList[0];
            poolList.RemoveAt(0);
        } else
        {
            bubbleToReturn = Instantiate(objectToPool);
            bubbleToReturn.transform.parent = transform;
        }

        bubbleToReturn.SetActive(true);
        bubbleToReturn.transform.position = position;
        bubbleToReturn.transform.rotation = rotation;

        return bubbleToReturn;
    }

    public void ReturnBubble(GameObject BubbleToReturn)
    {
        poolList.Add(BubbleToReturn);
        BubbleToReturn.SetActive(false);
    }
}
