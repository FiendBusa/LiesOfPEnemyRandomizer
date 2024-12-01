using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UAssetAPI;
using UAssetAPI.ExportTypes;
using UAssetAPI.PropertyTypes.Objects;
using UAssetAPI.PropertyTypes.Structs;
using UAssetAPI.UnrealTypes;
using UAssetAPI.Unversioned;


namespace LiesOfPEnemyRandomizer.src
{
    public class Randomizer
    {
        //Puppet & Carcass 
        public bool IncludePuppets { get; private set; } = true;
        public bool IncludeCarcass { get; private set; } = true;
        public bool IncludeBosses { get; private set; } = false;
        public bool FactionProtectionPuppetCaracss { get; private set; }
        public bool WanderingBoss { get; private set; }
        public float WanderingBossChance { get; private set; } = 0.0f;
        public bool BossProtection { get; private set; } = true;
        public bool StalkerProtection { get; private set; } = true;
        public bool LargeMiniBossProtection { get; private set; } = true;




        public void RandomizeEnemies(bool IncludePuppets, bool IncludeCarcass, bool IncludeBosses, bool WanderingBoss, float WanderingBossChance, int Seed)
        {
            this.IncludePuppets = IncludePuppets;
            this.IncludeCarcass = IncludeCarcass;
            this.WanderingBoss = WanderingBoss;
            this.WanderingBossChance = WanderingBossChance;
            this.IncludeBosses = IncludeBosses;

            Random random = new Random(Seed);
            List<string>enemyPool = new List<string>();

            if (IncludePuppets)
            {
                enemyPool.AddRange(NpcData.Npc[NpcData.NpcType.Puppet]);

            }

            if (IncludeCarcass)
            {
                enemyPool.AddRange(NpcData.Npc[NpcData.NpcType.Carcass]);
            }

            if (Include)
            {
                
            }

            //Usmap mapping = new Usmap("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\Mappings.usmap");
            //UAsset myAsset = new UAsset("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\MapRelease\\LV_OuterKrat\\LV_CentralStation\\LD_Outer_Station_DSN.umap", EngineVersion.VER_UE4_27, mapping);

            //List<NormalExport> npc = myAsset.Exports.OfType<NormalExport>().Where(x => x.ObjectName.ToString().StartsWith("Npc-LD_Outer_Station_DSN-")).ToList();

            //if(npc == null) { return; }

            //foreach(NormalExport npcExport in npc)
            //{
            //   foreach(PropertyData data in npcExport.Data)
            //    {
            //        if (data.Name.ToString() != "SpotCodeName") {  continue; }
            //        data.RawValue = FName.FromString(myAsset, enemyPool[random.Next(enemyPool.Count)]);

            //    }
            //}
            //myAsset.Write("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\MapRelease\\LV_OuterKrat\\LV_CentralStation\\LD_Outer_Station_DSN-Randomized.umap");

            //QUICK FACTION TEST

            Usmap mapping2 = new Usmap("D:\\FModel\\Outputs\\Exports\\unrealpak\\pakchunk2_s3-WindowsNoEditor_P\\LiesofP\\Content\\MapRelease\\LV_InnerKrat\\Mappings.usmap");
            UAsset myAsset2 = new UAsset("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\ContentInfo\\InfoAsset\\NPCInfo.uasset", EngineVersion.VER_UE4_27, mapping2);

            NormalExport? npc2 = (NormalExport?)myAsset2.Exports[0];

            NormalExport? npc3 = (NormalExport?)npc2.Asset.Exports[0];



            List<PropertyData> test1 = (List<PropertyData>?)npc3.Data[0].RawValue;

            ArrayPropertyData test2 = (ArrayPropertyData)test1[0];

            for (int i = 0; i < test2.Value.Length; i++)
            {
                StructPropertyData name = (StructPropertyData)test2.Value[i];
                List<PropertyData> name2 = (List<PropertyData>)name.RawValue;

                foreach(PropertyData name3 in name2)
                {
                    if (name3.Name.Value.ToString().Contains("_faction"))
                    {
                        name3.RawValue = FName.FromString(myAsset2, "E_MONSTER_CARCASSNPUPPET");
                    }
                }
            }
            myAsset2.Write("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\ContentInfo\\InfoAsset\\NPCInfo-NEW.uasset");


















            //List<object> data = (List<object>)npc3.RawValue;




            if (npc2 == null) { return; }

            //foreach (NormalExport npcExport2 in npc2)
            //{
            //    foreach (PropertyData data2 in npcExport2.Data)
            //    {
            //        if (data2.Name.ToString() != "SpotCodeName") { continue; }
            //        data2.RawValue = FName.FromString(myAsset2, enemyPool[random.Next(enemyPool.Count)]);

            //    }
            //}
            myAsset2.Write("C:\\Users\\g-gil\\Downloads\\FModel (1)\\Output\\Exports\\pakchunk0_s4\\LiesofP\\Content\\ContentInfo\\InfoAsset\\NPCInfo-new.uasset");



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





        }
        public int GenerateSeed()
        {
            return new Random(Guid.NewGuid().GetHashCode()).Next();
        }
        
        
    }
}
