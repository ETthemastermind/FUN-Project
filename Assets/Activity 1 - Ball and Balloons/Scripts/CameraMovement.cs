using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform[] CameraPositionsArray;

    public int CurrentCamera = 0;

    public Vector3 StartRotation;
    public float MaxRot;
    public float CurrentRot;

    public string ChosenRot;
    public bool _CameraMoving;
    public Vector3 RotationTarget;

    float LerpFraction;
    float LerpSpeed;
    

    // Start is called before the first frame update
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles;
        RotationTarget = new Vector3(StartRotation.x - MaxRot, StartRotation.y, StartRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            //RotateCameraDown();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            
        }



        /*
        if (_CameraMoving == true)
        {
            if (LerpFraction < 1)
            {
                LerpFraction += LerpSpeed * Time.deltaTime;
                gameObject.transform.eulerAngles = Vector3.Lerp(StartRotation, RotationTarget, LerpFraction);
            }
            else
            {
                _CameraMoving = false;
            }

            
        }
        */
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
    public void RotateCameraUp()
    {
        transform.Rotate(MaxRot, 0f, 0f);
    }


    
}
