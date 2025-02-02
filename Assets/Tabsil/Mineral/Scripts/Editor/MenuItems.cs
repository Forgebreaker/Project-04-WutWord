using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace Tabsil.Mineral
{
    public static class MenuItems
    {
        private const string menuItemPrefix = "Assets/Mineral/";

        [MenuItem("Tabsil/Mineral/Reset All Folder Icons")]
        private static void ResetMineralPrefs() => MineralEditor.ResetAll();

        [MenuItem(menuItemPrefix + "Black", false, 100000)]
        private static void SetBlackIcon() => ColoredFoldersEditor.SetIcon("Black");

        [MenuItem(menuItemPrefix + "Blue", false, 100000)]
        private static void SetBlueIcon() => ColoredFoldersEditor.SetIcon("Blue");

        [MenuItem("Assets/Mineral/Cyan", false, 100000)]
        private static void SetCyanIcon() => ColoredFoldersEditor.SetIcon("Cyan");

        [MenuItem("Assets/Mineral/Green", false, 100000)]
        private static void SetGreenIcon() => ColoredFoldersEditor.SetIcon("Green");

        [MenuItem("Assets/Mineral/Indigo", false, 100000)]
        private static void SetIndigoIcon() => ColoredFoldersEditor.SetIcon("Indigo");

        [MenuItem("Assets/Mineral/Lime", false, 100000)]
        private static void SetLimeIcon() => ColoredFoldersEditor.SetIcon("Lime");

        [MenuItem("Assets/Mineral/Magenta", false, 100000)]
        private static void SetMagentaIcon() => ColoredFoldersEditor.SetIcon("Magenta");

        [MenuItem("Assets/Mineral/Orange", false, 100000)]
        private static void SetOrangeIcon() => ColoredFoldersEditor.SetIcon("Orange");

        [MenuItem("Assets/Mineral/Pink", false, 100000)]
        private static void SetPinkIcon() => ColoredFoldersEditor.SetIcon("Pink");

        [MenuItem("Assets/Mineral/Purple", false, 100000)]
        private static void SetPurpleIcon() => ColoredFoldersEditor.SetIcon("Purple");

        [MenuItem("Assets/Mineral/Red", false, 100000)]
        private static void SetRedIcon() => ColoredFoldersEditor.SetIcon("Red");

        [MenuItem("Assets/Mineral/White", false, 100000)]
        private static void SetWhiteIcon() => ColoredFoldersEditor.SetIcon("White");

        [MenuItem("Assets/Mineral/Yellow", false, 100000)]
        private static void SetYellowIcon() => ColoredFoldersEditor.SetIcon("Yellow");

        [MenuItem("Assets/Mineral/Rainbow", false, 100000)]
        private static void SetRainbowIcon() => ColoredFoldersEditor.SetIcon("Rainbow");

        [MenuItem("Assets/Mineral/Custom...", false, 100011)]
        private static void SetCustomIcon() => IconFoldersEditor.ChooseCustomIcon();

        [MenuItem("Assets/Mineral/Reset", false, 100022)]
        private static void ResetIcon() => ColoredFoldersEditor.SetIcon("");



        #region Validate MenuItems

        [MenuItem("Assets/Mineral/Black", true)]
        [MenuItem("Assets/Mineral/Blue", true)]
        [MenuItem("Assets/Mineral/Cyan", true)]
        [MenuItem("Assets/Mineral/Green", true)]
        [MenuItem("Assets/Mineral/Indigo", true)]
        [MenuItem("Assets/Mineral/Lime", true)]
        [MenuItem("Assets/Mineral/Magenta", true)]
        [MenuItem("Assets/Mineral/Orange", true)]
        [MenuItem("Assets/Mineral/Pink", true)]
        [MenuItem("Assets/Mineral/Purple", true)]
        [MenuItem("Assets/Mineral/Red", true)]
        [MenuItem("Assets/Mineral/White", true)]
        [MenuItem("Assets/Mineral/Yellow", true)]
        [MenuItem("Assets/Mineral/Rainbow", true)]
        [MenuItem("Assets/Mineral/Custom...", true)]
        [MenuItem("Assets/Mineral/Reset", true)]
        private static bool ValidateFolder() => MineralEditor.IsFolder(Selection.activeObject);

        #endregion
    }
}

#endif