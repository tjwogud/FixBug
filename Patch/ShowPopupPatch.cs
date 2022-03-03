using DG.Tweening;
using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace FixBug.Patch
{
    [HarmonyPatch(typeof(scnEditor), "ShowPopup")]
    public static class ShowPopupPatch
    {
        public static void Postfix(scnEditor __instance, bool show, scnEditor.PopupType popupType = scnEditor.PopupType.SaveBeforeSongImport)
        {
            RectTransform component = __instance.popupWindow.GetComponent<RectTransform>();
            float num1 = (popupType == scnEditor.PopupType.ExportLevel || popupType == scnEditor.PopupType.MissingExportParams) ? 450f : 200f;
            float num2 = 20f;
            float endValue = show ? num2 : num1;
            DOTween.TweensByTarget(component).Last().OnKill(delegate
            {
                component.AnchorPosY(endValue);
                __instance.Set("popupIsAnimating", false);
                if (!show)
                    __instance.popupPanel.SetActive(false);
            });
        }
    }
}
