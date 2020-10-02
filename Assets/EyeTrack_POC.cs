using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyeTrack_POC : MonoBehaviour
{
    public float MouseX;
    public float MouseY;

    public List<float> TestList = new List<float>();
    public Vector2[] GazeArray = new Vector2[60];
    int i = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DataeryBoi", 0.0f, 1f / 60f);
        InvokeRepeating("TestClear",1f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        


        
    }

    public void DataeryBoi()
    {
        MouseX = Input.mousePosition.x;
        MouseY = Input.mousePosition.y;
        GazeArray[i] = new Vector2(MouseX, MouseY);
        i++;
        
        
    }

    public void TestClear()
    {
        Array.Clear(GazeArray,0, GazeArray.Length);
        i = 0;
    }

}
