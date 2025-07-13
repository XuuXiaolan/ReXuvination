using System;
using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(SandSpiderWebTrap))]
static class SandSpiderWebTrapPatch
{
    [HarmonyPatch(nameof(SandSpiderWebTrap.Awake)), HarmonyPostfix] // thanks, funo
    static void SandSpiderWebTrapStartPatch(SandSpiderWebTrap __instance)
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

        ReXuvination.Log.LogDebug($"{__instance.gameObject}'s colliders have been optimised!");
    }
}