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

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    // Start is called before the first frame update
    void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
    }

}
