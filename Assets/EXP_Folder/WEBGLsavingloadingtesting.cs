using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WEBGLsavingloadingtesting : MonoBehaviour  //https://forum.unity.com/threads/webgl-builds-and-streamingassetspath.366346/ https://answers.unity.com/questions/525737/cant-get-streaming-assets-folder-to-work-with-the.html https://stackoverflow.com/questions/43693213/application-streamingassetspath-and-webgl-build
{
    string FileName = "/WEGBL.txt";
    string FilePath;
    public TextMeshProUGUI Input;
    public Text DebugText;
    // Start is called before the first frame update
    void Start()
    {
        FilePath = Application.streamingAssetsPath + FileName;
        DebugText.text = FilePath;
        StartCoroutine(LoadData(FileName));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        Debug.Log("Saving Data");
        StreamWriter sw = new StreamWriter(FilePath);
        sw.WriteLine(Input.text);
        sw.Close();
    }
    public IEnumerator LoadData(string Filename)
    {
        //FilePath = Path.Combine(Application.streamingAssetsPath, FileName);
        //Debug.Log(FilePath);
        string result;
        if (FilePath.Contains("://") || FilePath.Contains(":///"))
        {
            WWW www = new WWW(FilePath);
            yield return www;
            result = www.text;
            DebugText.text = result;
            Input.text = (result.ToString());
        }
        else
        {
            StreamReader sr = new StreamReader(FilePath);
            string Contents = sr.ReadToEnd();
            Input.text = Contents;
        }

    }
    
    /*
    public void LoadData()
    {
        StreamReader sr = new StreamReader(FilePath);
        string Contents = sr.ReadToEnd();
        Input.text = Contents;
    }
    */
}
