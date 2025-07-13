using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(OutOfBoundsTrigger))]
static class OutOfBoundsTriggerPatch
{
    private static int playersAndRagdollMask = 0;

    [HarmonyPatch(nameof(OutOfBoundsTrigger.Start)), HarmonyPostfix]
    public static void OutOfBoundsTriggerStartPatch(OutOfBoundsTrigger __instance)
    {
        if (playersAndRagdollMask == 0)
        {
            playersAndRagdollMask = LayerMask.GetMask("PlayerRagdoll", "Player");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;

            collider.includeLayers |= playersAndRagdollMask;
            collider.excludeLayers = 0;
            collider.layerOverridePriority = -2;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in OutOfBoundsTrigger for Object: {__instance.gameObject}");
    }
}