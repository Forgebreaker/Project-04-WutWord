using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tabsil.Mineral
{
    public class MineralPrefs
    {
        [SerializeField]
        public List<FolderData> folderDatas;

        public MineralPrefs()
        {
            if(folderDatas == null)
                folderDatas = new List<FolderData>();        
        }

        public void Initialize()
        {
            if (folderDatas == null)
                folderDatas = new List<FolderData>();
        }

        public void Add(KeyValuePair<string, string> keyValuePair)
        {
            // We want to reset a folder
            // Just remove the key
            if (keyValuePair.Value == "")
            {
                for (int i = 0; i < folderDatas.Count; i++)
                {
                    if (folderDatas[i].GetKey() == keyValuePair.Key)
                    {
                        folderDatas.RemoveAt(i);
                        return;
                    }
                }
            }

            for (int i = 0; i < folderDatas.Count; i++)
            {
                if (folderDatas[i].GetKey() == keyValuePair.Key)
                {
                    folderDatas[i].SetValue(keyValuePair.Value);
                    return;
                }
            }            

            folderDatas.Add(new FolderData(keyValuePair.Key, keyValuePair.Value));
        }

        public void Remove(string key)
        {
            // Remove the key totally if no icon

            int indexToRemove = -1;

            for (int i = 0; i < folderDatas.Count; i++)
            {
                if (folderDatas[i].GetKey() == key)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove > 0)
                folderDatas.RemoveAt(indexToRemove);
        }

        public string GetValue(string key)
        {
            for (int i = 0; i < folderDatas.Count; i++)
                if (folderDatas[i].GetKey() == key)
                    return folderDatas[i].GetValue();

            return "";
        }

        private void Log()
        {
            for (int i = 0; i < folderDatas.Count; i++)
                Debug.Log(folderDatas[i].ToString());
        }
    }

    [System.Serializable]
    public class FolderData
    {
        [SerializeField]
        private string key;

        [SerializeField]
        private string value;

        public FolderData(string key, string value)
        {
            this.key = key;
            this.value = value;
        }

        public string GetKey()
        {
            return key;
        }

        public string GetValue()
        {
            return value;
        }

        public void SetValue(string newValue)
        {
            value = newValue;
        }

        public override string ToString()
        {
            return "Key : " + key + "\nValue : " + value;
        }
    }
}