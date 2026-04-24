using System.Collections.Generic;
using System.Linq;
using Jotunn.Entities;
using Jotunn.Configs;
using UnityEngine;
using UnityEngine.Rendering;
using Jotunn.Managers;
using Moonforged.BuildPieces;
using BepInEx.Configuration;

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

        // Configurable hammer categories
        private static ConfigEntry<string> FurnitureCategoryConfig;
        private static ConfigEntry<string> BuildingCategoryConfig;
        private static ConfigEntry<string> ClutterCategoryConfig;
        private static ConfigEntry<string> StatuesCategoryConfig;

        public static readonly List<RelicRegistration> AllRegistrations = new()
        {
            //--------------------- Furniture
            
            // 0.
            new RelicRegistration("M_FineWood_Throne","Moonforged FineWood Throne", new[]{
                new RequirementConfig("FineWood",20)
            },"A custom decorative piece.","furniture", 2),

            // 0.1

new RelicRegistration("M_Werewolf_Throne","Moonforged Werewolf Throne", new[]{
    new RequirementConfig("WolfPelt",5),
    new RequirementConfig("TrophyFenring",3),
    new RequirementConfig("Iron",5)
},"A custom decorative piece.","furniture", 2),

// 0.2

new RelicRegistration("M_Wolf_Throne","Moonforged Geirrhafa`s Throne", new[]{
    new RequirementConfig("FineWood",5),
    new RequirementConfig("WolfPelt",5),
    new RequirementConfig("Silver",5),
    new RequirementConfig("TrophyCultist_Hildir",1)
},"A custom decorative piece.","furniture", 2),

// 0.2

new RelicRegistration("M_Iron_Throne","Moonforged Iron Throne", new[]{
    new RequirementConfig("Iron",10),
    new RequirementConfig("Bronze",10)

},"A custom decorative piece.","furniture", 2),

                
            
            // ---------------------- Rugs ------------------------


            // 1.
            new RelicRegistration("M_PersianRug_1","Persian Rug I", new[]{
                new RequirementConfig("JuteRed",4)
            },"A custom decorative piece.","furniture"),

            // 2.
            new RelicRegistration("M_PersianRug_2","Persian Rug II", new[]{
                new RequirementConfig("JuteBlue",2)
            },"A custom decorative piece.","furniture"),

            // 3.
            new RelicRegistration("M_PersianRug_3","Persian Rug III", new[]{
                new RequirementConfig("JuteRed",2)
            },"A custom decorative piece.","furniture"),

            // 4.
            new RelicRegistration("M_PersianRug_4","Regal Persian Rug", new[]{
                new RequirementConfig("JuteRed",2), new RequirementConfig("JuteBlue",4)
            },"A custom decorative piece.","furniture"),

            // 5.
            new RelicRegistration("M_Jute_Stairs_Rug","Jute Stair Rug", new[]{
                new RequirementConfig("JuteRed",4)
            },"A custom decorative piece.","furniture"),

            // 6.
            new RelicRegistration("M_Jute_Stairs_Rug_Blue","Blue Jute Stair Rug", new[]{
                new RequirementConfig("JuteBlue",2)
            },"A custom decorative piece.","furniture"),

            // 7.
            new RelicRegistration("M_Jute_Stairs_Rug_Blue_Runed","Runed Blue Jute Stair Rug", new[]{
                new RequirementConfig("JuteBlue",2)
            },"A custom decorative piece.","furniture"),

            // 8.
            new RelicRegistration("M_Jute_Stairs_Rug_Small","Small Jute Stair Rug", new[]{
                new RequirementConfig("JuteRed",2)
            },"A custom decorative piece.","furniture"),

            // 9.
            new RelicRegistration("M_Jute_Stairs_Runed","Runed Jute Stair Rug", new[]{
                new RequirementConfig("JuteRed",4)
            },"A custom decorative piece.","furniture"),

            // 10.
            new RelicRegistration("M_Stairs_Rug_Wool","Wool Stair Rug", new[]{
                new RequirementConfig("DeerHide",4)
            },"A custom decorative piece.","furniture"),

            // 11.
            new RelicRegistration("M_Stairs_Rug_Green_Runed","Runed Green Stair Rug", new[]{
                new RequirementConfig("JuteRed",4), new RequirementConfig("Ooze",1)
            },"A custom decorative piece.","furniture"),

            // 12.
            new RelicRegistration("M_Stairs_Rug_Green","Green Stair Rug", new[]{
                new RequirementConfig("JuteRed",4), new RequirementConfig("Ooze",1)
            },"A custom decorative piece.","furniture"),

            // 13.
            new RelicRegistration("M_BlueRug","Blue Rug", new[]{
                new RequirementConfig("JuteBlue",4)
            },"A custom decorative piece.","furniture"),

            // 14.
            new RelicRegistration("M_Red_2m_semiround_rug","Red Semiround Rug", new[]{
                new RequirementConfig("JuteRed",4)
            },"A custom decorative piece.","furniture"),

            // 15.
            new RelicRegistration("M_2m_semiround_rug","Brown Semiround Rug", new[]{
                new RequirementConfig("DeerHide",4)
            },"A custom decorative piece.","furniture"),

            // 16.
            new RelicRegistration("M_Jute_Round_Red_Rug","Round Red Jute Rug", new[]{
                new RequirementConfig("JuteRed",4)
            },"A custom decorative piece.","furniture"),

                        // 16.1
            new RelicRegistration("M_TrollRoundRug","Troll Hide Round Rug", new[]{
                new RequirementConfig("TrollHide",5)
            },"A custom decorative piece.","furniture"),

                                    // 16.2
            new RelicRegistration("M_TrollHideRug","Troll Hide Rug", new[]{
                new RequirementConfig("TrollHide",10)
            },"A custom decorative piece.","furniture"),

                                                // 16.3
            new RelicRegistration("M_TrollSemiRoundRug","Troll Hide Semi Round Rug", new[]{
                new RequirementConfig("TrollHide",2)
            },"A custom decorative piece.","furniture"),

                                                            // 16.4
            new RelicRegistration("M_TrollSemiRoundRugRuned","Troll Hide Runed Semi Round Rug", new[]{
                new RequirementConfig("TrollHide",2),new RequirementConfig("GreydwarfEye",2),
            },"A custom decorative piece.","furniture"),

                                                                        // 16.5
            new RelicRegistration("M_TrollPeltRug","Troll Pelt Rug", new[]{
                new RequirementConfig("TrollHide",10), new RequirementConfig("TrophyForestTroll",1)
            },"A custom decorative piece.","furniture"),

            // 17.
            new RelicRegistration("M_Jute_Red_Simple_Rug","Simple Red Jute Rug", new[]{
                new RequirementConfig("JuteRed",2)
            },"A custom decorative piece.","furniture"),

            // ---------------------- Porcelain / Tea Set ---------

            // 18.
            new RelicRegistration("M_AsianTeaSetPlate","Tea Plate", new[]{
                new RequirementConfig("Wood",1), new RequirementConfig("FineWood",1)
            },"A custom decorative piece.","clutter"),

            // 19.
            new RelicRegistration("M_bigplate","Large Porcelain Plate", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 20.
            new RelicRegistration("M_cup1","Porcelain Tea Cup", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 21.
            new RelicRegistration("M_justplate1","Large Porcelain Plate II", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 22.
            new RelicRegistration("M_justcup1","Porcelain Tea Cup II", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 23.
            new RelicRegistration("M_teapot","Porcelain Teapot", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 24.
            new RelicRegistration("M_AsianTeaPot","Asian Teapot", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 25.
            new RelicRegistration("M_AsianTeaCup","Asian Tea Cup", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 26.
            new RelicRegistration("M_AsianTeaPotLid","Teapot Lid", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 27.
            new RelicRegistration("M_ChineseVase","Chinese Vase", new[]{
                new RequirementConfig("Crystal",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // 28.
            new RelicRegistration("M_ChineseTeaSet","Chinese Tea Set", new[]{
                new RequirementConfig("Stone",5), new RequirementConfig("Resin",2)
            },"A custom decorative piece.","clutter"),

            // ---------------------- Lamps / Bench / Bin ---------

            // 29.
            new RelicRegistration("M_Classic_Lamp","Classic Street Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // 30.
            new RelicRegistration("M_Japanese_Lamp","Japanese Street Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // 31.
            new RelicRegistration("M_Classic_Iron_Bench","Classic Iron Bench", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4)
            },"Dvergr Forged Classic Iron Bench.","furniture"),

            // 32.
            new RelicRegistration("M_Classic_Double_Lamp","Classic Double Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"Dvergr Forged Classic Iron 2x Lamp.","building"),

            // 33.
            new RelicRegistration("M_Classic_Double_Lamp_2","Classic Double Lamp II", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"Dvergr Forged Classic Iron 2x Lamp.","building"),

            // 34.
            new RelicRegistration("M_classic_bin","Classic Garden Bin", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4)
            },"Dvergr Forged Classic Iron Bin.","furniture"),

            // 35.
            new RelicRegistration("M_Classic_Single_Lamp","Classic Single Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"Dvergr Forged Classic Iron 1x Lamp.","building"),

            // 36.
            new RelicRegistration("M_Chinese_Lamp","Chinese Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // 37.
            new RelicRegistration("M_classic_quad_light","Classic Quad Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"Dvergr Forged Classic Iron 4x Lamp.","building"),

            // 38.
            new RelicRegistration("M_Classic_Wall_Lamp","Classic Wall Lamp", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"Dvergr Forged Classic Iron Wall Lamp.","building"),

            // 39.
            new RelicRegistration("M_Classic_Wall_Lamp_2","Classic Wall Lamp II", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"Dvergr Forged Classic Iron Wall Lamp.","building"),

            // ---------------------- Cannons ---------------------

            // 40.
            new RelicRegistration("M_Pirate_Ship_Cannon","Pirate Cannon", new[]{
                new RequirementConfig("Iron",10), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // 41.
            new RelicRegistration("M_Pirate_Ship_Cannon_Balls","Cannonballs", new[]{
                new RequirementConfig("Iron",10), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // 42.
            new RelicRegistration("M_M1857_12Pounder_Cannon_Cannonballs","12-Pounder Cannonballs", new[]{
                new RequirementConfig("Iron",10), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // 43.
            new RelicRegistration("M_M1857_12Pounder_Cannon","12-Pounder Cannon", new[]{
                new RequirementConfig("Iron",10), new RequirementConfig("Wood",4), new RequirementConfig("Resin",4)
            },"","building"),

            // ---------------------- Windows / Mosaics -----------

                        // 44
           new RelicRegistration("M_Round_Moonforged_Window_1","Moonforged Round Window I", new[]{
              new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

                                   // 45
           new RelicRegistration("M_Dome_Frame","Moonforged Dome Frame", new[]{
              new RequirementConfig("Iron",2)
            },"","building"),

            // 46
           new RelicRegistration("M_Dome_Silent_Night","Window Dome Silent Night", new[]{
              new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

                       // 47
           new RelicRegistration("M_Dome_Moonforged_Window_1","Moonforged Dome Window I", new[]{
              new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

                        // 48
            new RelicRegistration("M_Dome2x2","1/4 of a Dome Glas piece 2x2", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),
            
                        // 49
            new RelicRegistration("M_Dome2x2x2","1/4 of a Dome Glas piece 2x2x2", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 50
            new RelicRegistration("M_BrownBearMozaic","Bear Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 51
            new RelicRegistration("M_GreenCloverMozaic","Clover Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 52
            new RelicRegistration("M_CrowMozaic","Crow Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 53
            new RelicRegistration("M_RoundMozaic","Round Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 54
            new RelicRegistration("M_ChurchMozaic","Church Rose Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 55
            new RelicRegistration("M_WorldTreeMozaic","World Tree Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 56
            new RelicRegistration("M_OdinMozaic","Odin Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 57
            new RelicRegistration("M_ArchedWindowMozaic","Valkyrie Mosaic Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 58
            new RelicRegistration("M_ArchedWindowGreen","Green Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 59
            new RelicRegistration("M_ArchedWindowPurple","Purple Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 60
            new RelicRegistration("M_ArchedWindowPurpleM","Purple Mosaic Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 61
            new RelicRegistration("M_ArchedWindowRedM","Red Mosaic Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 62
            new RelicRegistration("M_ArchedWindowRed","Red Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 63
            new RelicRegistration("M_ArchedWindowBat","Bat Arched Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 64
            new RelicRegistration("M_ElfMozaic","Elf Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 65
            new RelicRegistration("M_WolfMozaic","Wolf Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 66
            new RelicRegistration("M_ArchedWindowC","Arched Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 67
            new RelicRegistration("M_Window_1","H-Pattern Window I", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 68
            new RelicRegistration("M_Window_2","H-Pattern Window II", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 69
            new RelicRegistration("M_Window_3","H-Pattern Window III", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 70
            new RelicRegistration("M_Window_2x1","2×1 Window", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            // 71
            new RelicRegistration("M_Window_2x2","2×2 Window", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            // 72
            new RelicRegistration("M_Window2x2","Sunflower 2×2 Window", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 73
            new RelicRegistration("M_WoodFrame_Rose_Window","Rosewood Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 74
            new RelicRegistration("M_Taker2x3","Underworld 2×3 Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 75
            new RelicRegistration("M_MageMozaic","Mage Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 76
            new RelicRegistration("M_Mage_Round_Window_Mozaic","Mage Round Stained Glass Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // 77
            new RelicRegistration("M_Round_Church_Window","Round Church Window", new[]{
                new RequirementConfig("Iron",2), new RequirementConfig("Wood",4), new RequirementConfig("Crystal",4)
            },"","building"),

            // ---------------------- Swamp -----------------------

            // 78
            new RelicRegistration("M_ArchedWindow_Swamp_2x2","Arched Window Swamp 2x2", new[]{
                new RequirementConfig("Iron",1), new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            // 79
            new RelicRegistration("M_ArchedWindow_SwampWraith_2x2","Arched Window Wraith from Swamp 2x2", new[]{
                new RequirementConfig("Iron",1), new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            // 80
            new RelicRegistration("M_ArchedWindow_SwampWraith_2x3","Arched Window Wraith from Swamp 2x3", new[]{
                new RequirementConfig("Iron",1), new RequirementConfig("Wood",4), new RequirementConfig("Stone",4)
            },"","building"),

            // 81
            new RelicRegistration("M_Trellis","Moonforged Trellis", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Raspberry",4)
            },"","building"),

            // ---------------------- Banners ---------------------

            // 82
            new RelicRegistration("M_Eikthyr_Banner","Moonforged Eikthyr Banner", new[]{
                new RequirementConfig("Wood",10), new RequirementConfig("Raspberry",4), new RequirementConfig("LeatherScraps",5)
            },"","building"),

            // 83
            new RelicRegistration("M_TheElder_Banner","Moonforged Elder Banner", new[]{
                new RequirementConfig("RoundLog",5), new RequirementConfig("Blueberries",4), new RequirementConfig("TrollHide",5)
            },"","building"),

            // 84
            new RelicRegistration("M_Bonemass_Banner","Moonforged Bonemass Banner", new[]{
                new RequirementConfig("ElderBark",10), new RequirementConfig("Guck",4), new RequirementConfig("DeerHide",5)
            },"","building"),

            // 85
            new RelicRegistration("M_Moder_Banner","Moonforged Moder Banner", new[]{
                new RequirementConfig("FineWood",10), new RequirementConfig("FreezeGland",4), new RequirementConfig("WolfPelt",5)
            },"","building"),

            // 86
            new RelicRegistration("M_Yagluth_Banner","Moonforged Yagluth Banner", new[]{
                new RequirementConfig("FineWood",10), new RequirementConfig("Cloudberry",4), new RequirementConfig("LoxPelt",5)
            },"","building"),

            // 87
            new RelicRegistration("M_TheQueen_Banner","Moonforged Queen Banner", new[]{
                new RequirementConfig("YggdrasilWood",5), new RequirementConfig("MushroomMagecap",4), new RequirementConfig("ScaleHide",5)
            },"","building"),

            // 88
            new RelicRegistration("M_Fader_Banner","Moonforged Fader Banner", new[]{
                new RequirementConfig("Blackwood",10), new RequirementConfig("CharredBone",4), new RequirementConfig("Fiddleheadfern",5), new RequirementConfig("AskHide",5)
            },"","building"),

            // 89
            new RelicRegistration("M_Haldor_Banner","Moonforged Haldor Banner", new[]{
                new RequirementConfig("Wood",4), new RequirementConfig("Raspberry",4), new RequirementConfig("DeerHide",4)
            },"","building"),

            // 90
            new RelicRegistration("M_Hildir_Banner","Moonforged Hildir Banner", new[]{
                new RequirementConfig("FineWood",4), new RequirementConfig("Cloudberry",4), new RequirementConfig("LoxPelt",4)
            },"","building"),

            // 91
            new RelicRegistration("M_BogWitch_Banner","Moonforged Bog Witch Banner", new[]{
                new RequirementConfig("ElderBark",4), new RequirementConfig("MushroomYellow",4), new RequirementConfig("TrophyDraugr",4)
            },"","building"),

            // 92
            new RelicRegistration("M_Brennan_Banner","Moonforged Brenna Banner", new[]{
                new RequirementConfig("RoundLog",4), new RequirementConfig("TrophySkeleton",4), new RequirementConfig("TrollHide",4)
            },"","building"),

            // 93
            new RelicRegistration("M_Geirrhafa_Banner","Moonforged Geirrhafa Banner", new[]{
                new RequirementConfig("FineWood",4), new RequirementConfig("WolfFang",4), new RequirementConfig("TrophyHatchling",2)
            },"","building"),

            // 94
            new RelicRegistration("M_ZilThungr_Banner","Moonforged Zil & Thungr Banner", new[]{
                new RequirementConfig("FineWood",4), new RequirementConfig("TrophyGoblin",1), new RequirementConfig("TrophyGoblinBrute",1), new RequirementConfig("LoxPelt",5)
            },"","building"),

            // 95
            new RelicRegistration("M_Window_Arch05m","Window Wood Arch 0.5m", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

            
            // 96
            new RelicRegistration("M_Window_Arch1m","Window Wood Arch 1m", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),


            
            // 97
            new RelicRegistration("M_Window_Archround2m","Window Wood Arch round 2m", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

                        // 98
            new RelicRegistration("M_Window_Arch05m_Stone","Window Stone Arch 0.5m", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),

            
            // 99
            new RelicRegistration("M_Window_Arch1m_Stone","Window Stone Arch 1m", new[]{
                new RequirementConfig("Wood",1)
            },"","building"),


            
            // 100
            new RelicRegistration("M_Window_Archround2m_Stone","Window Stone Arch round 2m", new[]{
                new RequirementConfig("Wood",1)
            },"","building")


            
            // 101
          //  new RelicRegistration("M_Runebound_Beam","Moonforged Runebound Beam", new[]{
          //      new RequirementConfig("Wood",1)
          //  },"","building"),

                        // 101
        //    new RelicRegistration("M_Runebound_Beam2","Moonforged Runebound Beam2", new[]{
          //      new RequirementConfig("Wood",1)
            //},"","building"),

                        // 101
          //  new RelicRegistration("M_Runebound_Beam_v","Moonforged Runebound Beam vertical", new[]{
            //  new RequirementConfig("Wood",1)
           //,"","building")

        };

        public static void InitConfig(ConfigFile cfg)
        {
            FurnitureCategoryConfig = RelicConfigManager.AddEntry(
                cfg, "Categories", "FurnitureCategory",
                "Moonforged Build Pieces",
                "Hammer tab name for all Moonforged furniture pieces."
            );

            BuildingCategoryConfig = RelicConfigManager.AddEntry(
                cfg, "Categories", "BuildingCategory",
                "Moonforged Build Pieces",
                "Hammer tab name for all Moonforged building pieces."
            );

            ClutterCategoryConfig = RelicConfigManager.AddEntry(
                cfg, "Categories", "ClutterCategory",
                "Moonforged Build Pieces",
                "Hammer tab name for all Moonforged clutter pieces."
            );

            StatuesCategoryConfig = RelicConfigManager.AddEntry(
                cfg, "Categories", "StatuesCategory",
                "Moonforged Build Pieces",
                "Hammer tab name for all Moonforged statue pieces."
            );
        }

        public static IEnumerable<string> GetAllCategories() =>
            AllRegistrations.Select(r => CategoryToTab(r.Category)).Distinct();

        private static string CategoryToTab(string category) =>
            category.ToLower() switch
            {
                "statue" => StatuesCategoryConfig != null ? StatuesCategoryConfig.Value : "Moonforged Statues",
                "clutter" => ClutterCategoryConfig != null ? ClutterCategoryConfig.Value : "Moonforged Clutter",
                "building" => BuildingCategoryConfig != null ? BuildingCategoryConfig.Value : "Moonforged Building",
                "furniture" => FurnitureCategoryConfig != null ? FurnitureCategoryConfig.Value : "Moonforged Furniture",
                _ => category
            };

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

            if (!prefab.GetComponent<ZSyncTransform>())
                prefab.AddComponent<ZSyncTransform>();

            Piece piece = prefab.GetComponent<Piece>() ?? prefab.AddComponent<Piece>();
            piece.m_name = reg.DisplayName;
            piece.m_description = reg.Description;
            piece.m_groundOnly = false;

            GameObject vfxPlace = null, sfxPlace = null, destroyVFX = null, destroySFX = null;

            string n = reg.PrefabName.ToLowerInvariant();

            bool isWindow = false;
            if (reg.DisplayName != null && reg.DisplayName.ToLower().Contains("window"))
                isWindow = true;
            if (reg.PrefabName.ToLower().Contains("mozaic"))
                isWindow = true;

            HashSet<string> ceramic = new()
            {
                "M_teapot","M_cup1","M_justcup1","M_justplate1",
                "M_bigplate","M_AsianTeaPot","M_AsianTeaCup",
                "M_AsianTeaPotLid","M_ChineseVase"
            };

            bool isCeramic = ceramic.Contains(reg.PrefabName);
            bool isRug = n.Contains("rug") || n.Contains("carpet");
            bool isLamp = n.Contains("lamp") || n.Contains("lantern") || n.Contains("light");
            bool isCannon = n.Contains("cannon");
            bool isBenchOrBin = n.Contains("bench") || n.Contains("bin");

            if (isCeramic)
            {
                vfxPlace = ZNetScene.instance?.GetPrefab("vfx_Place_crystal");
                sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_crystal");
                destroySFX = ZNetScene.instance?.GetPrefab("sfx_clay_pot_break");
            }
            else if (isRug)
            {
                vfxPlace = ZNetScene.instance?.GetPrefab("vfx_Place_wood");
                sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_wood");
                destroyVFX = ZNetScene.instance?.GetPrefab("vfx_destroyed");
                destroySFX = ZNetScene.instance?.GetPrefab("sfx_wood_break");
            }
            else if (isLamp || isCannon || isBenchOrBin)
            {
                vfxPlace = ZNetScene.instance?.GetPrefab("vfx_Place_stone");
                sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_metal");
                destroyVFX = ZNetScene.instance?.GetPrefab("vfx_destroyed");
                destroySFX = ZNetScene.instance?.GetPrefab("sfx_metal_blocked");
            }
            else if (isWindow)
            {
                vfxPlace = ZNetScene.instance?.GetPrefab("vfx_Place_crystal");
                sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_crystal");
                destroySFX = ZNetScene.instance?.GetPrefab("sfx_clay_pot_break");
            }
            else
            {
                vfxPlace = ZNetScene.instance?.GetPrefab("vfx_Place_wood");
                sfxPlace = ZNetScene.instance?.GetPrefab("sfx_build_hammer_wood");
                destroyVFX = ZNetScene.instance?.GetPrefab("vfx_destroyed");
                destroySFX = ZNetScene.instance?.GetPrefab("sfx_wood_break");
            }

            var placeFX = new EffectList();
            var placeList = new List<EffectList.EffectData>();
            if (vfxPlace != null) placeList.Add(new EffectList.EffectData { m_prefab = vfxPlace });
            if (sfxPlace != null) placeList.Add(new EffectList.EffectData { m_prefab = sfxPlace });
            placeFX.m_effectPrefabs = placeList.ToArray();
            piece.m_placeEffect = placeFX;

            WearNTear wear = prefab.GetComponent<WearNTear>() ?? prefab.AddComponent<WearNTear>();
            wear.m_health = 1000f;
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

            if (isLamp && prefab.GetComponent<LampColorSwitcher>() == null)
                prefab.AddComponent<LampColorSwitcher>();

            if (isWindow)
            {
                foreach (Renderer r in prefab.GetComponentsInChildren<Renderer>(true))
                {
                    if (!r) continue;
                    r.shadowCastingMode = ShadowCastingMode.On;
                    r.receiveShadows = true;
                }
            }

            bool requiresIron = reg.Requirements.Any(r => r.Item.ToLower() == "iron");
            string craftingStation = requiresIron ? "forge" : "piece_workbench";

            var config = new PieceConfig
            {
                PieceTable = "Hammer",
                Category = CategoryToTab(reg.Category),
                CraftingStation = craftingStation,
                Requirements = reg.Requirements
            };

            PieceManager.Instance.AddPiece(new CustomPiece(prefab, true, config));
        }
    }
}
