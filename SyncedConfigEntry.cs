using BepInEx.Configuration;

namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Wrapper around a BepInEx ConfigEntry that can be synchronized.
    /// </summary>
    public class SyncedConfigEntry<T> : OwnConfigEntryBase
    {
        public ConfigEntry<T> Entry { get; }

        public SyncedConfigEntry(ConfigEntry<T> entry)
        {
            Entry = entry;
        }

        public T Value
        {
            get => Entry.Value;
            set => Entry.Value = value;
        }

        public override string GetSerializedValue()
        {
            return Entry.GetSerializedValue();
        }

        public override void SetSerializedValue(string value)
        {
            Entry.SetSerializedValue(value);
        }
    }
}
