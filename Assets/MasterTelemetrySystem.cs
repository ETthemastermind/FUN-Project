using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class MasterTelemetrySystem : MonoBehaviour
{
    public bool TelemetryActive;

    public string ID;
    private string[] Headings = new string[6] { "Activity", "Interaction Style", "Action Completed", "Time Completed","X coord", "Y coord"};
    public string FilePath;
    public string LineToWrite;
    // Start is called before the first frame update
    public void Awake()
    {
        if (TelemetryActive == true) //if the telemetry system is wanted to run
        {
            CreateFile(); //run the create file function
        }
    }
    void Start()
    {
        
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
        FilePath = Application.streamingAssetsPath + "/Logs" + FileName;

        StreamWriter SW = new StreamWriter(FilePath, false);
        for (int i = 0; i < Headings.Length; i++)
        {
            LineToWrite += ((Headings[i]) + ",");
        }
        SW.WriteLine(LineToWrite);
        SW.Close();
    }

    public void AddLine(string ActionCompleted)
    {
        string Activity = SceneManager.GetActiveScene().name;
        string InteractionStyle = "Mouse (TEMPORARY HARD CODED OUTPUT)";
        string Action = ActionCompleted;
        string TimeCode = System.DateTime.Now.ToLongTimeString();
        string Xcoord = Input.mousePosition.x.ToString();
        string Ycoord = Input.mousePosition.y.ToString();
        LineToWrite = Activity + "," + InteractionStyle + "," + Action + "," + TimeCode + "," + Xcoord + "," + Ycoord + ",";
        if (TelemetryActive == true)
        {
            StreamWriter SW = new StreamWriter(FilePath, true);
            SW.WriteLine(LineToWrite);
            SW.Close();
        }
        
    }
    
    
}
