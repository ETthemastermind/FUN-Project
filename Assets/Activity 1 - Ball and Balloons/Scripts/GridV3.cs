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

    [Header("Grid Values")]
    public float[][] GridValueArray = new float[5][];

    public float[] GridPos1 = new float [4] { 5, 9, 2.25f, 1.85f };
    public float[] GridPos2 = new float[4] {6,10,1.78f,1.65f };
    public float[] GridPos3 = new float[4] {7,11,1.54f,1.5f };
    public float[] GridPos4 = new float[4] {8,12,1.3f,1.35f };
    public float[] GridPos5 = new float[4] {9,13,1.13f,1.24f };

    
    public int CurrentGrid = 1;

    // Start is called before the first frame update
    void Start()
    {
        
        GridValueArray[0] = GridPos1;
        GridValueArray[1] = GridPos2;
        GridValueArray[2] = GridPos3;
        GridValueArray[3] = GridPos4;
        GridValueArray[4] = GridPos5;
        

        //Debug.Log(GridValueArray[0][2]);
        CreateGrid();
        //Debug.Log((gameObject.GetComponent<Renderer>().bounds.size.x) / Height);
        //Debug.Log((gameObject.GetComponent<Renderer>().bounds.size.z) / Width);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowHideGrid();
        }
    }

    public void CreateGrid()
    {
        float[] Temp = GridValueArray[CurrentGrid];
        Height = (int)Temp[0];
        Width = (int)Temp[1];
        X_Space = Temp[2];
        Y_Space = Temp[3];
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

    public void NextGrid()
    {
        CurrentGrid++;
        {
            if (CurrentGrid > GridValueArray.Length - 1)
            {
                CurrentGrid = GridValueArray.Length - 1;
                
                //DeleteGrid();
                //CreateGrid();
            }
            DeleteGrid();
            CreateGrid();
        }
    }

    public void LastGrid()
    {
        CurrentGrid--;
        {
            if (CurrentGrid < 0)
            {
                CurrentGrid = 0;
                //DeleteGrid();
                //CreateGrid();
            }

            DeleteGrid();
            CreateGrid();

        }

    }

    public void ShowHideGrid()
    {
        if (GridGameObjects[0].GetComponent<MeshRenderer>().enabled == true)
        {
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }


}
