using FixBug.Utils;
using HarmonyLib;
using System;
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
                uri = "file://" + uri.Substring(7).Escape();
        }
    }
}
