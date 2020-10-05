using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReplayTelemetry : MonoBehaviour
{
    //public EyetrackingDataSave EDS;
    public EyetrackingDataSave[] EDS_Array;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Data", 0.0f, 1f / 60f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Data()
    {
        EyetrackingDataSave EDS = new EyetrackingDataSave();
        EDS.MouseX = Input.mousePosition.x;
        EDS.MouseY = Input.mousePosition.y;
        EDS.ButtonPressed = null;
        EDS_Array[i] = EDS;
        i++;
        /*
        EDS.MouseX = Input.mousePosition.x;
        EDS.MouseY = Input.mousePosition.y;
        
        //EDS_List.Add(EDS);
        */


    }
    
    public void DatawithButtonPress(GameObject ButtonPress)
    {
        EyetrackingDataSave EDS = new EyetrackingDataSave();
        EDS.MouseX = Input.mousePosition.x;
        EDS.MouseY = Input.mousePosition.y;
        EDS.ButtonPressed = ButtonPress;
        EDS_Array[i] = EDS;
        i++;




    }
    
}
