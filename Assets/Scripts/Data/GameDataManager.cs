using System.Collections.Generic;
using Singleton;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class SaveData
    {
        public int Level;
        public int Gold;
    }
    
    public class GameDataManager : Singleton<GameDataManager>
    {
        // Data with json file and load it from there.
        private const string SAVE_FILE_PATH = "savedata.json";

        public SaveData Data;

        private void Awake()
        {
            LoadData();
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }

        private void LoadData()
        {
            FileManager.Load(SAVE_FILE_PATH, (SaveData data) =>
            {
                if (data == null)
                {
                    Data = new SaveData();
                    Data.Level = 1;
                    Data.Gold = 0;
                }
                else
                {
                    Data = data;
                }
            });
        }

        public void IncrementLevel()
        {
            Data.Level++;
        }

        public void AddGold(int amount)
        {
            Data.Gold += amount;
        }

        private void SaveData()
        {
            FileManager.Save(SAVE_FILE_PATH, Data);
        }
    }
}