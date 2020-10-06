using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ReplayTelemetry : MonoBehaviour
{
    //public EyetrackingDataSave EDS;
    public EyetrackingDataSave[] EDS_Array;
    int i = 0;
    private bool TelemetryActive;
    
    // Start is called before the first frame update
    void Start()
    {
        TelemetryActive = gameObject.GetComponent<MasterTelemetrySystem>().TelemetryActive;
        if (TelemetryActive == true)
        {
            CreateFile();
            InvokeRepeating("Data", 0.0f, 1f / 60f);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Data()
    {
        if (i < 60)
        {
            EyetrackingDataSave EDS = new EyetrackingDataSave();
            EDS.MouseX = Input.mousePosition.x;
            EDS.MouseY = Input.mousePosition.y;
            EDS.ButtonPressed = null;
            EDS_Array[i] = EDS;
            i++;
        }
        else
        {
            PushData();
        }
        


    }
    
    public void DatawithButtonPress(GameObject ButtonPress)
    {
        if (i < 60)
        {
            EyetrackingDataSave EDS = new EyetrackingDataSave();
            EDS.MouseX = Input.mousePosition.x;
            EDS.MouseY = Input.mousePosition.y;
            EDS.ButtonPressed = ButtonPress;
            EDS_Array[i] = EDS;
            i++;
        }
        else
        {
            PushData();
        }
        




    }

    public void PushData()
    {
        if (TelemetryActive == true)
        {
            Debug.Log("Data Pushed");
            Array.Clear(EDS_Array, 0, EDS_Array.Length);
        }

    }

    public void CreateFile()
    {
        
        string ID = gameObject.GetComponent<MasterTelemetrySystem>().ID;
        string FileName = "/" + ID + ".FUNREPLAY";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.streamingAssetsPath + "/Replay Files" + FileName);
        fs.Close();

    }
    
}
