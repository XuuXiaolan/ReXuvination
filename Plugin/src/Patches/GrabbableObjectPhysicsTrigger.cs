using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(GrabbableObjectPhysicsTrigger))]
static class GrabbableObjectPhysicsTriggerPatch
{
    private static int enemiesAndPlayerMask = 0;

    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void GrabbableObjectPhysicsTriggerAwakePatch(GrabbableObjectPhysicsTrigger __instance)
    {
        if (enemiesAndPlayerMask == 0)
        {
            enemiesAndPlayerMask = LayerMask.GetMask("Enemies", "Player");
        }
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~enemiesAndPlayerMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in GrabbableObjectPhysicsTrigger for Object: {__instance.gameObject}");
    }
}