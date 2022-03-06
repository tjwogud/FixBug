using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scnEditor), "PublishToSteam")]
    public static class PublishToSteamPatch
    {
        public static void Prefix(List<string> includedFiles, string tempDir)
        {
            includedFiles.RemoveAll(file => !File.Exists(file));
            includedFiles.ForEach(file => {
                if (!file.StartsWith(tempDir))
                    try {
                        File.Delete(Path.Combine(tempDir, Path.GetFileName(file)));
                    } catch (Exception) {
                    }
            });
        }
    }
}
