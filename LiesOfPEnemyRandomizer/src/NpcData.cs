using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI.UnrealTypes;

namespace LiesOfPEnemyRandomizer.src
{
    public static class NpcData
    {
        public enum NpcType
        {
           Puppet,
           Carcass,
           Stalker,
           MiniBoss,
           MiniBossStalker,
           MiniBossCarcass,
           MiniBossPuppet,
           BossPuppet,
           BossPuppetPhase,
           BossHuman,
           Boss,
           BossCarcass,
           Dummy
        };

        public enum SpotCodeName
        {
            // Npc-LV_Hotel
            Hotel_Puppet_Training_Armless_00,
            Hotel_Puppet_Training_Basic_00,

            // Npc-LD_Outer_Station
            CH01_Puppet_ButtlerMelee_Station_Normal_00,
            CH01_Puppet_Police_Named_00,
            CH01_Puppet_ButtlerMelee_Normal_00,
            CH01_Puppet_Watchdog_Normal_00,
            CH01_Puppet_ButtlerRange_Normal_00,
            CH01_Puppet_Fguide_Boss_00,//BOSS
            CH01_Puppet_ButtlerMelee_HeadLess_Normal_00,
            CH01_Puppet_ButtlerMelee_Station_Elite_00
        }


        public struct NpcSpotData
        {
            public int exportID;
            public string spotUniqueID;
            public NpcType npcType;
            public SpotCodeName spotCodeNameOriginal;//ORIGINAL NPC
            
        }
        public static readonly Dictionary<NpcType, string[]> Npc = new Dictionary<NpcType, string[]>
        {
            { NpcType.Boss, new[]{
            "CH01_Puppet_Fguide_Boss_00",
            "CH02_Puppet_Judge_Boss_00",
            "CH03_Puppet_FireEater_Boss_00",
            "CH04_Carcass_FallenArchBishop_Boss_00",
            "CH04_Carcass_FallenArchBishop_Boss_01",
            //Black Rabbit Human
            "CH05_Stalker_BRabbit_StrongMale_Boss_00",
            "Stalker_BRabbit_Female_Boss_00",
            "Stalker_BRabbit_NormalMale_Boss_00",
            "Stalker_BRabbit_TallMale_Boss_00",
            //Black Rabbit Carcass
            "CH11_BRabbit_StrongMale_Boss_00",
            "Stalker_BRabbit_Female_Boss_01",
            "Stalker_BRabbit_NormalMale_Boss_01",
            "Stalker_BRabbit_TallMale_Boss_01",
            } },
            {NpcType.MiniBossStalker, new[]{
            "Stalker_MadStalker_00",
            "Stalker_SurvivorStalker_00",
            "Stalker_WhiteStalker_00",
            } },
            {NpcType.Puppet,new[]{
            "CH01_Puppet_ButtlerMelee_Station_Normal_00",
            "CH01_Puppet_Police_Named_00",
            "CH01_Puppet_ButtlerMelee_Normal_00",
            "CH01_Puppet_Watchdog_Normal_00",
            "CH01_Puppet_ButtlerRange_Normal_00",
            "CH01_Puppet_ButtlerMelee_HeadLess_Normal_00",
            "CH01_Puppet_ButtlerMelee_Station_Elite_00",
            "CH04_Puppet_ButtlerMelee_MidRange_Normal_00",
            "CH04_Puppet_ButtlerMidRange_ArmLess_NoRespawn_Normal_00",
            "CH04_Puppet_ButtlerMidRange_ArmLess_Normal_00",
            "CH04_Puppet_ButtlerMidRange_HeadLess_NoRespawn_Normal_00",
            "CH04_Puppet_ButtlerMidRange_HeadLess_Normal_00",
            "CH04_Puppet_ButtlerMidRange_Normal_00",
            "CH04_Puppet_ButtlerMidRange_Normal_NoRespawn_00",
            "CH04_Puppet_ButtlerThrower_MidRange_HeadLess_Normal_00",
            "CH04_Puppet_ButtlerThrower_MidRange_HeadTurn_Normal_00",
            "CH04_Puppet_ButtlerThrower_MidRange_Normal_00",
            "CH04_Puppet_FireStoker_Named_00"
            }},
            {NpcType.Carcass, new[]{
            //"CH04_Carcass_FallenArchBishop_Boss_01",
            "CH04_Carcass_GeneralFemale1_Normal_00",
            "CH04_Carcass_GeneralLeader_Elite_NoRespawn_00",
            "CH04_Carcass_GeneralMale1_Normal_00",
            "CH04_Carcass_GeneralMale1_Normal_NoRespawn_00",
            "CH04_Carcass_GeneralMale3_Normal_00",
            "CH04_Carcass_MucusSpray_Normal_00",
            "CH04_Carcass_OneArmed_Shield_Named_00"

            }}




        };

        
        //public static readonly List<NpcSpotData> NpcLvInnerUpperStreet = new List<NpcSpotData>()
        //{
        //    new NpcSpotData { id = 1, mapSpotName = "Npc-LV_Inner_UpperStreet_DSN-1", npcType = NpcType.Puppet },
        //    new NpcSpotData { id = 2, mapSpotName = "Npc-LV_Inner_UpperStreet_DSN-2", npcType = NpcType.Boss },
        //};


        //Firelink Shrine (Hotel) - Basically for the training dummies only
        public static readonly List<NpcSpotData> NpcLVKratHotel = new List<NpcSpotData>()
        {
            new NpcSpotData { exportID = 325, spotUniqueID = "Npc-LV_Hotel_DSN-1", npcType = NpcType.Dummy, spotCodeNameOriginal = SpotCodeName.Hotel_Puppet_Training_Armless_00 },
            new NpcSpotData { exportID = 326, spotUniqueID = "Npc-LV_Hotel_DSN-31", npcType = NpcType.Dummy, spotCodeNameOriginal = SpotCodeName.Hotel_Puppet_Training_Basic_00 },
        };

        //CHAPTER 1 - UP UNTIL THE CLOWN PUPPET 
        public static readonly List<NpcSpotData> NpcLDOuterStation = new List<NpcSpotData>()
        {
            new NpcSpotData{ exportID = 1330, spotUniqueID = "Npc-LD_Outer_Station_DSN-1", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1331, spotUniqueID = "Npc-LD_Outer_Station_DSN-10", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Police_Named_00 },
            new NpcSpotData{ exportID = 1332, spotUniqueID = "Npc-LD_Outer_Station_DSN-11", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1333, spotUniqueID = "Npc-LD_Outer_Station_DSN-12", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1334, spotUniqueID = "Npc-LD_Outer_Station_DSN-13", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Watchdog_Normal_00 },
            new NpcSpotData{ exportID = 1335, spotUniqueID = "Npc-LD_Outer_Station_DSN-14", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1336, spotUniqueID = "Npc-LD_Outer_Station_DSN-15", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1337, spotUniqueID = "Npc-LD_Outer_Station_DSN-16", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1338, spotUniqueID = "Npc-LD_Outer_Station_DSN-17", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1339, spotUniqueID = "Npc-LD_Outer_Station_DSN-18", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Watchdog_Normal_00 },
            new NpcSpotData{ exportID = 1340, spotUniqueID = "Npc-LD_Outer_Station_DSN-19", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1343, spotUniqueID = "Npc-LD_Outer_Station_DSN-21", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerRange_Normal_00 },
            //BOSS
            new NpcSpotData{ exportID = 1344, spotUniqueID = "Npc-LD_Outer_Station_DSN-22", npcType = NpcType.BossPuppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Fguide_Boss_00 },
            new NpcSpotData{ exportID = 1345, spotUniqueID = "Npc-LD_Outer_Station_DSN-23", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1346, spotUniqueID = "Npc-LD_Outer_Station_DSN-24", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1347, spotUniqueID = "Npc-LD_Outer_Station_DSN-25", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1348, spotUniqueID = "Npc-LD_Outer_Station_DSN-28", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerRange_Normal_00 },
            new NpcSpotData{ exportID = 1349, spotUniqueID = "Npc-LD_Outer_Station_DSN-3", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1350, spotUniqueID = "Npc-LD_Outer_Station_DSN-32", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1351, spotUniqueID = "Npc-LD_Outer_Station_DSN-33", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1352, spotUniqueID = "Npc-LD_Outer_Station_DSN-34", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerRange_Normal_00 },
            new NpcSpotData{ exportID = 1353, spotUniqueID = "Npc-LD_Outer_Station_DSN-35", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1354, spotUniqueID = "Npc-LD_Outer_Station_DSN-36", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1355, spotUniqueID = "Npc-LD_Outer_Station_DSN-37", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1356, spotUniqueID = "Npc-LD_Outer_Station_DSN-38", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1357, spotUniqueID = "Npc-LD_Outer_Station_DSN-39", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1360, spotUniqueID = "Npc-LD_Outer_Station_DSN-41", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1361, spotUniqueID = "Npc-LD_Outer_Station_DSN-42", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Watchdog_Normal_00 },
            new NpcSpotData{ exportID = 1362, spotUniqueID = "Npc-LD_Outer_Station_DSN-45", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_HeadLess_Normal_00 },
            new NpcSpotData{ exportID = 1363, spotUniqueID = "Npc-LD_Outer_Station_DSN-46", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_HeadLess_Normal_00 },
            new NpcSpotData{ exportID = 1365, spotUniqueID = "Npc-LD_Outer_Station_DSN-47", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1366, spotUniqueID = "Npc-LD_Outer_Station_DSN-48", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1367, spotUniqueID = "Npc-LD_Outer_Station_DSN-49", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Normal_00 },
            new NpcSpotData{ exportID = 1369, spotUniqueID = "Npc-LD_Outer_Station_DSN-51", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Watchdog_Normal_00 },
            new NpcSpotData{ exportID = 1370, spotUniqueID = "Npc-LD_Outer_Station_DSN-52", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1371, spotUniqueID = "Npc-LD_Outer_Station_DSN-6", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Watchdog_Normal_00 },
            new NpcSpotData{ exportID = 1372, spotUniqueID = "Npc-LD_Outer_Station_DSN-7", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Station_Elite_00 },
            new NpcSpotData{ exportID = 1373, spotUniqueID = "Npc-LD_Outer_Station_DSN-8", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_Normal_00 },
            new NpcSpotData{ exportID = 1374, spotUniqueID = "Npc-LD_Outer_Station_DSN-9", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_ButtlerMelee_HeadLess_Normal_00 }

        };
      
       






        
       
    }
}
