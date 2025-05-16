using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Spilbareklasse
    {
        public string KlasseNavn, SærligEvne;
        public int MaxLiv, Liv, Styrke, Forsvar;

        public Spilbareklasse() { } // Påkrævet fir JSON-Deserilisering

        public Spilbareklasse(string klasseNavn, string særligEvne, int maxLiv, int liv, int styrke, int forsvar)
        {
            KlasseNavn = klasseNavn;
            SærligEvne = særligEvne;
            MaxLiv = maxLiv;
            Liv = liv;
            Styrke = styrke;
            Forsvar = forsvar;
        }

        public void visInfo()
        {
            Console.Write($"Klasse: {KlasseNavn}, Liv: {MaxLiv}, Styrke: {Styrke} Forsvar: {Forsvar}, Særlig Evne {SærligEvne}, ");
        }
        public static void GemKlasser(List<Spilbareklasse> klasser, string sti)
        {
            string json = JsonSerializer.Serialize(klasser, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(sti, json);
        }

        public static List<Spilbareklasse> HentKlasser(string sti)
        {
            if (!File.Exists(sti)) return new List<Spilbareklasse>();
            string json = File.ReadAllText(sti);
            return JsonSerializer.Deserialize<List<Spilbareklasse>>(json);
        }
    }
}
