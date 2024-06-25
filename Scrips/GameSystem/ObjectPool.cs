using System;
using System.Collections.Generic;
using UnityEngine;

public enum TAG
{
    MobCharacter,
    CSE,
}

public class ObjectPool : MonoBehaviour
{

    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
        public Transform parent;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(pool.parent);  // 부모 트랜스폼 설정
                objectPool.Enqueue(obj);
            }

            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Transform parent)
    {
        if (!PoolDictionary.ContainsKey(tag)) return null;

        GameObject obj = PoolDictionary[tag].Dequeue();
        obj.SetActive(true);
        PoolDictionary[tag].Enqueue(obj);
        obj.transform.SetParent(parent);
        return obj;
    }
}
