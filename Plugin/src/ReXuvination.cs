using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using MonoMod.RuntimeDetour;
using ReXuvination.src.Dependency;

namespace ReXuvination.src;
[BepInPlugin(GUID, NAME, VERSION)]
[BepInDependency("BMX.LobbyCompatibility", Flags:BepInDependency.DependencyFlags.SoftDependency)]
[BepInDependency("ainavt.lc.lethalconfig", Flags:BepInDependency.DependencyFlags.SoftDependency)]
internal class ReXuvination : BaseUnityPlugin
{
    
    // ReSharper disable once CollectionNeverQueried.Global
    internal static readonly ISet<Hook> Hooks = new HashSet<Hook>();
    internal static readonly Harmony Harmony = new Harmony(GUID);
    
    public static ReXuvination INSTANCE { get; private set; }
    
    public const string GUID = MyPluginInfo.PLUGIN_GUID;
    public const string NAME = MyPluginInfo.PLUGIN_NAME;
    public const string VERSION = MyPluginInfo.PLUGIN_VERSION;

    internal static ManualLogSource Log;
        
    private void Awake()
    {
        
        INSTANCE = this;
        Log = Logger;
        try
        {
            if (LobbyCompatibilityChecker.Enabled)
                LobbyCompatibilityChecker.Init();
            
            Log.LogInfo("Initializing Configs");

            PluginConfig.Init();
            
            Log.LogInfo("Patching Methods");
            
            Harmony.PatchAll();
            
            Log.LogInfo(NAME + " v" + VERSION + " Loaded! also you lost the game!");
            
        }
        catch (Exception ex)
        {
            Log.LogError("Exception while initializing: \n" + ex);
        }
    }
    internal static class PluginConfig
    {
        public static ConfigEntry<string> ConfigEnemyBlacklist { get; private set; }

        internal static void Init()
        {
            var config = INSTANCE.Config;
            
            config.SaveOnConfigSet = false;
            //Initialize Configs
            ConfigEnemyBlacklist = config.Bind("EnemyAICollisionDetect Options",
                        "Enemies | Blacklist",
                        "",
                        "comma separated list of enemies to not optimise.");
            config.SaveOnConfigSet = true;
            CleanAndSave();
        }

        
        internal static void CleanAndSave()
        {
            var config = ReXuvination.INSTANCE.Config;
            //remove unused options
            var orphanedEntriesProp = AccessTools.Property(config.GetType(),"OrphanedEntries");

            var orphanedEntries = (Dictionary<ConfigDefinition, string>)orphanedEntriesProp!.GetValue(config, null);

            orphanedEntries.Clear(); // Clear orphaned entries (Unbinded/Abandoned entries)
            config.Save(); // Save the config file
        }
        
    }
}