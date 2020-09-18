using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControllerV2 : MonoBehaviour
{
    public float gridStep = 1f;
    public float MoveableDistance;

    public float MaxGrid_UD;
    public float MaxGrid_LR;

    public float LerpFraction;
    public float LerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MoveBackward();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LerpFraction = 0f;
        }


    }

    public void MoveForward()
    {
        if (transform.position.x != MaxGrid_UD)
        {
            if (transform.position.x != MaxGrid_UD * 1)
            {
                Debug.Log("Move Backward");
                transform.position += Vector3.right * gridStep;
            }
        }
        
    }

    public void MoveBackward()
    {
        if (transform.position.x != MaxGrid_UD * -1)
        {
            Debug.Log("Move Backward");
            transform.position += Vector3.left * gridStep;
        }
        
    }

    public void MoveRight()
    {
        if (transform.position.z != (MaxGrid_LR * -1))
        {
            Debug.Log("Move Right");
            transform.position += Vector3.back * gridStep;
        }
        
    }

    public void MoveLeft()
    {
        if (transform.position.z != MaxGrid_LR)
        {
            Debug.Log("Move Left");
            transform.position += Vector3.forward * gridStep;
        }
        
    }

    public IEnumerator Move(Vector3 direction)
    {

        yield return new WaitForEndOfFrame();
    }

}
