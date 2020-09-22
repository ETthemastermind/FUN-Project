using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridV3 : MonoBehaviour
{
    public List<GameObject> GridGameObjects = new List<GameObject>();

    

    [Header ("FromVid")] ////https://www.youtube.com/watch?v=WJimYq2Tczc
    public float X_Start; //default = -4.5
    public float Y_Start; // default = 6.8

    public int Height; // default = 7
    public int Width; //default = 11

    public float X_Space; //default = 1.5
    public float Y_Space; //default = 1.5
    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        Debug.Log((gameObject.GetComponent<Renderer>().bounds.size.x) / Height);
        Debug.Log((gameObject.GetComponent<Renderer>().bounds.size.z) / Width);
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



        if (Input.GetKeyDown(KeyCode.U))
        {

            RowUp();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            RowDown();

        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ColumnUp();

        }
        if (Input.GetKeyDown(KeyCode.P))
        {

            ColumnDown();
        }






    }

    public void CreateGrid()
    {
        for (int i = 0; i < Height * Width; i++) //from video //https://www.youtube.com/watch?v=WJimYq2Tczc
        {
            GridGameObjects.Add(Instantiate(prefab, new Vector3(X_Start + (X_Space * (i % Height)), transform.position.y - 0.53f, -Y_Start + (Y_Space * (i / Height))), Quaternion.identity));
        }
    }

    public void DeleteGrid()
    {
        for (int i = 0; i < GridGameObjects.Count; i++)
        {
            Destroy(GridGameObjects[i]);
        }
    }

    public void RowUp()
    {
        Height++;
        X_Space -= 0.375f;
        DeleteGrid();
        CreateGrid();
    }

    public void RowDown()
    {
        Height--;
        X_Space += 0.375f;
        DeleteGrid();
        CreateGrid();
    }

    public void ColumnUp()
    {
        Width++;
    }

    public void ColumnDown()
    {
        Width--;
    }
}
