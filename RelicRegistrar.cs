using System.Collections.Generic;
using System.Linq;
using BepInEx.Configuration;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;

namespace Moonforged.BuildPieces
{
    public class RelicRegistration
    {
        public string PrefabName;
        public string DisplayName;
        public RequirementConfig[] Requirements;
        public string Description;
        public string Category;
        public int Comfort;

        public RelicRegistration(string prefab, string display, RequirementConfig[] reqs, string desc, string cat, int comfort = 0)
        {
            PrefabName = prefab;
            DisplayName = display;
            Requirements = reqs;
            Description = desc;
            Category = cat;
            Comfort = comfort;
        }
    }

    public static class RelicRegistrar
    {
        private static bool wasAlreadyRegistered = false; // HOTFIX stays untouched

        private static ConfigEntry<string> BuildingCategoryConfig;

        public static readonly List<RelicRegistration> AllRegistrations = new()
        {
            // ---------------------- Gates/Doors ----------------------

            //.A
            new RelicRegistration("M_Rune_Carved_Door","Rune Carved Door", new[]{
                new RequirementConfig("RoundLog",10), new RequirementConfig("Wood",10), new RequirementConfig("Coal",5)
            },"","building"),

            //.B
            new RelicRegistration("M_Rune_Carved_Double_Door","Rune Carved Double Door", new[]{
                new RequirementConfig("RoundLog",20), new RequirementConfig("Wood",20), new RequirementConfig("Coal",10)
            },"","building"),

            //.C
            new RelicRegistration("M_Arched_Runed_Door","Runed Arched Door", new[]{
                new RequirementConfig("RoundLog",5), new RequirementConfig("Wood",5), new RequirementConfig("Coal",2)
            },"","building"),



            // ---------------------- Build Pieces ----------------------

            //.0
            new RelicRegistration("M_Wood_Pillar_Arched","Carved Wood Beam Arched", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),    
            
            //.0.0
            new RelicRegistration("M_Wood_Pillar_Arched_Flipped","Carved Wood Beam Arched Flipped", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.1
            new RelicRegistration("M_Wood_Beam_2m_H","Carved Wood Beam 2m Horizontal", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.2
            new RelicRegistration("M_Wood_Beam_2m_V","Carved Wood Beam 2m Vertical", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.3
            new RelicRegistration("M_Wood_Beam_45","Carved Wood Beam 45°", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.4
            new RelicRegistration("M_Wood_Beam_26","Carved Wood Beam 26°", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.5
            new RelicRegistration("M_Wood_Beam_1m_H","Carved Wood Beam 1m Horizontal", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.6
            new RelicRegistration("M_Wood_Beam_1m_V","Carved Wood Beam 1m Vertical", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.7
            new RelicRegistration("M_Wood_RoundBeam_2m_H","Carved Round Wood Beam 2m Horizontal", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.8
            new RelicRegistration("M_Wood_RoundBeam_2m_V","Carved Round Wood Beam 2m Vertical", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.9
            new RelicRegistration("M_Wood_RoundBeam_4m_h","Carved Round Wood Beam 4m Horizontal", new[]{
                new RequirementConfig("RoundLog",4), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.10
            new RelicRegistration("M_Wood_RoundBeam_4m_V","Carved Round Wood Beam 4m Vertical", new[]{
                new RequirementConfig("RoundLog",4), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.11
            new RelicRegistration("M_Wood_RoundBeam_Arch","Carved Round Wood Arch Beam", new[]{
                new RequirementConfig("RoundLog",4), new RequirementConfig("Coal",2)
            },"A carved wood building beam.","building"),

            //.12
            new RelicRegistration("M_Wood_RoundBeam_HalfArch","Carved Round Wood Half-Arch Beam", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.13
            new RelicRegistration("M_Wood_RoundBeam_4m_V_Large","Large Carved Round Wood Beam 4m Vertical", new[]{
                new RequirementConfig("RoundLog",5), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.14
            new RelicRegistration("M_Connection_Box","Carved Connection Box", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.15
            new RelicRegistration("M_Carved_Floor","Carved Floor 2×2", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.16
            new RelicRegistration("M_Runed_Floor","Runed Floor 2×2", new[]{
                new RequirementConfig("RoundLog",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.17
            new RelicRegistration("M_Wall","Carved Wall 2×2", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.18
            new RelicRegistration("M_Runed_Wall","Runed Wall 2×2", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.19
            new RelicRegistration("M_Runed_Wall_45_Out","Runed Wall 45° Out Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.20
            new RelicRegistration("M_Runed_Wall_45_In","Runed Wall 45° In Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.21
            new RelicRegistration("M_Runed_Wall_45_Out2","Runed Wall 45° Out Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.22
            new RelicRegistration("M_Runed_Wall_45_In2","Runed Wall 45° In Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.23
            new RelicRegistration("M_Wall_45_Out","Carved Wall 45° Out Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.24
            new RelicRegistration("M_Wall_45_In","Carved Wall 45° In Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.25
            new RelicRegistration("M_Wall_45_Out2","Carved Wall 45° Out Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.26
            new RelicRegistration("M_Wall_45_In2","Carved Wall 45° In Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.27
            new RelicRegistration("M_Wall_1m","Carved Wall 1×2m", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.28
            new RelicRegistration("M_Runed_Wall_1X1","Runed Wall 1×1m", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.29
            new RelicRegistration("M_Runed_Wall_26_Out","Runed Wall 26° Out Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.30
            new RelicRegistration("M_Runed_Wall_26_In","Runed Wall 26° In Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.31
            new RelicRegistration("M_Wall_26_Out","Carved Wall 26° Out Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.32
            new RelicRegistration("M_Wall_26_In","Carved Wall 26° In Right", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.33
            new RelicRegistration("M_Runed_Wall_26_Out2","Runed Wall 26° Out Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.34
            new RelicRegistration("M_Runed_Wall_26_In2","Runed Wall 26° In Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.35
            new RelicRegistration("M_Wall_26_Out2","Carved Wall 26° Out Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.36
            new RelicRegistration("M_Wall_26_In2","Carved Wall 26° In Left", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.37
            new RelicRegistration("M_Carved_Stairs","Carved Stairs", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.38
            new RelicRegistration("M_Runed_Stairs","Runed Stairs", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.39
            new RelicRegistration("M_Runic_Railing_2m","Runed Railing 2m", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.40
            new RelicRegistration("M_Runic_Stairs_Railing_26","Runed Railing 26°", new[]{
                new RequirementConfig("Wood",2), new RequirementConfig("Coal",1)
            },"A carved wood building beam.","building"),

            //.41
            new RelicRegistration("M_Runed_Wall_Corner","Runed/Carved Corner Piece", new[]{
                new RequirementConfig("Wood",1)
            },"A carved wood building beam.","building"),

            // ---------------------- Trellis ----------------------

            //.42
            new RelicRegistration("M_Trellis","Moonforged Trellis", new[]{
                new RequirementConfig("Wood",1), new RequirementConfig("Blueberries",10), new RequirementConfig("Dandelion",3)
            },"A Garden Trellis.","building"),

            // ---------------------- Windows ----------------------

            //.43
            new RelicRegistration("M_Round_Moonforged_Window_1","Moonforged Round Window I", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.44
            new RelicRegistration("M_Dome_Frame","Moonforged Dome Frame", new[]{
                new RequirementConfig("Iron",2)
            },"","building"),

            //.45
            new RelicRegistration("M_Dome_Silent_Night","Silent Night Dome Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.46
            new RelicRegistration("M_Dome_Moonforged_Window_1","Moonforged Dome Window I", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.47
            new RelicRegistration("M_Dome2x2","Quarter Dome Glass Piece 2×2", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.48
            new RelicRegistration("M_Dome2x2x2","Quarter Dome Glass Piece 2×2×2", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.49
            new RelicRegistration("M_BrownBearMozaic","Bear Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.50
            new RelicRegistration("M_GreenCloverMozaic","Clover Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.51
            new RelicRegistration("M_CrowMozaic","Crow Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.52
            new RelicRegistration("M_RoundMozaic","Round Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.53
            new RelicRegistration("M_ChurchMozaic","Church Rose Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.54
            new RelicRegistration("M_WorldTreeMozaic","World Tree Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.55
            new RelicRegistration("M_OdinMozaic","Odin Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.56
            new RelicRegistration("M_ArchedWindowMozaic","Valkyrie Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.57
            new RelicRegistration("M_ArchedWindowGreen","Green Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.58
            new RelicRegistration("M_ArchedWindowPurple","Purple Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.59
            new RelicRegistration("M_ArchedWindowPurpleM","Purple Mosaic Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.60
            new RelicRegistration("M_ArchedWindowRedM","Red Mosaic Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.61
            new RelicRegistration("M_ArchedWindowRed","Red Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.62
            new RelicRegistration("M_ArchedWindowBat","Bat Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.63
            new RelicRegistration("M_ElfMozaic","Elf Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.64
            new RelicRegistration("M_WolfMozaic","Wolf Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.65
            new RelicRegistration("M_ArchedWindowC","Arched Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.66
            new RelicRegistration("M_Window_1","H-Pattern Window I", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.67
            new RelicRegistration("M_Window_2","H-Pattern Window II", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.68
            new RelicRegistration("M_Window_3","H-Pattern Window III", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.69
            new RelicRegistration("M_Window_2x1","2×1 Window", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            //.70
            new RelicRegistration("M_Window_2x2","2×2 Window", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            //.71
            new RelicRegistration("M_Window2x2","Sunflower 2×2 Window", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.72
            new RelicRegistration("M_WoodFrame_Rose_Window","Wood-Framed Rose Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.73
            new RelicRegistration("M_Taker2x3","Underworld 2×3 Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.74
            new RelicRegistration("M_MageMozaic","Mage Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.75
            new RelicRegistration("M_Mage_Round_Window_Mozaic","Mage Round Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            //.76
            new RelicRegistration("M_Round_Church_Window","Round Church Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // ---------------------- Swamp Windows ----------------------


            //.79
            new RelicRegistration("M_ArchedWindow_SwampWraith_2x3","Swamp Wraith Arched Window 2×3", new[]{
                new RequirementConfig("Iron",1), new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            

            // ---------------------- Window Arches / Finishing Pieces ----------------------

            //.93
            new RelicRegistration("M_Window_Frame_2x2","Window Frame 2x2", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

            //.94
            new RelicRegistration("M_Window_Frame_4x4","Window Frame 4x4", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

            //.94+1
            
            //.94
            new RelicRegistration("M_Window_Frame_4x4_Flipped","Window Frame 4x4 Flipped", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

            //.95
            new RelicRegistration("M_Corner_Window_Frame","Corner Window Frame", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

            //.96
            new RelicRegistration("M_Corner_Window_Frame_Inv","Corner Window Frame Inv", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),




        };

        public static void InitConfig(ConfigFile cfg)
        {
            BuildingCategoryConfig = RelicConfigManager.AddEntry(
                cfg, "Categories", "BuildingCategory",
                "Moonforged Build Pieces",
                "Hammer tab name for Moonforged build pieces."
            );
        }

        private static string CategoryToTab(string category) =>
            BuildingCategoryConfig != null ? BuildingCategoryConfig.Value : "Moonforged Build Pieces";

        public static void RegisterAllRelics(AssetBundle bundle)
        {
            if (wasAlreadyRegistered) return;
            foreach (var reg in AllRegistrations)
                RegisterRelic(bundle, reg);
            wasAlreadyRegistered = true;
        }

        private static void RegisterRelic(AssetBundle bundle, RelicRegistration reg)
        {
            if (bundle == null) return;

            GameObject prefab = bundle.LoadAsset<GameObject>(reg.PrefabName);
            if (prefab == null)
            {
                Debug.LogWarning($"[Moonforged Build Pieces] Missing prefab in AssetBundle: {reg.PrefabName}");
                return;
            }

            prefab.name = reg.PrefabName;

            var znv = prefab.GetComponent<ZNetView>() ?? prefab.AddComponent<ZNetView>();
            znv.m_persistent = true;
            znv.m_syncInitialScale = true;

            var zsync = prefab.GetComponent<ZSyncTransform>() ?? prefab.AddComponent<ZSyncTransform>();
            zsync.m_syncPosition = false;
            zsync.m_syncRotation = false;
            zsync.m_syncScale = true;
            zsync.m_syncBodyVelocity = false;
            zsync.m_characterParentSync = false;

            if (prefab.GetComponent<BuildPieceVisualVariance>() == null)
                prefab.AddComponent<BuildPieceVisualVariance>();

            Piece piece = prefab.GetComponent<Piece>() ?? prefab.AddComponent<Piece>();
            piece.m_name = reg.DisplayName;
            piece.m_description = reg.Description;
            piece.m_enabled = true;
            piece.m_canBeRemoved = true;
            piece.m_canRotate = true;
            piece.m_groundOnly = false;

            GameObject vfxPlace = ZNetScene.instance?.GetPrefab("vfx_Place_wood");
            GameObject sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_wood");
            GameObject destroyVFX = ZNetScene.instance?.GetPrefab("vfx_destroyed");
            GameObject destroySFX = ZNetScene.instance?.GetPrefab("sfx_wood_break");

            string prefabNameLower = reg.PrefabName.ToLowerInvariant();
            string displayNameLower = (reg.DisplayName ?? "").ToLowerInvariant();
            bool isWindowPiece = prefabNameLower.Contains("window") || prefabNameLower.Contains("mozaic") || prefabNameLower.Contains("dome");

            if (isWindowPiece)
            {
                bool usesStone = (reg.Requirements != null && reg.Requirements.Any(r => r.Item != null && r.Item.ToLower() == "stone")) ||
                                 prefabNameLower.Contains("stone") || displayNameLower.Contains("stone");
                bool usesMetal = reg.Requirements != null && reg.Requirements.Any(r => r.Item != null && r.Item.ToLower() == "iron");
                bool usesWood = reg.Requirements != null && reg.Requirements.Any(r => r.Item != null && r.Item.ToLower() == "wood");

                if (usesStone)
                    sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_stone");
                else if (usesMetal)
                    sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_metal");
                else if (usesWood)
                    sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_wood");

                destroySFX = ZNetScene.instance?.GetPrefab("sfx_clay_pot_break");
            }

            var placeFX = new EffectList();
            var placeList = new List<EffectList.EffectData>();
            if (vfxPlace != null) placeList.Add(new EffectList.EffectData { m_prefab = vfxPlace });
            if (sfxPlace != null) placeList.Add(new EffectList.EffectData { m_prefab = sfxPlace });
            placeFX.m_effectPrefabs = placeList.ToArray();
            piece.m_placeEffect = placeFX;

            WearNTear wear = prefab.GetComponent<WearNTear>() ?? prefab.AddComponent<WearNTear>();
            wear.m_health = 2000f;
            wear.m_noRoofWear = true;

            var destroyFX = new EffectList();
            var destroyList = new List<EffectList.EffectData>();
            if (destroyVFX != null) destroyList.Add(new EffectList.EffectData { m_prefab = destroyVFX });
            if (destroySFX != null) destroyList.Add(new EffectList.EffectData { m_prefab = destroySFX });
            destroyFX.m_effectPrefabs = destroyList.ToArray();
            wear.m_destroyedEffect = destroyFX;

            if (reg.Comfort > 0)
                piece.m_comfort = reg.Comfort;

            Sprite icon = bundle.LoadAsset<Sprite>(reg.PrefabName);
            if (icon != null)
                piece.m_icon = icon;

            bool requiresIron = reg.Requirements != null && reg.Requirements.Any(r => r.Item.ToLower() == "iron");
            string craftingStation = requiresIron ? "forge" : "piece_workbench";

            var config = new PieceConfig
            {
                PieceTable = PieceTables.Hammer,
                Category = CategoryToTab(reg.Category),
                Usage = new[] { PieceUsages.Building },
                CraftingStation = craftingStation,
                Requirements = reg.Requirements ?? new RequirementConfig[0]
            };

            PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, config));
        }
    }
}
