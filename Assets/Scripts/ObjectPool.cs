using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public Transform parent;
    public int maxObject = 30;
    List<GameObject> pool;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //
    void Start()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < maxObject; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject Get()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
