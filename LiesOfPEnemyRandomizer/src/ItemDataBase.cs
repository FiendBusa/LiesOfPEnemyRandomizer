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
