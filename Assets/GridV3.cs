using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridV3 : MonoBehaviour
{
    public List<GameObject> GridGameObjects = new List<GameObject>();



    [Header ("FromVid")] ////https://www.youtube.com/watch?v=WJimYq2Tczc
    public float X_Start; //default = -4.5
    public float Y_Start; // default = 6.8

    public int Length; // default = 7
    public int Width; //default = 10

    public float X_Space; //default = 1.5
    public float Y_Space; //default = 1.5
    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            CreateGrid();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            DeleteGrid();
        }
    }

    public void CreateGrid()
    {
        for (int i = 0; i < Length * Width; i++) //from video //https://www.youtube.com/watch?v=WJimYq2Tczc
        {
            GridGameObjects.Add(Instantiate(prefab, new Vector3(X_Start + (X_Space * (i % Length)), transform.position.y, -Y_Start + (Y_Space * (i / Length))), Quaternion.identity));
        }
    }

    public void DeleteGrid()
    {
        for (int i = 0; i < GridGameObjects.Count; i++)
        {
            Destroy(GridGameObjects[i]);
        }
    }
}
