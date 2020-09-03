using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeButtonMove : MonoBehaviour
{
    public bool ElementPickedUp;
    public Vector3 Location;

    private float[] Anchor_TopLeft = new float[2] {0,1};
    private float[] Anchor_TopMid = new float[2] {0.5f, 1};
    private float[] Anchor_TopRight = new float[2] {1,1};

    private float[] Anchor_MidLeft = new float[2] {0,0.5f};
    private float[] Anchor_MidMid = new float[2] {0.5f, 0.5f };
    private float[] Anchor_MidRight = new float[2] { 1, 0.5f };

    private float[] Anchor_BottomLeft = new float[2] {0,0};
    private float[] Anchor_BottomMid = new float[2] { 0.5f, 0 };
    private float[] Anchor_BottomRight = new float[2] { 1 , 0 };



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (ElementPickedUp == true)
        {
            //Debug.Log(Input.mousePosition);
            Location = Input.mousePosition; //gets the current location 
            gameObject.transform.position = Location;
            
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(Anchor_BottomLeft[0], Anchor_BottomLeft[1]);
            this.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(Anchor_BottomLeft[0], Anchor_BottomLeft[1]);
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
            
        }



    }

    
}
