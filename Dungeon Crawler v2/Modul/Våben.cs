using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Våben
    {
        public string VåbenNavn {  get; set; }
        public string VåbenSærligEvne { get; set; }
        public int VåbenStyrke { get; set; } 
        public int VåbenForsvar { get; set; }
        public int VåbenId {  get; set; }
        public Våben() { } // Påkrævet fir JSON-Deserilisering

        public Våben(string våbenNavn, string våbensærligEvne, int våbenStyrke, int våbenForsvar, int våbenId)
        {
            VåbenNavn = våbenNavn;
            VåbenSærligEvne = våbensærligEvne;
            VåbenStyrke = våbenStyrke;
            VåbenForsvar = våbenForsvar;
            VåbenId = våbenId;
        }

        public void visInfo()
        {
            Console.Write($"Navn: {VåbenNavn}, Styrke: {VåbenStyrke} Forsvar: {VåbenForsvar}");
            if (VåbenSærligEvne != null)
            {
                Console.WriteLine($" Våben evne: {VåbenSærligEvne}");
            }
            else Console.WriteLine(".");
            
        }
        public static void GemVåben(List<Våben> våben, string sti)
        {
            string json = JsonSerializer.Serialize(våben, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(sti, json);
        }

        public static List<Våben> HentVåben(string sti)
        {
            if (!File.Exists(sti)) return new List<Våben>();
            string json = File.ReadAllText(sti);
            return JsonSerializer.Deserialize<List<Våben>>(json);
        }
    }
}
