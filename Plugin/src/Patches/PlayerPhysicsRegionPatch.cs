using HarmonyLib;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(PlayerPhysicsRegion))]
static class PlayerPhysicsRegionPatch
{
    [HarmonyPatch("Awake"), HarmonyPostfix]
    public static void PlayerPhysicsRegionAwakePatch(PlayerPhysicsRegion __instance)
    {
        ReXuvination.Log.LogInfo($"Patched PlayerPhysicsRegion.Awake in Object: {__instance.GetInstanceID()}");
    }
}