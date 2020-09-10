using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] CameraPositionsArray;

    public int CurrentCamera = 0;

    public Vector3 StartRotation;
    //public Vector3 RotationTarget;


    public float MaxRot;
    public float CurrentRot;

    public string ChosenRot;
    public bool _CameraMoving;
    

    public float LerpFraction;
    public float LerpSpeed;
    public float TimeBeforeReturn = 5f;
    public float CurrentTimeBeforeReturn;

    // Start is called before the first frame update
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles;
        
    }

    // Update is called once per frame
    void Update()
    {
        //RotateCameraUp();
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



    public void NextCamera()
    {
        CurrentCamera++;
        Debug.Log("Array length = " + CameraPositionsArray.Length);
        if (CurrentCamera > CameraPositionsArray.Length - 1)
        {
            CurrentCamera = 0;
        }
        transform.parent = CameraPositionsArray[CurrentCamera].transform;
        transform.position = CameraPositionsArray[CurrentCamera].transform.position;
        transform.rotation = CameraPositionsArray[CurrentCamera].transform.rotation;
        StartRotation = transform.rotation.eulerAngles;
    }

    public void RotateCameraUp()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x - MaxRot, StartRotation.y, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));
        }
        
    }

    public void RotateCameraDown()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x + MaxRot, StartRotation.y, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));

        }
        
    }

    public void RotateCameraRight()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x, StartRotation.y + MaxRot, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));
        }
    }

    public void RotateCameraLeft()
    {
        if (_CameraMoving == false)
        {
            _CameraMoving = true;
            Vector3 RotationTarget = new Vector3(StartRotation.x, StartRotation.y - MaxRot, StartRotation.z);
            StartCoroutine(RotCam(RotationTarget));
        }
    }

    #region Rotate the Camera
    //functions to write: rotate camera up, down, left, right and back to start

    public IEnumerator RotCam(Vector3 RotTarget)
    {
        
        while (LerpFraction < 1)
        {
            Debug.Log("Rotating Camera Up");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime;
            gameObject.transform.eulerAngles = Vector3.Lerp(StartRotation, RotTarget, LerpFraction);
        }
        LerpFraction = 0f;
        StartCoroutine(ReturnCamera(RotTarget));
    }




    public IEnumerator ReturnCamera(Vector3 RotTarget)
    {
        yield return StartCoroutine(RotPause());
        yield return StartCoroutine(RotCamStart(RotTarget));
    }

    public IEnumerator RotPause()
    {
        while (CurrentTimeBeforeReturn < TimeBeforeReturn)
        {
            CurrentTimeBeforeReturn++;
            yield return new WaitForSecondsRealtime(TimeBeforeReturn);
        }
        CurrentTimeBeforeReturn = 0f;


    }

    public IEnumerator RotCamStart(Vector3 RotTarget)
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
        _CameraMoving = false;
    }

    
    #endregion




}
