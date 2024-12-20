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
       
        //public struct RandomizeParameters
        //{
        //    bool IncludePuppets;
        //    bool IncludeCarcass;
        //    bool IncludeBossess;
        //    bool IncludeFactionProtection;
        //    bool WanderingBoss;
        //    bool WanderingBossChance;
        //    bool BossProtection;
        //    bool StalkerProtection;
        //    bool LargeMiniBossProtection;
        //}
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

        ItemDataBase ItemInfo { get; }

        //TEMP
        public bool ScaleBosses { get; set; }

        public bool skipChp1Boss { get; set; }//TEMP

        //TEMP
        public bool EnableDrops { get; set; }

        List<string> enemyPool;
        List<string> bossPool;
        List<string> wanderingPool;
        List<string> bossGuardianPool;
        List<string> itemPool;

        Random random;


        public Randomizer(bool includePuppets, bool includeCarcass, bool includeReborner, bool includeMiniBossStalker, bool includeMiniBossPuppet, bool includeBosses, bool includeMiniBossReborner, bool includeMiniBossCarcass, bool includeWanderingBoss, float wanderingBossChance, ItemDataBase itemData, bool randomizeDrops)
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

            WanderingBossChance = Math.Round(wanderingBossChance, 0);

            ItemInfo = itemData;

            enemyPool = new List<string>();
            bossPool = new List<string>();
            wanderingPool = new List<string>();
            bossGuardianPool = new List<string>();
            itemPool = new List<string>();

            
           
            
            //random = new Random(Seed);
            //enemyPool = ShufflePool(GeneratePool(includePuppets, includeCarcass, includeReborner, includeMiniBossStalker, includeMiniBossPuppet, includeBosses, includeMiniBossReborner, includeMiniBossCarcass, includeWanderingBoss), random);
            //bossPool = ShufflePool(GeneratePool(false,false,false,false,false,true,false,false,false), random);
            //wanderingPool = ShufflePool(GeneratePool(false, false, false, true, true, true, true, true, true), random);




        }

        //NEW 
        private List<string> ShufflePool(List<string> pool, Random random)
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
        List<string> GenerateItemPool(ItemDataBase itemdb, bool dropWeapon)
        {
            List<string> items = new List<string>();

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

            if (itemdb.BossHardErgo != null)
                items.AddRange(itemdb.BossHardErgo.Select(x => x.Id));

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
            //return await Task.Run(async () =>
            //{
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

                    string? mappingPath = Directory.GetFiles(fileHandler.tempPath, "mappings.usmap", SearchOption.AllDirectories).FirstOrDefault();
                    if (mappingPath == null) { return false; }

                    //Usmap mapping = new Usmap(Directory.GetFiles(fileHandler.tempPath, "mappingss.usmap", SearchOption.AllDirectories).FirstOrDefault());

                    Usmap mapping = new Usmap(mappingPath);

                    string pChunk;

                    string? npcInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "NPCInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();
                    if (npcInfoAsset == null) { return false; }

                   
                    string? itemPackageInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "ItemPackageInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();
                    string? itemDropInfoAsset = Directory.GetFiles(Path.Combine(fileHandler.tempPath, fileHandler.pakBaseDirectory[2]), "ItemDropInfo.uasset", SearchOption.AllDirectories).FirstOrDefault();

                    string assetName;
                    UAsset myAsset;


                    UAsset npcInfo = new UAsset(npcInfoAsset, EngineVersion.VER_UE4_27, mapping);
                    UAsset itemPackageInfo = new UAsset(itemPackageInfoAsset, EngineVersion.VER_UE4_27, mapping);
                    UAsset itemDropInfo = new UAsset(itemDropInfoAsset, EngineVersion.VER_UE4_27, mapping);

                    List<NormalExport> npc;
                    List<NpcData.NpcSpotData> importantNpcs;
                    Dictionary<string, string> allBossAssignments = new Dictionary<string, string>();//BOSS TRACKING FOR SCALING PURPOSES (ORIGINAL BOSS AND NEW BOSS)


                SetNpcInfo(npcInfoAsset, npcInfo, mapping, EngineVersion.VER_UE4_27, NpcData.GetAllMapNpcSpotData(), true, NpcData.FactionType.E_MONSTER_CARCASSNPUPPET, true, false, allBossAssignments);

                for (int i = 0; i < pakChunksOriginal.Length; i++)
                {
                    string umap = Path.GetFileName(pakChunksOriginal[i]);
                    umap = umap.Substring(0, umap.IndexOf(".umap"));
                    Dictionary<string, string> mapBossAssignments = new Dictionary<string, string>();

                    //DISGUSTING (BUT WAS IN A HURRY FOR TESTING, CONVERT TO DICTIONARY)
                    switch (umap)
                    {
                        case nameof(MapName.LD_Outer_Station_DSN):
                            //lvlAsset = new UAsset(pakChunksOriginal[i], EngineVersion.VER_UE4_27, mapping);
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLDOuterStation;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LD")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LD_Outer_Station_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);
                            break;
                        case nameof(MapName.LV_Inner_UpperStreet_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVInnerUpperStreet;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Inner_UpperStreet_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);

                            break;
                        case nameof(MapName.LV_Inner_Factory_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVInnerFactory;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Inner_Factory_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);

                            break;
                        case nameof(MapName.LV_Inner_Cathedral_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVInnerCathedral;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Inner_Cathedral_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);

                            break;
                        case nameof(MapName.LV_Outer_Underdark_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVOuterUnderdark;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_Underdark_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);

                            break;
                        case nameof(MapName.LV_Krat_EastEndWard_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVKratEastEndWard;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Krat_EastEndWard_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);

                            break;
                        case nameof(MapName.LV_Krat_Old_Town_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);

                            importantNpcs = NpcData.NpcLVKratOldTown;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Krat_Old_Town_DSN).ToString();

                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);

                            break;
                        case nameof(MapName.LV_Outer_Grave_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVOuterGrave;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_Grave_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);
                            break;
                        case nameof(MapName.LV_Monastery_A_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVMonasteryA;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Monastery_A_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);
                            break;
                        case nameof(MapName.LV_Monastery_B_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVMonasteryB;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Monastery_B_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);
                            break;
                        case nameof(MapName.LV_Outer_CentralStatinB_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVOuterCentralStatinB;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_CentralStatinB_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);
                            break;
                        case nameof(MapName.LV_Outer_Exhibition_DSN):
                            pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                            myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                            importantNpcs = NpcData.NpcLVOuterExhibition;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                            npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();//GET ALL SPAWN POINTS
                            assetName = nameof(MapName.LV_Outer_Exhibition_DSN).ToString();
                            mapBossAssignments = GenerateEnemies(pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc, true, true, true, true, true, true, false, assetName, pakChunksOriginal[i]);
                            break;
                    }
                    foreach (var entry in mapBossAssignments)
                    {
                        if (!allBossAssignments.ContainsKey(entry.Key))
                        {
                            allBossAssignments[entry.Key] = entry.Value;
                        }
                    }
                }


                SetNpcInfo(npcInfoAsset, npcInfo, mapping, EngineVersion.VER_UE4_27, NpcData.GetAllMapNpcSpotData(), true, NpcData.FactionType.E_MONSTER_CARCASSNPUPPET, true, true, allBossAssignments);

                if (RandomizeDrops)
                {
                    //TODO: QUICK TEST CODE FOR RELEASE - START REFACTORING ONCE RELEASED AND FIX BUGS
                    //ITEM WORLD
                    RandomizeItems(itemPackageInfoAsset, itemPackageInfo, mapping, EngineVersion.VER_UE4_27, ItemInfo, true);
                    //NPC ITEMS
                    RandomizeItemsNpc(itemDropInfoAsset, itemDropInfo, mapping, EngineVersion.VER_UE4_27, ItemInfo, false);
                }




                bool result = await fileHandler.UnrealPak(fileHandler.pakBaseDirectory, "C:\\loprandoalpha");
                    if (!result)
                    {
                        Debug.WriteLine($"Randomize: failed to copy randomized files to directory");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Randomize:", ex.Message);
                    return false;
                }

                return true;
            //});
        }

        //QUICK UGLY DIRTY WILL FIX
        void RandomizeItems(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion, ItemDataBase itemData, bool includeWeapons)
        {
            itemPool = ShufflePool(GenerateItemPool(ItemInfo, true), random);


            NormalExport? itemPackageInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            List<PropertyData>?itemProperties = itemPackageInfoTable?.Data.Count > 0 ? itemPackageInfoTable[0].RawValue as List<PropertyData> : null;
            ArrayPropertyData? arrayPropertyDatas = (ArrayPropertyData)itemProperties.Where(x => x.Name.Value.Equals("_ItemPackage_array")).FirstOrDefault();
            PropertyData[] structPropertyData = (PropertyData[])arrayPropertyDatas.RawValue;
            int quartzTotal = 0;

            for (int i = 0; i < structPropertyData.Length; i++)
            {
                if(itemPool.Count <= 0) { itemPool = ShufflePool(GenerateItemPool(ItemInfo,true), random); }
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
                
                if (item.StartsWith("WP_PC"))
                {
                    string weaponName = item.Substring(10);
                    if (item.Contains("HND"))
                    {
                        weaponName = "WP_PC_BLD_" + weaponName;
                        handle.RawValue = FName.FromString(uasset, item);
                        blade.RawValue = FName.FromString(uasset, weaponName);

                        

                    }
                    else
                    {
                        weaponName = "WP_PC_HND_" + weaponName;
                        blade.RawValue = FName.FromString(uasset, item);
                        handle.RawValue = FName.FromString(uasset, weaponName);

              
                    }
                    itemPool.Remove(item);
                    itemPool.Remove(weaponName);
                    if (itemPool.Count <= 0) { itemPool = ShufflePool(GenerateItemPool(ItemInfo,true), random); }


                }
                item = itemPool[random.Next(itemPool.Count)];
                if (item.StartsWith("WP_PC")) { continue; }

                if (slot1?.RawValue != null)
                {
                    if (slot1.RawValue.ToString().Contains("epic", StringComparison.OrdinalIgnoreCase) || slot1.RawValue.ToString().Contains("key", StringComparison.OrdinalIgnoreCase) && !slot1.RawValue.ToString().Equals("Key_1"))
                    {
                        itemPool.Remove(slot1.RawValue.ToString());
                        slot2.RawValue = FName.FromString(uasset, item);
                        slot2Count.RawValue = 1;
                        itemPool.Remove(item);
                        
                        continue;
                    }
                }

                slot1.RawValue = FName.FromString(uasset, item);
                slot1Count.RawValue = 1;
                itemPool.Remove(item);

                if (quartzTotal < 11 && random.Next(100) < 10)
                {
                    item = "quartz";
                    quartzTotal++;
                    slot3.RawValue = FName.FromString(uasset, item);
                    slot3Count.RawValue = 1;
                   
                }
                if (random.Next(100) < 15)
                {
                    item = "Reinforce_Blade_Common_G1";
                    slot4.RawValue = FName.FromString(uasset, item);
                    slot4Count.RawValue = random.Next(1, 5);

                    item = "Reinforce_Blade_Common_G2";
                    slot5.RawValue = FName.FromString(uasset, item);
                    slot5Count.RawValue = random.Next(1, 5);
                    continue;
                }

                if (random.Next(100) < 12) 
                {
                    item = "Reinforce_Blade_Common_G3";
                    slot4.RawValue = FName.FromString(uasset, item);
                    slot4Count.RawValue = random.Next(1, 5);

                    item = "Reinforce_Blade_Common_G4";
                    slot5.RawValue = FName.FromString(uasset, item);
                    slot5Count.RawValue = random.Next(1, 5);
                    continue;
                }
                if (random.Next(100) < 10)
                {
                    item = "Reinforce_Hero_G1";
                    slot4.RawValue = FName.FromString(uasset, item);
                    slot4Count.RawValue = random.Next(1, 5);

                    item = "Reinforce_Hero_G2";
                    slot5.RawValue = FName.FromString(uasset, item);
                    slot5Count.RawValue = random.Next(1, 5);

                }






            }


            uasset.Write(filePath);



        }

        void RandomizeItemsNpc(string? filePath, UAsset? uasset, Usmap mapping, EngineVersion engineVersion, ItemDataBase itemData, bool includeWeapons)
        {
            itemPool = ShufflePool(GenerateItemPool(ItemInfo, false), random);


            NormalExport? itemPackageInfoTable = uasset.Exports.Count > 0 ? uasset.Exports[0] as NormalExport : null;
            List<PropertyData>? itemProperties = itemPackageInfoTable?.Data.Count > 0 ? itemPackageInfoTable[0].RawValue as List<PropertyData> : null;
            ArrayPropertyData? arrayPropertyDatas = (ArrayPropertyData)itemProperties.Where(x => x.Name.Value.Equals("_PackageConfigureInfo_array")).FirstOrDefault();
            PropertyData[] structPropertyData = (PropertyData[])arrayPropertyDatas.RawValue;
            int quartzTotal = 0;

            for (int i = 0; i < structPropertyData.Length; i++)
            {
                if (itemPool.Count <= 0) { itemPool = ShufflePool(GenerateItemPool(ItemInfo,false), random); }
                List<PropertyData> test = (List<PropertyData>)structPropertyData[i].RawValue;
                var itemCodeName = test.Where(x => x.Name.Value.Equals("_item_code_name")).FirstOrDefault() as PropertyData;
                var percent = test.Where(x => x.Name.Value.Equals("_item_acquisition_percentage")).FirstOrDefault() as PropertyData;

                if(itemCodeName?.RawValue == null) { continue; }

                if(itemCodeName.RawValue.ToString().Contains("epic",StringComparison.OrdinalIgnoreCase) || itemCodeName.RawValue.ToString().Contains("key", StringComparison.OrdinalIgnoreCase) && !itemCodeName.RawValue.ToString().Equals("Key_1")) { itemPool.Remove(itemCodeName.RawValue.ToString()); continue; }
                string item;

                if (quartzTotal < 15 && random.Next(100) < 10)
                {
                    item = "quartz";
                    quartzTotal++;
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }
                if (random.Next(100) < 12)
                {
                    item = "Reinforce_Blade_Common_G1";
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }
                if (random.Next(100) < 11)
                {
                    item = "Reinforce_Blade_Common_G2";
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }
                if (random.Next(100) < 10)
                {
                    item = "Reinforce_Blade_Common_G3";
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }
                if (random.Next(100) < 8)
                {
                    item = "Reinforce_Blade_Common_G4";
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }
                if (random.Next(100) < 6)
                {
                    item = "Reinforce_Hero_G1";
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }
                if (random.Next(100) < 5)
                {
                    item = "Reinforce_Hero_G1";
                    itemCodeName.RawValue = FName.FromString(uasset, item);
                    continue;
                }



                item = itemPool[random.Next(itemPool.Count)];


                itemCodeName.RawValue = FName.FromString(uasset, item);
                itemPool.Remove(item);






            }


            uasset.Write(filePath);



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

            //if (setFaction && npcInfoArray != null)
            //{
            //    Debug.WriteLine($"Setting faction");

            //    structPropertyData = (List<PropertyData>?)npcInfoArray.Value.Where(x => x != null).ToList();
            //    SetRawValue(filePath, uasset, structPropertyData, AssetTableNames._Faction, nameof(NpcData.FactionType.E_MONSTER_CARCASSNPUPPET), null);
            //}
            //if (setExpDrop && npcStatInfoArray != null)
            //{
            //    Debug.WriteLine($"Setting exp");

            //    structPropertyData = (List<PropertyData>?)npcStatInfoArray.Value.Where(x => x != null).ToList();
            //    SetRawValue(filePath, uasset, structPropertyData, AssetTableNames._Exp, null, NpcData.GetAllMapNpcSpotData());
            //}
            ////TEMP
            //if(ScaleBosses && npcStatInfoArray != null)
            //{
            //    Debug.WriteLine($"Setting Scale");

            //    structPropertyData = (List<PropertyData>?)npcStatInfoArray.Value.Where(x => x != null).ToList();
            //    SetRawValue(filePath, uasset, structPropertyData, AssetTableNames._physical_reduce, null, NpcData.GetAllMapNpcSpotData());
            //    //SetRawValue(filePath, uasset, structPropertyData, AssetTableNames._physical_power, null, NpcData.GetAllMapNpcSpotData());
            //}
            // Set Faction
            if (setFaction && npcInfoArray != null)
            {
                Debug.WriteLine($"Setting faction");

                structPropertyData = npcInfoArray.Value.Where(x => x != null).ToList();
                SetRawValue2(filePath, uasset, structPropertyData, AssetTableNames._Faction, nameof(NpcData.FactionType.E_MONSTER_CARCASSNPUPPET), null);
            }

            // Set EXP Drop
            if (setExpDrop && npcStatInfoArray != null)
            {
                Debug.WriteLine($"Setting exp");

                structPropertyData = npcStatInfoArray.Value.Where(x => x != null).ToList();
                SetRawValue2(filePath, uasset, structPropertyData, AssetTableNames._Exp, null, NpcData.GetAllMapNpcSpotData());
            }

            // Scale Bosses
            if (ScaleBosses && npcStatInfoArray != null && scaleBosses)
            {
                Debug.WriteLine($"Setting Scale");
                structPropertyData = npcStatInfoArray.Value.Where(x => x != null).ToList();

                foreach (var assignment in bossAssignments)
                {
                    string originalBoss = assignment.Key;
                    string newBoss = assignment.Value;

                    Debug.WriteLine($"Scaling stats for: {originalBoss} -> {newBoss}");

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
                AssetTableNames._guard_stamina_damage
            })
                    {
                        SetRawValue(filePath, uasset, structPropertyData, tableName, newBoss, originalBoss, spotData);
                    }
                }
            }

            uasset.Write(filePath);
            return true;
        }

        void SetRawValue(string? filePath, UAsset? uasset, List<PropertyData>? propertyData, AssetTableNames tableName, string? bossSelected, string? originalBossID, List<NpcSpotData>? allNpcSpotMapData)
        {
            if (propertyData == null || filePath == null || uasset == null || bossSelected == null || originalBossID == null)
            {
                Debug.WriteLine($"SetRawValue: invalid input");
                return;
            }

            string logPath = @"C:\loprandoalpha\BossStatsLog.txt";

            for (int i = 0; i < propertyData.Count; i++)
            {
                var npcdata = propertyData[i]?.RawValue as List<PropertyData>;
                if (npcdata == null) { continue; }

                var codeNameProperty = npcdata.FirstOrDefault(x => x.Name.Value.ToString().Equals("_code_name", StringComparison.OrdinalIgnoreCase));
                if (codeNameProperty == null || !codeNameProperty.RawValue.ToString().Equals(bossSelected, StringComparison.OrdinalIgnoreCase))
                {
                    continue; 
                }

                // Lookup original boss using spotUniqueID
                var originalBoss = allNpcSpotMapData?.FirstOrDefault(x =>
                x.spotUniqueID.Equals(originalBossID, StringComparison.OrdinalIgnoreCase) &&
                x.npcType == NpcType.Boss &&
                x.npcImportant != true);

                if (originalBoss == null)
                {
                    Debug.WriteLine($"Original boss not found: {originalBossID}");
                    continue;
                }
 

                // Find attribute to update
                var attribute = npcdata.FirstOrDefault(x => x.Name.Value.ToString().Equals(tableName.ToString(), StringComparison.OrdinalIgnoreCase));
                if (attribute == null) { continue; }

                // Transfer the stat
                int valueToAssign = tableName switch
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
                    attribute.RawValue = valueToAssign;

                    string logEntry = $"Original Boss: {originalBoss.Value.spotCodeNameOriginal}\n" +
                                      $"Stat: {tableName}\n" +
                                      $"Transferred Value: {valueToAssign}\n" +
                                      $"----------------------------------\n" +
                                      $"Randomized Boss: {bossSelected}\n";





                    Debug.WriteLine(logEntry);
                    //try { File.AppendAllText(logPath, logEntry); }
                    //catch (Exception ex) { Debug.WriteLine($"Failed to write log: {ex.Message}"); }
                }
            }
        }




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
                        
                        if(blackRabbitCodeNames.Any(codename.RawValue.ToString().Contains)) 
                        {
                            Debug.WriteLine($"Skipping Faction BLACK RABBIT", codename.RawValue.ToString());
                            //if(codename.RawValue.ToString() == "CH11_Stalker_BRabbit_StrongMale_Boss_00")
                            //{
                            //    attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Faction), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                            //    if (attribute == null) { break; }
                            //    attribute.RawValue = FName.FromString(uasset, nameof(FactionType.E_MONSTER_CARCASSNPUPPET));
                            //}
                            break; 
                        }
                      
                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._grade), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        if (attribute.RawValue.ToString().Equals("E_BOSS"))
                        {

                            Debug.WriteLine($"Skipping Faction BOSS");
                            break;
                        }
                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Code_Name), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        if (attribute.RawValue.ToString().Contains("HelpMate"))
                        {
                            Debug.WriteLine("Skipping Faction:" + attribute.RawValue);
                            break;
                        }

                        attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Faction), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                        if (attribute == null) { break; }
                        if (attribute.RawValue.ToString().Equals("E_NEUTRAL") && (!codename.RawValue.ToString().ToLower().Contains("stalker")))
                        {
                            Debug.WriteLine($"Skipping Faction NEUTRAL");
                            break;
                        }
                        attribute.RawValue = FName.FromString(uasset, value);
                        Debug.WriteLine($"SetRawValue Faction: {value}");
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
                        break;
                    //case AssetTableNames._physical_reduce when allNpcSpotMapData != null:
                    //    attribute = npcdata.Where(x => x.Name.Value.ToString().Contains(nameof(AssetTableNames._Code_Name), StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    //    if (attribute == null) { break; }
                    //    int phyReduce = allNpcSpotMapData.FirstOrDefault(x => x.spotCodeNameOriginal.ToString().Contains(attribute.RawValue.ToString(), StringComparison.OrdinalIgnoreCase)).physicalReduce;
                    //    if (phyReduce == 0) { phyReduce = -500; }

                    //    attribute = npcdata.Where(x => x.Name.Value.ToString().Equals("_physical_reduce", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                    //    if (attribute == null) { break; }
                    //    //int originalHP = -1000;
                    //    //int scaledValueHP = ScaleValue(originalHP, hpScale);
                    //    attribute.RawValue = phyReduce;
                    //    Debug.WriteLine($"HP Scale: {string.Join(", ", npcdata[0].RawValue, phyReduce)}");
                    //    break;


                }
            }

        }

        int ScaleValue(int originalValue, double scalePercent)
        {
            double scaleFactor = 1.0 - (scalePercent / 100.0);

            int scaledValue = (int)Math.Round(originalValue * scaleFactor);

            //return scaledValue > 100 ? scaledValue : originalValue;
            return scaledValue;

        }


        public bool IsNumber(string? value, int min)
        {
            int number;
            if(int.TryParse(value, out number) && number > min){ return true; }

            return false;
            
        }







        Dictionary<string, string> GenerateEnemies(string pakChunk, UAsset uAsset, Usmap mapping, EngineVersion engineVersion, List<NpcData.NpcSpotData> importantNpcs, List<NormalExport> npcs,
    bool skipButterfly, bool skipImportantNpcs, bool skipExiledNpc, bool skipProjectile, bool removeNpcFromPool, bool scaleEnemies, bool scaleBosses, string fileName, string filePath)
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

            PropertyData? floatingStateOnSpawn = null;




            foreach (NormalExport npcExport in npcs)
            {
                string spotName = npcExport.ObjectName.ToString();


                foreach (PropertyData data in npcExport.Data)
                {

                    if (npcExport.ObjectName.Value.ToString().Contains(nameof(AssetTableNames.BossRoom))) { bossSpot = npcExport.Data.Where(x => x.Name.Value.ToString().StartsWith(nameof(AssetTableNames.BossNpcCodeName), StringComparison.OrdinalIgnoreCase)).FirstOrDefault(); continue; }


                    if (data.Name.ToString() != nameof(AssetTableNames.SpotCodeName)) continue;
                    //bossWorldEventChange = npcExport.Data.Where(x => x.Name.Value.ToString().StartsWith("WorldEventCodeName", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();



                    if (enemyPool.Count == 0)
                        enemyPool = ShufflePool(GeneratePool(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, false, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss), random);

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


                    //bossPool.Remove(bossSelected);
                    //enemyPool.Remove(enemySelected);
                    //wanderingPool.Remove(wanderingSelected);

                    //if (scaleBosses && bossSelected.ToLower().StartsWith("ch"))
                    //    bossSelected = bossSelected.Substring(bossSelected.IndexOf("CH") + 5);


                    //if (scaleBosses && wanderingSelected.ToLower().StartsWith("ch"))
                    //    wanderingSelected = wanderingSelected.Substring(wanderingSelected.IndexOf("CH") + 5);

                    //scaleEnemies = false;
                    //if (scaleEnemies && enemySelected.ToLower().StartsWith("ch"))
                    //    enemySelected = enemySelected.Substring(enemySelected.IndexOf("CH") + 5);


                    bool assignedValue = false;
                    List<NpcData.NpcSpotData> matchesToRemove = new List<NpcData.NpcSpotData>();

                    foreach (var match in matchingNpcs)
                    {
                        if (spotName != match.spotUniqueID.ToString()) { continue; }

                        if (spotName == "Npc-LV_Outer_Exhibition_DSN-82_6")
                        {
                            Debug.WriteLine(spotName);
                        }



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
                            matchesToRemove.Add(match);
                            assignedValue = true;
                            break;


                        }
                        else
                        {
                            //FLOATING NPC SPAWN (I.E SURPRISE ATTACK FROM AIR)
                            if (match.floatOnSpawn == true || match.floatOnSpawn == false)
                            {
                                floatingStateOnSpawn = npcExport.Data.Where(x => x.Name.Value.Equals(nameof(AssetTableNames.bFloatingStateOnSpawn))).FirstOrDefault();
                                if (floatingStateOnSpawn != null) { floatingStateOnSpawn.RawValue = FName.FromString(uAsset, match.floatOnSpawn.ToString()); }
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
                                    bossAssignmentMap[match.spotUniqueID] = bossSelected;
                                    break;
                                case NpcData.NpcType.BossGuardian:
                                    data.RawValue = FName.FromString(uAsset, bossGuardianSelected);
                                    bossGuardianPool.Remove(bossGuardianSelected);
                                    assignedValue = true;                                 
                                    break;


                                case NpcData.NpcType.ButterFly when skipButterfly:
                                case NpcData.NpcType.HelpMate when skipExiledNpc:
                                case NpcData.NpcType.Projectile when skipProjectile:
                                    data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                                    assignedValue = true;
                                    break;
                                default: assignedValue = false; break;


                            }
                           
                        }

                        //if (assignedValue) break;


                    }
                    matchingNpcs.RemoveAll(matchesToRemove.Contains);

                    //FLOATING SPAWN
                   

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
        
        
    }
}















