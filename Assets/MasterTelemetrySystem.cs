using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MasterTelemetrySystem : MonoBehaviour
{
    public bool TelemetryActive;

    public string ID;
    // Start is called before the first frame update
    void Start()
    {
        if (TelemetryActive == true) //if the telemetry system is wanted to run
        {
            CreateFile(); //run the create file function
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateFile()
    {
        System.Random random = new System.Random();

        char Letter1 = (char)random.Next('A', 'Z'); //generate a username at random
        char Letter2 = (char)random.Next('A', 'Z');
        char Num1 = (char)random.Next('0', '9');
        char Num2 = (char)random.Next('0', '9');
        char Num3 = (char)random.Next('0', '9');
        char Num4 = (char)random.Next('0', '9');

        ID = (Letter1.ToString() + Letter2.ToString() + Num1.ToString() + Num2.ToString() + Num3.ToString() + Num4.ToString());
        string CurrentDate = System.DateTime.Now.ToShortDateString();
        string CurrentTime = System.DateTime.Now.ToLongTimeString();

        CurrentDate = CurrentDate.Replace("/", "-");
        CurrentTime = CurrentTime.Replace(":", "-");

        string FileName = "/" + ID + CurrentDate + "_" + CurrentTime + "_" + ".csv";
        string FilePath = Application.streamingAssetsPath + "/Logs" + FileName;

        StreamWriter SW = new StreamWriter(FilePath, false);
        SW.WriteLine(ID + ID + ID);
        SW.Close();
    }
}
