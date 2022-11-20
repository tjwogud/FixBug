using FixBug.Utils;
using HarmonyLib;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(UnityWebRequestMultimedia), "GetAudioClip", new Type[] { typeof(string), typeof(AudioType) })]
    public static class GetAudioClipPatch
    {
        public static void Prefix(ref string uri)
        {
            if (uri.StartsWith("file://"))
                uri = Uri.EscapeDataString(uri)
                .Replace(Uri.HexEscape(Path.PathSeparator), Path.PathSeparator.ToString())
                .Replace(Uri.HexEscape(Path.DirectorySeparatorChar), Path.DirectorySeparatorChar.ToString())
                .Replace(Uri.HexEscape(Path.AltDirectorySeparatorChar), Path.AltDirectorySeparatorChar.ToString())
                .Replace(Uri.HexEscape(Path.VolumeSeparatorChar), Path.VolumeSeparatorChar.ToString());
        }
    }
}
