using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBallController : MonoBehaviour
{
    public float LerpFraction;
    public float LerpSpeed;
    public float TravelDistance = 1f;

    public bool _BallMoving = false;
    //public Vector3 Target;
    //public Vector3 StartRot;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            RotateForward();
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            RotateBackwards();

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RotateRight();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            RotateLeft();
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            Vector3 StartPos = transform.localPosition;
            Vector3 Target = transform.localPosition += (transform.right * TravelDistance);
            StartCoroutine(MoveTowards(StartPos, Target));
        }
    }

    public void RotateForward()
    {
        if (_BallMoving == false)
        {
            _BallMoving = true;
            Debug.Log("Rotate to forward");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            StartCoroutine(RotateTowardsDirection(Target, StartRot));
            Debug.Log("Rotation Complete");
        }
        
    }

    public void RotateBackwards()
    {
        if (_BallMoving == false)
        {
            _BallMoving = true;
            Debug.Log("Rotate to backwards");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            StartCoroutine(RotateTowardsDirection(Target, StartRot));
            Debug.Log("Rotation Complete");
        }
        
    }
    
    public void RotateRight()
    {
        if (_BallMoving == false)
        {
            _BallMoving = true;
            Debug.Log("Rotate to right");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
            StartCoroutine(RotateTowardsDirection(Target, StartRot));
            Debug.Log("Rotation Complete");
        }
        
    }

    public void RotateLeft()
    {
        if (_BallMoving == false)
        {
            _BallMoving = true;
            Debug.Log("Rotate to left");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 270f, transform.eulerAngles.z);
            StartCoroutine(RotateTowardsDirection(Target, StartRot));
            Debug.Log("Rotation Complete");

        }
        

    }
    
    
    
    
    public IEnumerator RotateTowardsDirection(Vector3 Target, Vector3 StartRot)
    {

        while (LerpFraction < 1)
        {
            yield return new WaitForEndOfFrame();
            LerpFraction += Time.deltaTime * LerpSpeed;
            transform.eulerAngles = Vector3.Lerp(StartRot, Target, LerpFraction);
            
        }
        LerpFraction = 0f;
        _BallMoving = false;
        
        
    }

    public IEnumerator MoveTowards(Vector3 StartPos, Vector3 Target)
    {
        if (_BallMoving == false)
        {
            _BallMoving = true;
            Debug.Log("Move Towards");


            while (LerpFraction < 1)
            {
                yield return new WaitForEndOfFrame();
                LerpFraction += Time.deltaTime * LerpSpeed;
                transform.localPosition = Vector3.Lerp(StartPos, Target, LerpFraction);
            }
            LerpFraction = 0f;
            _BallMoving = false;
        }
        

    }
}
