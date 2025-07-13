using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(InteractTrigger))]
static class InteractTriggerPatch
{
    private static int enemiesAndPlayersMask = 0;

    [HarmonyPatch(nameof(InteractTrigger.Start)), HarmonyPostfix]
    public static void InteractTriggerStartPatch(InteractTrigger __instance)
    {
        if (enemiesAndPlayersMask == 0)
        {
            enemiesAndPlayersMask = LayerMask.GetMask("Enemies", "Player");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;

            collider.includeLayers |= enemiesAndPlayersMask;
            collider.excludeLayers = 0;
            collider.layerOverridePriority = -2;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in InteractTrigger for Object: {__instance.gameObject}");
    }
}