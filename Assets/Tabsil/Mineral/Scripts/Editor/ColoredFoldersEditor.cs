using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    // Saving data in the EditorPrefs as a string in the following format
    // folderGUID,iconGuid
    // Improve this with Json

    [InitializeOnLoad]
    public static class ColoredFoldersEditor
    {
        private static List<string> iconGuids;

        private static string iconsPath;

        static ColoredFoldersEditor()
        {
            StoreIconGuids();

            EditorApplication.projectWindowItemOnGUI -= OnGUI;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI(string guid, Rect rect)
        {
            if (iconGuids.Count <= 0)
                StoreIconGuids();
            else
                ProcessGUIElement(guid, rect);
        }

        private static void ProcessGUIElement(string guid, Rect rect)
        {
            // Check if we have an editor pref with that guid key
            //string iconGuid = EditorPrefs.GetString(guidPrefix + guid, "");
            string iconGuid = MineralEditor.GetIconGuid(guid);

            // Nothing has been saved for that folder
            if (iconGuid == "" || iconGuid == "00000000000000000000000000000000")
                return;

            Texture2D folderTex = MineralEditor.GetTextureFromGuid(iconGuid);

            if (folderTex == null)
            {
                Debug.LogWarning("Mineral : Folder tex is null");
                return;
            }

            MineralEditor.DrawFolder(rect, folderTex);
        }

        private static void ResetFolderIcon()
        {
            // Multi Selection
            foreach (string guid in Selection.assetGUIDs)
            {
                // Is that a folder ?
                if (!MineralEditor.IsFolder(guid))
                    continue;

                MineralEditor.SaveFolderIcon(guid, "");
            }
        }

        public static void SetIcon(string iconName)
        {
            // Nothing Selected
            if (Selection.activeObject == null)
            {
                Debug.LogWarning("Mineral : Nothing selected. Please select a folder.");
                return;
            }

            // Something selected, but not a folder
            if (!MineralEditor.IsFolder(Selection.activeObject))
            {
                Debug.LogWarning("Mineral : Please select a folder.");
                return;
            }

            // We want to reset the folder icon
            if(iconName == "")
            {
                ResetFolderIcon();
                return;
            }

            string iconGuid = GetIconGuidFromName(iconName);

            if (iconGuid == "")
            {
                Debug.LogWarning("Mineral : invalid icon guid");
                return;
            }    

            // Multi Selection
            foreach(string guid in Selection.assetGUIDs)
            {
                // Is that a folder ?
                if (!MineralEditor.IsFolder(guid))
                    continue;


                MineralEditor.SaveFolderIcon(guid, iconGuid);
            }
        }        

        private static void StoreIconGuids()
        {
            if (iconGuids == null)
                iconGuids = new List<string>();

            iconGuids.Clear();

            // We should first look for the Tabsil folder
            string tabsilFolderPath = MineralEditor.FindFolderByName("Tabsil");

            if(tabsilFolderPath == null || tabsilFolderPath == "")
            {
                Debug.LogError("Mineral : No Tabsil folder found...");
                return;
            }

            iconsPath = tabsilFolderPath + "/" + Constants.localColoredIconsPath; 

            string[] iconsfolder = new string[] { iconsPath };
            string[] assetsGuids = AssetDatabase.FindAssets("t: Texture2D ", iconsfolder);

            foreach (string guid in assetsGuids)
                iconGuids.Add(guid);            
        }

        private static string GetIconGuidFromName(string iconName)
        {
            for (int i = 0; i < iconGuids.Count; i++)
            {
                string iconPath = AssetDatabase.GUIDToAssetPath(iconGuids[i]);
                Texture2D iconTex = AssetDatabase.LoadAssetAtPath<Texture2D>(iconPath);

                if (iconTex.name == iconName)
                    return iconGuids[i];                
            }

            return "";
        }
    }
}

#endif