namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Base class for synced config entries. Used by ConfigSync.
    /// </summary>
    public abstract class OwnConfigEntryBase
    {
        public abstract string GetSerializedValue();
        public abstract void SetSerializedValue(string value);
    }
}
