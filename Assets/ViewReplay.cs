using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class ViewReplay : MonoBehaviour //https://answers.unity.com/questions/43780/getting-list-of-files-from-specified-folder.html
{
    public bool ReplayFile;
    public FileInfo[] Files;
    public List<string> list;
    public enum MyEnum
    {


    };

    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Replay FIles");
        Files = dir.GetFiles("*.FUNREPLAY");
        
        /*
        foreach (FileInfo f in Files)
        {
            list.Add(f.FullName);
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
