using HarmonyLib;
using UnityEngine;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scnEditor), "Start")]
    public static class LateUpdatePatch
    {
        public static void Postfix()
        {
            scnEditor.instance.gameObject.AddComponent<VolumeSetter>();
        }

        public class VolumeSetter : MonoBehaviour
        {
            public void LateUpdate()
            {
                float num = scrController.volume * 0.1f;
                AudioListener.volume = 0.5f * num * num;
            }
        }
    }
}
