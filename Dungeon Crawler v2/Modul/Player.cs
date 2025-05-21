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

        public Evne KlasseEvne { get; set; }

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

            KlasseEvne = klasse.KlasseEvneObjekt;
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

            if (EvneCooldown > 0) EvneCooldown--;

        }

        public void BrugKlasseEvne(Monster target)
        {
            Console.Clear();
            UIManager.CTop();
            
            if (EvneCooldown == 0)
            {
                Console.WriteLine($"{Navn} bruger {KlasseEvne.Navn}");

                damage = KlasseEvne.Styrke-target.Forsvar;
                if (damage < 0) damage = 0;

                if (KlasseEvne.EvneId == 0) //Fireball
                {
                    target.Liv -= damage;
                    if (damage <= 0) Console.WriteLine($"Angrebet prelede af på {target.Navn}");
                    else Console.WriteLine($"{target.Navn} tog {damage} af din {KlasseEvne.Navn}");
                    EvneCooldown = 4;
                }

                else if (KlasseEvne.EvneId == 1) //Dobbelt Slag
                {
                    PlayerAttack(target);
                    PlayerAttack(target);
                    EvneCooldown = 3;
                }

                else if (KlasseEvne.EvneId == 2) //første indtryk
                {
                    if (target.MaxLiv == target.Liv)
                    {
                        damage = 40 - target.Forsvar;
                        if (damage < 0) damage = 0;
                        target.Liv -= damage;

                        if (damage <= 0) Console.WriteLine($"På trods af overraskelsen, prælede dit angreb af op {target.Navn}");
                        else Console.WriteLine($"Du gør noget af et første indtryk på {target.Navn} og de tager {damage} skade");
                    }
                }
                else { Console.WriteLine("Du har ingen særlig evne");  }
            }
        
        
        
        
        
        
        }

    }
}
