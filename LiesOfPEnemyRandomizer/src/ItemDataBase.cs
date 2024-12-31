using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LiesOfPEnemyRandomizer.src
{
    public class Item
    {
        [JsonPropertyName("Id")]
        public string Id { get; set; }

        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonPropertyName("Category")]
        public string Category { get; set; }
    }
    public static class DropTables
    {
        public static readonly Dictionary<string, int> MaterialDropTablePercent = new Dictionary<string, int>
        {
              {"Reinforce_Blade_Common_G1", 50},
              {string.Empty, 1000 },
              {"Reinforce_Blade_Common_G2", 40},
              {"Reinforce_Blade_Common_G3", 40},
              {"Reinforce_Blade_Common_G4", 40},
              {"Reinforce_Hero_G1", 20},
              {"Reinforce_Hero_G2", 20}
        };
        //I DIDNT WANT TO WASTE TIME RE-DOING THE JSON TO ADD DROP RATE PERCENTAGE SO I JUST DID THIS LoL
        public static readonly Dictionary<string, int> ImportantDropItemPercent = new Dictionary<string, int>
        {
            { "WP_PC", 100 },
            { "SlaveArm", 100 },
            { "AC_", 100 },
            { "part_", 100 },
            { "Krat_BlackBox", 100 },
            { "Grinder", 100 }
        };

        public static readonly Dictionary<string, int> ItemDropPercentReductions = new Dictionary<string, int>
        {
            { "Consume_ProtectDropErgo_3L", 30 },
            { "Consume_ProtectDropErgo_2L", 30 },
            { "Consume_ProtectDropErgo_1L", 30 },
            { "CH00_Boss_Ergo", 50 },
            { "CH01_Boss_Ergo", 50 },
            { "CH02_Boss_Ergo", 50 },
            { "CH03_Boss_Ergo", 50 },
            { "CH04_Boss_Ergo", 50 },
            { "CH05_Boss_Ergo", 50 },
            { "CH06_Boss_Ergo", 50 },
            { "CH07_Boss_Ergo", 50 },
            { "CH08_Boss_Ergo", 50 },
            { "CH09_Boss_Ergo", 50 },
            { "CH11_Boss_Ergo", 50 },
            { "CH12_Boss_Ergo", 50 },
            { "CH13_Boss_Ergo", 50 },
            { "Helpmate_Material", 30 }
        };
    }

    public class ItemDataBase
    {
        [JsonPropertyName("Melee")]
        public List<Item> Melee { get; set; }

        [JsonPropertyName("Left Arm")]
        public List<Item> SlaveArms { get; set; }

        [JsonPropertyName("Materials")]
        public List<Item> Materials { get; set; }

        [JsonPropertyName("Amulets")]
        public List<Item> Amulets { get; set; }

        [JsonPropertyName("Frame")]
        public List<Item> Frame { get; set; }

        [JsonPropertyName("Liner")]
        public List<Item> Liner { get; set; }

        [JsonPropertyName("Converter")]
        public List<Item> Converter { get; set; }

        [JsonPropertyName("Cartridge")]
        public List<Item> Cartridge { get; set; }

        [JsonPropertyName("Buff Consumables")]
        public List<Item> BuffConsumables { get; set; }

        [JsonPropertyName("Thrown Consumables")]
        public List<Item> ThrownConsumables { get; set; }

        [JsonPropertyName("Generic Hard Ergo")]
        public List<Item> GenericHardErgo { get; set; }

        [JsonPropertyName("Boss Hard Ergo")]
        public List<Item> BossHardErgo { get; set; }

        [JsonPropertyName("Wishstones")]
        public List<Item> Wishstones { get; set; }

        [JsonPropertyName("Grinder Buffs")]
        public List<Item> GrinderBuffs { get; set; }

        [JsonPropertyName("General")]
        public List<Item> General { get; set; }

        [JsonPropertyName("Gold Tree Boosters")]
        public List<Item> GoldTreeBoosters { get; set; }

        [JsonPropertyName("Supply Boxes")]
        public List<Item> SupplyBoxes { get; set; }

        [JsonPropertyName("Venigni Collections")]
        public List<Item> VenigniCollections { get; set; }

        [JsonPropertyName("Quest Items")]
        public List<Item> QuestItems { get; set; }

        [JsonPropertyName("Gestures")]
        public List<Item> Gestures { get; set; }

        [JsonPropertyName("Records")]
        public List<Item> Records { get; set; }

        [JsonPropertyName("Other Collectibles")]
        public List<Item> OtherCollectibles { get; set; }

        [JsonPropertyName("Body")]
        public List<Item> Body { get; set; }

        [JsonPropertyName("Masks")]
        public List<Item> Masks { get; set; }

        [JsonPropertyName("HeadItems")]
        public List<Item> HeadItems { get; set; }

       
        public static ItemDataBase LoadItems(string filename)
        {
           
            try
            {
            
                string itemdb = File.ReadAllText(filename);


                var itemDataBase = JsonSerializer.Deserialize<ItemDataBase>(itemdb, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReadCommentHandling = JsonCommentHandling.Skip,
                    AllowTrailingCommas = true
                });

                if (itemDataBase == null)
                {
                    throw new Exception("Failed to deserialize the JSON data.");
                }
                
                return itemDataBase;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error loading items: {ex.Message}");
              
            }
        }
    }
}
