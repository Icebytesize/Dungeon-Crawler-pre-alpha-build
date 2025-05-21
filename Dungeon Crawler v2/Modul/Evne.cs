using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Evne
    {
        public int EvneId { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int Styrke { get; set; }

        public Evne() { } //til Json desentralisering

        public static List<Evne> HentEvner(string sti)
        {
            if (!File.Exists(sti)) return new List<Evne>();
            string json = File.ReadAllText(sti);
            return JsonSerializer.Deserialize<List<Evne>>(json);
        }
    }
}
