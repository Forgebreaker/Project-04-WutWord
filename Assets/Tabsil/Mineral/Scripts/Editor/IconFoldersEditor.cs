using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SearchService;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    [InitializeOnLoad]
    static class IconFoldersEditor
    {
        private static string clickedFolderGuid;
        private static int controlId;

        static IconFoldersEditor()
        {
            EditorApplication.projectWindowItemOnGUI -= OnGUI;
            EditorApplication.projectWindowItemOnGUI += OnGUI;
        }

        private static void OnGUI(string guid, Rect rect)
        {
            ProcessFolderIcons(guid, rect);
            ProcessIconSelection(guid, rect);
        }

        private static void ProcessFolderIcons(string guid, Rect rect)
        {
            // Check if we have an editor pref with that guid key
            string iconGuid = MineralEditor.GetIconGuid(guid);

            // Nothing has been saved for that folder
            if (iconGuid == "" || iconGuid == "00000000000000000000000000000000")
                return;            

            Texture2D folderTex = MineralEditor.GetTextureFromGuid(iconGuid);

            if (folderTex == null)
            {
                Debug.Log("Icon guid : " + iconGuid);
                Debug.LogWarning("Mineral : null Texture");
                return;
            }
   
            MineralEditor.DrawFolder(rect, folderTex);
        }

        private static void ProcessIconSelection(string guid, Rect rect)
        {
            if (guid != clickedFolderGuid)
                return;

            if (Event.current.commandName == "ObjectSelectorUpdated" && EditorGUIUtility.GetObjectPickerControlID() == controlId)
            {
                Object selectedObject = EditorGUIUtility.GetObjectPickerObject();
                string iconGuid = MineralEditor.GetObjectGuid(selectedObject);

                // Multi Selection
                foreach (string _guid in Selection.assetGUIDs)
                {
                    // Is that a folder ?
                    if (!MineralEditor.IsFolder(_guid))
                        continue;


                    MineralEditor.SaveFolderIcon(_guid, iconGuid);
                }

                //MineralEditor.SaveFolderIcon(guid, iconGuid);
            }
        }

        public static void ChooseCustomIcon()
        {
            if(Selection.activeObject == null)
            {
                Debug.LogWarning("Please select a folder");
                return;
            }

            // Selected Object must be a folder
            if (!MineralEditor.IsFolder(Selection.activeObject))
                return;

            clickedFolderGuid = Selection.assetGUIDs[0];

            controlId = EditorGUIUtility.GetControlID(FocusType.Passive);
            EditorGUIUtility.ShowObjectPicker<Sprite>(null, false, "", controlId);
        }
    }
}

#endif