using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(TerrainObstacleTrigger))]
static class TerrainObstacleTriggerPatch
{
    private static int vehicleMask = 0;

    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void TerrainObstacleTriggerAwakePatch(TerrainObstacleTrigger __instance)
    {
        if (vehicleMask == 0)
        {
            vehicleMask = LayerMask.GetMask("Vehicle");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~vehicleMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in TerrainObstacleTrigger for Object: {__instance.gameObject}");
    }
}