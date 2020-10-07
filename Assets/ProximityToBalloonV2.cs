using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ProximityToBalloonV2 : MonoBehaviour
{
    public Activity1Settings ActSet;
    public GridV3 Grid;

    public bool BalloonFound;
    public Vector2 CurrentGridCoords;

    public Vector2[] FourDirArray_Coords = new Vector2[4];
    public Vector2[] EightDirArray_Corrds = new Vector2[4];

    public GameObject[] FourDirArray_Go = new GameObject[4];


    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        ActSet = GameObject.FindGameObjectWithTag("GameController").GetComponent<Activity1Settings>();
        Grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BalloonProxCheck()
    {
        BalloonFound = false;
        for (int p = 0; p < FourDirArray_Go.Length; p++)
        {
            FourDirArray_Go[p] = null;
        }


        GridAttributes CurrentGrid = gameObject.GetComponent<BallControllerV2>().CurrentGridGO.GetComponent<GridAttributes>();
        CurrentGridCoords = CurrentGrid.GridCoords;

        FourDirArray_Coords[0] = new Vector2(CurrentGridCoords.x, CurrentGridCoords.y + 1); //forwards
        FourDirArray_Coords[1] = new Vector2(CurrentGridCoords.x, CurrentGridCoords.y - 1); //backwards
        FourDirArray_Coords[2] = new Vector2(CurrentGridCoords.x + 1, CurrentGridCoords.y); //left
        FourDirArray_Coords[3] = new Vector2(CurrentGridCoords.x - 1, CurrentGridCoords.y); //right
        RaycastCheck(FourDirArray_Coords, FourDirArray_Go);
        
        
        if (ActSet.DiagonalControlsActive == true)
        {
            EightDirArray_Corrds[0] = new Vector2(CurrentGridCoords.x + 1, CurrentGridCoords.y + 1); //forwards left
            EightDirArray_Corrds[1] = new Vector2(CurrentGridCoords.x - 1, CurrentGridCoords.y + 1); //forwards right
            EightDirArray_Corrds[2] = new Vector2(CurrentGridCoords.x + 1, CurrentGridCoords.y - 1); //backwards left
            EightDirArray_Corrds[3] = new Vector2(CurrentGridCoords.x - 1, CurrentGridCoords.y - 1); //backwards right
        }
       

    }

    public void RaycastCheck(Vector2[] Targets, GameObject[] Destination)
    {
        List<GameObject> GridCubes = Grid.GridGameObjects;
        int A = 0;
        for (int i = 0; i < GridCubes.Count; i++)
        {
            GridAttributes G = GridCubes[i].GetComponent<GridAttributes>();
            for (int j = 0; j < Targets.Length; j++)
            {
                if (G.GridCoords == Targets[j])
                {
                    Debug.Log("Cube Found");
                    Destination[A] = GridCubes[i];
                    A++;
                }
            }
        }
        
       
        for (int t = 0; t < Destination.Length; t++)
        {
            if (Destination[t] = null)
            {


            }
            else
            {
                //Debug.Log("Running T");
                Vector3 EndPoint = new Vector3(FourDirArray_Go[t].transform.position.x, transform.position.y, FourDirArray_Go[t].transform.position.z);
                if (Physics.Linecast(transform.position, EndPoint, out RaycastHit hitInfo, layerMask))
                {
                    Debug.Log(hitInfo);

                    BalloonFound = true;
                }
            }

        }
        
    } 
    /*
    public void OnDrawGizmosSelected()
    {
        Color color = Color.red;
        Vector3 Destination = new Vector3(FourDirArray_Go[0].transform.position.x, transform.position.y,FourDirArray_Go[0].transform.position.z);
        Debug.DrawLine(transform.position, Destination);
        //Debug.DrawLine(transform.position, FourDirArray_Go[1].transform.position);
        //Debug.DrawLine(transform.position, FourDirArray_Go[2].transform.position);
        //Debug.DrawLine(transform.position, FourDirArray_Go[3].transform.position);
    }
    */
}
