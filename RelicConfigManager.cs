using BepInEx.Configuration;

namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Manages configuration entries for relics, using ConfigSync to allow sync across server/client.
    /// </summary>
    public static class RelicConfigManager
    {
        private static ConfigSync _configSync;

        public static void Init(string modName, ConfigFile config)
        {
            _configSync = new ConfigSync(modName);
        }

        public static SyncedConfigEntry<T> AddEntry<T>(ConfigFile cfg, string section, string key, T defaultValue, string description)
        {
            var entry = cfg.Bind(section, key, defaultValue, description);
            return _configSync.AddConfigEntry(entry);
        }

        public static void SetServerSync(bool enabled)
        {
            _configSync?.SetSourceOfTruth(enabled);
        }
    }
}
