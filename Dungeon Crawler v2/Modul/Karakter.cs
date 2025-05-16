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
        public string Klasse { get; set; }
        public string StartVåben { get; set; }
        


        public Karakter() {} // Påkrævet fir JSON-Deserilisering

        public Karakter(string navn, string klasseNavn, string startvåben)
        {
            Navn = navn;
            Klasse = klasseNavn;
            StartVåben = startvåben;
        }

        public void VisInfo()
        {
            Console.WriteLine($"Navn: {Navn}, Klasse: {Klasse}, Våben: {StartVåben}");
            
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
