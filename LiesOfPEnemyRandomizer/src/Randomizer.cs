using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.UnrealTypes.EngineEnums;
using UAssetAPI.Unversioned;
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

        public float WanderingBossChance { get; private set; }

        public int Seed { get; private set; }

        List<string> enemyPool;
        List<string> bossPool;
        List<string> wanderingPool;



        public Randomizer(bool includePuppets, bool includeCarcass, bool includeReborner, bool includeMiniBossStalker, bool includeMiniBossPuppet, bool includeBosses, bool includeMiniBossReborner, bool includeMiniBossCarcass, bool includeWanderingBoss, float wanderingBossChance)
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

            WanderingBossChance = WanderingBossChance;


            enemyPool = new List<string>();
            bossPool = new List<string>();
            wanderingPool = new List<string>();

        }

        private void WriteEnemiesGeneratedToFile(List<string?> enemiesGenerated, string filePath)
        {
            try
            {
                // Write all lines to the specified file path
                File.WriteAllLines(filePath, enemiesGenerated.Where(e => e != null).Select(e => e.ToString()));
                Console.WriteLine($"Enemies generated list has been written to: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing the file: {ex.Message}");
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
            if (includeBosses || includeWanderingBoss)
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
            if (includeMiniBossPuppet)
            {
                pool.AddRange(NpcData.Npc[NpcData.NpcType.MiniBossPuppet]);
            }

            return pool;


        }
        public async Task<bool> RandomizeEnemies(int seed)
        {

            Seed = seed;
            if(Seed <=0) {  Seed = GenerateSeed(); }

            Random random = new Random(Guid.NewGuid().GetHashCode());
            enemyPool = GeneratePool(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, IncludeBosses, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss);
            bossPool = GeneratePool(false,false,false,true,true,true,true,true,false);
            wanderingPool = GeneratePool(false, false, false, true, true, true, true, true, true);



            //COPY DATA TO TEMP FOLDER (PAK, MAPPINGS, UNREALPAK)
            string tempPath = Path.GetTempPath();
            FileHandler fileHandler = new FileHandler(tempPath);


            //GET ALL UNMODIFIED LEVEL FILES FROM TEMP
            string[]? pakChunksOriginal = await fileHandler.GenerateBaseTempFiles();
            if(pakChunksOriginal == null) { return false; }

            Usmap mapping = new Usmap(Directory.GetFiles(fileHandler.tempPath, "mappings.usmap", SearchOption.AllDirectories).FirstOrDefault());
            //UAsset lvlAsset;
            string pChunk;
            UAsset myAsset;
            List<NormalExport> npc;
            List<NpcData.NpcSpotData> importantNpcs;

            for (int i = 0; i < pakChunksOriginal.Length; i++)
            {
                string umap = Path.GetFileName(pakChunksOriginal[i]);
                umap = umap.Substring(0, umap.IndexOf(".umap"));     
                

                switch (umap)
                {
                    case nameof(MapName.LD_Outer_Station_DSN):
                        //lvlAsset = new UAsset(pakChunksOriginal[i], EngineVersion.VER_UE4_27, mapping);
                        pChunk = pakChunksOriginal[i].ToString();//PAK FILE CONTAINING NPC SPAWN DATA FROM TEMP                  
                        myAsset = new UAsset(pChunk, EngineVersion.VER_UE4_27, mapping);
                        importantNpcs = NpcData.NpcLDOuterStation;//IMPORTANT NPCS (I.E KEY ITEMS, BUTTERFLY ETC)
                        npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LD")).ToList();//GET ALL SPAWN POINTS
                        string assetName = nameof(MapName.LD_Outer_Station_DSN).ToString();

                        GenerateEnemies(random,pChunk, myAsset, mapping, EngineVersion.VER_UE4_27, importantNpcs, npc,true,true,true,true,true,true,false, assetName, pakChunksOriginal[i]);
                        break;


                      
                       
                }
            }
            await fileHandler.UnrealPak(fileHandler.pakBaseDirectory, "D:\\Steam\\steamapps\\common\\Lies of P\\LiesofP\\Content\\Paks\\~mods");

            return false;


        }
        bool GenerateEnemies(Random random, string pakChunk, UAsset uAsset, Usmap mapping, EngineVersion engineVersion, List<NpcData.NpcSpotData> importantNpcs, List<NormalExport> npcs, 
            bool skipButterfly, bool skipImportantNpcs, bool skipExiledNpc, bool skipProjectile, bool removeNpcFromPool, bool scaleEnemies, bool scaleBosses, string fileName, string filePath)
        {
            npcs = uAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LD", StringComparison.OrdinalIgnoreCase) || x.ObjectName.ToString().StartsWith("Npc-LV", StringComparison.OrdinalIgnoreCase)).ToList();
            List<string?>enemiesGenerated = new List<string>();

            if (npcs == null) { return false; }

            foreach (NormalExport npcExport in npcs)
            {
                

                foreach (PropertyData data in npcExport.Data)
                {
                    if (data.Name.ToString() != "SpotCodeName") { continue; }

                    if (enemyPool.Count <= 0) { enemyPool = GeneratePool(IncludePuppets, IncludeCarcass, IncludeReborner, IncludeMiniBossStalker, IncludeMiniBossPuppet, IncludeBosses, IncludeMiniBossReborner, IncludeMiniBossCarcass, WanderingBoss); }
                    if (bossPool.Count <= 0) { bossPool = GeneratePool(false, false, false, true, true, true, true, true, false); }
                    if (wanderingPool.Count <= 0) { GeneratePool(false, false, false, true, true, true, true, true, true); }

                    string bossSelected = bossPool[random.Next(bossPool.Count)];
                    string wanderingSelected = wanderingPool[random.Next(wanderingPool.Count)];
                    string enemySelected = enemyPool[random.Next(enemyPool.Count)];

                    //REMOVE BEFORE SCALING DUE TO STRING MANIPULATION
                    if (removeNpcFromPool)
                    {
                        bossPool.Remove(bossSelected);
                        enemyPool.Remove(enemySelected);
                        wanderingPool.Remove(wanderingSelected);
                    }

                    if (scaleBosses && bossSelected.ToLower().StartsWith("ch0")) { bossSelected = bossSelected.Substring(bossSelected.IndexOf("CH0") + 5); }

                    if(scaleBosses && wanderingSelected.ToLower().StartsWith("ch0")) { wanderingSelected = wanderingSelected.Substring(wanderingSelected.IndexOf("CH0") + 5); }
                


                    if (scaleEnemies && enemySelected.ToLower().StartsWith("ch0")) { enemySelected = enemySelected.Substring(enemySelected.IndexOf("CH0") + 5); }




                    


                    

                    //CHECK IMPORTANT NPCS && BOSSESS
                    List<NpcData.NpcSpotData> matchingNpcs = importantNpcs.Where(npc => npcExport.ObjectName.ToString().Contains(npc.spotUniqueID)).ToList();
                    if (matchingNpcs.Any())
                    {
                        foreach (NpcData.NpcSpotData match in matchingNpcs)
                        {
                            //BUTTERFLY SKIP
                            if (match.npcType == NpcData.NpcType.ButterFly && skipButterfly)
                            {
                                data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                                enemiesGenerated.Add(data.RawValue.ToString());
                                continue;
                            }
                            //HELP MATE
                            if (match.npcType == NpcData.NpcType.HelpMate && skipExiledNpc)
                            {
                                data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                                enemiesGenerated.Add(data.RawValue.ToString());
                                continue;
                            }
                            //PROJECTILE
                            if (match.npcType == NpcData.NpcType.Projectile && skipProjectile)
                            {
                                data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                                enemiesGenerated.Add(data.RawValue.ToString());
                                continue;
                            }
                            //BOSS
                            if (match.npcType == NpcData.NpcType.Boss || match.npcType == NpcData.NpcType.BossCarcass || match.npcType == NpcData.NpcType.BossHuman)
                            {
                                data.RawValue = FName.FromString(uAsset, bossPool[random.Next(bossPool.Count)]);
                                enemiesGenerated.Add(data.RawValue.ToString());
                                continue;
                            }
                            //ALL ELSE
                            if (match.npcImportant.HasValue && match.npcImportant.Value)
                            {
                                data.RawValue = FName.FromString(uAsset, match.spotCodeNameOriginal.ToString());
                                enemiesGenerated.Add(data.RawValue.ToString());
                                continue;
                            }
                        }
                    }
                    else
                    {
                        enemiesGenerated.Add(data.RawValue.ToString());
                        data.RawValue = FName.FromString(uAsset, enemyPool[random.Next(enemyPool.Count)]);
                    }
                }
            }
            uAsset.Write(filePath);
            string enemiesGeneratedFilePath = Path.Combine("D:\\Steam\\steamapps\\common\\Lies of P\\LiesofP\\Content\\Paks\\~mods", "GeneratedEnemies.txt");
            WriteEnemiesGeneratedToFile(enemiesGenerated, enemiesGeneratedFilePath);
            return true;





        }
        public int GenerateSeed()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next();
        }
        
        
    }
}

//Usmap mapping = new Usmap("C:\\users\\g-gil\\Documents\\Mappings.usmap");
//UAsset myAsset = new UAsset("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk2_s3\\LiesofP\\Content\\MapRelease\\LV_Krat_Factory\\LV_Inner_Factory_DSN.umap", EngineVersion.VER_UE4_27, mapping);

//List<NormalExport> npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LV")).ToList();


//if (npc == null) { return false; }

//foreach (NormalExport npcExport in npc)
//{
//    foreach (PropertyData data in npcExport.Data)
//    {
//        if (data.Name.ToString() != "SpotCodeName") { continue; }
//        data.RawValue = FName.FromString(myAsset, enemyPool[random.Next(enemyPool.Count)]);

//    }
//}
//myAsset.Write("C:\\users\\g-gil\\Documents\\LV_Inner_Factory_DSN.umap");

//QUICK FACTION TEST

//Usmap mapping2 = new Usmap("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\Mappings.usmap");
//UAsset myAsset2 = new UAsset("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\ContentInfo\\InfoAsset\\NPCInfo.uasset", EngineVersion.VER_UE4_27, mapping2);

//NormalExport? npc2 = (NormalExport?)myAsset2.Exports[0];

//NormalExport? npc3 = (NormalExport?)npc2.Asset.Exports[0];



//List<PropertyData> test1 = (List<PropertyData>?)npc3.Data[0].RawValue;

//ArrayPropertyData test2 = (ArrayPropertyData)test1[0];

//for (int i = 0; i < test2.Value.Length; i++)
//{
//    StructPropertyData name = (StructPropertyData)test2.Value[i];
//    List<PropertyData> name2 = (List<PropertyData>)name.RawValue;

//    foreach(PropertyData name3 in name2)
//    {
//        if (name3.Name.Value.ToString().Contains("_faction"))
//        {
//            name3.RawValue = FName.FromString(myAsset2, "E_MONSTER_CARCASSNPUPPET");
//        }
//    }
//}
//myAsset2.Write("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\ContentInfo\\InfoAsset\\NPCInfo-NEW.uasset");


















//List<object> data = (List<object>)npc3.RawValue;




//if (npc2 == null) { return; }

//foreach (NormalExport npcExport2 in npc2)
//{
//    foreach (PropertyData data2 in npcExport2.Data)
//    {
//        if (data2.Name.ToString() != "SpotCodeName") { continue; }
//        data2.RawValue = FName.FromString(myAsset2, enemyPool[random.Next(enemyPool.Count)]);

//    }
//}
//myAsset2.Write("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\ContentInfo\\InfoAsset\\NPCInfo-new.uasset");



//NamePropertyData test = new NamePropertyData() { RawValue = "TEST" };
//npc.Data[5].RawValue = test;
//myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");

//npc.Data[5].RawValue = FName.FromString(myAsset, "Npc-LV_Inner_UpperStreet_DSN-2");
//myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");
//     myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");


//if(npc is NormalExport export)
// {
//     export.Data[5] = new NamePropertyData() { RawValue = "Npc-LV_Inner_UpperStreet_DSN-2" };
//     myAsset.Write("C:\\Users\\g-gil\\Documents\\NG2\\test.umap");
// }