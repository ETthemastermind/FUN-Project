using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridV3 : MonoBehaviour //https://www.youtube.com/watch?v=WJimYq2Tczc
{
    public float X_Start;
    public float Y_Start;

    public int Length;
    public int Width;

    public float X_Space;
    public float Y_Space;
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Length * Width; i++)
        {
            Instantiate(prefab, new Vector3(X_Start + (X_Space * (i % Length)),transform.position.y, -Y_Start + (Y_Space * (i / Length))), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
