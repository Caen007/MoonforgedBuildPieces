using System.Collections;
using System.IO;
using System.Reflection;
using BepInEx;
using Jotunn.Managers;
using UnityEngine;

namespace Moonforged.BuildPieces
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    public class MoonforgedBuildPieces : BaseUnityPlugin
    {
        public const string PluginGUID = "Moonforged.BuildPieces";
        public const string PluginName = "Moonforged Build Pieces";
        public const string PluginVersion = "2.0.0";

        private AssetBundle buildPiecesBundle;

        private void Awake()
        {
            RelicConfigManager.Init(PluginGUID, Config);
            RelicRegistrar.InitConfig(Config);

            if (RelicRegistrar.AllRegistrations.Count == 0)
            {
                Logger.LogInfo("Moonforged Build Pieces template loaded. No build pieces are registered yet.");
                return;
            }

            string resourcePath = GetPlatformBundleResourcePath();
            buildPiecesBundle = EmbeddedAssetBundleLoader.LoadBundle(resourcePath);

            if (buildPiecesBundle == null)
            {
                Logger.LogError("Failed to load embedded AssetBundle: " + resourcePath);
                return;
            }

            PrefabManager.OnPrefabsRegistered += OnPrefabsRegistered;
        }

        private static string GetPlatformBundleResourcePath()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.OSXEditor:
                    return "Moonforged.BuildPieces.mbp_mac";

                default:
                    return "Moonforged.BuildPieces.mbp_windows";
            }
        }

        private void OnDestroy()
        {
            PrefabManager.OnPrefabsRegistered -= OnPrefabsRegistered;
        }

        private void OnPrefabsRegistered()
        {
            StartCoroutine(DelayedRegister(buildPiecesBundle));
        }

        private IEnumerator DelayedRegister(AssetBundle bundle)
        {
            while (ZNetScene.instance == null)
            {
                yield return null;
            }

            RelicRegistrar.RegisterAllRelics(bundle);
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

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return AssetBundle.LoadFromMemory(memoryStream.ToArray());
                }
            }
        }
    }
}
