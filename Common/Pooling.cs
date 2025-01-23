using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    [SerializeField] GameObject objectPrefab;
    [SerializeField] protected Transform objectParent;
    [SerializeField] int minimumCount;

    public Action OnInit { private get; set; }
    public Action OnReturn { private get; set; }

    protected bool isWorldParent = false;

    private Queue<GameObject> pool = new Queue<GameObject>();

    protected virtual void Awake()
    {
        objectParent = objectParent == null ? this.transform : objectParent;
    }

    public GameObject GetObject()
    {
        if (objectPrefab == null) return null;

        OnInit?.Invoke();

        if (minimumCount > pool.Count)
        {
            GameObject obj = Instantiate(objectPrefab, objectParent, false);
            obj.transform.localPosition = Vector3.zero;

            if (isWorldParent)
                obj.transform.SetParent(null);

            return obj;
        }
        else
        {
            if (pool.Count == 0)
                return null;

            GameObject obj = pool.Dequeue();
            obj.SetActive(true);

            return obj;
        }
    }

    public GameObject GetLastObject()
    {
        return pool.Peek();
    }

    public void ReturnObject(GameObject obj)
    {
        OnReturn?.Invoke();

        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    public IEnumerator ReturnAfterTime(float time, GameObject obj)
    {
        yield return new WaitForSeconds(time);

        ReturnObject(obj);
    }

    protected void AddObjects(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(objectPrefab, objectParent, false);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}
