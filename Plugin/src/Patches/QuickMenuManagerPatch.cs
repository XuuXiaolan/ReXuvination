using System;
using HarmonyLib;
using UnityEngine;

namespace ReXuvination.src.Patches;
[HarmonyPatch(typeof(QuickMenuManager))]
static class QuickMenuManagerPatch
{
    private static bool alreadyPatched = false;

    [HarmonyPatch(nameof(QuickMenuManager.Start)), HarmonyPostfix] // thanks, funo
    static void QuickMenuManagerStartPatch()
    {
        if (alreadyPatched)
            return;

        var enemyArray = Resources.FindObjectsOfTypeAll<EnemyType>(); // thanks, Zaggy1024
        LayerMask enemiesAndPlayerMask = LayerMask.GetMask("Enemies", "Player");
        foreach (EnemyType enemy in enemyArray)
        {
            if (String.IsNullOrEmpty(enemy.enemyName)) continue;
            if (enemy.enemyPrefab == null) continue;
            if (ReXuvination.PluginConfig.ConfigEnemyBlacklist.Value.Contains(enemy.enemyName)) continue;
            foreach (var enemyAICollisionDetect in enemy.enemyPrefab.GetComponentsInChildren<EnemyAICollisionDetect>())
            {
                foreach (var collider in enemyAICollisionDetect.gameObject.GetComponents<Collider>())
                {
                    if (!collider.isTrigger) continue;
                    collider.excludeLayers = ~enemiesAndPlayerMask;
                }
            }
            ReXuvination.Log.LogDebug($"{enemy.enemyName}'s colliders have been optimised!");
        }

        alreadyPatched = true; // exiting to the menu launches this too, so we prevent the search just in case
    }
}