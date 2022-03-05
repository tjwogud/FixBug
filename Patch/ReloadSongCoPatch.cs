using HarmonyLib;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(CustomLevel), "ReloadSongCo")]
    public static class FindOrLoadAudioClipExternalPatch
    {
        public static void Prefix(CustomLevel __instance)
        {
            string key = __instance.Get<string>("currentSongKey");
            if (key == null)
                return;
            if (!AudioManager.Instance.audioLib.ContainsKey(key))
                __instance.Set("currentSongKey", null);
        }
    }
}
