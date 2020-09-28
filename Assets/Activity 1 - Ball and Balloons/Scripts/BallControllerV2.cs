using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallControllerV2 : MonoBehaviour
{
    public float LerpFraction; //reference for the lerp fraction so i can see it in the inspector
    public float LerpSpeed; //reference for the speed of the ball

    public LayerMask layerMask; //reference for the layermask for the movement raycast
    public bool _BallMoving; //bool to check if the ball is moving
    public float RotSpeed = 500f; //speed at which the ball rotates
    public bool _InBoundary = false;
    private AudioSource AS;
    public AudioClip BoundaryHit;

    public MasterTelemetrySystem TelSystem;



    public UnityEvent RunAfterMove = new UnityEvent();
    public UnityEvent RunBeforeMove = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        AS = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow)) //debug controls
        {
            //MoveForward();
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //MoveRight();
            
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //MoveBackward();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            //LerpFraction = 0f;
        }


    }

    public void MoveForward() ///script to move the ball forward/backwards/left/right
    {
        if (_BallMoving == false) //if the ball isnt moving
        {
            
            Debug.Log("Move Forward"); //print to console that the ball is moving
            RaycastHit hit; //reference for the hit
            if (Physics.SphereCast(transform.position, 0.5f, Vector3.right, out hit, 100f, layerMask.value)) //shoot the ray in the direction the ball is going to move
            {
                if (hit.transform.tag == "GridCube") //if the hit object has the grid cube tag
                {
                    _BallMoving = true; // the ball is now moving
                    Transform HT = hit.transform; //reference for the transform of the hit object
                    Vector3 Target; //reference for a vector3 called target
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z); //calculate the target location, based on the cube hit
                    StartCoroutine(Move(Target, "F")); //start the move coroutine passing in the target and a string of F/B/L/R depending on which way the ball needs to rotate
                    TelSystem.AddLine("Ball moved forward");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                    
                }
                else
                {

                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);
                
            }
            
        }

        
       
        
    }

    public void MoveBackward() //same as above
    {
        if (_BallMoving == false)
        {
            
            Debug.Log("Move Backwards");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.5f, Vector3.left, out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target,"B"));
                    TelSystem.AddLine("Ball moved backward");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                }
                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);
                

            }
            
        }

        
       

    }

    public void MoveRight() //same as above
    {
        if (_BallMoving == false)
        {
            
            Debug.Log("Move Right");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position,0.5f, Vector3.back, out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target,"R"));
                    TelSystem.AddLine("Ball moved right");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                }
                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);
                

            }
        }

    }

    public void MoveLeft() //same as above
    {
        if (_BallMoving == false)
        {
            
            Debug.Log("Move Left");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position,0.5f, Vector3.forward, out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "L"));
                    TelSystem.AddLine("Ball moved left");
                }

               
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));

                }
                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);
                

            }
        }
    }

    

    public void MoveForwardRight()
    {
        if (_BallMoving == false)
        {
            Debug.Log("Move Forward Right");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.right + Vector3.back), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "F"));
                    //TelSystem.AddLine("Ball moved left");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                }
                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);


            }
        }
    }

    public void MoveForwardLeft()
    {
        if (_BallMoving == false)
        {
            Debug.Log("Move Forward Left");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.right + Vector3.forward), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "F"));
                    //TelSystem.AddLine("Ball moved left");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                }
                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);


            }
        }
    }

    public void MoveBackwardRight()
    {
        if (_BallMoving == false)
        {
            Debug.Log("Move Forward Right");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.left + Vector3.back), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "F"));
                    //TelSystem.AddLine("Ball moved left");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                }

                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);


            }
        }
    }

    public void MoveBackwardLeft()
    {
        if (_BallMoving == false)
        {
            Debug.Log("Move Forward Left");
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.left + Vector3.forward), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Vector3 Target;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "L"));
                    //TelSystem.AddLine("Ball moved left");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    Vector3 Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target));
                }

                else
                {
                    _BallMoving = false;
                }

                //Debug.Log(hit.transform.name);
                //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);


            }
        }
    }
    

    public IEnumerator Move(Vector3 Target, string FauxRot) //ienum to move the ball, taking in a target vector
    {
        RunBeforeMove.Invoke();
        LerpFraction = 0f; //set the lerp fraction to 0
        Vector3 StartPos = transform.position; //get the current start position of the ball
        while (LerpFraction < 1) // while the lerp fraction is less than 0
        {
            LerpFraction += (LerpSpeed * Time.deltaTime); //increment the lerp fraction
            transform.position = Vector3.Lerp(StartPos, Target, LerpFraction); //move the ball based on the lerp fraction
            switch (FauxRot) //switch case statement to determine which way the ball should spin
            {
                case "F": // F/B/R/L for forwards, backwards, Right and Left
                    //Debug.Log("Ball Moving Forwards");
                    gameObject.transform.Rotate(0, 0, (RotSpeed * -1) * Time.deltaTime, Space.World); //rotate the ball appropriately 
                    break;
                case "B":
                    //Debug.Log("Ball Moving Backwards");
                    gameObject.transform.Rotate(0, 0, (RotSpeed * 1) * Time.deltaTime, Space.World);
                    break;

                case "R":
                    //Debug.Log("Ball Moving Right");
                    gameObject.transform.Rotate((RotSpeed * -1) * Time.deltaTime, 0, 0, Space.World);
                    break;
                case "L":
                    //Debug.Log("Ball Moving Right");
                    gameObject.transform.Rotate((RotSpeed * 1) * Time.deltaTime, 0, 0, Space.World);
                    break;

                default: //in the unlikley case of the switch running without getting a value
                    Debug.Log("Unknown Value");
                    break;


            }
            yield return new WaitForEndOfFrame();
        }
        _BallMoving = false; //ball has stopped moving, so change the bool to false
        RunAfterMove.Invoke();
    }

    public IEnumerator BoundaryHitMove(Vector3 Target)
    {
        RunBeforeMove.Invoke();
        LerpFraction = 0f; //set the lerp fraction to 0
        Vector3 StartPos = transform.position; //get the current start position of the ball

        while (LerpFraction < 1) // while the lerp fraction is less than 0
        {
            if (_InBoundary == false)
            {
                LerpFraction += (LerpSpeed * Time.deltaTime); //increment the lerp fraction
                transform.position = Vector3.Lerp(StartPos, Target, LerpFraction); //move the ball based on the lerp fraction
                yield return new WaitForEndOfFrame();
            }
            else
            {
                break;
            }
            
        }
        _BallMoving = false; //ball has stopped moving, so change the bool to false
        RunAfterMove.Invoke();
        AS.PlayOneShot(BoundaryHit);
        HapticFeedback();
        StartCoroutine(BoundaryHitPause(StartPos));


    }

    public IEnumerator BoundaryHitPause(Vector3 Target)
    {
        float timeBeforeReturn = 1;
        float currentTimeBeforeReturn = 0;
        while (currentTimeBeforeReturn < timeBeforeReturn)
        {
            currentTimeBeforeReturn++;
            yield return new WaitForSecondsRealtime(timeBeforeReturn);

        }
        StartCoroutine(BoundaryHitReturn(Target));
    }

    public IEnumerator BoundaryHitReturn(Vector3 Target)
    {
        LerpFraction = 0f; //set the lerp fraction to 0
        Vector3 StartPos = transform.position; //get the current start position of the ball
        while (LerpFraction < 1)
        {
            LerpFraction += (LerpSpeed * Time.deltaTime); //increment the lerp fraction
            transform.position = Vector3.Lerp(StartPos, Target, LerpFraction); //move the ball based on the lerp fraction
            yield return new WaitForEndOfFrame();
        }
        
    }















    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            //Debug.Log("Boundary Hit");
            _InBoundary = true;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            //Debug.Log("Boundary Hit");
            _InBoundary = false;
        }
    }
    

    public void HapticFeedback()
    {
        Debug.Log("Bzz Bzz Haptic Feedback Bzz Bzz"); //buzz buzz
    }

    

}
