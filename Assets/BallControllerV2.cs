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

    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveForward();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveBackward();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
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
        Debug.Log("Move Forward");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.right * 100f, out hit, layerMask))
        {
            if (hit.transform.tag == "GridCube")
            {
                Transform HT = hit.transform;
                Vector3 Target;
                Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                StartCoroutine(Move(Target));
            }
            
            Debug.Log(hit.transform.name);
            //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);

        }
        
    }

    public void MoveBackward()
    {
        Debug.Log("Move Backwards");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.left * 100f, out hit, layerMask))
        {
            if (hit.transform.tag == "GridCube")
            {
                Transform HT = hit.transform;
                Vector3 Target;
                Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                StartCoroutine(Move(Target));
            }
            
            Debug.Log(hit.transform.name);
            //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);

        }

    }

    public void MoveRight()
    {
        Debug.Log("Move Right");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.back * 100f, out hit,layerMask))
        {
            if (hit.transform.tag == "GridCube")
            {
                Transform HT = hit.transform;
                Vector3 Target;
                Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                StartCoroutine(Move(Target));
            }
            
            Debug.Log(hit.transform.name);
            //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);
            
        }

    }

    public void MoveLeft()
    {
        Debug.Log("Move Left");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward * 100f, out hit,layerMask))
        {
            if (hit.transform.tag == "GridCube")
            {
                Transform HT = hit.transform;
                Vector3 Target;
                Target = new Vector3(HT.position.x, transform.position.y, HT.transform.position.z);
                StartCoroutine(Move(Target));
            }
            
            Debug.Log(hit.transform.name);
            //hit.transform.position = new Vector3(hit.transform.position.x, hit.transform.position.y + 10, hit.transform.position.z);

        }

    }

    public IEnumerator Move(Vector3 Target)
    {
        LerpFraction = 0f;
        Vector3 StartPos = transform.position;
        while (LerpFraction < 1)
        {
            LerpFraction += (LerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(StartPos, Target, LerpFraction);
            yield return new WaitForEndOfFrame();
        }
        
    }

}
