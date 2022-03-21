using FixBug.Patch;
using HarmonyLib;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityModManagerNet;

namespace FixBug
{
    public static class Main
    {
        public static UnityModManager.ModEntry.ModLogger Logger;
        public static Harmony harmony;
        public static bool IsEnabled = false;

        public static void Setup(UnityModManager.ModEntry modEntry)
        {
            Logger = modEntry.Logger;
            modEntry.OnToggle = OnToggle;
            modEntry.OnGUI = OnGUI;
            string path = Path.Combine(Path.GetFullPath("."), "steam_appid.txt");
            if (!File.Exists(path))
                using (StreamWriter sw = File.CreateText(path))
                    sw.WriteLine("977950");
        }

        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            IsEnabled = value;
            if (value)
            {
                harmony = new Harmony(modEntry.Info.Id);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                if (typeof(FloorMesh).GetMethod("GenerateCollider") != null)
                    harmony.Patch(typeof(scnEditor).GetMethod("ObjectsAtMouse", BindingFlags.NonPublic | BindingFlags.Instance), new HarmonyMethod(typeof(ObjectsAtMousePatch), "Prefix"));
            }
            else
            {
                harmony.UnpatchAll(modEntry.Info.Id);
            }
            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (GUILayout.Button(RDString.language == SystemLanguage.Korean ? "현재 장면 재시작" : "Reload Current Scene", GUILayout.Width(150)))
                ADOBase.RestartScene();
            if (GUILayout.Button(RDString.language == SystemLanguage.Korean ? "게임 강제종료" : "Force Quit Game", GUILayout.Width(150)))
            {
                scnEditor.instance?.Set("forceQuit", true);
                Application.Quit();
            }
        }
    }
}