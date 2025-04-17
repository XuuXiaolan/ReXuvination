using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(PlayerPhysicsRegion))]
static class PlayerPhysicsRegionPatch
{
    private static int playersRagdollAndPropsMask = 0;

    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void PlayerPhysicsRegionAwakePatch(PlayerPhysicsRegion __instance)
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
        ReXuvination.Log.LogDebug($"Optimised Collider layers in PlayerPhysicsRegion for Object: {__instance.gameObject}");
    }
}