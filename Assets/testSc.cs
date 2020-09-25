using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.IO;

public class testSc : MonoBehaviour
{

    //public GameObject[] Buttons; //https://answers.unity.com/questions/828666/46-how-to-get-name-of-button-that-was-clicked.html
    public string[] Data;
    public List<string> TestList = new List<string>();
    public bool WritingToFile;
    public StreamReader TestCSV;

    public string FileName = "/ReplayTestingFile.csv";
    public string path;
    public string LastButtonPress;
    public string[] Temp;
    public GameObject GO;
    public string name;
    public string LineToWrite;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Replay());
        path = Application.streamingAssetsPath + FileName;
        GO = GameObject.Find("Control Arrow - Up");



        if (File.Exists(path))
        {
            Debug.Log("File does exist");
            WritingToFile = false;
            TestCSV = new StreamReader(path);
            string Temp = TestCSV.ReadToEnd();
            Data = Temp.Split('\n');
            Debug.Log(TestCSV);
            //StartCoroutine(Replay());
        }
        else
        {
            Debug.Log("File does not exist");
            StreamWriter SW = new StreamWriter(path, true);
            SW.Close();
            WritingToFile = true;
            //InvokeRepeating("Track", 0.1f, 0.1f);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        string MouseX = Input.mousePosition.x.ToString();
        string MouseY = Input.mousePosition.y.ToString();
        string ButtonInput;
        /*
        if (LastButtonPress == EventSystem.current.currentSelectedGameObject.name)
        {
            ButtonInput = "";
        }
        else
        {
            ButtonInput = EventSystem.current.currentSelectedGameObject.name;
            LastButtonPress = ButtonInput;

        }
        */
        LineToWrite = MouseX + "," + MouseY + ",";

        TestList.Add(LineToWrite);
        

    }

    public void Track()
    {
        if (WritingToFile == true)
        {

            string MouseX = Input.mousePosition.x.ToString();
            string MouseY = Input.mousePosition.y.ToString();
            string ButtonInput;
            if (LastButtonPress == EventSystem.current.currentSelectedGameObject.name)
            {
                ButtonInput = "";
            }
            else
            {
                ButtonInput = EventSystem.current.currentSelectedGameObject.name;
                LastButtonPress = ButtonInput;

            }
            string LineToWrite = MouseX +","+ MouseY + "," + ButtonInput;
            StreamWriter SW = new StreamWriter(path, true);
            SW.WriteLine(LineToWrite);
            SW.Close();


        }

    }
    
    public IEnumerator Replay()
    {
        for (int i = 0; i < Data.Length + 1; i++)
        {
            
            Temp = Data[i].Split(',');
            gameObject.transform.position = new Vector2(float.Parse(Temp[0]), float.Parse(Temp[1]));
            name = Temp[2].ToString();
            
            
            if (name != "")
            {
                //GO = GameObject.Find(name);
                GO.gameObject.GetComponent<Button>().onClick.Invoke(); 
            }
            else
            {
                
                
                

                
            }

            yield return new WaitForSecondsRealtime(1f);


        }
        
    }
    
    
}
