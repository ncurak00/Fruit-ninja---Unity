using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveScore
{
    [Serializable]
    public class Data
    {
        public int highScore;
        public int index = 0;
    }

    public static string directory = "/SaveData";
    public static string fileName = "save.txt";

    public static void SaveMyData(Data data)
    {
        string dir = Application.persistentDataPath + directory; //dohvati mi path igre

        if(!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(dir + fileName, json); 
    }

    public static Data LoadMyData()
    {
        string fullPath = Application.persistentDataPath + directory + fileName; //dohvati path igre + ime filea
        Data data = new Data();

        if(File.Exists(fullPath)) //ako postoji file
        {
            string json = File.ReadAllText(fullPath);
            data = JsonUtility.FromJson<Data>(json);
        }
        else
        {
            Debug.Log("Error saving file");
        }

        return data;
    }
}
