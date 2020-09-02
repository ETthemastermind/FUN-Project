using UnityEngine;

public class BallMovement : MonoBehaviour
{

    public float MoveValue = 0.5f;
    public bool _BallIsMoving = false;

    public Vector3 BallStart;
    public Vector3 BallDestination;

    private string ChosenDirection;

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

                switch (ChosenDirection)
                {
                    case "F":
                        Debug.Log("Ball Moving Forwards");
                        gameObject.transform.Rotate(0, 0, (RotSpeed * -1) * Time.deltaTime, Space.World);
                        break;
                    case "B":
                        Debug.Log("Ball Moving Backwards");
                        gameObject.transform.Rotate(0, 0, (RotSpeed * 1) * Time.deltaTime, Space.World);
                        break;

                    case "R":
                        Debug.Log("Ball Moving Right");
                        gameObject.transform.Rotate((RotSpeed * - 1) * Time.deltaTime, 0, 0, Space.World);
                        break;
                    case "L":
                        Debug.Log("Ball Moving Right");
                        gameObject.transform.Rotate((RotSpeed * 1) * Time.deltaTime, 0, 0, Space.World);
                        break;

                    default:
                        Debug.Log("Unknown Value");
                        break;


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
                ChosenDirection = "F";
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

            if (BallStart.z != -5)
            {
                _BallIsMoving = true;
                ChosenDirection = "R";
            }

            else
            {
                Debug.Log("Ball at the end of the grid");
            }

            
            

        }
        
    }


    public void BallLeft()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball forwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, BallStart.z + MoveValue); //move ball right by the increment value


            if (BallStart.z != 5)
            {
                _BallIsMoving = true;
                ChosenDirection = "L";
            }
            else
            {
                Debug.Log("Ball at the end of the grid");
            }
            

        }
        

    }


    public void BallBackwards()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball backwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(BallStart.x - MoveValue, gameObject.transform.position.y, gameObject.transform.position.z); //move ball right by the increment value

            if (BallStart.x != -5)
            {
                _BallIsMoving = true;
                ChosenDirection = "B";
            }
            else
            {
                Debug.Log("Ball at the end of the grid");
            }



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
