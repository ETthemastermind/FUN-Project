using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] CameraPositionsArray; //array of camera positions
    public GameObject[] CameraInterface_Canvas; //array of canvases, canvas 0 is for camera 0 etc

    public int CurrentCamera = 0; //ref to the current active camera

    public Vector3 StartRotation; //starting rotation of the camera
    //public Vector3 RotationTarget;


    public float MaxRot_UD; //max distance that the camera can rotate up and down (degrees, i think)
    public float MaxRot_LR; //max distance that the camera can rotate left and right

    public bool _CameraMoving; //bool to keep track if the camera is currently moving (stops multiple inputs)
    

    public float LerpFraction; //reference for the lerp fraction
    public float LerpSpeed; //reference for the lerp speed
    public float TimeBeforeReturn; //how long the camera gets hold in rotation for
    public float CurrentTimeBeforeReturn; //how long the user has already waited for

    // Start is called before the first frame update
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles; //gets a reference to the starting rotation of the camera
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateCameraUp(); //debugs so i dont need to use the interface
        if (Input.GetKeyDown(KeyCode.C))
        {
            NextCamera();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            RotateCameraUp();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            RotateCameraDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            RotateCameraRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            RotateCameraLeft();
        }
        
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
        CameraInterface_Canvas[CurrentCamera].SetActive(true); //activate the corresponding canvas
        transform.parent = CameraPositionsArray[CurrentCamera].transform; //set the camera to a be a child of the position game object and sets its transforms too
        transform.position = CameraPositionsArray[CurrentCamera].transform.position;
        transform.rotation = CameraPositionsArray[CurrentCamera].transform.rotation;
        StartRotation = transform.rotation.eulerAngles;
    }

    public void RotateCameraUp() //rotate the camera up, down, left, right
    {
        if (_CameraMoving == false) //if the camera isnt moving
        {
            _CameraMoving = true; //change the camera moving bool to true
            Vector3 RotationTarget = new Vector3(StartRotation.x - MaxRot_UD, StartRotation.y, StartRotation.z); // create the rotation target based on where the camera needs to go
            StartCoroutine(RotCam(RotationTarget)); //run the rot cam coroutine with the rotation target as a parameter
        }
        
    }

    public void RotateCameraDown()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x + MaxRot_UD, StartRotation.y, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));

        }
        
    }

    public void RotateCameraRight()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x, StartRotation.y + MaxRot_LR, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));
        }
    }

    public void RotateCameraLeft()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x, StartRotation.y - MaxRot_LR, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));
        }
    }

    #region Rotate the Camera // "this is where the fun begins" - Anakin Skywalker
    //functions to write: rotate camera up, down, left, right and back to start

    public IEnumerator RotCam(Vector3 RotTarget) //rotate the camera
    {
        
        while (LerpFraction < 1) // while the lerp fraction is less than 1 i.e not complete
        {
            Debug.Log("Rotating Camera Up"); //print to console that the camera is rotating up
            yield return new WaitForEndOfFrame();// wait until the end of the frame
            LerpFraction += LerpSpeed * Time.deltaTime; // increment the lerp fraction
            gameObject.transform.eulerAngles = Vector3.Lerp(StartRotation, RotTarget, LerpFraction); //lerp between the start rotation and the rotation target
        }
        LerpFraction = 0f; //once the while loop is completed, reset the lerp fraction
        StartCoroutine(ReturnCamera(RotTarget)); //start the coroutine for the camera return
    }




    public IEnumerator ReturnCamera(Vector3 RotTarget) //return the camera 
    {
        yield return StartCoroutine(RotPause()); //start the coroutine to hold the camera in place
        yield return StartCoroutine(RotCamStart(RotTarget)); //start the corountine to return the camera to its start rotation
    }

    public IEnumerator RotPause() //pause the camera
    {
        while (CurrentTimeBeforeReturn < TimeBeforeReturn) //run a timer to hold the camera is place
        {
            CurrentTimeBeforeReturn++; //increment the current time
            yield return new WaitForSecondsRealtime(TimeBeforeReturn);
        }
        CurrentTimeBeforeReturn = 0f;


    }

    public IEnumerator RotCamStart(Vector3 RotTarget) //basically the same as the RotCam ienum, just in reverse
    {
        //RotationTarget = new Vector3(StartRotation.x - MaxRot, StartRotation.y, StartRotation.z);
        while (LerpFraction < 1)
        {
            Debug.Log("Rotating Camera to Start");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = Vector3.Lerp(RotTarget, StartRotation, LerpFraction);
        }
        LerpFraction = 0f;
        _CameraMoving = false; //turn the camera moving varibale back off;
    }

    
    #endregion




}
