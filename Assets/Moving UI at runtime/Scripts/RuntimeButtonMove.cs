using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuntimeButtonMove : MonoBehaviour //im pretty sure this script is not longer in use!
{
    public bool ElementPickedUp;
    public bool ActiveElement;

    public Vector2 CurrentMouseLocation;
    public float ScreenRes_W;
    public float ScreenRes_H;


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

    public float[] NewAnchor = new float[2];

    // Start is called before the first frame update
    void Start()
    {
        ScreenRes_W = (Screen.width);
        ScreenRes_H = (Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        
        CurrentMouseLocation = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        /*
        if (Input.GetKeyDown(KeyCode.I))
        {
            CalculateAnchor();
        }
        
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
        */
        //scaling testing ========================================================================================

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            
            ScaleCellUp();
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            ScaleCellDown();
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

    
    public void ScaleCellUp()
    {
        Debug.Log("Scale Up");
        float NewY = gameObject.transform.localScale.y + 0.1f;
        float NewX = gameObject.transform.localScale.x + 0.1f;
        gameObject.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);
      
        
    }

    public void ScaleCellDown()
    {
        Debug.Log("Scale Down");
        float NewY = gameObject.transform.localScale.y - 0.1f;
        float NewX = gameObject.transform.localScale.x - 0.1f;
        gameObject.transform.localScale = new Vector3(NewX, NewY, transform.localScale.z);
        
        
    }

    public void CalculateAnchor()
    {
        Debug.Log("Calculating Anchor Point");
        float ScreenRes_W_OneThird = ScreenRes_W / 3;
        float ScreenRes_W_TwoThird = ScreenRes_W_OneThird * 2;

        //Debug.Log(ScreenRes_W_OneThird);
        //Debug.Log(ScreenRes_W_TwoThird);
        //Debug.Log(ScreenRes_W);


        // work out which third of the screen the mouse is on (width)
        if (CurrentMouseLocation.x >= 0 && CurrentMouseLocation.x <= ScreenRes_W_OneThird) //left third
        {
            Debug.Log("Left");
            NewAnchor[0] = 0f;
        }

        else if (CurrentMouseLocation.x >= ScreenRes_W_OneThird && CurrentMouseLocation.x <= ScreenRes_W_TwoThird) // middle third
        {
            Debug.Log("Middle");
            NewAnchor[0] = 0.5f;
        }

        else if (CurrentMouseLocation.x >= ScreenRes_W_TwoThird && CurrentMouseLocation.x <= ScreenRes_W) // top third
        {
            Debug.Log("Top");
            NewAnchor[0] = 1f;
        }

        //work out which third of the screen the mouse is on (height)
        float ScreenRes_H_OneThird = ScreenRes_H / 3;
        float ScreenRes_H_TwoThird = ScreenRes_H_OneThird * 2;

        if (CurrentMouseLocation.y >= 0 && CurrentMouseLocation.y <= ScreenRes_H_OneThird) //left third
        {
            Debug.Log("Bottom");
            NewAnchor[1] = 0f;
        }

        else if (CurrentMouseLocation.y >= ScreenRes_H_OneThird && CurrentMouseLocation.y <= ScreenRes_H_TwoThird) // middle third
        {
            Debug.Log("Middle");
            NewAnchor[1] = 0.5f;
        }

        else if (CurrentMouseLocation.y >= ScreenRes_H_TwoThird && CurrentMouseLocation.y <= ScreenRes_H) // top third
        {
            Debug.Log("Top");
            NewAnchor[1] = 1f;
        }
    }
    
    
}
