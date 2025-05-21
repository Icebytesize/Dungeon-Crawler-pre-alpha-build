using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Dungeon_Crawler_v2.Modul;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Monster
    {
        
        public static readonly string StiTilMonstre = Path.Combine(AppContext.BaseDirectory, "Modul", "monstre.json");

        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public string SærligEvne { get; set; }
        public string Evnebeskrivelse { get; set; }

        public int MonsterId { get; set; }
        public int MaxLiv { get; set; }
        public int Liv { get; set; }
        public int Styrke { get; set; }
        public int Forsvar { get; set; }
        public int EvneCooldown;


        private int damage;

        public Monster() { } //Tom konstruktor kræves for JSON deserialisering

        public Monster(string navn, string beskrivelse, string særligEvne, string evnebeskrivelse, int maxLiv, int liv, int styrke, int forsvar, int monsterId)
        {
            Navn = navn;
            Beskrivelse = beskrivelse;
            SærligEvne = særligEvne;
            Evnebeskrivelse = evnebeskrivelse;
            MaxLiv = maxLiv;
            Liv = liv;
            Styrke = styrke;
            Forsvar = forsvar;
            MonsterId = monsterId;
        }

        public void visInfo()
        {
            Console.Write($"Navn: {Navn}, Liv: {MaxLiv}, Styrke: {Styrke} Forsvar: {Forsvar}, Særlig Evne {SærligEvne}.");
        }

        public void MonsterAttack(Player target)
        {
            damage = Styrke - target.Forsvar;
            if ( damage < 0 ) damage = 0;

            target.Liv -= damage;
            if (damage <= 0) Console.WriteLine($"{Navn}'s Angreb prelede af på dig");
            else Console.WriteLine($"Du tog {damage} skade af {Navn}'s angreb");
        }

        public static List<Monster> HentMonstre(string sti)
        {
            if (!File.Exists(sti)) return new List<Monster>();
            string json = File.ReadAllText(sti);
            return JsonSerializer.Deserialize<List<Monster>>(json);
        }

        public static List<Monster> monsterListe { get; } = HentMonstre(StiTilMonstre);

        public static Monster RandomMonster(List<Monster> monsterListe)
        {
            if (monsterListe == null || monsterListe.Count == 0) return null;
            Random rnd = new Random();
            return monsterListe[rnd.Next(monsterListe.Count)];
        }


    }
}
