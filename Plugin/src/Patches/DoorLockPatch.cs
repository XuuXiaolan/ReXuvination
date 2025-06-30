using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(DoorLock))]
static class DoorLockPatch
{
    private static int enemiesAndPlayersMask = 0;

    [HarmonyPatch(nameof(DoorLock.Awake)), HarmonyPostfix]
    public static void DoorLockAwakePatch(DoorLock __instance)
    {
        if (enemiesAndPlayersMask == 0)
        {
            enemiesAndPlayersMask = LayerMask.GetMask("Enemies") | StartOfRound.Instance.playersMask;
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;

            collider.excludeLayers = ~enemiesAndPlayersMask;
            collider.layerOverridePriority = -2;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in DoorLock for Object: {__instance.gameObject}");
    }
}