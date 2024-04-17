using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data
{
    public class FileManager
    {
        public static void Save<T>(string filePath, T data)
        {
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, jsonData);
        }

        public static void Load<T>(string filePath, Action<T> callback)
        {
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                T data = JsonUtility.FromJson<T>(jsonData);
                callback(data);
            }
            else
            {
                callback(default(T));
            }
        }
    }
}