using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    public GameObject[] obstaclePrefabs;
    public int initialPoolSize = 10;

    private Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (GameObject prefab in obstaclePrefabs)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            for (int i = 0; i < initialPoolSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(false);
                pool.Enqueue(obj);
            }
            objectPool.Add(prefab.name, pool);
        }
    }

    public GameObject GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!objectPool.ContainsKey(prefab.name))
        {
            Debug.LogWarning("Object pool does not contain prefab: " + prefab.name);
            return null;
        }

        Queue<GameObject> pool = objectPool[prefab.name];
        if (pool.Count == 0)
        {
            Debug.LogWarning("Object pool is empty for prefab: " + prefab.name);
            return null;
        }

        GameObject obj = pool.Dequeue();
        obj.SetActive(true);
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        return obj;
    }

    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool[obj.name].Enqueue(obj);
    }
}
