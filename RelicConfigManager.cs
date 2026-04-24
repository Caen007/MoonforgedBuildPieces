using BepInEx.Configuration;

namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Manages local BepInEx configuration entries for Moonforged Build Pieces.
    /// </summary>
    public static class RelicConfigManager
    {
        public static void Init(string modName, ConfigFile config)
        {
            // Local config only. No server-side config sync is used by this mod.
        }

        public static ConfigEntry<T> AddEntry<T>(ConfigFile cfg, string section, string key, T defaultValue, string description)
        {
            return cfg.Bind(section, key, defaultValue, description);
        }
    }
}
