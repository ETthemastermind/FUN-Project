using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ProximityToBalloonV2 : MonoBehaviour
{
    public Activity1Settings ActSet;  //reference to the activity settings on the game controller object
    public GridV3 Grid; //reference to the grid script on the grid object

    public bool BalloonFound; //bool that triggers if a balloon is found
    public Vector2 CurrentGridCoords; //vector2 to hold the current grid coords on the playerball

    public Vector2[] FourDirArray_Coords = new Vector2[4]; //vector2 of directions for what should be infront, behind, left and right. can go outside the bounds of the grid
    public Vector2[] EightDirArray_Coords = new Vector2[4]; //vector2 of directions for what should be forwardleft, forwardright, backwardsleft and backwardsright. can go outside the bounds of the grid

    public GameObject PrepareToBang;
    public AudioClip PrepareToBang_Audio;
    public AudioSource AS;


    public LayerMask layerMask; //refrence for a layermask for the linecast
    // Start is called before the first frame update
    void Start()
    {
        ActSet = GameObject.FindGameObjectWithTag("GameController").GetComponent<Activity1Settings>(); //find the activity one settings
        Grid = GameObject.FindGameObjectWithTag("GridObject").GetComponent<GridV3>(); //find the grid
    }


    public void BalloonProxCheck() //function to check if a balloon is in proximity of a player
    {
        BalloonFound = false; //reset the balloon found boolean
        PrepareToBang.SetActive(false);
        GridAttributes CurrentGrid = gameObject.GetComponent<BallControllerV2>().CurrentGridGO.GetComponent<GridAttributes>(); //gets the current grid location of the playerball
        CurrentGridCoords = CurrentGrid.GridCoords; //assigns the current grid location to a varibale in this script

        FourDirArray_Coords[0] = new Vector2(CurrentGridCoords.x, CurrentGridCoords.y + 1); // calculates forwards
        FourDirArray_Coords[1] = new Vector2(CurrentGridCoords.x, CurrentGridCoords.y - 1); //backwards
        FourDirArray_Coords[2] = new Vector2(CurrentGridCoords.x + 1, CurrentGridCoords.y); //left
        FourDirArray_Coords[3] = new Vector2(CurrentGridCoords.x - 1, CurrentGridCoords.y); //right
        RaycastCheck(FourDirArray_Coords); // runs the raycast function
        
        
        if (ActSet.DiagonalControlsActive == true) //if the diagonal controls are active
        {
            EightDirArray_Coords[0] = new Vector2(CurrentGridCoords.x + 1, CurrentGridCoords.y + 1); // calculate forwards left
            EightDirArray_Coords[1] = new Vector2(CurrentGridCoords.x - 1, CurrentGridCoords.y + 1); //forwards right
            EightDirArray_Coords[2] = new Vector2(CurrentGridCoords.x + 1, CurrentGridCoords.y - 1); //backwards left
            EightDirArray_Coords[3] = new Vector2(CurrentGridCoords.x - 1, CurrentGridCoords.y - 1); //backwards right
            RaycastCheck(EightDirArray_Coords); //run the raycast function
        }
       

    }


    public void RaycastCheck(Vector2[] Targets) //function to see if any balls are in the neighbouring grid boxes
    {
        List<GameObject> GridCubes = Grid.GridGameObjects; //reference to the list of grid cubes in the grid script
        for (int i = 0; i < GridCubes.Count; i++) //for each of the cubes in the grid cubes list
        {
            GridAttributes G = GridCubes[i].GetComponent<GridAttributes>(); //get the grid attributes of the current cube
            for (int j = 0; j < Targets.Length; j++) //for each of the targets in the target array (possible next locations for grid cubes)
            {
                if (G.GridCoords == Targets[j]) //if one of the grid coords from the current grid attribute matches any of the targets
                {
                   
                    Vector3 EndPoint = new Vector3(GridCubes[i].transform.position.x, transform.position.y, GridCubes[i].transform.position.z); //calculate the target for the line cast based on the found grid box
                    if (Physics.Linecast(transform.position, EndPoint, out RaycastHit hitInfo, layerMask)) //linecast between the current ball position and the end point, layermask is set up in the inspector to only look for collisions on the balloon layer, therefore it can ONLY hit balloons
                    {

                        BalloonFound = true; //if a ballon is found, set this bool to true
                        PrepareToBang.SetActive(true); //activate the prepare to bang graphic
                        if (AS.isPlaying == false) //if the audiosource is not playing
                        {
                            AS.PlayOneShot(PrepareToBang_Audio); //play the prepare to bang audio
                        }
                    }
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
