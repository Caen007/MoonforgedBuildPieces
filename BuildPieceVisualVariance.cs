using System.Collections.Generic;
using UnityEngine;

namespace Moonforged.BuildPieces
{
    /// <summary>
    /// Adds a tiny visual-only position + rotation variance to renderer branches.
    /// Root transform, snap points, colliders and structural position stay exact.
    /// This helps reduce coplanar Z-fighting when many build pieces overlap in L-shapes / corners.
    /// </summary>
    public class BuildPieceVisualVariance : MonoBehaviour
    {
        // Very small visual-only local position variance
        private const float MinPosOffset = 0.0005f;  // 0.5 mm
        private const float MaxPosOffset = 0.0010f;  // 1.0 mm

        // Very small visual-only local rotation variance
        private const float MinYawOffset = 0.05f;    // degrees
        private const float MaxYawOffset = 0.12f;

        private const float MinPitchRollOffset = 0.02f;
        private const float MaxPitchRollOffset = 0.06f;

        private bool _applied;

        private void Start()
        {
            TryApply();
        }

        private void OnEnable()
        {
            TryApply();
        }

        private void TryApply()
        {
            if (_applied)
                return;

            var targets = CollectVisualTargets();
            if (targets.Count == 0)
                return;

            int baseSeed = GetBaseSeed();

            foreach (Transform target in targets)
            {
                if (target == null)
                    continue;

                Vector3 originalLocalPosition = target.localPosition;
                Quaternion originalLocalRotation = target.localRotation;

                int targetSeed = CombineHash(baseSeed, GetTransformPath(target).GetHashCode());

                Vector3 posOffset = new Vector3(
                    SignedRange(targetSeed, 11, MinPosOffset, MaxPosOffset),
                    SignedRange(targetSeed, 23, MinPosOffset, MaxPosOffset),
                    SignedRange(targetSeed, 37, MinPosOffset, MaxPosOffset)
                );

                float pitch = SignedRange(targetSeed, 41, MinPitchRollOffset, MaxPitchRollOffset);
                float yaw = SignedRange(targetSeed, 53, MinYawOffset, MaxYawOffset);
                float roll = SignedRange(targetSeed, 67, MinPitchRollOffset, MaxPitchRollOffset);

                target.localPosition = originalLocalPosition + posOffset;
                target.localRotation = originalLocalRotation * Quaternion.Euler(pitch, yaw, roll);
            }

            _applied = true;
        }

        private List<Transform> CollectVisualTargets()
        {
            var result = new List<Transform>();
            var chosen = new HashSet<Transform>();

            Renderer[] renderers = GetComponentsInChildren<Renderer>(true);
            foreach (Renderer renderer in renderers)
            {
                if (renderer == null || !renderer.enabled)
                    continue;

                if (renderer is ParticleSystemRenderer)
                    continue;

                Transform t = renderer.transform;
                if (t == null || t == transform)
                    continue;

                string n = t.name.ToLowerInvariant();
                if (n.Contains("snap") || n.Contains("snapp") || n.Contains("collider") || n.Contains("trigger"))
                    continue;

                // Prefer the highest renderer transform below root so we offset the whole visual branch once.
                Transform target = t;
                while (target.parent != null && target.parent != transform && HasRenderableAncestor(target.parent))
                {
                    string pn = target.parent.name.ToLowerInvariant();
                    if (pn.Contains("snap") || pn.Contains("snapp") || pn.Contains("collider") || pn.Contains("trigger"))
                        break;

                    target = target.parent;
                }

                if (target != null && target != transform && chosen.Add(target))
                    result.Add(target);
            }

            return result;
        }

        private bool HasRenderableAncestor(Transform t)
        {
            if (t == null || t == transform)
                return false;

            return t.GetComponent<Renderer>() != null;
        }

        private int GetBaseSeed()
        {
            Vector3 p = transform.position;
            Vector3 r = transform.eulerAngles;

            int px = Mathf.RoundToInt(p.x * 1000f);
            int py = Mathf.RoundToInt(p.y * 1000f);
            int pz = Mathf.RoundToInt(p.z * 1000f);

            int rx = Mathf.RoundToInt(r.x * 100f);
            int ry = Mathf.RoundToInt(r.y * 100f);
            int rz = Mathf.RoundToInt(r.z * 100f);

            int seed = gameObject.name.GetHashCode();
            seed = CombineHash(seed, px);
            seed = CombineHash(seed, py);
            seed = CombineHash(seed, pz);
            seed = CombineHash(seed, rx);
            seed = CombineHash(seed, ry);
            seed = CombineHash(seed, rz);

            return seed;
        }

        private string GetTransformPath(Transform t)
        {
            if (t == null)
                return string.Empty;

            var parts = new List<string>();
            Transform cur = t;

            while (cur != null && cur != transform)
            {
                parts.Add(cur.name);
                cur = cur.parent;
            }

            parts.Reverse();
            return string.Join("/", parts);
        }

        private static int CombineHash(int a, int b)
        {
            unchecked
            {
                return (a * 397) ^ b;
            }
        }

        private static float SignedRange(int seed, int salt, float minAbs, float maxAbs)
        {
            float magnitude = Mathf.Lerp(minAbs, maxAbs, Hash01(seed, salt));
            float sign = Hash01(seed, salt + 911) < 0.5f ? -1f : 1f;
            return magnitude * sign;
        }

        private static float Hash01(int seed, int salt)
        {
            unchecked
            {
                int h = seed;
                h = (h * 397) ^ salt;
                h ^= (h << 13);
                h ^= (h >> 17);
                h ^= (h << 5);

                uint u = (uint)h;
                return (u & 0x00FFFFFF) / 16777215f;
            }
        }
    }
}