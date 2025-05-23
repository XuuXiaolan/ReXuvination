using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(InteractTrigger))]
static class InteractTriggerPatch
{
    [HarmonyPatch(nameof(InteractTrigger.Start)), HarmonyPostfix]
    public static void InteractTriggerStartPatch(InteractTrigger __instance)
    {
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~StartOfRound.Instance.playersMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in InteractTrigger for Object: {__instance.gameObject}");
    }
}