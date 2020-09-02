using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{

    public float MoveValue = 0.5f;
    public bool _BallIsMoving = false;

    public Vector3 BallStart;
    public Vector3 BallDestination;

    public float LerpFraction;
    public float LerpSpeed = 0.5f;
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
            _BallIsMoving = true;
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

        }
        
    }
}
