using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosMovement : MonoBehaviour
{
    
    public Quaternion TargetRot;    // Start is called before the first frame update
    public Quaternion StartRotation;
    public Vector3 StartPos;
    public float LerpSpeed = 1f;
    public float LerpFraction;
    public float TimeBeforeReturn; //how long the camera gets hold in rotation for
    public float CurrentTimeBeforeReturn; //how long the user has already waited for

    public float UpDownRotAngle;
    public float LeftRightRotAngle;

    [Header("MovementPlus")] //all variables relating to the movement plus feature
    public bool MovementPlus;
    public Quaternion QuatStore; //debug because idk how to access quaterions in the inspector
    public Quaternion[] TargetRot_MP; //array of rotations -> 0 = up, 1 = down, 2 = left, 3 = right
    public Vector3[] TargetPos_MP; //array of positions -> 0 = up, 1 = down, 2 = left, 3 = right

    void Start()
    {
        TargetRot = transform.rotation; //initialise some variables
        StartRotation = transform.rotation;
        StartPos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)) //debug so i can get quaterion values
        {
            GetQuaterion();
        }
    }

    public void RotateUp() //function for rotating/moving the camera up/down/left/right
    {
        if (MovementPlus == false) //if the movement plus is not enabled
        {
            TargetRot *= Quaternion.AngleAxis(UpDownRotAngle, Vector3.left); //get the target rotation, depending on the function
            StartCoroutine(RotCam(TargetRot, transform.position)); //run the rot cam coroutine, passing in the target rot calculated and the current position
            TargetRot = StartRotation; //ngl i dont remember why this is here but i dont want to get rid of it just in case
        }
        else if (MovementPlus == true) // if movement plus is enabled
        {
            
            Debug.Log("Rotate Up Movement Plus");
            StartCoroutine(MoveCam(TargetRot_MP[0], TargetPos_MP[0])); //run the rot cam coroutine, passing the target rot and target pos assigned by the user in the corressponding array
            //StartCoroutine(MoveCam(TargetPos_MP[0]));
            
        }
        
    }
    public void RotateRight() //same as above but right
    {
        if (MovementPlus == false)
        {
            TargetRot *= Quaternion.AngleAxis(LeftRightRotAngle, Vector3.up);
            StartCoroutine(RotCam(TargetRot, transform.position));
            TargetRot = StartRotation;
            
        }
        else if (MovementPlus == true)
        {
            Debug.Log("Rotate Right Movement Plus");
            StartCoroutine(MoveCam(TargetRot_MP[3], TargetPos_MP[3]));
            //StartCoroutine(MoveCam(TargetPos_MP[3]));
        }


    }
    public void RotateDown() //same as above but down
    {
        if (MovementPlus == false)
        {
            TargetRot *= Quaternion.AngleAxis(UpDownRotAngle, Vector3.right);
            StartCoroutine(RotCam(TargetRot, transform.position));
            TargetRot = StartRotation;
        }
        else if (MovementPlus == true)
        {
            Debug.Log("Rotate Down Movement Plus");
            StartCoroutine(MoveCam(TargetRot_MP[1], TargetPos_MP[1]));
            //StartCoroutine(MoveCam(TargetPos_MP[1]));
        }

    }
    public void RotateLeft() //same as above but left
    {
        if (MovementPlus == false)
        {
            TargetRot *= Quaternion.AngleAxis(LeftRightRotAngle, Vector3.down);
            StartCoroutine(RotCam(TargetRot, transform.position));
            TargetRot = StartRotation;
        }

        else if (MovementPlus == true)
        {
            Debug.Log("Rotate Left Movement Plus");
            //StartCoroutine(RotCam(TargetRot_MP[2], TargetPos_MP[2]));
            StartCoroutine(MoveCam(TargetRot_MP[2], TargetPos_MP[2]));
        }

    }

    public void GetQuaterion() //debug to get the current quaternion
    {
        if (MovementPlus == true)
        {
            QuatStore = transform.rotation;
            Debug.Log("Current Quaterion is: " + transform.rotation);
        }
        
    }



    public IEnumerator RotCam(Quaternion RotTarget, Vector3 PosTarget) //coroutine to rotate the camera
    {
        //Debug.Log(StartRotation);
        //Debug.Log(RotTarget);
        while (LerpFraction < 1) //while the lerp fraction is less than 1
        {
            Debug.Log("Rotating Camera Up");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime; //increment the lerp fraction
            transform.rotation = Quaternion.Lerp(StartRotation, RotTarget, LerpFraction); //set the rotation based on the lerp fraction
        }
        LerpFraction = 0f; //reset the lerp fraction
        StartCoroutine(ReturnCamera(RotTarget, PosTarget)); // start the coroutine for returning the camera

    }

    public IEnumerator MoveCam(Quaternion RotTarget, Vector3 PosTarget)
    {
        float LerpFraction = 0f;
        float LerpSpeed = 1f;
        
        while (LerpFraction < 1)
        {
            LerpFraction += LerpSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(StartPos, PosTarget, LerpFraction);
            yield return new WaitForEndOfFrame();

        }
        StartCoroutine(RotCam(RotTarget, PosTarget));
        
        
    }

    



    
    #region Rotate the Camera // "It's over, Anakin. I have the high ground"
    public IEnumerator ReturnCamera(Quaternion RotTarget, Vector3 StartPos) //return the camera 
    {
        yield return StartCoroutine(RotPause()); //start the coroutine to hold the camera in place
        yield return StartCoroutine(RotCamStart(RotTarget, StartPos)); //start the corountine to return the camera to its start rotation
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

    public IEnumerator RotCamStart(Quaternion RotTarget, Vector3 PosTarget) //same as the first rot cam
    {
        while (LerpFraction < 1)
        {
            Debug.Log("Returning Camera to position");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(RotTarget, StartRotation, LerpFraction);
            if (MovementPlus == true)
            {
                transform.position = Vector3.Slerp(PosTarget, StartPos, LerpFraction);
            }
        }
        LerpFraction = 0f;
    }

    #endregion


}
