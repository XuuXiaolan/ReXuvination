using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(BridgeTrigger))]
static class BridgeTriggerPatch
{
    private static int enemiesAndPlayerMask = 0;

    [HarmonyPatch(nameof(BridgeTrigger.OnEnable)), HarmonyPostfix]
    public static void BridgeTriggerOnEnablePatch(BridgeTrigger __instance)
    {
        if (enemiesAndPlayerMask == 0)
        {
            enemiesAndPlayerMask = LayerMask.GetMask("Enemies", "Player");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~enemiesAndPlayerMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in BridgeTrigger for Object: {__instance.gameObject}");
    }
}