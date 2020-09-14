using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosMovement : MonoBehaviour
{
    
    public Quaternion TargetRot;    // Start is called before the first frame update
    public Quaternion StartRotation;
    public float LerpSpeed = 1f;
    public float LerpFraction;
    public float TimeBeforeReturn; //how long the camera gets hold in rotation for
    public float CurrentTimeBeforeReturn; //how long the user has already waited for

    void Start()
    {
        TargetRot = transform.rotation;
        StartRotation = transform.rotation;
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Space Pressed");
            TargetRot *= Quaternion.AngleAxis(20, Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Space Pressed");
            TargetRot *= Quaternion.AngleAxis(20, Vector3.down);
        }

        else if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Space Pressed");
            TargetRot *= Quaternion.AngleAxis(20, Vector3.right);
        }

        else if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Space Pressed");
            TargetRot *= Quaternion.AngleAxis(20, Vector3.left);
        }
        
         
        
        float t = Mathf.Clamp(10 * Smooth * Time.deltaTime, 0f, 0.99f);
        transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, t);
        */
    }

    public void DeclarePosition()
    {
        Debug.Log(this.name + " is the current active camera view");
    }

    public void RotateUp()
    {
        TargetRot *= Quaternion.AngleAxis(20, Vector3.left);
        StartCoroutine(RotCam(TargetRot));
        TargetRot = StartRotation;
    }
    public void RotateRight()
    {
        TargetRot *= Quaternion.AngleAxis(20, Vector3.up);
        StartCoroutine(RotCam(TargetRot));
        TargetRot = StartRotation;
    }
    public void RotateDown()
    {
        TargetRot *= Quaternion.AngleAxis(20, Vector3.right);
        StartCoroutine(RotCam(TargetRot));
        TargetRot = StartRotation;
    }
    public void RotateLeft()
    {
        TargetRot *= Quaternion.AngleAxis(20, Vector3.down);
        StartCoroutine(RotCam(TargetRot));
        TargetRot = StartRotation;
    }



    public IEnumerator RotCam(Quaternion RotTarget)
    {
        while (LerpFraction < 1)
        {
            Debug.Log("Rotating Camera Up");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(StartRotation, RotTarget, LerpFraction);
        }
        LerpFraction = 0f;
        StartCoroutine(ReturnCamera(RotTarget));

    }

    public IEnumerator ReturnCamera(Quaternion RotTarget) //return the camera 
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

    public IEnumerator RotCamStart(Quaternion RotTarget)
    {
        while (LerpFraction < 1)
        {
            Debug.Log("Returning Camera to position");
            yield return new WaitForEndOfFrame();
            LerpFraction += LerpSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(RotTarget, StartRotation, LerpFraction);
        }
        LerpFraction = 0f;
    }

    /*
     * public IEnumerator RotCamStart(Vector3 RotTarget) //basically the same as the RotCam ienum, just in reverse
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
     
    
    */
}
