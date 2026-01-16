using System.Reflection;
using System.IO;
using BepInEx;
using UnityEngine;
using Jotunn.Managers;
using HarmonyLib;
using System.Collections.Generic;

namespace Moonforged.BuildPieces
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    public class MoonforgedBuildPieces : BaseUnityPlugin
    {
        public const string PluginGUID = "Moonforged.BuildPieces";
        public const string PluginName = "Moonforged Build Pieces";
        public const string PluginVersion = "1.0.2";

        private AssetBundle relicsBundle;

        private static readonly List<GameObject> placedObjects = new();

        public static void TrackAllPrefabsInBundle(AssetBundle bundle)
        {
            foreach (var prefab in bundle.LoadAllAssets<GameObject>())
            {
                if (prefab != null && prefab.GetComponent<PlacementWatcher>() == null)
                {
                    prefab.AddComponent<PlacementWatcher>().RegisterList = placedObjects;
                }
            }
        }

        private void Awake()
        {
            new Harmony("moonforged.buildpieces.scalingdebug").PatchAll();

            // INIT CONFIG SYSTEM
            RelicConfigManager.Init(PluginGUID, Config);

            // DEBUG: PRINT ALL EMBEDDED RESOURCES SO WE KNOW THE REAL NAME
            foreach (var res in Assembly.GetExecutingAssembly().GetManifestResourceNames())
                Logger.LogInfo("FOUND RESOURCE: " + res);

            // FIXED: use the REAL embedded resource name found in the log
            string resourcePath = "Moonforged.BuildPieces.mbp";

            relicsBundle = EmbeddedAssetBundleLoader.LoadBundle(resourcePath);

            if (relicsBundle == null)
            {
                Logger.LogError("Failed to load embedded AssetBundle.");
                return;
            }

            TrackAllPrefabsInBundle(relicsBundle);

            // Initialize configurable hammer categories (furniture/building/clutter/statues)
            RelicRegistrar.InitConfig(Config);

            foreach (var category in RelicRegistrar.GetAllCategories())
                PieceManager.Instance.AddPieceCategory(category);

            PrefabManager.OnPrefabsRegistered += () =>
            {
                RelicRegistrar.RegisterAllRelics(relicsBundle);
            };
        }
    }

    public static class EmbeddedAssetBundleLoader
    {
        public static AssetBundle LoadBundle(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    Debug.LogError("AssetBundle resource not found: " + resourcePath);
                    return null;
                }
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return AssetBundle.LoadFromMemory(buffer);
            }
        }
    }
}
