using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ViewReplay : MonoBehaviour //https://answers.unity.com/questions/43780/getting-list-of-files-from-specified-folder.html
{
    public bool ReplayFile;
    FileInfo[] Files;
    public List<string> Pathlist;
    public List<string> NameList;
    public GameObject ReplayCanvas;
    public TMP_Dropdown DropdownList;

    public EyetrackingDataSave[] EDS_Array;
    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath + "/Replay FIles");
        Files = dir.GetFiles("*.FUNREPLAY");
        
        
        foreach (FileInfo f in Files)
        {

            
            Pathlist.Add(f.FullName);
            NameList.Add(f.Name);

        }
        DropdownList.AddOptions(NameList);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectFile()
    {
        Debug.Log(Pathlist[DropdownList.value - 1]);

    }

    public void PlayFile()
    {
        string path = Pathlist[DropdownList.value - 1];
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Open(path, FileMode.Open);
        EDS_Array = (EyetrackingDataSave[])bf.Deserialize(fs);
        fs.Close();
    }
}
