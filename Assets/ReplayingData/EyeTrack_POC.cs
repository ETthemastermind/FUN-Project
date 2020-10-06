using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class EyeTrack_POC : MonoBehaviour
{
    public float MouseX;
    public float MouseY;

    public List<float> TestList = new List<float>();
    public Vector2[] GazeArray = new Vector2[60];
    public int i = 0;
    public List<Vector2> ReplayData = new List<Vector2>();

    public string path;
    public string Rpath;
    StreamWriter sw;

    public bool Gather;
    public bool Replay;
    public string[] TempTemp;

    public GameObject Cursor;
    // Start is called before the first frame update
    void Start()
    {

        path = Application.streamingAssetsPath + "/filename.csv";
        Rpath = Application.streamingAssetsPath + "/TEST_filename.csv";
        if (Gather == true)
        {
            InvokeRepeating("Data", 0.0f, 1f / 60f);
        }

        else if (Replay == true)
        {
            Debug.Log("Replaying");
            StreamReader CSV = new StreamReader(Rpath);
            string Temp = CSV.ReadToEnd();
            TempTemp = Temp.Split('\n');
            for (int i = 0; i < TempTemp.Length - 1; i++)
            {
                string DataEntry = TempTemp[i];
                DataEntry = DataEntry.Replace("(", "");
                DataEntry = DataEntry.Replace(")", "");
                string[] combinedVector = DataEntry.Split(',');
                
                ReplayData.Add(new Vector2(float.Parse(combinedVector[0]), float.Parse(combinedVector[1])));
                Debug.Log("Data added to replay");
            }
            StartCoroutine(ReplayCSV());
            
            
            
            
        }
        
        sw = new StreamWriter(path, true);
        sw.Close();

        
    }

    public IEnumerator ReplayCSV()
    {
        for (int i = 0; i < ReplayData.Count; i++)
        {
            Cursor.transform.position = ReplayData[i];
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        


        
    }

    public void Data()
    {
        if (i <  60)
        {
            MouseX = Input.mousePosition.x;
            MouseY = Input.mousePosition.y;
            GazeArray[i] = new Vector2(MouseX, MouseY);
            i++;
        }
        else
        {
            sw = new StreamWriter(path, true);
            for (int a = 0; a < GazeArray.Length; a++)
            {
                sw.WriteLine(GazeArray[a]);
            }
            sw.Close();
            Array.Clear(GazeArray, 0, GazeArray.Length);
            i = 0;
            
            
        }

    }

    

    

}
