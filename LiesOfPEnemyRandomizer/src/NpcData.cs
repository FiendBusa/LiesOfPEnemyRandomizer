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
           Boss,
           Dummy
        };

        public enum SpotCodeName
        {
            //Npc-LV_Hotel
            Hotel_Puppet_Training_Armless_00,
            Hotel_Puppet_Training_Basic_00
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
            {NpcType.MiniBoss, new[]{
            "Stalker_MadStalker_00",
            "Stalker_SurvivorStalker_00",
            "Stalker_WhiteStalker_00",
            } }


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
      
       






        
       
    }
}
