using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GridV3 : MonoBehaviour
{
    public List<GameObject> GridGameObjects = new List<GameObject>();
    public GameObject PlayerBall;
    public float[] CurrentGridParam;

    [Header("FromVid - changing these value does nothing, its just for debug")] ////https://www.youtube.com/watch?v=WJimYq2Tczc
    public float X_Start; //default = -4.5
    public float Y_Start; // default = 6.8

    public int Height; // default = 7
    public int Width; //default = 11

    public float X_Space; //default = 1.5
    public float Y_Space; //default = 1.5
    public GameObject prefab;

    [Header("Grid Values")]

    private float[][] GridValueArray = new float[5][];
    public float[] GridPos1 = new float[6] {(float)-4.138,(float)6.957, 5, 9, (float)2.06, (float)1.765};
    public float[] GridPos2 = new float[6] {(float)-4.3, (float)7.05, 6, 10, (float)1.72, (float)1.59};
    public float[] GridPos3 = new float[6] {(float)-4.45, (float)7.1, 7, 11, (float)1.48, (float)1.44};
    public float[] GridPos4 = new float[6] {(float)-4.538, (float)7.194, 8, 12, (float)1.29, (float)1.325};
    public float[] GridPos5 = new float[6] {(float)-4.603, (float)7.24, 9, 13, (float)1.145, (float)1.222 };
    

    public int CurrentGrid;
    ///public bool GridHidden = false;

    public bool GridLinesHidden = false;
    public bool GridBoxesHidden = false;

    int CurrentY = 1;
    int CurrentX = 1;

    public Texture FourDirGrid_Tex;
    public Texture EightDirGrid_Tex;


    public MasterTelemetrySystem TelSystem;
    public void InitGridValues()
    {
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        PlayerBall = GameObject.FindGameObjectWithTag("Player");
        GridValueArray[0] = GridPos1;
        GridValueArray[1] = GridPos2;
        GridValueArray[2] = GridPos3;
        GridValueArray[3] = GridPos4;
        GridValueArray[4] = GridPos5;
        CreateGrid(2);
    }
    
    // Start is called before the first frame update
    void Start()
    {

        


        //Debug.Log(GridValueArray[0][2]);
        //CreateGrid();
        //Debug.Log((gameObject.GetComponent<Renderer>().bounds.size.x) / Height);
        //Debug.Log((gameObject.GetComponent<Renderer>().bounds.size.z) / Width);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowHideGridLines();
            
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ShowHideGridBoxes();
        }
        */
    }

    public void CreateGrid(int CG)
    {
        
        CurrentX = 1;
        CurrentY = 1;
        float[] Temp = GridValueArray[CG];
        X_Start = Temp[0];
        Y_Start = Temp[1];
        Height = (int)Temp[2];
        Width = (int)Temp[3];
        X_Space = Temp[4];
        Y_Space = Temp[5];

        TelSystem.AddLine("Grid of " + Height + " x " + Width + " created");
        for (int i = 0; i < Height * Width; i++) //from video //https://www.youtube.com/watch?v=WJimYq2Tczc
        {
            GameObject G = Instantiate(prefab, new Vector3(X_Start + (X_Space * (i % Height)), transform.position.y - 0.45f, -Y_Start + (Y_Space * (i / Height))), Quaternion.identity);
            GridGameObjects.Add(G);
            G.transform.parent = this.gameObject.transform;
            GridAttributes GA = G.GetComponent<GridAttributes>();
            if (CurrentY != Height + 1)
            {
                GA.GridCoords = new Vector2(CurrentX,CurrentY);
                CurrentY++;

            }
            else
            {
                CurrentY = 1;
                CurrentX++;
                GA.GridCoords = new Vector2(CurrentX, CurrentY);
                CurrentY++;

            }


        }
        
        Material gridLines = gameObject.GetComponent<Renderer>().material;
        gridLines.mainTextureScale = new Vector2(Height, Width);
        if (GridLinesHidden == true)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }

        if (GridBoxesHidden == true)
        {
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = false;

            }
        }

        

    }

    public void DeleteGrid()
    {
        for (int i = 0; i < GridGameObjects.Count; i++)
        {
            Destroy(GridGameObjects[i]);
        }
        GridGameObjects.Clear();
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
            else
            {
                Debug.Log("Grid Size Increased");
                DeleteGrid();
                CreateGrid(CurrentGrid);
            }

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
            else
            {
                Debug.Log("Grid Size Decreased");
                DeleteGrid();
                CreateGrid(CurrentGrid);
            }



        }

    }

    public void FourDirectionalGrid()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.SetTexture("_MainTex", FourDirGrid_Tex);
        if (TelSystem == null)
        {

        }
        else
        {
            TelSystem.AddLine("Grid set to 4 directional");
        }
        
    }

    public void EightDirectionalGrid()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.SetTexture("_MainTex", EightDirGrid_Tex);
        if (TelSystem == null)
        {

        }
        else
        {
            TelSystem.AddLine("Grid set to 8 directional");
        }
        
    }

    public void ShowHideGridLines()
    {
        if (GridLinesHidden == true) //if the grid lines are hidden
        {
            GridLinesHidden = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            if (TelSystem == null)
            {

            }
            else
            {
                TelSystem.AddLine("Grid lines unhidden");
            }
            
        }
        else //if the grid lines arent hidden
        {
            GridLinesHidden = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            if (TelSystem == null)
            {

            }
            else
            {
                TelSystem.AddLine("Grid lines hidden");
            }
            
        }


    }

    public void ShowHideGridBoxes()
    {
        if (GridBoxesHidden == true) //if the grid boxes are hidden
        {
            GridBoxesHidden = false;
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = true;

            }
            if (TelSystem == null)
            {

            }
            else
            {
                //TelSystem.AddLine("Grid boxes unhidden");
            }
            
        }

        else //if the grid boxes arent hidden
        {
            GridBoxesHidden = true;
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = false;

            }
            if (TelSystem == null)
            {

            }
            else
            {
                //TelSystem.AddLine("Grid boxes hidden");
            }
            
        }
    }

}
