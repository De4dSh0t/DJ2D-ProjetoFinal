using System.IO;
using UnityEngine;

public static class SaveManager
{
    private const string Dir = "/SaveData/";
    private const string FileName = "SaveData.dat";
    
    public static void Save(SaveData saveData)
    {
        string dir = Application.persistentDataPath + Dir;
        
        // Create directory if it doesn't already exist
        if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(dir + FileName, json);
    }
    
    public static SaveData Load()
    {
        string path = Application.persistentDataPath + Dir + FileName;
        
        // Check if file exists
        if (!File.Exists(path)) return null;
        
        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }
    
    public static bool ContainsSave()
    {
        string path = Application.persistentDataPath + Dir + FileName;
        if (File.Exists(path)) return true;
        return false;
    }
}