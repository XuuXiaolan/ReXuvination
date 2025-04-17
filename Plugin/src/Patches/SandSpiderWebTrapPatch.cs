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
        Collider[] colliders = __instance.GetComponents<Collider>();
        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger) continue;
            collider.excludeLayers = ~StartOfRound.Instance.playersMask;
        }

        ReXuvination.Log.LogDebug($"{__instance.gameObject}'s colliders have been optimised!");
    }
}