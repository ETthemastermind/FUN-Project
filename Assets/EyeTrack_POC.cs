using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EyeTrack_POC : MonoBehaviour
{
    public float MouseX;
    public float MouseY;

    public List<float> TestList = new List<float>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseX = Input.mousePosition.x;
        MouseY = Input.mousePosition.y;
        //TestList.Add((MouseX + MouseY));


        
    }
}
