using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dungeon_Crawler_v2.Modul;
using System.IO;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Item
    {
        public static readonly string StiTilItems = Path.Combine(AppContext.BaseDirectory, "Modul", "Items.json");
        public int ItemId { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int Styrke { get; set; }
        public int Antal { get; set; }


        public Item() { } //til Json desentralisering

        public Item(int itemId, string navn, string beskrivelse, int styrke, int antal)
        {
            ItemId = itemId;
            Navn = navn;
            Beskrivelse = beskrivelse;
            Styrke = styrke;
            Antal = antal;
        }

        public static List<Item> HentItems(string sti)
        {
            if (!File.Exists(sti)) return new List<Item>();
            string json = File.ReadAllText(sti);
            return JsonSerializer.Deserialize<List<Item>>(json);
        }

        public static List<Item> items { get; } = HentItems(SpilData.StiTilItems);

        public static Item RandomItem(List<Item> items)
        {
            if (items == null || items.Count == 0) return null;
            Random rnd = new Random();
            return items[rnd.Next(items.Count)];
        }

    }
}

