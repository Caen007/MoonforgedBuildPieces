using UnityEngine;

namespace Moonforged.BuildPieces
{
    public class LampColorSwitcher : MonoBehaviour, Interactable, Hoverable
    {
        private int index = 0;
        private const int OFF_INDEX = -1;

        private Light[] lights;
        private Renderer[] allRenderers;
        private Renderer[] bulbRenderers;   // ONLY the bulb meshes

        private LightFlicker[] flickers;
        private LightLod[] lods;
        private ParticleSystem[] particles;

        private ZNetView znv;

        // ---- COLOR CYCLE ----
        private readonly Color[] colors = new Color[]
        {
            new Color(1.00f, 0.82f, 0.28f),   // Yellow
            new Color(0.22f, 0.85f, 0.32f),   // Green
            new Color(0.28f, 0.48f, 0.95f),   // Blue
            new Color(0.95f, 0.30f, 0.70f)    // Dvergr Pink
        };

        private readonly string[] names = new string[]
        {
            "Yellow", "Green", "Blue", "Dvergr Pink"
        };

        private const string ZDO_KEY = "LampColorIndex";
        private const string ZDO_INIT = "initialized";

        void Awake()
        {
            lights = GetComponentsInChildren<Light>(true);
            allRenderers = GetComponentsInChildren<Renderer>(true);

            // ONLY grab renderers that contain the bulb material
            bulbRenderers = System.Array.FindAll(allRenderers, r =>
            {
                foreach (var m in r.sharedMaterials)
                {
                    if (m != null && m.name.StartsWith("M_Bulb"))
                        return true;
                }
                return false;
            });

            flickers = GetComponentsInChildren<LightFlicker>(true);
            lods = GetComponentsInChildren<LightLod>(true);
            particles = GetComponentsInChildren<ParticleSystem>(true);

            Collider col = GetComponentInChildren<Collider>();
            if (col == null)
            {
                var c = gameObject.AddComponent<CapsuleCollider>();
                c.isTrigger = true;
                c.radius = 0.25f;
                c.height = 1.2f;
            }

            znv = GetComponent<ZNetView>();
            if (!znv) return;

            znv.Register<int>("SetLampState", RPC_SetLampState);

            var zdo = znv.GetZDO();
            if (zdo == null) return;

            if (znv.IsOwner())
            {
                if (!zdo.GetBool(ZDO_INIT))
                {
                    index = Random.Range(0, colors.Length);
                    zdo.Set(ZDO_KEY, index);
                    zdo.Set(ZDO_INIT, true);
                }
            }

            index = zdo.GetInt(ZDO_KEY, index);

            ApplyState();
        }

        public bool Interact(Humanoid user, bool hold, bool alt)
        {
            if (hold) return false;
            if (!znv || !znv.IsOwner()) return false;

            var zdo = znv.GetZDO();
            if (zdo == null) return false;

            if (index == OFF_INDEX)
            {
                index = 0;
                zdo.Set(ZDO_KEY, index);
                znv.InvokeRPC(ZNetView.Everybody, "SetLampState", index);
                return true;
            }

            if (index < colors.Length - 1)
                index++;
            else
                index = OFF_INDEX;

            zdo.Set(ZDO_KEY, index);
            znv.InvokeRPC(ZNetView.Everybody, "SetLampState", index);

            return true;
        }

        public bool Interact(Humanoid user, bool hold) =>
            Interact(user, hold, false);

        public bool UseItem(Humanoid user, ItemDrop.ItemData item) => false;

        private void RPC_SetLampState(long sender, int newState)
        {
            index = newState;
            ApplyState();
        }

        private void ApplyState()
        {
            if (index == OFF_INDEX)
            {
                // lights OFF
                foreach (var l in lights)
                    if (l) l.enabled = false;

                foreach (var f in flickers)
                    if (f) f.enabled = false;

                foreach (var ld in lods)
                    if (ld) ld.enabled = false;

                foreach (var p in particles)
                {
                    if (!p) continue;
                    p.Stop(true);
                    p.gameObject.SetActive(false);
                }

                // only bulbs get emission control
                foreach (var r in bulbRenderers)
                {
                    foreach (var m in r.materials)
                    {
                        if (m.HasProperty("_EmissionColor"))
                        {
                            m.DisableKeyword("_EMISSION");
                            m.SetColor("_EmissionColor", Color.black);
                        }
                    }
                }

                return;
            }

            // ON: set light color
            Color c = colors[index];

            foreach (var l in lights)
            {
                if (!l) continue;
                l.enabled = true;
                l.color = c;
            }

            foreach (var f in flickers)
                if (f) f.enabled = true;

            foreach (var ld in lods)
                if (ld) ld.enabled = true;

            foreach (var p in particles)
            {
                if (!p) continue;
                p.gameObject.SetActive(true);
                p.Play();
            }

            // ONLY affect bulb materials
            foreach (var r in bulbRenderers)
            {
                foreach (var m in r.materials)
                {
                    if (m.HasProperty("_EmissionColor"))
                    {
                        m.EnableKeyword("_EMISSION");
                        m.SetColor("_EmissionColor", c * 3f);
                    }
                }
            }
        }

        public string GetHoverText()
        {
            if (index == OFF_INDEX)
                return "[E] Turn On";

            return $"[E] Change color ({names[index]})";
        }

        public string GetHoverName() => "Lamp";
    }
}
