using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Dungeon_Crawler_v2.Modul;
using System.IO;
using System.Text.Json;

namespace Dungeon_Crawler_v2.Modul
{
    public class Karakter
    {

        public string Navn { get; set; }
        public int KlasseId { get; set; }
        public int StartVåbenId { get; set; }
        


        public Karakter() {} // Påkrævet fir JSON-Deserilisering

        public Karakter(string navn, int klasseId, int startvåbenId)
        {
            Navn = navn;
            KlasseId = klasseId;
            StartVåbenId = startvåbenId;
        }

        public void VisInfo()
        {
            string StiTilKlasser = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\SpilbareKlasser.json";
            List<Spilbareklasse> spilbareklasser = Spilbareklasse.HentKlasser(StiTilKlasser);

            string StiTilVåben = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\Våben.json";
            List<Våben> våben = Våben.HentVåben(StiTilVåben);

            Console.WriteLine($"Navn: {Navn}, Klasse: {spilbareklasser[KlasseId].KlasseNavn}, Våben: {våben[StartVåbenId].VåbenNavn}");
            
        }
    

        public static void GemKarakterer(List<Karakter> karakterer, string sti)
        {
            string json = JsonSerializer.Serialize(karakterer, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(sti, json);
        }

        public static List<Karakter> HentKarakterer(string sti)
        {
            if (!File.Exists(sti)) return new List<Karakter>();
            string json = File.ReadAllText(sti);
            return JsonSerializer.Deserialize<List<Karakter>>(json);
        }
    }
}
