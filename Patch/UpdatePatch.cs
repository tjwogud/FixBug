using HarmonyLib;
using UnityEngine;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scnEditor), "Update")]
    public static class UpdatePatch
    {
        public static void Postfix(scnEditor __instance)
        {
            if (!__instance.playMode && !__instance.Get<bool>("userIsEditingAnInputField") && !__instance.Get<bool>("showingPopup") &&
                !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) &&
                 (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand)) &&
                  Input.GetKeyDown(KeyCode.N))
            {
                __instance.Method("DeselectAnyUIGameObject");
                __instance.Method("NewLevel");
            }
        }
    }
}
