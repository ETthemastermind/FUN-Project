using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementV2 : MonoBehaviour
{
    public Transform[] CameraPositionsArray; //array of camera positions
    public GameObject[] CameraInterface_Canvas; //array of canvases, canvas 0 is for camera 0 etc
    public int CurrentCamera = 0; //ref to the current active camera
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCamera() //function to go to the next camera
    {
        CameraInterface_Canvas[CurrentCamera].SetActive(false); //set the current canvas to false;
        CurrentCamera++; //increment the current camera
        Debug.Log("Array length = " + CameraPositionsArray.Length); //just a debug
        if (CurrentCamera > CameraPositionsArray.Length - 1) //if the current camera value is greater than the amount of cameras avaliable
        {
            CurrentCamera = 0; //resets the camera back to 0 if true
        }
        CameraPositionsArray[CurrentCamera].gameObject.GetComponent<CameraPosMovement>().DeclarePosition();
        CameraInterface_Canvas[CurrentCamera].SetActive(true); //activate the corresponding canvas
        
        transform.parent = CameraPositionsArray[CurrentCamera].transform; //set the camera to a be a child of the position game object and sets its transforms too
        transform.position = CameraPositionsArray[CurrentCamera].transform.position;
        transform.rotation = CameraPositionsArray[CurrentCamera].transform.rotation;
        /*
        StartRotation = transform.rotation.eulerAngles;
        */
    }

    public void RotateCameraUp()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateUp();
    }
    public void RotateCameraRight()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateRight();
    }
    public void RotateCameraDown()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateDown();
    }
    public void RotateCameraLeft()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateLeft();
    }
}
