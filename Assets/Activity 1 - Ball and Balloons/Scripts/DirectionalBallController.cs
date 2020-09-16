using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalBallController : MonoBehaviour
{
    public float LerpFraction; //public float for keeping track of the lerp fraction
    public float RotSpeed; //rotation speed 
    public float TravelSpeed; //travel speed
    public float TravelDistance = 1f; //distance the ball travels

    public bool _BallMoving = false; //bool to keep track if the ball is moving
    public Quaternion from;
    //public Vector3 Target;
    //public Vector3 StartRot;

    
    // Start is called before the first frame update
    void Start()
    {
        from = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.localPosition);
        if (Input.GetKey(KeyCode.UpArrow)) //debug controls
        {
            RotateForward();
            
            
            
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateBackwards();
            
            

        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
           
            
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
            
            
        }

        else if (Input.GetKeyDown(KeyCode.Return))
        {
            //MoveBall();
        }

        
    }

    /*
    public void MoveBall() //fucntion to move the ball
    {
        Debug.Log("Moving Ball");
        Vector3 StartPos = transform.localPosition; //gets the current local position of the ball
        Vector3 Target = transform.localPosition + (transform.right * TravelDistance); //calculates the destination of the ball
        StartCoroutine(MoveTowards(StartPos, Target)); //run the coroutine to move the ball
    }
    */
    public void RotateForward() //functions to set the ball to rotate to face the chosen direction, 
    {
        if (_BallMoving == false ) //if the ball is not moving
        {
            _BallMoving = true;
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.AngleAxis(0f, Vector3.up);
            if (from != to)
            {
                //rotate
                StartCoroutine(RotateTowardsDirection(to, from));
            }
            else
            {
                //just move
                StartCoroutine(MoveTowards());
            }
            #region old code
            /*
            _BallMoving = true; //turn the bool to true, therefore the ball is moving
            Debug.Log("Rotate to forward"); //debug which way the ball is rotating to the console
            Vector3 StartRot = transform.eulerAngles; //gets the current rotation
            Vector3 Target = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z); //calculates the target rotation
            if (StartRot != Target)
            {
                StartCoroutine(RotateTowardsDirection(Target, StartRot)); //runs the coroutine to rotate the ball
            }
            else
            {
                StartCoroutine(MoveTowards());
            }
           
            
            Debug.Log("Rotation Complete"); //debug to console that the rotation is complete
            */
            #endregion

        }

    }

    public void RotateBackwards() //same as above but backwards
    {
        if (_BallMoving == false)
        {

            _BallMoving = true;
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.AngleAxis(180f, Vector3.up);
            if (from != to)
            {
                //rotate
                StartCoroutine(RotateTowardsDirection(to, from));
            }
            else
            {
                StartCoroutine(MoveTowards());
                //just move
            }
            #region old code
            /*
            _BallMoving = true;
            Debug.Log("Rotate to backwards");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 180f, transform.eulerAngles.z);
            if (StartRot != Target)
            {
                StartCoroutine(RotateTowardsDirection(Target, StartRot));
            }
            else
            {
                StartCoroutine(MoveTowards());
            }
            
            Debug.Log("Rotation Complete");
            */
            #endregion

        }

    }
    
    public void RotateRight() //same as above but right
    {
        if (_BallMoving == false)
        {

            _BallMoving = true;
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.AngleAxis(90f, Vector3.up);
            if (from != to)
            {
                //rotate
                StartCoroutine(RotateTowardsDirection(to, from));
            }
            else
            {
                //just move
                StartCoroutine(MoveTowards());
            }
            #region old code
            /*
            _BallMoving = true;
            Debug.Log("Rotate to right");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z);
            if (StartRot != Target)
            {
                StartCoroutine(RotateTowardsDirection(Target, StartRot));
            }
            else
            {
                StartCoroutine(MoveTowards());
            }
            
            Debug.Log("Rotation Complete");
            */
            #endregion


        }

    }

    public void RotateLeft() //same as above but left
    {
        if (_BallMoving == false)
        {

            _BallMoving = true;
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.AngleAxis(270f, Vector3.up);
            if (from != to)
            {
                //rotate
                StartCoroutine(RotateTowardsDirection(to, from));
            }
            else
            {
                StartCoroutine(MoveTowards());
            }
            #region old code
            /*
            _BallMoving = true;
            Debug.Log("Rotate to left");
            Vector3 StartRot = transform.eulerAngles;
            Vector3 Target = new Vector3(transform.eulerAngles.x, 270f, transform.eulerAngles.z);
            if (StartRot != Target)
            {
                StartCoroutine(RotateTowardsDirection(Target, StartRot));
            }
            else
            {
                StartCoroutine(MoveTowards());
            }
            
            Debug.Log("Rotation Complete");
            */
            #endregion


        }


    }
    
    
    
    
    public IEnumerator RotateTowardsDirection(Quaternion Target, Quaternion StartRot) //Ienum to rotate the ball to the desired direction
    {

        while (LerpFraction < 1) //while the lerp fraction is less than 1
        {
            yield return new WaitForEndOfFrame(); //wait until the end of the frame
            LerpFraction += Time.deltaTime * RotSpeed; //adjust the lerp fraction
            transform.rotation = Quaternion.Lerp(StartRot, Target, LerpFraction); //rotate the ball between start and target based on the lerp fraction
            
        }
        LerpFraction = 0f; //reset the lerp fraction
        StartCoroutine(MoveTowards());
        
        
        
    }

    public IEnumerator MoveTowards() //Ienum to move the ball forwards
    {
        
        _BallMoving = true; //set the bool to true as the ball is moving
        Debug.Log("Move Towards"); //print to console
        Vector3 StartPos = transform.localPosition; //gets the current local position of the ball
        Vector3 Target = transform.localPosition + (transform.right * TravelDistance); //calculates the destination of the ball

        while (LerpFraction < 1) //while the lerp fraction is less than 1
        {
            yield return new WaitForEndOfFrame(); //wait unti the end of the frame
            LerpFraction += Time.deltaTime * TravelSpeed; //adjust the lerp fraction
            transform.localPosition = Vector3.Lerp(StartPos, Target, LerpFraction); //move the ball between start and target based on the lerp fraction
        }
        LerpFraction = 0f; //reset the lerp fraction
        _BallMoving = false; //change the bool as the ball is not moving


    }
}
