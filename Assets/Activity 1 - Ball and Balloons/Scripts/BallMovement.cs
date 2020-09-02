using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    public float MoveValue = 0.5f;
    public bool _BallIsMoving = false;

    public Vector3 BallStart;
    public Vector3 BallDestination;
    public bool ForwardDirection;
    public bool SidewaysDirection;

    public float LerpFraction;
    public float LerpSpeed = 0.5f;
    public float RotSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.UpArrow))  //debug arrow key movement
        {
            BallForwards();

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            BallRight();

        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BallLeft();

        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            BallBackwards();

        }




        if (_BallIsMoving == true)
        {
            if (LerpFraction < 1)
            {
                LerpFraction += Time.deltaTime * LerpSpeed;
                gameObject.transform.position = Vector3.Lerp(BallStart, BallDestination, LerpFraction);

                //gameObject.transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
                if (ForwardDirection == true)
                {
                    gameObject.transform.Rotate(0, 0, RotSpeed * Time.deltaTime);
                }
                else if (ForwardDirection == false)
                {
                    gameObject.transform.Rotate(0, 0, (RotSpeed * -1) * Time.deltaTime);
                }
               
            }

            else
            {
                _BallIsMoving = false;
                LerpFraction = 0f;
                
            }


            
        }
        
    }

    public void BallForwards()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball forwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(BallStart.x + MoveValue, gameObject.transform.position.y, gameObject.transform.position.z); //move ball right by the increment value
            if (BallStart.x != 5)
            {
                _BallIsMoving = true;

                ForwardDirection = true;

            }
            else
            {
                Debug.Log("Ball at the end of the grid");
            }
            
            
        }
        
    }

    public void BallRight()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball forwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, BallStart.z - MoveValue); //move ball right by the increment value
            _BallIsMoving = true;
            SidewaysDirection = true;

        }
        
    }


    public void BallLeft()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball forwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, BallStart.z + MoveValue); //move ball right by the increment value
            _BallIsMoving = true;
            

        }
        

    }


    public void BallBackwards()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball backwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(BallStart.x - MoveValue, gameObject.transform.position.y, gameObject.transform.position.z); //move ball right by the increment value
            _BallIsMoving = true;

            ForwardDirection = false;

        }
        
    }
    
    
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            Debug.Log("Bzz Bzz Haptic Feedback Bzz Bzz");
        }
    }

    
}
