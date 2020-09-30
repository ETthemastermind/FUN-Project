using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovementV2 : MonoBehaviour
{
    public Transform[] CameraPositionsArray; //array of camera positions
    //public GameObject[] CameraInterface_Canvas; //array of canvases, canvas 0 is for camera 0 etc
    public int CurrentCamera = 100000; //ref to the current active camera

    public Quaternion TargetRot;    
    public Quaternion StartRotation;

    public float LerpSpeed = 1f;
    public float LerpFraction;

    public MasterTelemetrySystem TelSystem;

    public UnityEvent Command = new UnityEvent();
    public UnityEvent View = new UnityEvent();

    private Transform ThisTransform;

    // Start is called before the first frame update
    void Start()
    {
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        ThisTransform = gameObject.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextCamera() //function to go to the next camera
    {
        View.Invoke();
        //CameraInterface_Canvas[CurrentCamera].SetActive(false); //set the current canvas to false;
        CameraPositionsArray[CurrentCamera].GetComponent<CanvasController>().DeactivateCanvas();
        CurrentCamera++; //increment the current camera
        //Debug.Log("Array length = " + CameraPositionsArray.Length); //just a debug
        if (CurrentCamera > CameraPositionsArray.Length - 1) //if the current camera value is greater than the amount of cameras avaliable
        {
            CurrentCamera = 0; //resets the camera back to 0 if true
        }
        //CameraPositionsArray[CurrentCamera].gameObject.GetComponent<CameraPosMovement>().DeclarePosition();
        //CameraInterface_Canvas[CurrentCamera].SetActive(true); //activate the corresponding canvas
        CameraPositionsArray[CurrentCamera].GetComponent<CanvasController>().ActivateCanvas();

        transform.parent = CameraPositionsArray[CurrentCamera].transform; //set the camera to a be a child of the position game object and sets its transforms too

        
        Vector3 StartPos = transform.position;
        Quaternion StartRot = transform.rotation;

        Vector3 TargetPos = CameraPositionsArray[CurrentCamera].transform.position;
        Quaternion TargetRot = CameraPositionsArray[CurrentCamera].transform.rotation;

        StartCoroutine(CameraLerp(StartPos, StartRot, TargetPos, TargetRot));
        TelSystem.AddLine("Next Camera button pressedS active camera is  " + CameraPositionsArray[CurrentCamera].name);
        //transform.position = CameraPositionsArray[CurrentCamera].transform.position;
        //transform.rotation = CameraPositionsArray[CurrentCamera].transform.rotation;
    }

    public void LastCamera() //function to go to the next camera
    {
        View.Invoke();
        //CameraInterface_Canvas[CurrentCamera].SetActive(false); //set the current canvas to false;
        CameraPositionsArray[CurrentCamera].GetComponent<CanvasController>().DeactivateCanvas();
        CurrentCamera--; //increment the current camera
        //Debug.Log("Current Camera" + CurrentCamera); //just a debug
        if (CurrentCamera == -1) //if the current camera value is greater than the amount of cameras avaliable
        {
            CurrentCamera = CameraPositionsArray.Length - 1; //resets the camera back to 0 if true
        }
        //CameraPositionsArray[CurrentCamera].gameObject.GetComponent<CameraPosMovement>().DeclarePosition();
        //CameraInterface_Canvas[CurrentCamera].SetActive(true); //activate the corresponding canvas
        CameraPositionsArray[CurrentCamera].GetComponent<CanvasController>().ActivateCanvas();
        transform.parent = CameraPositionsArray[CurrentCamera].transform; //set the camera to a be a child of the position game object and sets its transforms too
        //transform.position = CameraPositionsArray[CurrentCamera].transform.position;
        //transform.rotation = CameraPositionsArray[CurrentCamera].transform.rotation;

        Vector3 StartPos = transform.position;
        Quaternion StartRot = transform.rotation;

        Vector3 TargetPos = CameraPositionsArray[CurrentCamera].transform.position;
        Quaternion TargetRot = CameraPositionsArray[CurrentCamera].transform.rotation;

        StartCoroutine(CameraLerp(StartPos, StartRot, TargetPos, TargetRot));
        TelSystem.AddLine("Last Camera button pressed active camera is - " + CameraPositionsArray[CurrentCamera].name);
    }



    public void RotateCameraUp()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateUp();
        TelSystem.AddLine("Camera Rotated Up");

    }
    public void RotateCameraRight()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateRight();
        TelSystem.AddLine("Camera Rotated Right");
    }
    public void RotateCameraDown()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateDown();
        TelSystem.AddLine("Camera Rotated Down");
    }
    public void RotateCameraLeft()
    {
        CameraPositionsArray[CurrentCamera].GetComponent<CameraPosMovement>().RotateLeft();
        TelSystem.AddLine("Camera Rotated Left");
    }

    public IEnumerator CameraLerp(Vector3 StartPos, Quaternion StartRot, Vector3 TargetPos,Quaternion TargetRot)
    {
        float LerpFraction = 0f;
        float LerpSpeed = 1f;
        while (LerpFraction < 1)
        {
            LerpFraction += LerpSpeed * Time.deltaTime;
            ThisTransform.position = Vector3.Slerp(StartPos, TargetPos, LerpFraction);
            ThisTransform.rotation = Quaternion.Slerp(StartRot, TargetRot, LerpFraction);
            yield return new WaitForEndOfFrame();


        }
        Command.Invoke();
        
    }
   
}
