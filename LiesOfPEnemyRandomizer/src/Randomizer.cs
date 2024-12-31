using LiesOfPEnemyRandomizer.ViewModels;
using LiesOfPEnemyRandomizer.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.UnrealTypes.EngineEnums;
using UAssetAPI.Unversioned;
using static LiesOfPEnemyRandomizer.src.NpcData;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LiesOfPEnemyRandomizer.src
{
    public class Randomizer
    {
        public event Action<string>? LogUpdated;

        public bool IncludePuppets { get; private set; }
        public bool IncludeCarcass { get; private set; }
        public bool IncludeReborner { get; private set; }

        public bool IncludeMiniBossStalker { get; private set; }

        public bool IncludeMiniBossPuppet { get; private set; }

        public bool IncludeBosses { get; private set; }

        public bool IncludeMiniBossReborner { get; private set; }

        public bool IncludeMiniBossCarcass { get; private set; }

        public bool WanderingBoss { get; private set; }

        public double WanderingBossChance { get; private set; }

        public int Seed { get; private set; }

        public bool RandomizeDrops { get; private set; }

        public bool FactionProtection { get; private set; }
        public bool RandomizeDropsNpcBalancePercent { get; private set; }

        public bool RandomizeDropsNpcImportantItemMaxPercent { get; private set; }

        public bool RandomizeWeaponDropStack { get; private set; }

        public double RandomizeWeaponDropStackChanceRate { get; private set; }

        ItemDataBase ItemInfo { get; }

        public bool IncreaseBossAttributes { get; private set; }

        //TEMP
        public bool ScaleBosses { get; set; }

        //LOG MESSAGE
        public bool skipChp1Boss { get; set; }

        //TEMP
        public bool EnableDrops { get; set; }

        List<string> enemyPool;
        List<string> bossPool;
        List<string> wanderingPool;
        List<string> bossGuardianPool;
        List<string> itemPool;
        Dictionary<string,string> weaponPool;//HANDLE + BLADE

        Random random;


        public Randomizer(bool includePuppets, bool includeCarcass, bool includeReborner, bool includeMiniBossStalker, bool includeMiniBossPuppet, bool includeBosses, bool includeMiniBossReborner, bool includeMiniBossCarcass, bool includeWanderingBoss, double wanderingBossChance, 
            ItemDataBase itemData, bool randomizeDrops, bool factionProtection, bool randomizeDropsNpcBalancePercent, bool randomizeDropsNpcImportantItemMaxPercent, bool randomizeWeaponDropStack, double randomizeWeaponDropStackChanceRate, bool increaseBossAttributes)
        {
            IncludePuppets = includePuppets;
            IncludeCarcass = includeCarcass;
            IncludeReborner = includeReborner;
            IncludeMiniBossStalker = includeMiniBossStalker;
            IncludeMiniBossPuppet = includeMiniBossPuppet;
            IncludeBosses = includeBosses;
            IncludeMiniBossReborner = includeMiniBossReborner;
            IncludeMiniBossCarcass = includeMiniBossCarcass;
            WanderingBoss = includeWanderingBoss;
            EnableDrops = true;
            RandomizeDrops = randomizeDrops;
            FactionProtection = factionProtection;
            RandomizeDropsNpcBalancePercent = randomizeDropsNpcBalancePercent;
            RandomizeDropsNpcImportantItemMaxPercent = randomizeDropsNpcImportantItemMaxPercent;

            WanderingBossChance = Math.Round(wanderingBossChance, 0);

            ItemInfo = itemData;

            enemyPool = new List<string>();
            bossPool = new List<string>();
            wanderingPool = new List<string>();
            bossGuardianPool = new List<string>();
            itemPool = new List<string>();
            weaponPool = new Dictionary<string, string>();
            RandomizeWeaponDropStack = randomizeWeaponDropStack;
            RandomizeWeaponDropStackChanceRate = randomizeWeaponDropStackChanceRate;
            IncreaseBossAttributes = increaseBossAttributes;
        }


        private List<string> ShufflePool(List<string> pool, Random random)//legacy remove Random since its global now L
        {
            return pool.OrderBy(x => random.Next()).ToList();
        }

        private void WriteEnemiesGeneratedToFile(List<string?> enemiesGenerated, string filePath)
        {
            try
            {

                File.WriteAllLines(filePath, enemiesGenerated.Where(e => e != null).Select(e => e.ToString()));
                Debug.WriteLine($"Enemies generated list has been written to: {filePath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An error occurred while writing the file: {ex.Message}");
            }
        }


        List<string> GeneratePool(bool includePuppets, bool includeCarcass, bool includeReborner, bool includeMiniBossStalker, bool includeMiniBossPuppet, bool includeBosses, bool includeMiniBossReborner, bool includeMiniBossCarcass, bool includeWanderingBoss)
        {
            LogGUIMessage("Generating Enemy Pool");

            List<string> pool = new List<string>();

            if (includePuppets)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.Puppet]);
            }
            if (includeCarcass)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.Carcass]);
            }
            if (includeReborner)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.Reborner]);
            }
            if (includeMiniBossStalker)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.MiniBossStalker]);
            }
            if (includeMiniBossPuppet)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.MiniBossPuppet]);
            }
            if (includeBosses)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.Boss]);
            }
            if (includeMiniBossReborner)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.MiniBossReborner]);
            }
            if (includeMiniBossCarcass)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.MiniBossCarcass]);
            }

            return pool;
        }
        List<string> GenerateItemPool(ItemDataBase itemdb, bool dropWeapon, bool weaponsOnly)//CONVERT TO ENUM LATER
        {

            List<string> items = new List<string>();
            

            if (itemdb.Melee != null && weaponsOnly)
            {
                List<string> handles = (List<string>)itemdb.Melee.Where(x => x.Id.StartsWith(GlobalStrings.handleStartWith)).Select(x => x.Id).ToList();
                List<string> blades = (List<string>)itemdb.Melee.Where(x => x.Id.StartsWith(GlobalStrings.bladeStartWith)).Select(x => x.Id).ToList();

                if (handles.Count >= blades.Count)
                {
                    ShufflePool(handles, random);
                    ShufflePool(blades, random);
                    foreach(var handle in handles)
                    {
                        string bladeSelected = blades[random.Next(blades.Count)];
                        if (string.IsNullOrEmpty(bladeSelected)) { LogGUIMessage("WEAPON DROP STACK: NO MORE BLADES ADDING FROM DB"); blades = (List<string>)itemdb.Melee.Where(x => x.Id.StartsWith(GlobalStrings.bladeStartWith)); }
                        weaponPool.Add(handle, bladeSelected);
                        blades.Remove(bladeSelected);
                    }
                }
                return items;
          
            }

            if (itemdb.SlaveArms != null)
                items.AddRange(itemdb.SlaveArms.Select(x => x.Id));

            if (itemdb.Melee != null && dropWeapon)
                items.AddRange(itemdb.Melee.Select(x => x.Id));

            if (itemdb.Frame != null)
                items.AddRange(itemdb.Frame.Select(x => x.Id));

            if (itemdb.General != null)
                items.AddRange(itemdb.General.Select(x => x.Id));

            if (itemdb.QuestItems != null)
                items.AddRange(itemdb.QuestItems.Select(x => x.Id));

            if (itemdb.Materials != null)
                items.AddRange(itemdb.Materials.Select(x => x.Id));

            if (itemdb.Amulets != null)
                items.AddRange(itemdb.Amulets.Select(x => x.Id));

            if (itemdb.Liner != null)
                items.AddRange(itemdb.Liner.Select(x => x.Id));

            if (itemdb.Converter != null)
                items.AddRange(itemdb.Converter.Select(x => x.Id));

            if (itemdb.Cartridge != null)
                items.AddRange(itemdb.Cartridge.Select(x => x.Id));

            if (itemdb.BuffConsumables != null)
                items.AddRange(itemdb.BuffConsumables.Select(x => x.Id));

            if (itemdb.ThrownConsumables != null)
                items.AddRange(itemdb.ThrownConsumables.Select(x => x.Id));

            if (itemdb.GenericHardErgo != null)
                items.AddRange(itemdb.GenericHardErgo.Select(x => x.Id));

            //if (itemdb.BossHardErgo != null)
            //    items.AddRange(itemdb.BossHardErgo.Select(x => x.Id));

            if (itemdb.Wishstones != null)
                items.AddRange(itemdb.Wishstones.Select(x => x.Id));

            if (itemdb.GrinderBuffs != null)
                items.AddRange(itemdb.GrinderBuffs.Select(x => x.Id));

            if (itemdb.GoldTreeBoosters != null)
                items.AddRange(itemdb.GoldTreeBoosters.Select(x => x.Id));

            if (itemdb.SupplyBoxes != null)
                items.AddRange(itemdb.SupplyBoxes.Select(x => x.Id));

            if (itemdb.VenigniCollections != null)
                items.AddRange(itemdb.VenigniCollections.Select(x => x.Id));

            if (itemdb.Gestures != null)
                items.AddRange(itemdb.Gestures.Select(x => x.Id));

            if (itemdb.Records != null)
                items.AddRange(itemdb.Records.Select(x => x.Id));

            if (itemdb.OtherCollectibles != null)
                items.AddRange(itemdb.OtherCollectibles.Select(x => x.Id));

            if (itemdb.Body != null)
                items.AddRange(itemdb.Body.Select(x => x.Id));

            if (itemdb.Masks != null)
                items.AddRange(itemdb.Masks.Select(x => x.Id));

            if (itemdb.HeadItems != null)
                items.AddRange(itemdb.HeadItems.Select(x => x.Id));

            return items;
        }

        public async Task<bool> RandomizeEnemies(int seed, bool chp1BossSkip)
        {


            try
            {
                Seed = seed;

                random = new Random(Seed);
                enemyPool = ShufflePool(GeneratePool(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, IncludeBosses, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss), random);
                bossPool = ShufflePool(GeneratePool(false, false, false, false, false, true, false, false, false), random);
                wanderingPool = ShufflePool(GeneratePool(false, false, false, false, true, true, true, true, true), random);
                bossGuardianPool = ShufflePool(GeneratePool(false, false, false, false, false, true, false, false, false), random);


                //COPY DATA TO TEMP FOLDER (PAK, MAPPINGS, UNREALPAK)
                string tempPath = Path.GetTempPath();
                FileHandler fileHandler = new FileHandler(tempPath);


                //GET ALL UNMODIFIED LEVEL FILES FROM TEMP
                string[]? pakChunksOriginal = await fileHandler.GenerateBaseTempFiles();
                if (pakChunksOriginal == null) { return false; }

                string? mappingPath = Directory.GetFiles(fileHandler.tempPath, GlobalStrings.mappingsFileNameWithExt, SearchOption.AllDirectories).FirstOrDefault();
                if (mappingPath == null) { return false; }

                //Usmap mapping = new Usmap(Directory.GetFiles(fileHandler.tempPath, "mappingss.usmap", SearchOption.AllDirectories).FirstOrDefault());

                Usmap mapping = new Usmap(mappingPath);

                string pChunk;

                string? npcInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "NPCInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();
                if (npcInfoAsset == null) { return false; }


                string? itemPackageInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "ItemPackageInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();
                string? itemDropInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "ItemDropInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();
                string? dialogMonsterInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[0]), GlobalStrings.dialogMonsterMonologueInfoUasset, SearchOption.AllDirectories).FirstOrDefault();

                //TEST
                //string? skillInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "SkillInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();

                string assetName;
                UAsset myAsset;


                UAsset npcInfo = new UAsset(npcInfoAsset, EngineVersion.VER_UE4_27, mapping);
                UAsset itemPackageInfo = new UAsset(itemPackageInfoAsset, EngineVersion.VER_UE4_27, mapping);
                UAsset itemDropInfo = new UAsset(itemDropInfoAsset, EngineVersion.VER_UE4_27, mapping);
                UAsset dialogMonsterInfo = new UAsset(dialogMonsterInfoAsset, EngineVersion.VER_UE4_27, mapping);
                //TEST
                //UAsset skillInfo = new UAsset(skillInfoAsset, EngineVersion.VER_UE4_27, mapping);

                List<NormalExport> npc;
                List<NpcData.NpcSpotData> importantNpcs;
                Dictionary<string, string> allBossAssignments = new Dictionary<string, string>();//BOSS TRACKING FOR SCALING PURPOSES (ORIGINAL BOSS AND NEW BOSS)

                //TEST
                //SetNpcSkillLinkInfo(skillInfoAsset, skillInfo, mapping, EngineVersion.VER_UE4_27);

                //SET FACTION
                SetNpcInfo(npcInfoAsset, npcInfo, mapping, EngineVersion.VER_UE4_27, NpcData.GetAllMapNpcSpotData(), true, NpcData.FactionType.E_MONSTER_CARCASSNPUPPET, FactionProtection, false, allBossAssignments);

                HashSet<string> alreadySpawnedNpcs = new HashSet<string>();
                HashSet<string> dontReshuffleNpcs = new HashSet<string>();

                for (int i = 0; i < pakChunksOriginal.Length; i++)
                {
                    string umap = Path.GetFileName(pakChunksOriginal[i]);
                    umap = umap.Substring(0, umap.IndexOf(GlobalStrings.umapFileIndex));
                    Dictionary<string, string> mapBossAssignments = new Dictionary<string, string>();

                    //DISGUSTING (BUT WAS IN A HURRY FOR TESTING, CONVERT TO DICTIONARY)
                    switch (umap)
                    {
                        case nameof(MapName.LD_Outer_Station_DSN):
                            //lvlAsset = new UAsset(pakChunksOriginal[i], EngineVersion.VER_UE4_27, mapping);
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLDOuterStation;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLD)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LD_Outer_Station_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);
                            break;
                        case nameof(MapName.LV_Inner_UpperStreet_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVInnerUpperStreet;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Inner_UpperStreet_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs,dontReshuffleNpcs);

                            break;
                        case nameof(MapName.LV_Inner_Factory_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVInnerFactory;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Inner_Factory_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);

                            break;
                        case nameof(MapName.LV_Inner_Cathedral_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVInnerCathedral;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Inner_Cathedral_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs,dontReshuffleNpcs);

                            break;
                        case nameof(MapName.LV_Outer_Underdark_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVOuterUnderdark;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_Underdark_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);

                            break;
                        case nameof(MapName.LV_Krat_EastEndWard_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVKratEastEndWard;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Krat_EastEndWard_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);

                            break;
                        case nameof(MapName.LV_Krat_Old_Town_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVKratOldTown;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Krat_Old_Town_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);

                            break;
                        case nameof(MapName.LV_Outer_Grave_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVOuterGrave;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_Grave_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);
                            break;
                        case nameof(MapName.LV_Monastery_A_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVMonasteryA;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Monastery_A_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i],alreadySpawnedNpcs, dontReshuffleNpcs);
                            break;
                        case nameof(MapName.LV_Monastery_B_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVMonasteryB;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Monastery_B_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);
                            break;
                        case nameof(MapName.LV_Outer_CentralStatinB_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVOuterCentralStatinB;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_CentralStatinB_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);
                            break;
                        case nameof(MapName.LV_Outer_Exhibition_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVOuterExhibition;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith(GlobalStrings.NpcLV)).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_Exhibition_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i], alreadySpawnedNpcs, dontReshuffleNpcs);
                            break;
                    }
                    alreadySpawnedNpcs.Clear();
                    foreach (var entry in mapBossAssignments)
                    {
                        if (!allBossAssignments.ContainsKey(entry.Key))
                        {
                            allBossAssignments[entry.Key] = entry.Value;
                        }
                    }
                }
                dontReshuffleNpcs.Clear();

                //Set ERGO XP and Scale Bosses
                SetNpcInfo(npcInfoAsset, npcInfo, mapping, EngineVersion.VER_UE4_27, NpcData.GetAllMapNpcSpotData(), true, NpcData.FactionType.E_MONSTER_CARCASSNPUPPET, false, true, allBossAssignments);
                //I.E ENSURES BOSS DIALOG PLAYS OUTSIDE ORIGINAL ARENA (I.E VICTOR - YOU'RE SO WEAK SO PATHETIC lol)
                //SetNpcDialogue(dialogMonsterInfoAsset, dialogMonsterInfo, mapping, EngineVersion.VER_UE4_27, NpcData.GetAllMapNpcSpotData(), allBossAssignments);

                //SetNpcSkillLinkInfo(npcInfoAsset, npcInfo, mapping, EngineVersion.VER_UE4_27);

                if (RandomizeDrops)
                {
                    //TODO: QUICK TEST CODE FOR RELEASE - START REFACTORING ONCE RELEASED AND FIX BUGS
                    //ITEM WORLD
                    LogGUIMessage("Randomizing World Item Drops");
                    RandomizeItems(itemPackageInfoAsset, itemPackageInfo, mapping, EngineVersion.VER_UE4_27, ItemInfo, true);
                    //NPC ITEMS
                    LogGUIMessage("Randomizing NPC Item Drops");
                    RandomizeItemsNpc(itemDropInfoAsset, itemDropInfo, mapping, EngineVersion.VER_UE4_27, ItemInfo, false);
                }


                bool result = await fileHandler.UnrealPak(fileHandler.pakBaseDirectory, GlobalStrings.generatedFileDirectory);
                if (!result)
                {
                    Debug.WriteLine($"Randomize: failed to copy randomized files to directory");
                    LogGUIMessage("Randomize: failed to copy randomized files to directory");
                    return false;
                }

                fileHandler.OpenDirectory(GlobalStrings.generatedFileDirectory);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Randomize:", ex.Message);
                LogGUIMessage(ex.Message);
                return false;
            }

            return true;

        }

        Dictionary<string, string>? GetBladeAndHandle(string weaponName, string originalItemName, bool sameHandleAndBlade)
        {
            if (string.IsNullOrEmpty(weaponName)) { LogGUIMessage("Could not randomize a weapon blande and handle"); return null; }

            string? handle = null;
            string? blade = null;


            if (originalItemName.Contains(GlobalStrings.handleStartWith))
            {
                handle = originalItemName;
                var blades = itemPool.Where(x => x.StartsWith(GlobalStrings.bladeStartWith, StringComparison.OrdinalIgnoreCase)).ToList();
                if (blades.Any()) { blade = blades[random.Next(blades.Count)]; }
            }
            else
            {
                blade = originalItemName;
                var handles = itemPool.Where(x => x.StartsWith(GlobalStrings.handleStartWith, StringComparison.OrdinalIgnoreCase)).ToList();
                if (handles.Any()) { handle = handles[random.Next(handles.Count)]; }
            }
        

                                                                         
            if (string.IsNullOrEmpty(handle) || string.IsNullOrEmpty(blade)) { LogGUIMessage("Could not randomize a weapon blande and handle"); return null; }

            return new Dictionary<string, string> {
                { "Handle", handle },  
                { "Blade", blade } 
            };
        }
       


        //QUICK UGLY DIRTY WILL FIX(OLD RANDOMIZER)
        void RandomizeItems(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion, ItemDataBase itemData, bool includeWeapons)
        {
            itemPool = ShufflePool(GenerateItemPool(ItemInfo, true, false), random);
            HashSet<string> alreadySpawnedItems = new HashSet<string>();

            NormalExport? itemPackageInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            List<PropertyData>? itemProperties = itemPackageInfoTable?.Data.Count > 0 ? itemPackageInfoTable[0].RawValue as List<PropertyData> : null;
            ArrayPropertyData? arrayPropertyDatas = (ArrayPropertyData)itemProperties.Where(x => x.Name.Value.Equals("_ItemPackage_array")).FirstOrDefault();
            PropertyData[] structPropertyData = (PropertyData[])arrayPropertyDatas.RawValue;
            int quartzTotal = 0;
         ;

            for (int i = 0; i < structPropertyData.Length; i++)
            {
                if (itemPool.Count <= 0) { itemPool = ShufflePool(GenerateItemPool(ItemInfo, true, false), random); }
                List<PropertyData> test = (List<PropertyData>)structPropertyData[i].RawValue;
                var slot1 = test.Where(x => x.Name.Value.Equals("_Item_1_code_name")).FirstOrDefault() as PropertyData;
                var slot1Count = test.Where(x => x.Name.Value.Equals("_Item_1_count")).FirstOrDefault() as PropertyData;
                var slot2 = test.Where(x => x.Name.Value.Equals("_Item_2_code_name")).FirstOrDefault() as PropertyData;
                var slot2Count = test.Where(x => x.Name.Value.Equals("_Item_2_count")).FirstOrDefault() as PropertyData;
                var slot3 = test.Where(x => x.Name.Value.Equals("_Item_3_code_name")).FirstOrDefault() as PropertyData;
                var slot3Count = test.Where(x => x.Name.Value.Equals("_Item_3_count")).FirstOrDefault() as PropertyData;

                var slot4 = test.Where(x => x.Name.Value.Equals("_Item_4_code_name")).FirstOrDefault() as PropertyData;
                var slot4Count = test.Where(x => x.Name.Value.Equals("_Item_4_count")).FirstOrDefault() as PropertyData;

                var slot5 = test.Where(x => x.Name.Value.Equals("_Item_5_code_name")).FirstOrDefault() as PropertyData;
                var slot5Count = test.Where(x => x.Name.Value.Equals("_Item_5_count")).FirstOrDefault() as PropertyData;

                var slot6 = test.Where(x => x.Name.Value.Equals("_Item_6_code_name")).FirstOrDefault() as PropertyData;
                var slot6Count = test.Where(x => x.Name.Value.Equals("_Item_6_count")).FirstOrDefault() as PropertyData;

                var handle = test.Where(x => x.Name.Value.Equals("_weapon_item_1_handle")).FirstOrDefault() as PropertyData;
                var blade = test.Where(x => x.Name.Value.Equals("_weapon_item_1_blade")).FirstOrDefault() as PropertyData;

                string item = itemPool[random.Next(itemPool.Count)];
                bool alreadySwappedWeapons = false;

                if (item.StartsWith(GlobalStrings.wpPC))
                {
                    string weaponName = item.Substring(10);
                    //if (item.Contains(GlobalStrings.handleStartWith))
                    //{
                    //    weaponName = GlobalStrings.bladeStartWith + weaponName;
                    //    handle.RawValue = FName.FromString(uasset, item);
                    //    blade.RawValue = FName.FromString(uasset, weaponName);
                    //    //weaponName = GlobalStrings.bladeStartWith;
                    //}
                    //else
                    //{
                    //    weaponName = GlobalStrings.handleStartWith + weaponName;
                    //    blade.RawValue = FName.FromString(uasset, item);
                    //    handle.RawValue = FName.FromString(uasset, weaponName);


                    //}
                    Dictionary<string, string>? weaponPairSelected = GetBladeAndHandle(weaponName, item, true);
                    if(weaponPairSelected != null)
                    {
                        itemPool.Remove(weaponPairSelected["Handle"]);
                        itemPool.Remove(weaponPairSelected["Blade"]);
                        alreadySpawnedItems.Add(weaponPairSelected["Handle"]);
                        alreadySpawnedItems.Add(weaponPairSelected["Blade"]);
                        Debug.WriteLine("HANDLE:" + weaponPairSelected["Handle"] + " BLADE:" + weaponPairSelected["Blade"]);
                        blade.RawValue = FName.FromString(uasset, weaponPairSelected["Blade"]);
                        handle.RawValue = FName.FromString(uasset,weaponPairSelected["Handle"]);
                        alreadySwappedWeapons = true;

                    }
                    if (itemPool.Contains(item)){ itemPool.Remove(item); }              

                    alreadySpawnedItems.Add(item);

                    if (itemPool.Count <= 0) { itemPool = ShufflePool(GenerateItemPool(ItemInfo, true, false), random); }
                    item = itemPool[random.Next(itemPool.Count)];
                    if (item.StartsWith(GlobalStrings.wpPC)) { continue; }

                }

                if (RandomizeWeaponDropStack && !alreadySwappedWeapons && (random.Next(100) < RandomizeWeaponDropStackChanceRate))
                {
                    if (weaponPool?.Keys?.Count <= 0 || weaponPool?.Keys == null) { GenerateItemPool(ItemInfo, false, true); }

                    List<string> handles = weaponPool.Keys.ToList();
                    string? handleSelected = handles[random.Next(handles.Count)];

                    if (!(string.IsNullOrEmpty(handleSelected)) && weaponPool.TryGetValue(handleSelected, out var bladeSelected))
                    {
                        handle.RawValue = FName.FromString(uasset, handleSelected);
                        blade.RawValue = FName.FromString(uasset, bladeSelected);
                        weaponPool.Remove(handleSelected);
                        LogGUIMessage("WORLD WEAPON CHANCE:" +  handleSelected + bladeSelected);
                        Debug.WriteLine("WORLD WEAPON CHANCE:" + handleSelected + bladeSelected);
                    }
                }
                    
                

                if (slot1?.RawValue == null) { continue; }

                if ((slot1.RawValue.ToString().Contains(GlobalStrings.dropKey, StringComparison.OrdinalIgnoreCase) || slot1.RawValue.ToString().Contains(GlobalStrings.dropKey, StringComparison.OrdinalIgnoreCase))
                && !slot1.RawValue.ToString().Equals(GlobalStrings.dropKey1, StringComparison.OrdinalIgnoreCase))
                {
                    slot6.RawValue = FName.FromString(uasset, slot1.RawValue.ToString());
                    slot6Count.RawValue = 1;
                }




                slot1.RawValue = FName.FromString(uasset, item);
                slot1Count.RawValue = 1;
                itemPool.Remove(item);
                alreadySpawnedItems.Add(item);

                if (quartzTotal < 11 && random.Next(100) < 10)
                {
                    item = GlobalStrings.quartz;
                    quartzTotal++;
                    slot3.RawValue = FName.FromString(uasset, item);
                    slot3Count.RawValue = 1;
                    Debug.WriteLine("WORLD QUARTZ");

                }


                string? materialDrop = RollDropTablePercent(DropTables.MaterialDropTablePercent, 0);
                if (string.IsNullOrEmpty(materialDrop)) { continue; }

                slot4.RawValue = FName.FromString(uasset, materialDrop);
                slot4Count.RawValue = random.Next(1, 2);
                Debug.WriteLine("WORLD DROP:" + materialDrop);





            }

            uasset.Write(filePath);



        }

        void RandomizeItemsNpc(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion, ItemDataBase itemData, bool includeWeapon)
        {
            NormalExport? itemPackageInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            List<PropertyData>? itemProperties = itemPackageInfoTable?.Data.Count > 0 ? itemPackageInfoTable[0].RawValue as List<PropertyData> : null;
            ArrayPropertyData? arrayPropertyDatas = (ArrayPropertyData)itemProperties.Where(x => x.Name.Value.Equals(nameof(AssetTableNames._PackageConfigureInfo_array))).FirstOrDefault();
            PropertyData[] structPropertyData = (PropertyData[])arrayPropertyDatas.RawValue;
            int quartzTotal = 0;

            for (int i = 0; i < structPropertyData.Length; i++)
            {
                if (itemPool.Count <= 0) { itemPool = ShufflePool(GenerateItemPool(ItemInfo, true, false), random); }
                List<PropertyData> test = (List<PropertyData>)structPropertyData[i].RawValue;
                var itemCodeName = test.Where(x => x.Name.Value.Equals("_item_code_name")).FirstOrDefault() as PropertyData;
                var percent = test.Where(x => x.Name.Value.Equals("_item_acquisition_percentage")).FirstOrDefault() as PropertyData;

                if (itemCodeName?.RawValue == null) { continue; }
                string item;

                if ((itemCodeName.RawValue.ToString().Contains(GlobalStrings.dropEpic, StringComparison.OrdinalIgnoreCase) || itemCodeName.RawValue.ToString().Contains(GlobalStrings.dropKey, StringComparison.OrdinalIgnoreCase))
                && !itemCodeName.RawValue.ToString().Equals(GlobalStrings.dropKey1, StringComparison.OrdinalIgnoreCase) && !itemCodeName.RawValue.ToString().Equals(GlobalStrings.dropKeySlumHouse, StringComparison.OrdinalIgnoreCase))
                {
                    itemPool.Remove(itemCodeName.RawValue.ToString());
                    continue;
                }
                //QUICK FIX
                if(itemCodeName.RawValue.ToString().Contains(GlobalStrings.bossErgo, StringComparison.OrdinalIgnoreCase))
                {
                    itemPool.Remove(itemCodeName.RawValue.ToString());
                    continue;
                }

                item = itemPool[random.Next(itemPool.Count)];


                itemCodeName.RawValue = FName.FromString(uasset, item);
                itemPool.Remove(item);

                if(percent == null) { continue; }

                int importantDropPercent = GetDropRatePercent(DropTables.ImportantDropItemPercent, item, true);
                
                if (importantDropPercent > 0 && RandomizeDropsNpcImportantItemMaxPercent)
                {
                    percent.RawValue = 100;
                    Debug.WriteLine($"MAX RATE:{item} {percent.RawValue}");
                    continue;
                }

                if (!RandomizeDropsNpcBalancePercent) { continue; }

                importantDropPercent = GetDropRatePercent(DropTables.ItemDropPercentReductions, item, true);

                if(importantDropPercent > 0 && IsNumber(percent.RawValue.ToString(), (importantDropPercent + 1)))
                {
                  
                    if((int)percent.RawValue < 70) { continue; }
                    percent.RawValue = (int)percent.RawValue - importantDropPercent;
                    Debug.WriteLine($"REDUCING RATE: {item} {percent.RawValue}");
                    continue;
                }

                if (quartzTotal < 15 && random.Next(100) < 10)
                {
                    item = GlobalStrings.quartz;
                    quartzTotal++;
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    Debug.WriteLine("NPC DROP: QUARTZ");

                    continue;
                }

                string? materialDrop = RollDropTablePercent(DropTables.MaterialDropTablePercent, 0);

                if (!(string.IsNullOrEmpty(materialDrop)))
                {
                    itemCodeName.RawValue = FName.FromString(uasset, materialDrop);
                    Debug.WriteLine("NPC DROP:" + materialDrop);
                    continue;
                }

            }
            uasset.Write(filePath);
        }

        int GetDropRatePercent(Dictionary<string, int> table, string item, bool startsWith)
        {
            if (string.IsNullOrEmpty(item)) { return 0; }

            if (startsWith)
            {
                string? matchingKey = table.Keys.FirstOrDefault(key => item.StartsWith(key, StringComparison.OrdinalIgnoreCase));
                if (matchingKey == null) { return 0; }
                return table[matchingKey];
            }
            return table.TryGetValue(item, out int value) ? value : 0;
        }



        string RollDropTablePercent(Dictionary<string, int> dropTable, int min)
        {
            int totalWeight = dropTable.Values.Sum();
            int roll = random.Next(min, totalWeight);
            Debug.WriteLine($"Random Roll: {roll}/{totalWeight}");

            int cumulativeWeight = 0;
            foreach (var kvp in dropTable)
            {
                cumulativeWeight += kvp.Value;
                if (roll < cumulativeWeight)
                {
                    Debug.WriteLine($"Selected Item: {kvp.Key} (Cumulative: {cumulativeWeight})");
                    return kvp.Key;
                }
            }
            return string.Empty;
        }

        //SHOULD of just made a single function to handle multiple files with params to start but now cba to refactor - too much work fml
        bool SetNpcDialogue(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion, List<NpcData.NpcSpotData> spotData, Dictionary<string, string> bossAssignments)
        {
            if (filePath == null || uasset == null || mapping == null) { return false; }

            NormalExport? statInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            statInfoTable = statInfoTable?.Asset.Exports.Count > 0 ? statInfoTable.Asset.Exports[0] as NormalExport : null;
            List<PropertyData>? propData = statInfoTable?.Data.Count > 0 ? statInfoTable?.Data[0].RawValue as List<PropertyData> : null;

            if (propData == null) { return false; }

           // ArrayPropertyData? npcInfo = (ArrayPropertyData?)propData.FirstOrDefault(x => x.Name.ToString().Contains(nameof(AssetTableNames._DialogMonsterMonologueInfo_array), StringComparison.OrdinalIgnoreCase));
            ArrayPropertyData? npcInfoArray = (ArrayPropertyData?)propData.Where(x => x.Name.ToString().Contains(nameof(AssetTableNames._DialogMonsterMonologue_array), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();


            List<PropertyData>? structPropertyData;

            Debug.WriteLine($"Setting Dialogue");
            structPropertyData = npcInfoArray?.Value.Where(x => x != null).ToList();

            foreach (var assignment in bossAssignments)
            {
                string originalBoss = assignment.Key;
                string? newBoss = spotData.Where(x => x.spotCodeNameOriginal.ToString().Equals(assignment.Value, StringComparison.OrdinalIgnoreCase)).Select(x => x.spotUniqueID).FirstOrDefault(); //assignment.Value; 
                if (string.IsNullOrEmpty(newBoss)) { LogGUIMessage("DIALOUGE: COULD NOT LOCATE NEW BOSS SPOT UNIQUE ID"); continue; }

                Debug.WriteLine($"Swapping dialogue from: {originalBoss} -> {newBoss}");
                LogGUIMessage("Swapping dialogue from:" + originalBoss + "->" + newBoss);
                foreach (var tableName in new[]
                {
                    AssetTableNames._spot_unique_id,
                    AssetTableNames._param1

                })
                {
                    SetRawValue(filePath, uasset, structPropertyData, tableName, newBoss, originalBoss, spotData, UassetFileTye.DialogMonsterMonologueInfo);
                }
                
            }

            uasset.Write(filePath);
            return true;
        }


        bool SetNpcInfo(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion, List<NpcData.NpcSpotData> spotData, bool setExpDrop, NpcData.FactionType faction, bool setFaction, bool scaleBosses, Dictionary<string, string> bossAssignments)
        {
            if (filePath == null || uasset == null || mapping == null) { return false; }

            NormalExport? statInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            statInfoTable = statInfoTable?.Asset.Exports.Count > 0 ? statInfoTable.Asset.Exports[0] as NormalExport : null;
            List<PropertyData>? propData = statInfoTable?.Data.Count > 0 ? statInfoTable?.Data[0].RawValue as List<PropertyData> : null;

            if (propData == null) { return false; }

            ArrayPropertyData? npcInfo = (ArrayPropertyData?)propData.FirstOrDefault(x => x.Name.ToString().Contains(nameof(AssetTableNames._NpcInfo), StringComparison.OrdinalIgnoreCase));
            ArrayPropertyData? npcStatInfo = (ArrayPropertyData?)propData.FirstOrDefault(x => x.Name.ToString().Contains(nameof(AssetTableNames._NpcStatInfo), StringComparison.OrdinalIgnoreCase));

            ArrayPropertyData? npcInfoArray = (ArrayPropertyData?)propData.Where(x => x.Name.ToString().Contains(nameof(AssetTableNames._NpcInfo), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            ArrayPropertyData? npcStatInfoArray = (ArrayPropertyData?)propData.Where(x => x.Name.ToString().Contains(nameof(AssetTableNames._NpcStatInfo), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

            List<PropertyData>? structPropertyData;

            if (setFaction && npcInfoArray != null)
            {
                Debug.WriteLine($"Setting faction");
                LogGUIMessage("Assigning factions" + nameof(NpcData.FactionType.E_MONSTER_CARCASSNPUPPET));

                structPropertyData = npcInfoArray.Value.Where(x => x != null).ToList();
                SetRawValue2(filePath, uasset, structPropertyData, AssetTableNames._Faction, nameof(NpcData.FactionType.E_MONSTER_CARCASSNPUPPET), null);
            }

      
            if (setExpDrop && npcStatInfoArray != null)
            {
                Debug.WriteLine($"Setting exp");
                LogGUIMessage("Patching Phase 1 ergo drops");
                structPropertyData = npcStatInfoArray.Value.Where(x => x != null).ToList();
                SetRawValue2(filePath, uasset, structPropertyData, AssetTableNames._Exp, null, NpcData.GetAllMapNpcSpotData());
            }

         
            if (ScaleBosses && npcStatInfoArray != null && scaleBosses)
            {
                Debug.WriteLine($"Setting Scale");
                structPropertyData = npcStatInfoArray.Value.Where(x => x != null).ToList();

                foreach (var assignment in bossAssignments)
                {
                    string originalBoss = assignment.Key;
                    string newBoss = assignment.Value;

                    Debug.WriteLine($"Scaling stats for: {originalBoss} -> {newBoss}");
                    LogGUIMessage("Scaling stats from:" + originalBoss + "->" + newBoss);
                    foreach (var tableName in new[]
                    {
                        AssetTableNames._health_power,
                        AssetTableNames._physical_power,
                        AssetTableNames._physical_defence,
                        AssetTableNames._physical_slash_defence,
                        AssetTableNames._physical_strike_defence,
                        AssetTableNames._physical_pierce_defence,
                        AssetTableNames._physical_reduce,
                        AssetTableNames._physical_slash_reduce,
                        AssetTableNames._physical_strike_reduce,
                        AssetTableNames._physical_pierce_reduce,
                        AssetTableNames._tough,
                        AssetTableNames._tough_restore_base,
                        AssetTableNames._tough_attack_power_base,
                        AssetTableNames._tough_defence_power_base,
                        AssetTableNames._guard_stamina_damage,
                        
                    })
                    {
                        SetRawValue(filePath, uasset, structPropertyData, tableName, newBoss, originalBoss, spotData, UassetFileTye.NPCInfo);
                    }
                }
            }

            uasset.Write(filePath);
            return true;
        }
        PropertyData? GetCodeNameProperty(List<PropertyData> propertyData, int hash)
        {
            foreach(var p in propertyData)
            {
                if(p.Name.Value.GetHashCode() == hash) {  return p; }
            }
            return null;
        }
        void SetRawValue(string? filePath, UAsset? uasset, List<PropertyData>? propertyData, AssetTableNames tableName, string? bossSelected, string? originalBossID, List<NpcSpotData>? allNpcSpotMapData, UassetFileTye fileType)
        {
            if (propertyData == null || filePath == null || uasset == null || bossSelected == null || originalBossID == null)
            {
                Debug.WriteLine($"SetRawValue: invalid input");
                return;
            }

            string logPath = @"C:\loprandoalpha\BossStatsLog.txt";
            string logEntry = "";
           
            //int codeNameHash = nameof(AssetTableNames._code_name).GetHashCode();
            //int spotUniqueIdHash = nameof(AssetTableNames._spot_unique_id).GetHashCode();
            //int bossSelectedHash = bossSelected.GetHashCode();

            var originalBoss = allNpcSpotMapData?.FirstOrDefault(x =>
              x.spotUniqueID.Equals(originalBossID, StringComparison.OrdinalIgnoreCase) &&
              x.npcType == NpcType.Boss &&
              x.npcImportant != true);

            if (originalBoss == null)
            {
                Debug.WriteLine($"Original boss not found: {originalBossID}");
                return;
            }

            for (int i = 0; i < propertyData.Count; i++)
            {
                var npcdata = propertyData[i]?.RawValue as List<PropertyData>;
                if (npcdata == null) { continue; }

                //LINQ IS TOO SLOW FOR THIS OPERATION, SWITCHING TO FOREACH
                PropertyData? codeNameProperty = fileType switch
                {
                    UassetFileTye.NPCInfo => npcdata.FirstOrDefault(x => x.Name.Value.Value.Equals(nameof(AssetTableNames._code_name))),
                    UassetFileTye.DialogMonsterMonologueInfo => npcdata.FirstOrDefault(x => x.Name.Value.Value.Equals(nameof(AssetTableNames._spot_unique_id))),
                    _ => null
                }; ;

                if(codeNameProperty == null) { continue; }

                if (fileType == UassetFileTye.NPCInfo && codeNameProperty.RawValue?.ToString()?.Equals(bossSelected) != true) { continue; }
                if (fileType == UassetFileTye.DialogMonsterMonologueInfo && codeNameProperty.RawValue?.ToString()?.Equals(originalBossID) != true) { continue; }

            

                //if (originalBoss == null)
                //{
                //    Debug.WriteLine($"Original boss not found: {originalBossID}");
                //    continue;
                //}



                // Find attribute to update
                var attribute = npcdata.FirstOrDefault(x => x.Name.Value.ToString().Equals(tableName.ToString(), StringComparison.OrdinalIgnoreCase));
                if (attribute == null) { continue; }
                int valueToAssign = int.MinValue;
                string? dialogeToAssign;
                

                switch (fileType)
                {
                    case UassetFileTye.NPCInfo:
                        valueToAssign = tableName switch
                        {
                            AssetTableNames._health_power => originalBoss.Value.healthPower,
                            AssetTableNames._physical_power => originalBoss.Value.physicalPower,
                            AssetTableNames._physical_defence => originalBoss.Value.physicalDefence,
                            AssetTableNames._physical_slash_defence => originalBoss.Value.physicalSlashDefence,
                            AssetTableNames._physical_strike_defence => originalBoss.Value.physicalStrikeDefence,
                            AssetTableNames._physical_pierce_defence => originalBoss.Value.physicalPierceDefence,
                            AssetTableNames._physical_reduce => originalBoss.Value.physicalReduce,
                            AssetTableNames._physical_slash_reduce => originalBoss.Value.physicalSlashReduce,
                            AssetTableNames._physical_strike_reduce => originalBoss.Value.physicalStrikeReduce,
                            AssetTableNames._physical_pierce_reduce => originalBoss.Value.physicalPierceReduce,
                            AssetTableNames._tough => originalBoss.Value.tough,
                            AssetTableNames._tough_restore_base => originalBoss.Value.toughRestoreBase,
                            AssetTableNames._tough_attack_power_base => originalBoss.Value.toughAttackPowerBase,
                            AssetTableNames._tough_defence_power_base => originalBoss.Value.toughDefencePowerBase,
                            AssetTableNames._guard_stamina_damage => originalBoss.Value.guardStaminaDamage,
                            _ => int.MinValue


                        };
                        if (valueToAssign != int.MinValue)
                        {
                            if(IncreaseBossAttributes && valueToAssign > 0)
                            {
                                valueToAssign = ScaleValue(valueToAssign, 40.00, false);
                            }
                            attribute.RawValue = valueToAssign;



                            logEntry += $"Original Boss: {originalBoss.Value.spotCodeNameOriginal}\n" +
                                         $"Stat: {tableName}\n" +
                                         $"Transferred Value: {valueToAssign}\n" +
                                         $"----------------------------------\n" +
                                         $"Randomized Boss: {bossSelected}\n";

                        }
                        break;
                    case UassetFileTye.DialogMonsterMonologueInfo:
                        dialogeToAssign = tableName switch
                        {
                            AssetTableNames._spot_unique_id => bossSelected,
                            AssetTableNames._param1 => bossSelected,
                            _ => null


                        } ;
                        if (!(string.IsNullOrEmpty(dialogeToAssign)))
                        {


                            var attType = attribute.GetType();
                            switch (attribute.PropertyType.Value)
                            {
                                case nameof(PropertyTypes.NameProperty):
                                    attribute.RawValue = FName.FromString(uasset,dialogeToAssign);
                                    break;
                                case nameof(PropertyTypes.StrProperty):
                                    attribute.RawValue = FString.FromString(dialogeToAssign);
                                    break;
                            }



                            logEntry += $"Original Boss: {originalBoss.Value.spotCodeNameOriginal}\n" +
                                       $"Original Dialoge: {originalBossID}\n" +
                                         $"Table: {tableName}\n" +
                                         $"Transferred Value: {dialogeToAssign}\n" +
                                         $"----------------------------------\n" +
                                         $"Randomized Boss: {bossSelected}\n";
                        }

                        
                        break;

                }
            }
            Debug.WriteLine(logEntry);
        }
               


      



        //OLD CODE LOL
        void SetRawValue2(string? filePath, UAsset? uasset, List<PropertyData>? propertyData, AssetTableNames tableName, string? value, List<NpcSpotData>? allNpcSpotMapData)
        {
            if (propertyData == null || filePath == null || uasset == null) { Debug.WriteLine($"SetRawValue: invalid propertydata, filepath, or uasset"); return; };
            //Will make it cleaner soon TM
            for (int i = 0; i < propertyData.Count; i++)
            {
                List<PropertyData>? npcdata = (List<PropertyData>)propertyData[i].RawValue;
                if (npcdata == null) { continue; }
                PropertyData? attribute = null;
                switch (tableName)
                {
                    //REFACTOR LATE WAS IN HURRY FOR TESTING THIS IS SO FUCKING NASTY LOL
                    case AssetTableNames._Faction when !(string.IsNullOrEmpty(value)):
                        PropertyData? codename = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Code_Name), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (codename?.RawValue == null) { break; }

                        //if (blackRabbitCodeNames.Any(codename.RawValue.ToString().Contains))
                        //{
                        //    Debug.WriteLine($"Skipping Faction BLACK RABBIT", codename.RawValue.ToString());
                        //    LogGUIMessage("Skipping Faction Assignment:" + codename.RawValue.ToString());
                        //    break;
                        //}

                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._grade), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        if (attribute.RawValue.ToString().Equals("E_BOSS"))
                        {
                            Debug.WriteLine($"Skipping Faction BOSS");
                            LogGUIMessage("Skipping Faction Assignment For Boss");
                            break;
                        }
                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Code_Name), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        if (attribute.RawValue.ToString().Contains("HelpMate"))
                        {
                            Debug.WriteLine("Skipping Faction:" + attribute.RawValue);
                            LogGUIMessage("Skipping Faction:" + attribute.RawValue);
                            break;
                        }

                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Faction), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        if (attribute.RawValue.ToString().Equals("E_NEUTRAL") && (!codename.RawValue.ToString().ToLower().Contains("stalker")))
                        {
                            Debug.WriteLine($"Skipping Faction NEUTRAL");
                            LogGUIMessage("Skipping Faction NEUTRAL");
                            break;
                        }
                        attribute.RawValue = FName.FromString(uasset, value);
                        Debug.WriteLine($"SetRawValue Faction: {value}");
                        LogGUIMessage("Setting Faction:" + value);
                        break;
                    case AssetTableNames._Exp when allNpcSpotMapData != null:
                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Code_Name), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        int exp = allNpcSpotMapData.FirstOrDefault(x => x.spotCodeNameOriginal.ToString().Contains(attribute.RawValue.ToString(), StringComparison.OrdinalIgnoreCase)).uexp;
                        if (exp <= 0) { break; }
                        attribute = npcdata.Where(x => x.Name.Value.ToString().Equals(nameof(AssetTableNames._Exp), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        attribute.RawValue = exp;
                        Debug.WriteLine($"SetRawValue EXP: {exp}");
                        LogGUIMessage("Setting ERGO XP:" + exp.ToString());
                        break;

                }
            }

        }

        int ScaleValue(int originalValue, double scalePercent, bool scaleDown)
        {
            double scaleFactor;
            if (scaleDown)
            {
                scaleFactor = 1.0 - (scalePercent / 100.0);
            }
            else
            {
               scaleFactor = 1.0 + (scalePercent / 100.0);
            }
           
            int scaledValue = (int)Math.Round(originalValue * scaleFactor);

            //return scaledValue > 100 ? scaledValue : originalValue;
            return scaledValue;

        }


        public bool IsNumber(string? value, int min)
        {
            int number;
            if (int.TryParse(value, out number) && number > min) { return true; }

            return false;

        }







        Dictionary<string, string> GenerateEnemies(string pakChunk, UAsset uAsset, Usmap mapping, EngineVersion engineVersion, List<NpcData.NpcSpotData> importantNpcs, List<NormalExport> npcs,
    bool skipButterfly, bool skipImportantNpcs, bool skipExiledNpc, bool skipProjectile, bool removeNpcFromPool, bool scaleEnemies, bool scaleBosses, string fileName, string filePath, HashSet<string> alreadySpawnedNpcs, HashSet<string> dontReshuffleNpcs)
        {
            npcs = uAsset.Exports.OfType<NormalExport>()
                .Where(x => x.ObjectName.ToString().StartsWith("Npc-LD", StringComparison.OrdinalIgnoreCase) || x.ObjectName.ToString().StartsWith("Npc-LV", StringComparison.OrdinalIgnoreCase) || x.ObjectName.ToString().StartsWith("BossRoom", StringComparison.OrdinalIgnoreCase))
                .ToList();
            Dictionary<string, string> bossAssignmentMap = new Dictionary<string, string>();
            List<string?> enemiesGenerated = new List<string>();

            if (npcs == null) return bossAssignmentMap;

            // Debug logs for initial pool states
            Debug.WriteLine($"Initial enemyPool: {string.Join(", ", enemyPool)}");
            Debug.WriteLine($"Initial bossPool: {string.Join(", ", bossPool)}");
            Debug.WriteLine($"Initial wanderingPool: {string.Join(", ", wanderingPool)}");

            List<NpcData.NpcSpotData> matchingNpcs = importantNpcs.Where(npc => npcs.Any(npcExport => npcExport.ObjectName.ToString().Contains(npc.spotUniqueID))).ToList();
            PropertyData? bossSpot = null;
            PropertyData? bossWorldEventChange = null;





          
            foreach (NormalExport npcExport in npcs)
            {
                string spotName = npcExport.ObjectName.ToString();


                foreach (PropertyData data in npcExport.Data)
                {

                    if (npcExport.ObjectName.Value.ToString().Contains(nameof(AssetTableNames.BossRoom))) { bossSpot = npcExport.Data.Where(x => x.Name.Value.ToString().StartsWith(nameof(AssetTableNames.BossNpcCodeName), StringComparison.OrdinalIgnoreCase)).FirstOrDefault(); continue; }


                    if (data.Name.ToString() != nameof(AssetTableNames.SpotCodeName)) continue;
                    //bossWorldEventChange = npcExport.Data.Where(x => x.Name.Value.ToString().StartsWith("WorldEventCodeName", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();



                    if (enemyPool.Count == 0)
                        enemyPool = ShufflePool(GeneratePool(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, false, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss).Where(x => !alreadySpawnedNpcs.Contains(x)).ToList(), random);
                        for (int i = enemyPool.Count - 1; i >= 0; i--)
                        {
                            if (dontReshuffleNpcs.Contains(enemyPool[i]))
                            {
                                Debug.WriteLine($"Removing: {enemyPool[i]}");
                                enemyPool.RemoveAt(i);
                            }
                        }


                    if (bossPool.Count == 0)
                        bossPool = ShufflePool(GeneratePool(false, false, false, false, false, true, false, false, false), random);

                    if (wanderingPool.Count == 0)
                        wanderingPool = ShufflePool(GeneratePool(false, false, false, false, false, true, true, true, false), random);

                    if (bossGuardianPool.Count == 0)
                        bossGuardianPool = ShufflePool(GeneratePool(false, false, false, false, false, true, false, false, false), random);

                    string bossSelected = bossPool[random.Next(bossPool.Count)];
                    string wanderingSelected = wanderingPool[random.Next(wanderingPool.Count)];
                    int wanderingBossRoll = random.Next(1, 100);
                    string enemySelected = enemyPool[random.Next(enemyPool.Count)];
                    string bossGuardianSelected = bossGuardianPool[random.Next(bossGuardianPool.Count)];

                    bool assignedValue = false;
                    List<NpcData.NpcSpotData> matchesToRemove = new List<NpcData.NpcSpotData>();

                    foreach (var match in matchingNpcs)
                    {
                        if (spotName != match.spotUniqueID.ToString()) { continue; }

                        //if (spotName == "Npc-LV_Outer_Exhibition_DSN-82_6")
                        //{
                        //    Debug.WriteLine(spotName);
                        //}



                        //QUICK DIRTY CHP1 EXILE HELP (REFACTOR LATER)
                        if (spotName == "Npc-LD_Outer_Station_DSN-15" && skipChp1Boss)
                        {
                            data.RawValue = FName.FromString(uAsset, "CH13_HelpMate_Exile");
                            Debug.WriteLine($"Skipping important NPC: {npcExport.ObjectName}");
                            matchesToRemove.Add(match);
                            assignedValue = true;
                            break;
                        }

                        if (match.npcImportant == true && skipImportantNpcs)
                        {
                            data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                            Debug.WriteLine($"Skipping important NPC: {match.spotCodeNameOriginal}");
                            LogGUIMessage("Skipping Important NPC:" + match.spotCodeNameOriginal);
                            matchesToRemove.Add(match);
                            assignedValue = true;
                            break;


                        }
                        else
                        {
                            //FLOATING NPC SPAWN (I.E SURPRISE ATTACK FROM AIR)
                            if (match.floatOnSpawn == true || match.floatOnSpawn == false)
                            {
                                PropertyData? floatingStateOnSpawn = npcExport.Data.Where(x => x.Name.Value.Equals(nameof(AssetTableNames.bFloatingStateOnSpawn))).FirstOrDefault();
                                if (floatingStateOnSpawn != null) { floatingStateOnSpawn.RawValue = match.floatOnSpawn; }
                            }

                            switch (match.npcType)
                            {
                                case NpcData.NpcType.Boss:
                                    data.RawValue = FName.FromString(uAsset, bossSelected);
                                    bossPool.Remove(bossSelected);
                                    matchesToRemove.Add(match);
                                    assignedValue = true;
                                    if (bossSpot != null) { bossSpot.RawValue = FName.FromString(uAsset, bossSelected); Debug.WriteLine($"BOSSSPOT: {bossSpot.RawValue}"); }
                                    //if (bossWorldEventChange != null) { bossWorldEventChange.RawValue = FName.FromString(uAsset, "CH04_Die_Boss_01"); Debug.WriteLine($"BOSSSPOT: {bossSpot.RawValue}"); }
                                    Debug.WriteLine($"BOSS: {npcExport.ObjectName}");
                                    LogGUIMessage("Randomizing: BOSS");
                                    bossAssignmentMap[match.spotUniqueID] = bossSelected;
                                    break;
                                case NpcData.NpcType.BossGuardian:
                                    data.RawValue = FName.FromString(uAsset, bossGuardianSelected);
                                    bossGuardianPool.Remove(bossGuardianSelected);
                                    LogGUIMessage("Randomizing: GUARDIAN");
                                    assignedValue = true;
                                    break;


                                case NpcData.NpcType.ButterFly when skipButterfly:
                                case NpcData.NpcType.HelpMate when skipExiledNpc:
                                case NpcData.NpcType.Projectile when skipProjectile:
                                    data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                                    assignedValue = true;
                                    LogGUIMessage("Randomizing: SKIPPING" + match.spotCodeNameOriginal);
                                    break;
                                default: assignedValue = false; break;


                            }

                        }

                        //if (assignedValue) break;


                    }
                    matchingNpcs.RemoveAll(matchesToRemove.Contains);




                    if (assignedValue) { break; }

                    if (WanderingBoss && WanderingBossChance >= wanderingBossRoll)
                    {
                        data.RawValue = FName.FromString(uAsset, wanderingSelected);
                        wanderingPool.Remove(wanderingSelected);
                    }
                    else
                    {
                        data.RawValue = FName.FromString(uAsset, enemySelected);
                        enemyPool.Remove(enemySelected);
                        alreadySpawnedNpcs.Add(enemySelected);
                        if (NpcData.npcDontReshuffle.Contains(enemySelected))
                        {
                            dontReshuffleNpcs.Add(enemySelected);
                            Debug.WriteLine("DONT RESHUFFLE:" + enemySelected);
                        }



                    }



                        //Debug.WriteLine($"ENEMY: {npcExport.ObjectName}");
                        //enemyPool.Remove(enemySelected);
                    }
            }
            uAsset.Write(filePath);

            return bossAssignmentMap;
        }



        private bool IsBoss(string value)
        {
            return NpcData.Npc[NpcData.NpcType.Boss].Any(boss => value.Contains(boss, StringComparison.OrdinalIgnoreCase));
        }

        public int GenerateSeed()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next();
        }
        private void LogGUIMessage(string message, int delayms)
        {
            Task.Run(async () =>
            {
                LogUpdated?.Invoke(string.Join(GlobalStrings.txtRandomizerStart, message));
                await Task.Delay(delayms);
            });
        }
        private void LogGUIMessage(string message)
        {
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                LogUpdated?.Invoke(string.Join(GlobalStrings.txtRandomizerStart, message));

            });
        }

        //FUCK THIS GOING TO DO DLL MEM REALTIME MODDING FOR BUFFS
        bool SetNpcSkillLinkInfo(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion)
        {
            NormalExport? statInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            statInfoTable = statInfoTable?.Asset.Exports.Count > 0 ? statInfoTable.Asset.Exports[0] as NormalExport : null;
            List<PropertyData>? propData = statInfoTable?.Data.Count > 0 ? statInfoTable?.Data[0].RawValue as List<PropertyData> : null;

            if (propData == null) { return false; }

            ArrayPropertyData? npcInfo = (ArrayPropertyData?)propData.FirstOrDefault(x => x.Name.ToString().Contains("_SkillInfoAsset", StringComparison.OrdinalIgnoreCase));


            ArrayPropertyData? npcInfoArray = (ArrayPropertyData?)propData.Where(x => x.Name.ToString().Contains("_Skill", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();


            List<PropertyData>? structPropertyData;
            
            structPropertyData = npcInfoArray.Value.Where(x => x != null).ToList();

            List<string> attributes = new List<string>
            {
                "_walk_speed_warning",
                "_walk_speed_combat",
                "_run_speed_warning",
                "_run_speed_combat",
                "_sight_distance_peace",
                "_sight_distance_warning",
                "_sight_distance_combat",
                "_sight_height",
                "_sight_angle",
                "_pursuit_distance",
                "_fatal_behit_enable_cooltime",
                "_targetdecision_range"
            };

          






            uasset.Write(filePath);
            return true;
        }
    }
}














    















