using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GridV4 : MonoBehaviour
{
    [System.Serializable]
    public class GridSettings
    {
        public float X_Start;
        public float Y_Start;
        public int Height;
        public int Width;
        public float X_Space;
        public float Y_Space;
    }
    public List<GameObject> ActiveGrids;
    public ObjectPooler objectPooler;
    public GridSettings[] gridSettings;

    public bool GridInit = false;
    public int CurrentGrid = 0;
    public Material Mat;
    

    public bool GridLinesHidden;
    public bool GridBoxesHidden;

    public Texture FourDirGrid_Tex;
    public Texture EightDirGrid_Tex;

    public UnityEvent CallOnceGridIsMade = new UnityEvent();

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        //objectPooler = ObjectPooler.Instance;
        Mat = gameObject.GetComponent<Renderer>().material;
        CreateGrid();

    }
    

    // Update is called once per frame
    public void CreateGrid()
    {
        ActiveGrids.Clear();
        int CurrentX = 1;
        int CurrentY = 1;

        for (int i = 0; i < gridSettings[CurrentGrid].Height * gridSettings[CurrentGrid].Width; i++)
        {
            Vector3 pos = new Vector3(gridSettings[CurrentGrid].X_Start + (gridSettings[CurrentGrid].X_Space * (i % gridSettings[CurrentGrid].Height)), transform.position.y, -gridSettings[CurrentGrid].Y_Start + (gridSettings[CurrentGrid].Y_Space * (i / gridSettings[CurrentGrid].Height)));
            GameObject SpawnedGrid = objectPooler.SpawnFromPool("Grid", pos, Quaternion.identity);
            ActiveGrids.Add(SpawnedGrid);
            GridAttributes GA = SpawnedGrid.GetComponent<GridAttributes>();
            if (CurrentY != gridSettings[CurrentGrid].Height + 1)
            {
                GA.GridCoords = new Vector2(CurrentX, CurrentY);
                CurrentY++;

            }
            else
            {
                CurrentY = 1;
                CurrentX++;
                GA.GridCoords = new Vector2(CurrentX, CurrentY);
                CurrentY++;

            }
            //set the grid lines
            Mat.mainTextureScale = new Vector2(gridSettings[CurrentGrid].Height, gridSettings[CurrentGrid].Width);
            ShowHideGridLines();
            ShowHideGridBoxes();
        }
        CallOnceGridIsMade.Invoke();
    }
    /*
    public void Update()
    {
        if (GridInit == false)
        {
            GridInit = true;
            CreateGrid();
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            LastGrid();
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            NextGrid();
        }
    }
    */
    public void NextGrid()
    {
        CurrentGrid++;
        if (CurrentGrid > gridSettings.Length - 1)
        {
            CurrentGrid = gridSettings.Length - 1;

        }
        else
        {
            objectPooler.ResetPool("Grid");
            CreateGrid();
        }
        


    }

    public void LastGrid()
    {
        CurrentGrid--;
        if (CurrentGrid < 0)
        {
            CurrentGrid = 0;
        }
        else
        {
            objectPooler.ResetPool("Grid");
            CreateGrid();
        }
    }

    public void ShowHideGridLines()
    {
        if (GridLinesHidden == true)
        {
            GetComponent<MeshRenderer>().enabled = true;
        }

        else if (GridLinesHidden == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ShowHideGridBoxes()
    {
        if (GridBoxesHidden == true)
        {
            foreach (Transform child in objectPooler.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = true;
            }
        }

        else if (GridBoxesHidden == false)
        {
            foreach (Transform child in objectPooler.transform)
            {
                child.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    public void FourDirMovement()
    {
        Mat.SetTexture("_MainTex", FourDirGrid_Tex);
    }

    public void EightDirMovement()
    {
        Mat.SetTexture("_MainTex", EightDirGrid_Tex);
    }


}
