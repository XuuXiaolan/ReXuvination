using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(Landmine))]
static class LandminePatch
{
    private static int playersRagdollAndPropsMask = 0;

    [HarmonyPatch(nameof(Landmine.Start)), HarmonyPostfix]
    public static void LandmineStartPatch(Landmine __instance)
    {
        if (playersRagdollAndPropsMask == 0)
        {
            playersRagdollAndPropsMask = LayerMask.GetMask("Props", "Player", "PlayerRagdoll");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~playersRagdollAndPropsMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in Landmine for Object: {__instance.gameObject}");
    }
}