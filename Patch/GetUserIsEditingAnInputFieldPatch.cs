using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scnEditor), "userIsEditingAnInputField", MethodType.Getter)]
    public static class GetUserIsEditingAnInputFieldPatch
    {
        public static void Postfix(ref bool __result, scnEditor __instance)
        {
            GameObject currentSelectedGameObject = __instance.eventSystem.currentSelectedGameObject;
            if (currentSelectedGameObject != null) {
                InputField inputField = currentSelectedGameObject.GetComponent<InputField>();
                __result = inputField != null && inputField.isFocused;
            }
        }
    }
}
