using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeButtonMove : MonoBehaviour
{
    public bool ElementPickedUp;
    
    public Vector2 CurrentMouseLocation;


    //a series of arrays keeping track of what the anchor values are
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
        CurrentMouseLocation = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        if (ElementPickedUp == true)
        {
            //Debug.Log(Input.mousePosition);

            gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);            
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.gameObject.GetComponent<RectTransform>().anchorMax = new Vector2(Anchor_TopRight[0], Anchor_TopRight[1]);
            this.gameObject.GetComponent<RectTransform>().anchorMin = new Vector2(Anchor_TopRight[0], Anchor_TopRight[1]);
        }

        //scaling testing ========================================================================================

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            Debug.Log("Scale Up");
            float NewY = gameObject.transform.localScale.y + 0.1f;
            float NewX = gameObject.transform.localScale.x + 0.1f;
            gameObject.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Debug.Log("Scale Down");
            float NewY = gameObject.transform.localScale.y - 0.1f;
            float NewX = gameObject.transform.localScale.x - 0.1f;
            gameObject.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);
        }

       
        //========================================================================================================

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
