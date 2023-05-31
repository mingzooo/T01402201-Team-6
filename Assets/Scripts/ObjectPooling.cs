using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    [SerializeField]
    private GameObject objectPrefab;
    Queue<GameObject> ObjectPool = new Queue<GameObject>();
    public static ObjectPooling instance = null;

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            for(int i = 0; i < 30; i++)
            {
                CreateObject();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    GameObject CreateObject()
    {
        GameObject newObj = Instantiate(objectPrefab, instance.transform);
        newObj.gameObject.SetActive(false);
        return newObj;
    }
    public static GameObject GetObject()
    {
        if (instance.ObjectPool.Count > 0)
        {
            GameObject objectInPool = instance.ObjectPool.Dequeue();

            objectInPool.gameObject.SetActive(true);
            objectInPool.transform.SetParent(null);
            return objectInPool;
        }
        else
        {
            GameObject objectInPool = instance.CreateObject();

            objectInPool.gameObject.SetActive(true);
            objectInPool.transform.SetParent(null);
            return objectInPool;
        }
    }
    public static void ReturnObjectToQueue(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(instance.transform);
        instance.ObjectPool.Enqueue(obj);
    }
}
