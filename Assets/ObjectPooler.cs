using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour //https://www.youtube.com/watch?v=tdSmKaJvCoA
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject Prefab;
        public int size;
    }
    #region Singleton
    public static ObjectPooler Instance;
    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> ObjectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject Obj = Instantiate(pool.Prefab);
                Obj.transform.parent = this.transform;
                Obj.SetActive(false);
                ObjectPool.Enqueue(Obj);
            }

            PoolDictionary.Add(pool.tag, ObjectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }
        GameObject ObjectToSpawn = PoolDictionary[tag].Dequeue();
        ObjectToSpawn.SetActive(true);
        ObjectToSpawn.transform.position = position;
        ObjectToSpawn.transform.rotation = rotation;

        PoolDictionary[tag].Enqueue(ObjectToSpawn);
        return ObjectToSpawn;

    }

    public void ResetPool(string tag)
    {
        Debug.Log("resetting pool");
        if (!PoolDictionary.ContainsKey(tag))
        {
            return;
        }

        for (int i = 0; i < PoolDictionary[tag].Count;i++)
        {
            GameObject GridCube = PoolDictionary[tag].Dequeue();
            GridCube.SetActive(false);
            PoolDictionary[tag].Enqueue(GridCube);
        }
        

    }


    

}
