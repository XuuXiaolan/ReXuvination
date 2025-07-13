using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(QuicksandTrigger))]
static class QuicksandTriggerPatch
{
    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void QuicksandTriggerAwakePatch(QuicksandTrigger __instance)
    {
        if (StartOfRound.Instance == null)
            return;

        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.includeLayers |= StartOfRound.Instance.playersMask;
            collider.excludeLayers = 0;
            collider.layerOverridePriority = -2;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in QuicksandTrigger for Object: {__instance.gameObject}");
    }
}