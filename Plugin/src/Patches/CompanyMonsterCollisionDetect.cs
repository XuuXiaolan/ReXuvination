using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(CompanyMonsterCollisionDetect))]
static class CompanyMonsterCollisionDetectPatch
{
    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void CompanyMonsterCollisionDetectAwakePatch(CompanyMonsterCollisionDetect __instance)
    {
        Collider[] colliders = __instance.GetComponents<Collider>();

        foreach (Collider collider in colliders)
        {
            if (!collider.isTrigger)
                continue;
            
            collider.excludeLayers = ~StartOfRound.Instance.playersMask;
        }
        ReXuvination.Log.LogDebug($"Optimised Collider layers in CompanyMonsterCollisionDetect for Object: {__instance.gameObject}");
    }
}