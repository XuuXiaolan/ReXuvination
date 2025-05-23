using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(KillLocalPlayer))]
static class KillLocalPlayerPatch
{
    private static int enemiesMask = 0;

    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void KillLocalPlayerAwakePatch(KillLocalPlayer __instance)
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
        ReXuvination.Log.LogDebug($"Optimised Collider layers in KillLocalPlayer for Object: {__instance.gameObject}");
    }
}