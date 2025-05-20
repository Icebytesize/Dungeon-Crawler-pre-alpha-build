using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dungeon_Crawler_v2.Modul;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Player
    {

        public string Navn { get; set; }
        public string Klasse { get; set; }
        public string Våben { get; set; }
        public string SærligEvne { get; set; }

        public int MaxLiv { get; set; }
        public int Liv { get; set; }
        public int Styrke { get; set; }
        public int Forsvar { get; set; }
        public int EvneCooldown = 0;


        private int damage;



        public Player (Karakter karakter, Spilbareklasse klasse, Våben våben)
        {
            Navn = karakter.Navn;
            Klasse = klasse.KlasseNavn; 
            Våben = våben.VåbenNavn;

            MaxLiv = klasse.MaxLiv;
            Liv = klasse.Liv;
            Styrke = klasse.Styrke + våben.VåbenStyrke;
            Forsvar = klasse.Forsvar + våben.VåbenForsvar;
            SærligEvne = klasse.SærligEvne;


        }

        public void VisInfo()
        {
            Console.WriteLine($"Navn: {Navn}, Klasse: {Klasse}, Våben: {Våben}");
            Console.WriteLine($"Liv: {Liv}, Styrke: {Styrke}, Forsvar: {Forsvar}, Evne: {SærligEvne}");
        }

        public void PlayerAttack(Monster target)
        {
            damage = Styrke - target.Forsvar;
            if (damage < 0) damage = 0;

            target.Liv -= damage;
            if (damage <= 0) Console.WriteLine($"Angrebet prelede af på {target.Navn}");
            else Console.WriteLine($"{target.Navn} tog {damage} af dit angreb");

        }

        public void Evne(Monster target)
        { 
        
          
        
        
        
        
        
        
        }

    }
}
