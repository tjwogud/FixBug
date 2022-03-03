using HarmonyLib;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scrController), "OnMusicScheduled")]
    public static class OnMusicScheduledPatch
    {
        public static bool Prefix()
        {
            return scnEditor.instance == null || scnEditor.instance.playMode;
        }
    }
}
