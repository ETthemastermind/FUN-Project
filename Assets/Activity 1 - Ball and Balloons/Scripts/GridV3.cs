using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridV3 : MonoBehaviour
{
    public List<GameObject> GridGameObjects = new List<GameObject>();



    [Header("FromVid - changing these value does nothing, its just for debug")] ////https://www.youtube.com/watch?v=WJimYq2Tczc
    public float X_Start; //default = -4.5
    public float Y_Start; // default = 6.8

    public int Height; // default = 7
    public int Width; //default = 11

    public float X_Space; //default = 1.5
    public float Y_Space; //default = 1.5
    public GameObject prefab;

    [Header("Grid Values")]
    public float[][] GridValueArray = new float[5][];

    public float[] GridPos1 = new float[6];
    public float[] GridPos2 = new float[6];
    public float[] GridPos3 = new float[6];
    public float[] GridPos4 = new float[6];
    public float[] GridPos5 = new float[6];


    public int CurrentGrid = 1;
    ///public bool GridHidden = false;

    public bool GridLinesHidden = false;
    public bool GridBoxesHidden = false;

    int CurrentY = 1;
    int CurrentX = 1;

    public Texture FourDirGrid_Tex;
    public Texture EightDirGrid_Tex;


    MasterTelemetrySystem TelSystem;

    // Start is called before the first frame update
    void Start()
    {
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
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

    public void CreateGrid()
    {
        CurrentX = 1;
        CurrentY = 1;
        float[] Temp = GridValueArray[CurrentGrid];
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
                GA.X = CurrentX;
                GA.Y = CurrentY;
                CurrentY++;

            }
            else
            {
                CurrentY = 1;
                CurrentX++;
                GA.X = CurrentX;
                GA.Y = CurrentY;
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
        /*
        if (GridHidden == true)
        {
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = false;

            }
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        */

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
                CreateGrid();
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
                CreateGrid();
            }



        }

    }

    public void FourDirectionalGrid()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.SetTexture("_MainTex", FourDirGrid_Tex);
        TelSystem.AddLine("Grid set to 4 directional");
    }

    public void EightDirectionalGrid()
    {
        Material mat = gameObject.GetComponent<Renderer>().material;
        mat.SetTexture("_MainTex", EightDirGrid_Tex);
        TelSystem.AddLine("Grid set to 8 directional");
    }
    /*
    public void ShowHideGrid()
    {
        if (GridHidden == true) //if the grid is already hidden
        {
            GridHidden = false;
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = true;

            }
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            TelSystem.AddLine("Grid hidden");
            //show the grid
        }
        else //if the grid is not hidden
        {
            GridHidden = true;
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = false;
            }
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            TelSystem.AddLine("Grid unhidden");
            //hide the grid
        }

    }
    */

    public void ShowHideGridLines()
    {
        if (GridLinesHidden == true) //if the grid lines are hidden
        {
            GridLinesHidden = false;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            TelSystem.AddLine("Grid lines unhidden");
        }
        else //if the grid lines arent hidden
        {
            GridLinesHidden = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            TelSystem.AddLine("Grid lines hidden");
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
            TelSystem.AddLine("Grid boxes unhidden");
        }

        else //if the grid boxes arent hidden
        {
            GridBoxesHidden = true;
            for (int i = 0; i < GridGameObjects.Count; i++)
            {
                GridGameObjects[i].GetComponent<MeshRenderer>().enabled = false;

            }
            TelSystem.AddLine("Grid boxes hidden");
        }
    }

}
