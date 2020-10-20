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
    public AudioSource AS;
    public AudioClip BoundaryHit;
    public ParticleSystem WallCollisionFX;
    public MasterTelemetrySystem TelSystem;

    public Vector3 HitLocation;
    public RaycastHit hit;
    Vector3 Target;
    Vector3 StartPos;


    public GameObject CurrentGridGO;
    public UnityEvent RunAfterMove = new UnityEvent();
    public UnityEvent RunBeforeMove = new UnityEvent();
    

    public WaitForSecondsRealtime delay = new WaitForSecondsRealtime(0.01f);
    // Start is called before the first frame update
    void Start()
    {
        TelSystem = GameObject.FindGameObjectWithTag("TelSystem").GetComponent<MasterTelemetrySystem>();
        
        
    }

    public void MoveForward() ///script to move the ball forward/backwards/left/right
    {
        if (_BallMoving == false) //if the ball isnt moving
        {
            
            //Debug.Log("Move Forward"); //print to console that the ball is moving
            
            if (Physics.SphereCast(transform.position, 0.5f, Vector3.right, out hit, 100f, layerMask.value)) //shoot the ray in the direction the ball is going to move
            {
                if (hit.transform.tag == "GridCube") //if the hit object has the grid cube tag
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true; // the ball is now moving
                    Transform HT = hit.transform; //reference for the transform of the hit object
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z); //calculate the target location, based on the cube hit
                    StartCoroutine(Move(Target, "F")); //start the move coroutine passing in the target and a string of F/B/L/R depending on which way the ball needs to rotate
                    TelSystem.AddLine("Ball moved forward");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    HitLocation = hit.point;
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target, "F"));
                    
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
            
            //Debug.Log("Move Backwards");
            
            if (Physics.SphereCast(transform.position, 0.5f, Vector3.left, out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target,"B"));
                    TelSystem.AddLine("Ball moved towards");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target, "B"));
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
            
            //Debug.Log("Move Right");
            
            if (Physics.SphereCast(transform.position,0.5f, Vector3.back, out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target,"R"));
                    TelSystem.AddLine("Ball moved right");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target,"R"));
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
            
            //Debug.Log("Move Left");
            
            if (Physics.SphereCast(transform.position,0.5f, Vector3.forward, out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "L"));
                    TelSystem.AddLine("Ball moved left");
                }

               
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target, "L"));

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
            //Debug.Log("Move Forward Right");
            
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.right + Vector3.back), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "FR"));
                    TelSystem.AddLine("Ball moved forward right");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target, "FR"));
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
            //Debug.Log("Move Forward Left");
       
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.right + Vector3.forward), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "FL"));
                    TelSystem.AddLine("Ball moved forward left");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target, "FL"));
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
            //Debug.Log("Move Forward Right");
            
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.left + Vector3.back), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "BR"));
                    TelSystem.AddLine("Ball moved towards right");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target,"BR"));
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
            //Debug.Log("Move Forward Left");
         
            if (Physics.SphereCast(transform.position, 0.1f, (Vector3.left + Vector3.forward), out hit, 100f, layerMask.value))
            {
                if (hit.transform.tag == "GridCube")
                {
                    CurrentGridGO = hit.transform.gameObject;
                    _BallMoving = true;
                    Transform HT = hit.transform;
                    
                    Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                    StartCoroutine(Move(Target, "BL"));
                    TelSystem.AddLine("Ball moved towards left");
                }
                else if (hit.transform.tag == "Boundary")
                {
                    _BallMoving = true; // the ball is now moving
                    Target = hit.point;
                    StartCoroutine(BoundaryHitMove(Target, "BL"));
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
            FauxRotFunc(FauxRot);



            yield return delay;
        }
        _BallMoving = false; //ball has stopped moving, so change the bool to false
        //RunAfterMove.Invoke();
    }

    public IEnumerator BoundaryHitMove(Vector3 Target, string FauxRot)
    {
        RunBeforeMove.Invoke();
        LerpFraction = 0f; //set the lerp fraction to 0
        StartPos = transform.position; //get the current start position of the ball

        while (LerpFraction < 1) // while the lerp fraction is less than 0
        {
            if (_InBoundary == false)
            {
                LerpFraction += (LerpSpeed * Time.deltaTime); //increment the lerp fraction
                transform.position = Vector3.Lerp(StartPos, Target, LerpFraction); //move the ball based on the lerp fraction
                FauxRotFunc(FauxRot);
                
                yield return delay;
            }
            else
            {
                break;
            }
                

            }
       
        
        AS.PlayOneShot(BoundaryHit);
        HapticFeedback();
        StartCoroutine(BoundaryHitPause(StartPos, FauxRot));


    }

    public IEnumerator BoundaryHitPause(Vector3 Target, string FauxRot)
    {
        float timeBeforeReturn = 1;
        float currentTimeBeforeReturn = 0;
        while (currentTimeBeforeReturn < timeBeforeReturn)
        {
            currentTimeBeforeReturn++;
            yield return new WaitForSecondsRealtime(timeBeforeReturn);

        }
        StartCoroutine(BoundaryHitReturn(Target, FauxRot));
    }

    public IEnumerator BoundaryHitReturn(Vector3 Target, string FauxRot)
    {
        LerpFraction = 0f; //set the lerp fraction to 0
        StartPos = transform.position; //get the current start position of the ball
        switch (FauxRot)
        {
            case "F":
                FauxRot = "B";
                break;
            case "B":
                FauxRot = "F";
                break;
            case "L":
                FauxRot = "R";
                break;
            case "R":
                FauxRot = "L";
                break;
            case "FL":
                FauxRot = "BR";
                break;
            case "FR":
                FauxRot = "BL";
                break;
            case "BL":
                FauxRot = "FR";
                break;
            case "BR":
                FauxRot = "BL";
                break;
        }
        while (LerpFraction < 1)
        {
            LerpFraction += (LerpSpeed * Time.deltaTime); //increment the lerp fraction
            transform.position = Vector3.Lerp(StartPos, Target, LerpFraction); //move the ball based on the lerp fraction
            FauxRotFunc(FauxRot);
            yield return delay;
        }
        _BallMoving = false; //ball has stopped moving, so change the bool to false
        //RunAfterMove.Invoke();

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {

            //Debug.Log("Boundary Hit");
            _InBoundary = true;
            
            //Instantiate(WallCollisionFX, transform.position, transform.rotation);
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
        //Debug.Log("Bzz Bzz Haptic Feedback Bzz Bzz"); //buzz buzz
    }
    
    public void FauxRotFunc(string FauxRot)
    {
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
                //Debug.Log("Ball Moving Left");
                gameObject.transform.Rotate((RotSpeed * 1) * Time.deltaTime, 0, 0, Space.World);
                break;

            case "FR":
                gameObject.transform.Rotate((RotSpeed * -1) * Time.deltaTime, 0, (RotSpeed * -1), Space.World);
                break;

            case "FL":
                gameObject.transform.Rotate((RotSpeed * 1) * Time.deltaTime, 0, (RotSpeed * -1), Space.World);
                break;

            case "BR":
                gameObject.transform.Rotate((RotSpeed * -1) * Time.deltaTime, 0, (RotSpeed * 1), Space.World);
                break;

            case "BL":
                gameObject.transform.Rotate((RotSpeed * 1) * Time.deltaTime, 0, (RotSpeed * 1), Space.World);
                break;

            default: //in the unlikley case of the switch running without getting a value
                Debug.Log("Unknown Value");
                break;
        }

    }



    
}
