using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosMovementV2 : MonoBehaviour
{
    public Quaternion StartRot;
    public Vector3 StartPos;

    public Quaternion[] TargetRot;
    public Vector3[] TargetPos;

    public float LerpFraction;
    public float LerpSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        StartRot = transform.rotation;
        StartPos = transform.localPosition;

        //TargetRot[0] = StartRot;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            LerpFraction += (LerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(StartRot, TargetRot[0], LerpFraction);
            transform.localPosition = Vector3.Slerp(StartPos, TargetPos[0], LerpFraction);
            //transform.rotation = TargetRot[0];
            //transform.localPosition = TargetPos[0];

            Debug.Log("Camera Pos at new position");
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            LerpFraction += (LerpSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(TargetRot[0], StartRot, LerpFraction);
            transform.localPosition = Vector3.Slerp(TargetPos[0], StartPos, LerpFraction);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LerpFraction = 0f;
        }

    }
}
