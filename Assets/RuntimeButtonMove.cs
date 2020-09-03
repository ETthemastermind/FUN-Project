using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeButtonMove : MonoBehaviour
{
    public bool ElementPickedUp;
    public Vector3 Location;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ElementPickedUp == true)
        {
            Debug.Log(Input.mousePosition);
            Location = Input.mousePosition;
            gameObject.transform.position = Location;
        }
       
    }

    public void PickUp()
    {
        if (ElementPickedUp == false)
        {
            ElementPickedUp = true;
            Debug.Log("Element Picked Up");

        }

        else if (ElementPickedUp == true)
        {
            ElementPickedUp = false;
            Debug.Log("Element Put Down");
        }



    }
}
