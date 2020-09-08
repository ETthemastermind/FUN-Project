using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePrefs(Activity1Settings gameSettings)
    {
        BinaryFormatter BF = new BinaryFormatter();
        string path = Application.streamingAssetsPath + "/filename.txt";
        FileStream fs = new FileStream(path, FileMode.Create);

        Profile data = new Profile(gameSettings);

        BF.Serialize(fs, data);
        fs.Close();
            
    }

    public static Profile LoadPrefs()
    {
        string path = Application.streamingAssetsPath + "/filename.txt";
        
        if (File.Exists(path))
        {
            BinaryFormatter BF = new BinaryFormatter();
            FileStream fs = new FileStream(path, FileMode.Open);

            Profile data = BF.Deserialize(fs) as Profile;
            fs.Close();
            return data;
        }
        else
        {
            Debug.Log("No matching player prefs found" + path);
            return null;
         
        }

    }
}
