using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Tabsil.Sijil
{
    [Serializable]
    public class GameData
    {
        [SerializeField] Dictionary<string, GameDataItem> data = new Dictionary<string, GameDataItem>();

        public void Add(string key, Type dataType, string value)
        {
            if (data.ContainsKey(key))
                data.Remove(key);

            GameDataItem item = new GameDataItem(dataType.AssemblyQualifiedName, value);
            data.Add(key, item);
        }

        public bool TryGetValue(string key, out Type dataType, out string value)
        {
            if (data.ContainsKey(key))
            {
                dataType = Type.GetType(data[key].dataType);
                value = data[key].value;
                return true;
            }

            dataType = null;
            value = null;
            return false;
        }
    }

    [Serializable]
    public struct GameDataItem
    {
        [SerializeField] public string dataType;
        [SerializeField] public string value;

        public GameDataItem(string dataType, string value)
        {
            this.dataType = dataType;
            this.value = value;
        }
    }
}