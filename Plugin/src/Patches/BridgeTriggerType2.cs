using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(BridgeTriggerType2))]
static class BridgeTriggerType2Patch
{
    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void BridgeTriggerType2AwakePatch(BridgeTriggerType2 __instance)
    {
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~StartOfRound.Instance.playersMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in BridgeTriggerType2 for Object: {__instance.gameObject}");
    }
}