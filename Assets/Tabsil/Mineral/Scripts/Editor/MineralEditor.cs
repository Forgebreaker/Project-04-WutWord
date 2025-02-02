using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    [InitializeOnLoad]
    public static class MineralEditor 
    {
        private static MineralPrefs mineralPrefs;
        private static string dataPath;

        static MineralEditor()
        {
            LoadData();
        }


        public static Texture2D GetTextureFromGuid(string guid)
        {
            return AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid));
        }

        public static bool IsFolder(string guid)
        {
            return AssetDatabase.IsValidFolder(AssetDatabase.GUIDToAssetPath(guid));
            //return Path.GetExtension(AssetDatabase.GUIDToAssetPath(guid)) == "";
        }

        public static bool IsFolder(Object obj)
        {
            return AssetDatabase.IsValidFolder(AssetDatabase.GetAssetPath(obj));
            //return Path.GetExtension(AssetDatabase.GetAssetPath(obj)) == "";
        }

        public static void ResetAll()
        {
            mineralPrefs = new MineralPrefs();
            SaveData();
        }

        public static void SaveFolderIcon(string folderGuid, string iconGuid)
        {
            mineralPrefs.Add(new KeyValuePair<string, string>(folderGuid, iconGuid));
            SaveData();
        }

        private static void SaveData()
        {
            string data = JsonUtility.ToJson(mineralPrefs, true);
            File.WriteAllText(dataPath, data);
        }

        private static void LoadData()
        {
            // We should first look for the Tabsil folder
            string tabsilFolderPath = FindFolderByName("Tabsil");
            dataPath = tabsilFolderPath + "/" + Constants.localDataPath;

            if (!File.Exists(dataPath))
            {
                FileStream fs = new FileStream(dataPath, FileMode.Create);

                mineralPrefs = new MineralPrefs();

                string worldDataString = JsonUtility.ToJson(mineralPrefs, true);

                byte[] worldDataBytes = Encoding.UTF8.GetBytes(worldDataString);

                fs.Write(worldDataBytes);

                fs.Close();
            }
            else
            {
                string data = File.ReadAllText(dataPath);
                mineralPrefs = JsonUtility.FromJson<MineralPrefs>(data);

                mineralPrefs.Initialize();
            }
        }

        public static string GetIconGuid(string folderGuid)
        {
            return mineralPrefs.GetValue(folderGuid);
        }



        public static string FindFolderByName(string folderName)
        {
            // Use AssetDatabase to find the folder
            string[] folderGuids = AssetDatabase.FindAssets("t:Folder " + folderName);

            if (folderGuids.Length > 0)
            {
                // Get the path of the first found folder
                string folderPath = AssetDatabase.GUIDToAssetPath(folderGuids[0]);
                return folderPath;
            }

            // Folder not found
            return null;
        }

        public static string GetObjectGuid(Object obj)
        {
            string objPath = AssetDatabase.GetAssetPath(obj);
            return AssetDatabase.GUIDFromAssetPath(objPath).ToString();
        }

        public static void DrawFolder(Rect rect, Texture2D folderTex)
        {
            bool isTreeView;
            Rect folderRect = GetFolderRect(rect, out isTreeView);

            Color rectColor = Constants.viewColor;

            if (isTreeView)
                rectColor = Constants.treeColor;

            // Draw a background color
            EditorGUI.DrawRect(folderRect, rectColor);

            // Draw the actual folder
            GUI.DrawTexture(folderRect, folderTex, ScaleMode.ScaleToFit);
        }

        private static Rect GetFolderRect(Rect rect, out bool treeView)
        {
            treeView = false;

            // Second Colum, Small Scale
            if (rect.x < 15)
                return new Rect(rect.x + 3, rect.y, rect.height, rect.height);

            // First Column
            else if (rect.x > 15 && rect.height < 30)
            {
                treeView = true;
                return new Rect(rect.x, rect.y, rect.height, rect.height);
            }

            // Second Colum, Big Scale
            return new Rect(rect.x, rect.y, rect.width, rect.width);
        }

    }

}

#endif