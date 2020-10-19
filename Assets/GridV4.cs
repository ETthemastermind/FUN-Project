using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public ObjectPooler objectPooler;
    public GridSettings[] gridSettings;

    public bool GridInit = false;
    public int CurrentGrid = 0;
    // Start is called before the first frame update
    void Start()
    {
        //objectPooler = ObjectPooler.Instance;
        CreateGrid();

    }
    

    // Update is called once per frame
    public void CreateGrid()
    {
        
        int CurrentX = 1;
        int CurrentY = 1;

        for (int i = 0; i < gridSettings[CurrentGrid].Height * gridSettings[CurrentGrid].Width; i++)
        {
            Vector3 pos = new Vector3(gridSettings[CurrentGrid].X_Start + (gridSettings[CurrentGrid].X_Space * (i % gridSettings[CurrentGrid].Height)), transform.position.y, -gridSettings[CurrentGrid].Y_Start + (gridSettings[CurrentGrid].Y_Space * (i / gridSettings[CurrentGrid].Height)));
            GameObject SpawnedGrid = objectPooler.SpawnFromPool("Grid", pos, Quaternion.identity);
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

        }
    }

    public void Update()
    {
        if (GridInit == false)
        {
            GridInit = true;
            CreateGrid();
        }
    }
}
