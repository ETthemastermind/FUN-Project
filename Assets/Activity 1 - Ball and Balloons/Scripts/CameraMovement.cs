using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] CameraPositionsArray;

    public int CurrentCamera = 0;

    public Vector3 StartRotation;
    public Vector3 RotationTarget;


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

            //RotateCameraUp();
            StartCoroutine(RotCamUp());
            
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            //RotateCameraDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            
        }



        
        if (_CameraMoving == true)
        {
            if (LerpFraction < 1)
            {
                LerpFraction += LerpSpeed * Time.deltaTime;
                gameObject.transform.eulerAngles = Vector3.Lerp(StartRotation, RotationTarget, LerpFraction);
            }
            else
            {
                if (CurrentTimeBeforeReturn < TimeBeforeReturn)
                {
                    CurrentTimeBeforeReturn += Time.deltaTime;
                }

                else
                {
                    Debug.Log("Return Function");


                }
            }

            
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

    }




    //functions to write: rotate camera up, down, left, right and back to start

    public IEnumerator RotCamUp()
    {
        RotationTarget = new Vector3(StartRotation.x - MaxRot, StartRotation.y, StartRotation.z);
        while (LerpFraction < 1)
        {
            Debug.Log("Rotating Camera Up");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime * LerpSpeed;
            gameObject.transform.eulerAngles = Vector3.Lerp(StartRotation, RotationTarget, LerpFraction);
        }
        LerpFraction = 0f;
    }

    public void RotateCameraUp()
    {
        Debug.Log("Rotate Camera Up");
        while (LerpFraction < 1)
        {
            Debug.Log("Rotating Camera Up");
            LerpFraction += LerpSpeed * Time.deltaTime * LerpSpeed;
            gameObject.transform.eulerAngles = Vector3.Lerp(StartRotation, RotationTarget, LerpFraction);
        }
        /*
        _CameraMoving = true;
        RotationTarget = new Vector3(StartRotation.x - MaxRot, StartRotation.y, StartRotation.z);
        //transform.Rotate(MaxRot, 0f, 0f);

        /*
        LerpFraction += Time.deltaTime * LerpSpeed;
        if (LerpFraction < 1)
        {
            transform.eulerAngles = Vector3.Lerp(StartRotation, RotationTarget, LerpFraction);
            RotateCameraUp();
        }
        else
        {
            
        }
        */
        
        
    }

    public void RotateCameraToStart()
    {
        _CameraMoving = true;
        RotationTarget = new Vector3(StartRotation.x, StartRotation.y, StartRotation.z);
        //transform.Rotate(MaxRot, 0f, 0f);

       


    }



}
