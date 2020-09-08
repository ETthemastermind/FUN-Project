using UnityEngine;

public class BallController : MonoBehaviour
{

    public float MoveValue = 1f; //default moving increment for the ball
    public bool _BallIsMoving = false; //boolean that turns on if the ball is moving

    public Vector3 BallStart; //starting vector for the ball
    public Vector3 BallDestination; //destination vector for the ball

    private string ChosenDirection; //direction that the ball is moving 

    public float LerpFraction; //current lerp fraction
    public float LerpSpeed = 0.5f; //speed from transisiting from start to destination
    public float RotSpeed = 100f; //speed at which the ball rotates

    public int PlayerScore; //integer to hold the player score i.e. how many balloons popped
    public GameObject _HUDController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.UpArrow))  //debug arrow key movement
        {
            BallForwards(); //call the ball forwards/right/left/backwards script

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




        if (_BallIsMoving == true) //if the ball is moving
        {
            if (LerpFraction < 1) //if the lerp isnt complete
            {
                LerpFraction += Time.deltaTime * LerpSpeed; //increase the lerp fraction
                gameObject.transform.position = Vector3.Lerp(BallStart, BallDestination, LerpFraction); //change the position of the ball based of the lerp fraction



                switch (ChosenDirection) //switch case statement to determine which way the ball should spin
                {
                    case "F": // F/B/R/L for forwards, backwards, Right and Left
                        Debug.Log("Ball Moving Forwards");
                        gameObject.transform.Rotate(0, 0, (RotSpeed * -1) * Time.deltaTime, Space.World); //rotate the ball appropriately 
                        break;
                    case "B":
                        Debug.Log("Ball Moving Backwards");
                        gameObject.transform.Rotate(0, 0, (RotSpeed * 1) * Time.deltaTime, Space.World);
                        break;

                    case "R":
                        Debug.Log("Ball Moving Right");
                        gameObject.transform.Rotate((RotSpeed * -1) * Time.deltaTime, 0, 0, Space.World);
                        break;
                    case "L":
                        Debug.Log("Ball Moving Right");
                        gameObject.transform.Rotate((RotSpeed * 1) * Time.deltaTime, 0, 0, Space.World);
                        break;

                    default: //in the unlikley case of the switch running without getting a value
                        Debug.Log("Unknown Value");
                        break;


                }        
            }

            else
            {
                _BallIsMoving = false; //ball is no longer moving
                LerpFraction = 0f; //reset the fraction so it can be used again
                
                
            }


            
        }
        
    }

    public void BallForwards() //moving the ball forwards/right/left/backwards, pretty much the exact same code for forwards/right/left/back functions
    {
        if (_BallIsMoving == false) //if the ball isnt already moving
        {
            Debug.Log("Ball forwards button press active"); //confirms button press
            BallStart = gameObject.transform.position; //gets the current starting vector of the ball
            BallDestination = new Vector3(BallStart.x + MoveValue, gameObject.transform.position.y, gameObject.transform.position.z); //calculate the destination vector of the ball
            if (BallStart.x != 5) //if the starting location X value of the ball is not equal to 5, the walls have been adjusted so that the ball will be at the walls when the vector is at 5
            {
                _BallIsMoving = true; //tell the ball to start moving
                ChosenDirection = "F"; //set the chosen direction to F/B/R/L
            }
            else
            {
                Debug.Log("Ball at the end of the grid"); //returns that the ball has reached its limit
            }
            
            
        }
        
    }

    public void BallRight()
    {
        if (_BallIsMoving == false)
        {
            Debug.Log("Ball forwards button press active");
            BallStart = gameObject.transform.position;
            BallDestination = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, BallStart.z - MoveValue);

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
            BallDestination = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, BallStart.z + MoveValue);


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
    
    
    public void OnTriggerStay(Collider other) //when the ball stays in a trigger area
    {
        if (other.gameObject.tag == "Boundary") //if trigger area entered belongs to the boundary wall
        {
            Debug.Log("Boundary Hit");
            HapticFeedback(); //run the haptic feedback function
        }
    }
    /*
    public void OnTriggerEnter(Collider other) //when the ball enters a trigger area
    {
        if (other.gameObject.tag == "Balloon") // if the other object is a balloon
        {
            PlayerScore++; //increment the player's score
            _HUDController.GetComponent<HudController>().IncrementScore(PlayerScore); //update the players score text in the hud controller
            Destroy(other.gameObject); //destroy the balloon
            HapticFeedback(); //run the haptic feedback function
        }
    }
    */

    public void HapticFeedback()
    {
        Debug.Log("Bzz Bzz Haptic Feedback Bzz Bzz"); //buzz buzz
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Balloon") // if the other object is a balloon
        {
            PlayerScore++; //increment the player's score
            _HUDController.GetComponent<HudController>().IncrementScore(PlayerScore); //update the players score text in the hud controller
            Destroy(collision.gameObject); //destroy the balloon
            HapticFeedback(); //run the haptic feedback function
        }
    }

}
