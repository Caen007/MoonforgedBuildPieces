using System.Collections.Generic;
using UnityEngine;

namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Tracks placed instances in a shared list. Matches your current safeguard pattern.
    /// </summary>
    public class PlacementWatcher : MonoBehaviour
    {
        /// <summary>
        /// The shared list this instance should register into (provided by the registrar).
        /// </summary>
        public List<GameObject> RegisterList;

        private void Start()
        {
            if (RegisterList != null && !RegisterList.Contains(gameObject))
            {
                RegisterList.Add(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (RegisterList != null)
            {
                // Remove if present; tolerate already-removed entries
                int idx = RegisterList.IndexOf(gameObject);
                if (idx >= 0)
                {
                    RegisterList.RemoveAt(idx);
                }
            }
        }
    }
}
