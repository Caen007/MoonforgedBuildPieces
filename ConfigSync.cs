using System;
using System.Collections.Generic;
using BepInEx.Configuration;

namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Minimal config sync layer. Keeps config entries consistent across client/server.
    /// </summary>
    public class ConfigSync
    {
        public bool IsSourceOfTruth { get; private set; } = true;

        private readonly List<OwnConfigEntryBase> _entries = new();

        public ConfigSync(string name)
        {
            ModName = name;
        }

        public string ModName { get; }

        public void AddEntry(OwnConfigEntryBase entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            if (!_entries.Contains(entry))
            {
                _entries.Add(entry);
            }
        }

        public SyncedConfigEntry<T> AddConfigEntry<T>(ConfigEntry<T> entry)
        {
            if (entry == null) throw new ArgumentNullException(nameof(entry));
            var synced = new SyncedConfigEntry<T>(entry);
            AddEntry(synced);
            return synced;
        }

        public void SetSourceOfTruth(bool isSource)
        {
            IsSourceOfTruth = isSource;
        }
    }
}
