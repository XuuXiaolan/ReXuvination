using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(ToggleFogTrigger))]
static class ToggleFogTriggerPatch
{
    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void ToggleFogTriggerAwakePatch(ToggleFogTrigger __instance)
    {
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~StartOfRound.Instance.playersMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in ToggleFogTrigger for Object: {__instance.gameObject}");
    }
}