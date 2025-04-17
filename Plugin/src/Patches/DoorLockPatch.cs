using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(DoorLock))]
static class DoorLockPatch
{
    private static int enemiesMask = 0;

    [HarmonyPatch(nameof(DoorLock.Awake)), HarmonyPostfix]
    public static void DoorLockAwakePatch(DoorLock __instance)
    {
        if (enemiesMask == 0)
        {
            enemiesMask = LayerMask.GetMask("Enemies");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~enemiesMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in DoorLock for Object: {__instance.gameObject}");
    }
}