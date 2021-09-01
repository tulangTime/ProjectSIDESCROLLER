using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slice : MonoBehaviour
{

    [SerializeField] List<GameObject> splices = new List<GameObject>();
    Queue<GameObject> spliceQueue = new Queue<GameObject>();

    private void Awake()
    {
        foreach(GameObject obj in splices)
        {
            spliceQueue.Enqueue(obj);
        }
    }

    public GameObject GetSlice()
    {
        int rnd = UnityEngine.Random.Range(0, spliceQueue.Count);
        GameObject instance = spliceQueue.Dequeue();
        spliceQueue.Enqueue(instance);
        return instance;
    }
}
