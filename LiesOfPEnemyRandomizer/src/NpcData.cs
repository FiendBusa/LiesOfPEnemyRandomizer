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
            MiniBossReborner,
            BossPuppet,
            BossPuppetPhase,
            BossHuman,
            Boss,
            BossCarcass,
            Dummy,
            Reborner,
            ButterFly,
            HelpMate,
            Important,
            Projectile
        };

        public enum SpotCodeName
        {
            Hotel_Puppet_Training_Armless_00,
            Hotel_Puppet_Training_Basic_00,
            CH01_Puppet_ButtlerMelee_Station_Normal_00,
            CH01_Puppet_Police_Named_00,
            CH01_Puppet_ButtlerMelee_Normal_00,
            CH01_Puppet_Watchdog_Normal_00,
            CH01_Puppet_ButtlerRange_Normal_00,
            CH01_Puppet_Fguide_Boss_00,
            CH01_Puppet_ButtlerMelee_HeadLess_Normal_00,
            CH01_Puppet_ButtlerMelee_Station_Elite_00,
            CH02_Puppet_Judge_Boss_00,
            CH02_RedButterFly_Red_00,
            CH04_Carcass_FallenArchBishop_Boss_00,
            CH04_Carcass_FallenArchBishop_Boss_01,
            CH07_Reborner_Victor_Boss_00,
            CH07_Reborner_Victor_Boss_01,
            CH03_Puppet_FireEater_Boss_00,
            CH03_RedButterFly_Red_01,
            CH03_Puppet_DefenseWall_Named_00,
            CH03_HelpMate_Exile,
            CH03_RedButterFly_Red_00,
            CH03_Puppet_Tomorrow_Seed_00,
            CH02_HelpMate_Exile,
            CH07_RedButterFly_Purple_00,
            CH07_Reborner_Muscular_Named_00,
            CH07_RedButterFly_Orange_00,
            CH07_RedButterFly_Red_01,
            CH07_HelpMate_Exile,
            CH07_RedButterFly_Red_00,
            CH07_Puppet_LaborDrill_Elite_NoRespawn_00,
            CH07_Carcass_HammerArm_Named_00,
            CH07_Puppet_Butcher_Named_00,
            CH07_Puppet_Captain_Electric_Named_01,
            CH07_Carcass_GeneralMale2_Normal_00,
            CH07_Puppet_Captain_Electric_Named_00,
            CH09_Carcass_GraveKeeper_Seed_00,
            CH09_RedButterFly_Red_00,
            CH09_RedButterFly_Purple_00,
            CH09_Carcass_FourFootBig_Named_00,
            CH09_RedButterFly_Orange_00,
            CH09_Carcass_OneArmed_Named_00,
            CH09_Carcass_GeneralLeader_Named_00,
            CH09_Stalker_Weasel_00,
            CH09_Reborner_Illusion_Seed_00,
            CH08_Carcass_GreenHunter_Main_Boss_00,
            CH08_Carcass_GreenHunter_Fusion_Boss_00,
            CH08_RedButterFly_Orange_00,
            CH08_Puppet_Tomorrow_Electronic_Named_00,
            CH08_Carcass_OneArmed_Shield_Named_00,
            CH08_Puppet_Clown_Proto_Named_00,
            CH08_Structure_Missile_Normal_00,
            CH08_HelpMate_Exile,
            CH08_RedButterFly_Red_03,
            CH08_RedButterFly_Red_04,
            CH08_RedButterFly_Purple_00,
            CH08_RedButterFly_Purple_01,
            CH08_RedButterFly_Red_00,
            CH08_RedButterFly_Red_01,
            CH08_RedButterFly_Red_02,
            CH08_Puppet_FailDoll_Named_01,
            CH05_Stalker_BRabbit_Female_Boss_00,
            CH05_Stalker_BRabbit_StrongMale_Boss_00,
            CH05_Stalker_BRabbit_NormalMale_Boss_00,
            CH05_Stalker_BRabbit_TallMale_Boss_00,
            CH05_Carcass_MutantBear_Elite_00,
            CH05_RedButterFly_Purple_00,
            CH05_Carcass_CarcassSM_Named_00,
            CH05_RedButterFly_Red_00,
            CH05_HelpMate_Exile,
            CH05_Carcass_Tyrant_Elite_NoRespawn_00,
            CH05_Carcass_HammerArm_Named_00,
            CH11_Stalker_BRabbit_Female_Boss_00,
            CH11_Stalker_BRabbit_NormalMale_Boss_00,
            CH11_Stalker_BRabbit_TallMale_Boss_00,
            CH11_Stalker_BRabbit_StrongMale_Boss_00,
            CH11_RedButterFly_Red_01,
            CH11_RedButterFly_Red_00,
            CH11_HelpMate_Exile,
            CH11_RedButterFly_Red_02,
            CH11_Puppet_Captain_Named_00,
            CH06_Puppet_PuppetKingP3_Boss_00,
            CH06_Puppet_PuppetKing_Boss_00,
            CH06_RedButterFly_Red_00,
            CH06_RedButterFly_Red_01,
            CH06_HelpMate_Exile,
            CH12_Reborner_Raxasia_Boss_00,
            CH12_Reborner_Raxasia_Boss_01,
            CH12_RedButterFly_Red_01,
            CH12_Carcass_FourFootBig_Elite_00,
            CH12_Carcass_BallHand_Normal_00,
            CH13_RedButterFly_Orange_00,
            CH12_Puppet_FailDoll_Named_00,
            CH12_RedButterFly_Red_02,
            CH12_RedButterFly_Red_00,
            CH12_Carcass_GuardianDoor_Seed_00,
            CH12_HelpMate_Exile,
            CH12_Carcass_TwoHand_Named_00,
            CH12_Stalker_Cat_00,
            CH13_Puppet_1stPinoccio_Boss_P2_00,
            CH13_Puppet_1stPinoccio_Boss_00,
            CH13_Reborner_Simon_Boss_00,
            CH13_Reborner_Simon_Boss_01,
            CH13_Reborner_ErgoControl_Named_001,
            CH13_Stalker_Fox_00,
            CH13_HelpMate_Exile







        }

        public enum NpcLevel
        {
            LD_Outer_Station,
            LV_Inner_UpperStreet_DSN,
            LV_Inner_Cathedral_DSN,
            LV_Outer_Exhibition,
            LV_Inner_Factory_DSN

        };

        public enum FactionType
        {
            E_MONSTER_PUPPET,
            E_MONSTER_CARCASS,
            E_MONSTER_CARCASSNPUPPET,
            E_ENEMY_TOONLYPLAYER,
            E_MONSTER_STALKER,
            E_NEUTRAL
        }




        public struct NpcSpotData
        {
            public int exportID;
            public string spotUniqueID;
            public NpcType npcType;
            public SpotCodeName spotCodeNameOriginal;
            public bool? npcImportant; //Won't randomize if set to TRUE

        }

        //RESUME CHAPTER 8 I DIDNT DO ANY IN CHAPTER 8
        public static readonly Dictionary<NpcType, string[]> Npc = new Dictionary<NpcType, string[]>
        {
            { NpcType.Boss, new[]{
            "CH01_Puppet_Fguide_Boss_00",
            "CH02_Puppet_Judge_Boss_00",
            "CH03_Puppet_FireEater_Boss_00",
            "CH04_Carcass_FallenArchBishop_Boss_00",
            "CH04_Carcass_FallenArchBishop_Boss_01",
            //Black Rabbit Human
            //"CH05_Stalker_BRabbit_Female_Boss_00",
            //"CH05_Stalker_BRabbit_NormalMale_Boss_00",
            "CH05_Stalker_BRabbit_StrongMale_Boss_00",
            //"CH05_Stalker_BRabbit_TallMale_Boss_00",

            "CH06_Puppet_PuppetKing_Boss_00",
            "CH06_Puppet_PuppetKingP3_Boss_00",

            "CH07_Reborner_Victor_Boss_00",
            "CH07_Reborner_Victor_Boss_01",

            "CH08_Carcass_GreenHunter_Fusion_Boss_00",
            "CH08_Carcass_GreenHunter_Main_Boss_00",

            //CARCASS PARADE MASTER
            "CH09_Carcass_GraveKeeper_Seed_00",

            //Black Rabbit Carcass
            //"CH11_Stalker_BRabbit_Female_Boss_00",
            //"CH11_Stalker_BRabbit_NormalMale_Boss_00",
            "CH11_Stalker_BRabbit_StrongMale_Boss_00",
            //"CH11_Stalker_BRabbit_TallMale_Boss_00",

            "CH12_Reborner_Raxasia_Boss_00",
            "CH12_Reborner_Raxasia_Boss_01",

            "CH13_Puppet_1stPinoccio_Boss_00",
            "CH13_Puppet_1stPinoccio_Boss_P2_00",

            "CH13_Reborner_Simon_Boss_00",
            "CH13_Reborner_Simon_Boss_01",

            } },
            {NpcType.MiniBossStalker, new[]{
            "Stalker_MadStalker_00",
            "Stalker_SurvivorStalker_00",
            "Stalker_PilgrimStalker_00",
            "Stalker_WhiteStalker_00",
            "Stalker_ArmySurgeon_00",
            "Stalker_Weasel_00",
            "Stalker_Cat_00",
            "Stalker_Fox_00",
            "Stalker_BRabbit_NormalMale_Boss_00",
            "Stalker_BRabbit_StrongMale_Boss_00",
            "Stalker_BRabbit_TallMale_Boss_00",

            /*"CH02_Stalker_MadStalker_00",
            "CH03_Stalker_SurvivorStalker_00",
            "CH04_Stalker_PilgrimStalker_00",
            "CH06_Stalker_WhiteStalker_00",
            "CH08_Stalker_ArmySurgeon_00",
            "CH09_Stalker_Weasel_00",
            "CH12_Stalker_Cat_00",
            "CH13_Stalker_Fox_00",
            "CH05_Stalker_BRabbit_NormalMale_Boss_00",
            "CH05_Stalker_BRabbit_StrongMale_Boss_00",
            "CH05_Stalker_BRabbit_TallMale_Boss_00",*/
            } },
            {NpcType.MiniBossPuppet, new[]{
            "CH03_Puppet_Tomorrow_Seed_00",
            "CH06_Puppet_Clown_Seed_00",
            "CH08_Puppet_Clown_Proto_Named_00",
            "CH08_Puppet_Tomorrow_Electronic_Named_00",


            } },
            {NpcType.MiniBossReborner, new[]{
            "CH09_Reborner_Illusion_Seed_00",
            } },
            {NpcType.Reborner, new[]{
            "CH07_Reborner_Muscular_Named_00",
            "CH07_Reborner_Screamer_Elite_00",
            "CH07_Reborner_Spore_Normal_00",
            "CH09_Reborner_Baptist_Normal_00",
            "CH09_Reborner_IllusionClone_Named_00",
            "CH09_Reborner_Luminary_Normal_00",
            "CH09_Reborner_Spore_Normal_00",
            "CH12_Reborner_Baptist_Enforce_Elite_00",
            "CH12_Reborner_Baptist_Normal_00",
            "CH12_Reborner_Gas_NoneMove_Normal_00",
            "CH12_Reborner_Gas_Normal_00",
            "CH12_Reborner_Muscular_Elite_00",
            "CH12_Reborner_Spore_Normal_00",
            "CH13_Reborner_Spore_Normal_00",
            } },
            {NpcType.Puppet,new[]{
            "CH01_Puppet_ButtlerMelee_HeadLess_Normal_00",
            "CH01_Puppet_ButtlerMelee_Normal_00",
            "CH01_Puppet_ButtlerMelee_Station_Elite_00",
            "CH01_Puppet_ButtlerMelee_Station_Normal_00",
            "CH01_Puppet_ButtlerRange_Normal_00",
            "CH01_Puppet_Police_Named_00",
            "CH01_Puppet_Watchdog_Aggro_Normal_00",
            "CH01_Puppet_Watchdog_Normal_00",
            "CH01_Puppet_Watchdog_Test_00",


            "CH02_Puppet_ButtlerMelee_ArmLess_Normal_00",
            "CH02_Puppet_ButtlerMelee_HeadLess_Normal_00",
            "CH02_Puppet_ButtlerMelee_Normal_00",
            "CH02_Puppet_ButtlerMelee_White_Elite_00",
            "CH02_Puppet_ButtlerMidRange_Normal_00",
            "CH02_Puppet_ButtlerRange_Normal_00",
            "CH02_Puppet_ButtlerThrower_Normal_00",
            "CH02_Puppet_Horseman_Elite_00",
            "CH02_Puppet_Horseman_Headless_Elite_00",
            "CH02_Puppet_Police_Named_00",
            "CH02_Puppet_SoldierSword_Normal_00",
            "CH02_Puppet_Watchdog_Aggro_Normal_00",
            "CH02_Puppet_Watchdog_Normal_00",
            //"CH02_RedButterFly_Red_00",

            "CH03_Puppet_ButtlerMelee_Proto2_Normal_00",
            "CH03_Puppet_ButtlerMelee_Proto3_Normal_00",
            "CH03_Puppet_ButtlerMelee_Proto4_Normal_00",
            "CH03_Puppet_ButtlerMelee_Proto_Normal_00",
            "CH03_Puppet_ButtlerMidRange_Normal_01",
            "CH03_Puppet_ButtlerMidRange_Normal_02",
            "CH03_Puppet_ButtlerMidRange_Normal_03",
            "CH03_Puppet_ButtlerMidRange_Proto_Normal_00",
            "CH03_Puppet_ButtlerThrower_MidRange_Normal_00",
            "CH03_Puppet_DefenseWall_Named_00",
            "CH03_Puppet_FireStoker_Named_00",
            "CH03_Puppet_FireStoker_Named_01",
            "CH03_Puppet_Horseman_Wheel_Elite_00",
            "CH03_Puppet_Marionette_Oil_Normal_00",
            //"CH03_Puppet_Tomorrow_Seed_00",
            "CH03_Puppet_UpperBody_Aggro_Normal_00",
            "CH03_Puppet_UpperBody_HeadLess_Normal_00",
            "CH03_Puppet_UpperBody_Normal_00",
            "CH03_Puppet_UpperBody_Proto2_Float_Normal_00",
            "CH03_Puppet_UpperBody_Proto_Float_Normal_00",
            //"CH03_RedButterFly_Red_00",
            //"CH03_RedButterFly_Red_01",

             "CH04_Puppet_ButtlerMelee_MidRange_Normal_00",
             //"CH04_Puppet_ButtlerMidRange_ArmLess_NoRespawn_Normal_00",
             "CH04_Puppet_ButtlerMidRange_ArmLess_Normal_00",
             //"CH04_Puppet_ButtlerMidRange_HeadLess_NoRespawn_Normal_00",
             "CH04_Puppet_ButtlerMidRange_HeadLess_Normal_00",
             "CH04_Puppet_ButtlerMidRange_Normal_00",
             //"CH04_Puppet_ButtlerMidRange_Normal_NoRespawn_00",
             "CH04_Puppet_ButtlerThrower_MidRange_HeadLess_Normal_00",
             "CH04_Puppet_ButtlerThrower_MidRange_HeadTurn_Normal_00",
             "CH04_Puppet_ButtlerThrower_MidRange_Normal_00",
             "CH04_Puppet_FireStoker_Named_00",
             ///"CH04_RedButterFly_Purple_00",
             //"CH04_RedButterFly_Red_00",
             //"CH04_RedButterFly_Red_01",
             //"CH04_Stalker_PilgrimStalker_00",

             "CH06_Puppet_BabyDoll_Normal_00",
             "CH06_Puppet_Bomber_Normal_00",
             "CH06_Puppet_Bomber_Normal_00_QST",
             "CH06_Puppet_ButtlerMelee_Normal_00",
             "CH06_Puppet_Horseman_Headless_Elite_00",
             "CH06_Puppet_Maid_Normal_00",
             "CH06_Puppet_Marionette_Blood_Elite_00",
             "CH06_Puppet_Marionette_Normal_00",
             "CH06_Puppet_ShamDuo_Elite_00",
             "CH06_Puppet_SoldierFlame_Normal_00",
             "CH06_Puppet_SoldierRifle_Normal_00",
             "CH06_Puppet_SpiderLady_Elite_00",
             "CH06_Puppet_StatueMage_Normal_00",
             "CH06_Puppet_UpperBody_Aggro_Normal_00",
             "CH06_Puppet_UpperBody_Float_Normal_00",
             "CH06_Puppet_UpperBody_Normal_00",
             //"CH06_RedButterFly_Red_00",
             //"CH06_RedButterFly_Red_01",
             "CH07_Puppet_Butcher_Named_00",
             "CH07_Puppet_ButtlerMelee_Normal_00",
             "CH07_Puppet_Captain_Electric_Named_00",
             "CH07_Puppet_Captain_Electric_Named_01",
             "CH07_Puppet_DefenseWall_Elite_00",
             "CH07_Puppet_DefenseWall_Elite_01",
             "CH07_Puppet_LaborDrill_Elite_00",
             //"CH07_Puppet_LaborDrill_Elite_NoRespawn_00",
             "CH07_Puppet_Maid_Normal_00",
             "CH07_Puppet_Marionette_Blood_Elite_00",
             "CH07_Puppet_Marionette_Blood_Elite_01",
             "CH07_Puppet_Marionette_Proto_Normal_00",
             "CH07_Puppet_SoldierSword_Normal_00",
             "CH07_Puppet_StatueMage_Electric_Normal_00",
             "CH07_Puppet_Watchdog_Blade_Normal_00",
             "CH07_Puppet_Watchdog_Saw_Normal_00",

            "CH08_Puppet_ButtlerMelee_Proto2_Normal_00",
            "CH08_Puppet_ButtlerMelee_Proto3_Normal_00",
            "CH08_Puppet_ButtlerMidRange_HeadTurn_Proto_Normal_00",
            "CH08_Puppet_ButtlerMidRange_Normal_00",
            "CH08_Puppet_ButtlerMidRange_Normal_01",
            "CH08_Puppet_ButtlerMidRange_Normal_02",
            "CH08_Puppet_ButtlerMidRange_Normal_03",
            "CH08_Puppet_FailDoll_Named_01",
            "CH08_Puppet_FireStoker_Elite_00",
            "CH08_Puppet_HorsemanStone_Elite_Test_00",
            "CH08_Puppet_Police_Proto_Elite_00",
            "CH08_Puppet_SoldierRifle_Normal_00",
            "CH08_Puppet_SoldierSword_Elite_00",
            "CH08_Puppet_SoldierSword_Normal_00",
            "CH08_Puppet_UpperBody_Proto2_Normal_00",
            "CH08_Puppet_UpperBody_Proto_Normal_00",
            "CH08_Puppet_Watchdog_Blade_Normal_00",
            "CH08_Puppet_Watchdog_Bomb_Normal_00",

            "CH09_Puppet_ButtlerMelee_Proto2_Normal_00",
            "CH09_Puppet_ButtlerMelee_Proto3_Normal_00",
            "CH09_Puppet_ShamDuo_Elite_01",
            "CH09_Puppet_UpperBody_Normal_TF",

            "CH11_Puppet_ButtlerMelee_MidRange_Normal_00",
            "CH11_Puppet_ButtlerMidRange_Normal_00",
            "CH11_Puppet_Captain_Named_00",

            "CH12_Puppet_Butcher_Elite_00",
            "CH12_Puppet_FailDoll_Named_00",


            }},
            {NpcType.MiniBossCarcass,new[]{

            "CH12_Carcass_GuardianDoor_Seed_00",
                
            } },
            {NpcType.Carcass, new[]{
            "CH04_Carcass_GeneralFemale1_Normal_00",
            //"CH04_Carcass_GeneralLeader_Elite_NoRespawn_00",
            "CH04_Carcass_GeneralMale1_Normal_00",
            //"CH04_Carcass_GeneralMale1_Normal_NoRespawn_00",
            "CH04_Carcass_GeneralMale1_Raw_Normal_00_QST",
            "CH04_Carcass_GeneralMale3_Normal_00",
            "CH04_Carcass_MucusSpray_Normal_00",
            "CH04_Carcass_OneArmed_Shield_Named_00",

            "CH05_Carcass_BoneTranform_Elite_00",
            "CH05_Carcass_CarcassSM_Named_00",
            "CH05_Carcass_GeneralFemale1_Normal_00",
            "CH05_Carcass_GeneralFemale1_WP3_Normal_00",
            "CH05_Carcass_GeneralFemale2_WP1_Elite_00",
            "CH05_Carcass_GeneralMale1_Elite_00",
            "CH05_Carcass_GeneralMale1_Normal_00",
            "CH05_Carcass_GeneralMale1_Normal_00_QST",
            "CH05_Carcass_GeneralMale1_Normal_00_TF",
            "CH05_Carcass_GeneralMale2_Normal_00",
            "CH05_Carcass_GeneralMale2_WP2_Normal_00",
            "CH05_Carcass_HammerArm_Named_00",
            "CH05_Carcass_MucusSpray_Normal_00",
            "CH05_Carcass_MutantBear_Elite_00",
            "CH05_Carcass_MutantDog_Normal_00",
            //"CH05_Carcass_Tyrant_Elite_NoRespawn_00",
            //"CH05_Commander_Brabbit",
            //"CH05_HelpMate_Exile",
            //"CH05_RedButterFly_Purple_00",
            //"CH05_RedButterFly_Red_00",

            "CH07_Carcass_BoneTranform_Elite_00",
            "CH07_Carcass_BulkUp_Elite_00",
            "CH07_Carcass_GeneralFemale2_Normal_00",
            "CH07_Carcass_GeneralFemale3_Normal_00",
            "CH07_Carcass_GeneralMale1_Normal_00_TF",
            "CH07_Carcass_GeneralMale1_Raw_Normal_00",
            "CH07_Carcass_GeneralMale2_Normal_00",
            "CH07_Carcass_HammerArm_Named_00",
            "CH07_Carcass_MucusSpray_Normal_00",
            "CH07_Carcass_UpperLess_Normal_00",
            "CH07_Carcass_Vampire_Normal_00",

            "CH08_Carcass_CarcassSM_Elite_00",
            "CH08_Carcass_GeneralFemale1_WP1_Normal_00",
            "CH08_Carcass_GeneralFemale2_WP1_Elite_00",
            "CH08_Carcass_GeneralFemale2_WP2_Elite_00",
            "CH08_Carcass_GeneralFemale2_WP3_Elite_00",
            "CH08_Carcass_GeneralFemale3_WP4_Normal_00",
            "CH08_Carcass_GeneralMale1_Elite_00",
            "CH08_Carcass_GeneralMale2_WP3_Normal_00",
            "CH08_Carcass_MutantBear_Elite_00",
            "CH08_Carcass_OneArmed_Shield_Named_00",
            "CH08_Carcass_UpperBody_Normal_00",

            "CH09_Carcass_AcidArm_Normal_00",
            "CH09_Carcass_AcidArm_Raw_Normal_00",
            "CH09_Carcass_AcidArm_Raw_SeedSpawn_Normal_00",
            "CH09_Carcass_BabyDoll_Normal_00",
            "CH09_Carcass_BallHand_Normal_00",
            "CH09_Carcass_BallHand_Raw_SeedSpawn_Normal_00",
            "CH09_Carcass_BulkUp_Elite_00",
            "CH09_Carcass_ButtlerMelee_Station_Elite_00",
            "CH09_Carcass_ButtlerMelee_Station_Normal_00",
            "CH09_Carcass_Crystal_Elite_00",
            "CH09_Carcass_FourFootBig_Named_00",
            "CH09_Carcass_FourFootPoison_Normal_00",
            "CH09_Carcass_GeneralFemale1_Normal_QST",
            "CH09_Carcass_GeneralLeader_Named_00",
            "CH09_Carcass_GeneralMale1_Normal_00",
            "CH09_Carcass_GeneralMale1_Raw_Normal_00",
            "CH09_Carcass_GeneralMale1_Raw_SeedSpawn_Normal_00",
            "CH09_Carcass_HeadLess_Normal_00",
            "CH09_Carcass_Horseman_Elite_00",
            "CH09_Carcass_MutantDog_Normal_00",
            "CH09_Carcass_OneArmed_Named_00",
            "CH09_Carcass_UpperLess_Normal_00",
            "CH09_Carcass_Vampire_Normal_00",

            "CH11_Carcass_Horseman_Elite_00",
            "CH11_Carcass_LizardDog_Normal_00",

            "CH12_Carcass_AcidArm_Normal_00",
            "CH12_Carcass_BabyDoll_Normal_00",
            "CH12_Carcass_BallHand_Normal_00",
            "CH12_Carcass_BallHand_Raw_Normal_00",
            "CH12_Carcass_ButtlerMelee_Station_Elite_00",
            "CH12_Carcass_ButtlerMelee_Station_Normal_01",
            "CH12_Carcass_ButtlerMelee_Station_Normal_02",
            "CH12_Carcass_FourFootBig_Elite_00",
            "CH12_Carcass_FourFootBlade_Elite_00",
            "CH12_Carcass_FourFootPoison_Normal_00",
            "CH12_Carcass_HeadLess_Normal_00",
            "CH12_Carcass_Horseman_Elite_00",
            "CH12_Carcass_LizardDog_Normal_00",
            "CH12_Carcass_TwoHand_Named_00",
            "CH12_Carcass_Tyrant_Elite_00",
            "CH12_Carcass_UpperBody_Normal_00",
            "CH12_Carcass_UpperLess_Normal_00",
            "CH12_Carcass_Vampire_Normal_00",

            }}




        };



        //Firelink Shrine (Hotel) - Basically for the training dummies only
        public static readonly List<NpcSpotData> NpcLVKratHotel = new List<NpcSpotData>()
        {
            new NpcSpotData { exportID = 325, spotUniqueID = "Npc-LV_Hotel_DSN-1", npcType = NpcType.Dummy, spotCodeNameOriginal = SpotCodeName.Hotel_Puppet_Training_Armless_00 },
            new NpcSpotData { exportID = 326, spotUniqueID = "Npc-LV_Hotel_DSN-31", npcType = NpcType.Dummy, spotCodeNameOriginal = SpotCodeName.Hotel_Puppet_Training_Basic_00 },
        };

        //CHAPTER 1
        //S0_04
        public static readonly List<NpcSpotData> NpcLDOuterStation = new List<NpcSpotData>()
        {
            new NpcSpotData{ exportID = 1331, spotUniqueID = "Npc-LD_Outer_Station_DSN-22", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Fguide_Boss_00, npcImportant = true },
            //KRAT CENTRAL KEY DON'T REPLACE UNTIL I CAN CHANGE STARTING ITEM (key_1)
            new NpcSpotData{ exportID = 1331, spotUniqueID = "Npc-LD_Outer_Station_DSN-10", npcType = NpcType.Important, spotCodeNameOriginal = SpotCodeName.CH01_Puppet_Police_Named_00, npcImportant = true },


        };
        //CHAPTER 02
        //S2_S3 
        public static readonly List<NpcSpotData> NpcLVInnerUpperStreet = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1155, spotUniqueID = "Npc-LV_Inner_UpperStreet_DSN-49", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH02_Puppet_Judge_Boss_00 },


             new NpcSpotData{ exportID = 1155, spotUniqueID = "Npc-LV_Inner_UpperStreet_DSN-52", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH02_RedButterFly_Red_00 },
             new NpcSpotData{ exportID = 1133, spotUniqueID = "Npc-LV_Inner_UpperStreet_DSN-28", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH02_HelpMate_Exile, npcImportant = true }
        };

        //CHAPTER 03
        //S2_S3 
        public static readonly List<NpcSpotData> NpcLVInnerFactory = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1476, spotUniqueID = "Npc-LV_Inner_Factory_DSN-1", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH03_Puppet_FireEater_Boss_00 },
             new NpcSpotData{ exportID = 1488, spotUniqueID = "Npc-LV_Inner_Factory_DSN-21", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH03_Puppet_DefenseWall_Named_00 },
             new NpcSpotData{ exportID = 1494, spotUniqueID = "Npc-LV_Inner_Factory_DSN-27", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH03_HelpMate_Exile , npcImportant = true},
             new NpcSpotData{ exportID = 1493, spotUniqueID = "Npc-LV_Inner_Factory_DSN-26", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH03_Puppet_Tomorrow_Seed_00, npcImportant=true },
             new NpcSpotData{ exportID = 1534, spotUniqueID = "Npc-LV_Inner_Factory_DSN-70", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH03_RedButterFly_Red_00 },
             new NpcSpotData{ exportID = 1485, spotUniqueID = "Npc-LV_Inner_Factory_DSN-18", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH03_RedButterFly_Red_01 },
        };

        //CHAPTER 04
        //S2_S3 
        public static readonly List<NpcSpotData> NpcLVInnerCathedral = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Inner_Cathedral_DSN-1", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH04_Carcass_FallenArchBishop_Boss_00 },
             new NpcSpotData{ exportID = 1559, spotUniqueID = "Npc-LV_Inner_Cathedral_DSN-59", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH04_Carcass_FallenArchBishop_Boss_01 }
        };

        //CHAPTER 05
        //S2_S4 
        public static readonly List<NpcSpotData> NpcLVKratOldTown = new List<NpcSpotData>()
        {
             //BLACK RABBIT BROTHERHOOD
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-1", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH05_Stalker_BRabbit_Female_Boss_00},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-2", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH05_Stalker_BRabbit_StrongMale_Boss_00},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-3", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH05_Stalker_BRabbit_NormalMale_Boss_00 },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-4", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH05_Stalker_BRabbit_TallMale_Boss_00 },

            //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-14", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH05_Carcass_MutantBear_Elite_00, npcImportant=true},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-17", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH05_RedButterFly_Purple_00,npcImportant = true},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-23", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH05_Carcass_CarcassSM_Named_00, npcImportant = true},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-33", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH05_RedButterFly_Red_00, npcImportant = true},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-38", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH05_HelpMate_Exile, npcImportant = true},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-42", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH05_Carcass_Tyrant_Elite_NoRespawn_00, npcImportant = true},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_Old_Town_DSN-42", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH05_Carcass_HammerArm_Named_00, npcImportant = true},
        };

        //CHAPTER 06
        //S2_S4 
        public static readonly List<NpcSpotData> NpcLVKratEastEndWard = new List<NpcSpotData>()
        {
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-30", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH06_Puppet_PuppetKingP3_Boss_00},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-58", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH06_Puppet_PuppetKing_Boss_00},
            
            //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-16", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH06_RedButterFly_Red_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-22", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH06_RedButterFly_Red_01, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-28", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH06_HelpMate_Exile, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-16", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH06_RedButterFly_Red_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Krat_EastEndWard_DSN-16", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH06_RedButterFly_Red_00, npcImportant = true },




        };







        //CHAPTER 07
        //S2_S3 
        public static readonly List<NpcSpotData> NpcLVOuterExhibition = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-82", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH07_Reborner_Victor_Boss_00 },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-71", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH07_Reborner_Victor_Boss_01 },

             //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-102", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH07_RedButterFly_Purple_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-122", npcType = NpcType.Reborner, spotCodeNameOriginal = SpotCodeName.CH07_Reborner_Muscular_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-16", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH07_RedButterFly_Orange_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-21", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH07_RedButterFly_Red_01, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-23", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH07_HelpMate_Exile, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-25", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH07_RedButterFly_Red_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-3", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH07_Puppet_LaborDrill_Elite_NoRespawn_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-51", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH07_Carcass_HammerArm_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-62", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH07_Puppet_Butcher_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-64", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH07_Puppet_Captain_Electric_Named_01, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-00", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH07_Carcass_GeneralMale2_Normal_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Exhibition_DSN-90", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH07_Puppet_Captain_Electric_Named_00, npcImportant = true },
        };

        //CHAPTER 08
        //S2_S3 
        public static readonly List<NpcSpotData> NpcLVOuterGrave = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-1", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH08_Carcass_GreenHunter_Main_Boss_00 },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-113", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH08_Carcass_GreenHunter_Fusion_Boss_00 },
            
            //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-10", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Orange_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-12", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH08_Puppet_Tomorrow_Electronic_Named_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-14", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH08_Carcass_OneArmed_Shield_Named_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-18", npcType = NpcType.MiniBossPuppet, spotCodeNameOriginal = SpotCodeName.CH08_Puppet_Clown_Proto_Named_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-2", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH08_Structure_Missile_Normal_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-20", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH08_Carcass_OneArmed_Shield_Named_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-14", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH08_HelpMate_Exile, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-33", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH08_Puppet_Tomorrow_Electronic_Named_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-4", npcType = NpcType.Projectile, spotCodeNameOriginal = SpotCodeName.CH08_Structure_Missile_Normal_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-50", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Red_03, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-60", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Red_04, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-67", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Purple_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-76", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Purple_01, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-80", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Red_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-84", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Red_01, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-89", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH08_RedButterFly_Red_02, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Grave_DSN-99", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH08_Puppet_FailDoll_Named_01, npcImportant = true },


        };


        //CHAPTER 09
        //S2_S3 
        public static readonly List<NpcSpotData> NpcLVOuterCentralStatinB = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-122", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH09_Carcass_GraveKeeper_Seed_00, npcImportant = true },
            
             //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-14", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH09_RedButterFly_Red_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-2", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH09_RedButterFly_Purple_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-22", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH09_Carcass_FourFootBig_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-45", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH09_RedButterFly_Orange_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-62", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH09_Carcass_OneArmed_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-72", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH09_Carcass_GeneralLeader_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-74", npcType = NpcType.MiniBossStalker, spotCodeNameOriginal = SpotCodeName.CH09_Stalker_Weasel_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-90", npcType = NpcType.MiniBossReborner, spotCodeNameOriginal = SpotCodeName.CH09_Reborner_Illusion_Seed_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-122", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH09_Carcass_GraveKeeper_Seed_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-14", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH09_RedButterFly_Red_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_CentralStatinB_DSN-2", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH09_RedButterFly_Purple_00, npcImportant = true },
        };

        //CHAPTER 11
        //S2_S4 
        public static readonly List<NpcSpotData> NpcLVOuterUnderdark = new List<NpcSpotData>()
        {
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-50", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH11_Stalker_BRabbit_Female_Boss_00},
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-52", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH11_Stalker_BRabbit_NormalMale_Boss_00},
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-53", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH11_Stalker_BRabbit_TallMale_Boss_00},
              new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-8", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH11_Stalker_BRabbit_StrongMale_Boss_00},
            
             //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-36", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH11_RedButterFly_Red_01, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-38", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH11_RedButterFly_Red_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-42", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH11_HelpMate_Exile, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-43", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH11_RedButterFly_Red_02, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-46", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH11_Puppet_Captain_Named_00, npcImportant = true },


        };

        //CHAPTER 12 & CH13
        //S2_S4 
        public static readonly List<NpcSpotData> NpcLVMonasteryA = new List<NpcSpotData>()
        {
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-83", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH12_Reborner_Raxasia_Boss_00},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-111", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH12_Reborner_Raxasia_Boss_01},
             
            
             //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-10", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH12_RedButterFly_Red_01, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-101", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_FourFootBig_Elite_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-110", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_BallHand_Normal_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-114", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_BallHand_Normal_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-123", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH13_RedButterFly_Orange_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-139", npcType = NpcType.Puppet, spotCodeNameOriginal = SpotCodeName.CH12_Puppet_FailDoll_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-1", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_FourFootBig_Elite_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-21", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_FourFootBig_Elite_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-28", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH12_RedButterFly_Red_02, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-29", npcType = NpcType.ButterFly, spotCodeNameOriginal = SpotCodeName.CH12_RedButterFly_Red_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-4", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_GuardianDoor_Seed_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-41", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH12_HelpMate_Exile, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-63", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_BallHand_Normal_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-80", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_TwoHand_Named_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-89", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Carcass_FourFootBig_Elite_00, npcImportant = true },
             new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_A_DSN-95", npcType = NpcType.Carcass, spotCodeNameOriginal = SpotCodeName.CH12_Stalker_Cat_00, npcImportant = true },



        };

        //CHAPTER CH13
        //S2_S4 
        public static readonly List<NpcSpotData> NpcLVMonasteryB = new List<NpcSpotData>()
        {
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-34", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH13_Puppet_1stPinoccio_Boss_P2_00},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-38", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH13_Puppet_1stPinoccio_Boss_00},

            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-37", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH13_Reborner_Simon_Boss_00},
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-43", npcType = NpcType.Boss, spotCodeNameOriginal = SpotCodeName.CH13_Reborner_Simon_Boss_01},          
            
            //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-30", npcType = NpcType.Reborner, spotCodeNameOriginal = SpotCodeName.CH13_Reborner_ErgoControl_Named_001, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-5", npcType = NpcType.MiniBossStalker, spotCodeNameOriginal = SpotCodeName.CH13_Stalker_Fox_00, npcImportant = true },
            new NpcSpotData{ exportID = 1501, spotUniqueID = "Npc-LV_Monastery_B_DSN-2", npcType = NpcType.HelpMate, spotCodeNameOriginal = SpotCodeName.CH13_HelpMate_Exile, npcImportant = true },



        };

        ////CHAPTER 10 - seems to be unused level
        ////S2_S3 
        //public static readonly List<NpcSpotData> NpcLVOuterUnderdark = new List<NpcSpotData>()
        //{


        //    //MARKED AS IMPORTANT NPC ORIGINALLY (SO IF ITS FALSE THAT MEANS NO ISSUE WITH RANDOMIZING, REVERT TO TRUE IF CAUSING SOFT LOCKS (KEY ITEMS)
        //    new NpcSpotData { exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-1", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH10_Carcass_GreenHunter_Main_Boss_00 },
        //    new NpcSpotData { exportID = 1501, spotUniqueID = "Npc-LV_Outer_Underdark_DSN-1", npcType = NpcType.MiniBossCarcass, spotCodeNameOriginal = SpotCodeName.CH10_Carcass_GreenHunter_Main_Boss_00 },
        //};




    }
}









