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
    public class Menu
    {
        int input;
        bool GyldigtInput = false;
        List<string> HovedMenuMuligheder = new List<string>{ "Start spil", "Vælg karakter", "Afslut spil" };
        string valgtkarakter;
        
        public string StiTilKaraktere = Path.Combine(AppContext.BaseDirectory, "Modul", "Karaktere.json");
        public string StiTilKlasser = Path.Combine(AppContext.BaseDirectory, "Modul", "SpilbareKlasser.json");
        public string StiTilVåben = Path.Combine(AppContext.BaseDirectory, "Modul", "Våben.json");
        public string StiTilEvner = Path.Combine(AppContext.BaseDirectory, "Modul", "evner.json");
        
       

        public void HovedMenu() //Hovedmenu funktionellitete
        {
            var actions = new List<Action> { SpilMenu, KarakterMenu, AfslutSpil }; //Ting der kan ske fra hovedmenuen

            
            
            while (true) //While løkke til ikke at lukke programmet, hvis indput ikke er forstået
            {
                UIManager.OOCTop();
                Console.WriteLine("Du har nu følgende muligheder");

                for (int i = 0; i < HovedMenuMuligheder.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {HovedMenuMuligheder[i]}");
                }

                Console.Write("\n> ");
                if (int.TryParse(Console.ReadLine(), out int input)) //Tjekker om indputtet er et tal.
                {
                    int ValgtIndex = input - 1;
                    if (ValgtIndex >= 0 && ValgtIndex < actions.Count)
                    {
                        Console.Clear();
                        actions[ValgtIndex](); // Kalder det punkt som er ønsket, fx afslut spil
                        
                    }
                    else
                    {
                        FejlBesked();
                    }
                }

                else
                {
                    FejlBesked();
                }

            }
        }


        public void SpilMenu()
        {
            if (SpilState.AktivSpiller == null) //Til hvis man på magiskvis får fravalgt sin karakter
            {
                Console.WriteLine("Du skal vælge en karakter, før du kan starte spillet!");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            
            SpilState.Dungeon = new Dungeon();
            SpilState.Dungeon.BuildStaticDungeon();
            SpilState.Dungeon.CurrentRoom.Enter();



        }
        public void KarakterMenu()
        {
            List<Karakter> karaktere = Karakter.HentKarakterer(StiTilKaraktere);

            List<Spilbareklasse> spilbareklasser = Spilbareklasse.HentKlasser(StiTilKlasser);

            List<Våben> våben = Våben.HentVåben(StiTilVåben);

            List<Evne> evner = Evne.HentEvner(StiTilEvner);

            Console.Clear();
            Console.WriteLine("Velkommen til karakter menuen, her kan du vælge hvilken karakter du vil spille som");
            while (true)
            {
                Console.WriteLine("Du kan vælge mellem følgende karaktere eller oprette en ny karakter");
                Console.SetCursorPosition(3, 2);
                Console.WriteLine("Navn:");
                Console.SetCursorPosition(30, 2);
                Console.WriteLine("Klasse:");
                Console.SetCursorPosition(40, 2);
                Console.WriteLine("Våben:");


                for (int i = 0; i < karaktere.Count; i++)
                {
                    Console.SetCursorPosition(0, i + 3);
                    Console.Write($"{i + 1}: {karaktere[i].Navn}");
                    Console.SetCursorPosition(30, i + 3);
                    Console.Write(spilbareklasser[karaktere[i].KlasseId].KlasseNavn);
                    Console.SetCursorPosition(40, i + 3);
                    Console.WriteLine(våben[karaktere[i].StartVåbenId].VåbenNavn);
                }
                Console.WriteLine("\n99: Opret ny karakter\n100: Afslut");
                Console.Write("\n> ");
                if (int.TryParse(Console.ReadLine(), out int input)) //Tjekker om indputtet er et tal.
                {
                    int ValgtIndex = input - 1;
                    if (ValgtIndex >= 0 && ValgtIndex < karaktere.Count)
                    {
                        Karakter valgt = karaktere[ValgtIndex];
                        Spilbareklasse klasse = spilbareklasser[valgt.KlasseId];
                        Våben valgtVåben = våben[valgt.StartVåbenId];

                        SpilState.AktivSpiller = SpilData.BootUpKarakter(valgt);
                        
                        
                        Console.Clear();
                        SpilState.AktivSpiller.VisInfo(); // Viser information om den valgt karakter
                        Console.WriteLine("Karakteren er valgt og gemt som aktic spiller.");
                        Console.WriteLine("\nTryk på en tast for at vende tilbage til hovedmenuen.");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    }
                    else if (input == 99)
                    {
                        OpretKarakterMenu();
                        Console.Clear();
                        break;
                    }
                    else if (input == 100)
                    {
                        Console.WriteLine("Tryk på en tast for at vende tilbage til hovedmenuen.");
                        Console.ReadKey();
                        Console.Clear();
                        break;

                    }
                    else
                    {
                        FejlBesked();
                    }
                }

            }

        }
        public void OpretKarakterMenu()
        {
            List<Karakter> karaktere = Karakter.HentKarakterer(StiTilKaraktere);

            List<Spilbareklasse> spilbareklasser = Spilbareklasse.HentKlasser(StiTilKlasser);

            List<Våben> våben = Våben.HentVåben(StiTilVåben);

            Console.Clear();
            Console.Write("Giv din karakter en navn\n> ");
            string NyKarakterNavn = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Hvilken klasse skal din karakter have");
            for (int i = 0; i < spilbareklasser.Count; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.WriteLine($"{i + 1}: {spilbareklasser[i].KlasseNavn}");
                Console.SetCursorPosition(15, i + 1);
                Console.WriteLine($"Liv: {spilbareklasser[i].MaxLiv}");
                Console.SetCursorPosition(30, i + 1);
                Console.WriteLine($"Styrke: {spilbareklasser[i].Styrke}");
                Console.SetCursorPosition(45, i + 1);
                Console.WriteLine($"Forsvar: {spilbareklasser[i].Forsvar}");
            }
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int NyKlasseId);
            NyKlasseId = NyKlasseId - 1;


            Console.Clear();
            Console.WriteLine("Hvilket våben skal din karakter starte med");
            for (int i = 0; i < våben.Count; i++)
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write($"{i + 1}: {våben[i].VåbenNavn} ");
                Console.SetCursorPosition(20, i + 1);
                Console.Write($"Styrke: {våben[i].VåbenStyrke}");
                Console.SetCursorPosition(35, i + 1);
                Console.WriteLine($"forsvar: {våben[i].VåbenForsvar}");
            }
            Console.Write("> ");
            int.TryParse(Console.ReadLine(), out int NyVåbenId);
            NyVåbenId = NyVåbenId - 1;
            

            Console.Clear();
            Console.Write($"Ønsker du at oprette denne karakter\nNavn: {NyKarakterNavn}\nKlasse: {spilbareklasser[NyKlasseId].KlasseNavn}\nVåben: {våben[NyVåbenId].VåbenNavn}\n\nLiv: {spilbareklasser[NyKlasseId].MaxLiv}\nStyrke: {spilbareklasser[NyKlasseId].Styrke + våben[NyVåbenId].VåbenStyrke}\nForsvar: {spilbareklasser[NyKlasseId].Forsvar + våben[NyVåbenId].VåbenForsvar}\nEvne: {spilbareklasser[NyKlasseId].SærligEvne}\n\n1: Opret\n2: Fortryd\n\n> ");
            if (int.TryParse(Console.ReadLine(), out int input) && input == 1) //Tjekker om indputtet er et tal.
            {
                // Gemmer karakteren og opretter den i JSON filen
                Console.Clear();
                Karakter nyKarakter = new Karakter(NyKarakterNavn, spilbareklasser[NyKlasseId].KlasseId, våben[NyVåbenId].VåbenId);
                karaktere.Add(nyKarakter);
                Karakter.GemKarakterer(karaktere, StiTilKaraktere);

                // Sætter nyoprette karakter som aktive karakter
                Spilbareklasse klasse = spilbareklasser[NyKlasseId];
                Våben valgtVåben = våben[NyVåbenId];
                SpilState.AktivSpiller = SpilData.BootUpKarakter(nyKarakter);


                //besled
                Console.WriteLine("Din karakter oprettet, gemt og valgt.\nDu vil nu blive sendt tilbage til Hovedmenuen");
                Console.ReadKey();
                Console.Clear();
            }
            else if (int.TryParse(Console.ReadLine(), out input) && input == 2) //Tjekker om indputtet er et tal.
            {
                Console.Clear();
                Console.WriteLine("Du vil nu blive sendt tilbage til hovedmenuen");
                Console.ReadKey();
                Console.Clear();
            }
            else
            {
                FejlBesked();
            }


        }
        public void KampMenu()
        {
            while (SpilState.AktivSpiller.Liv > 0 && SpilState.AktivMonster.Liv > 0)
            {
                GyldigtInput = false;
                Console.Clear();

                while (!GyldigtInput)
                {
                    UIManager.CTop();
                    Console.Write("\n1: Angrib\n2: Item\n\n> ");
                    int.TryParse(Console.ReadLine(), out input);
                    if (input == 1) 
                    {
                        while (!GyldigtInput)
                        {
                            Console.Clear();
                            UIManager.CTop();
                            Console.Write($"\n1: Standard Angreb\n2: {SpilState.AktivSpiller.SærligEvne} ({(SpilState.AktivSpiller.EvneCooldown == 0 ? "Brugbar" : "Ikke brugbar")})\n\n> ");

                            int.TryParse(Console.ReadLine(), out input);

                            if (input == 1)
                            { SpilState.AktivSpiller.PlayerAttack(SpilState.AktivMonster); GyldigtInput = true; }

                            else if (input == 2)
                            { SpilState.AktivSpiller.BrugKlasseEvne(SpilState.AktivMonster); GyldigtInput = true; }
                        }
                    }

                    else if (input == 2) 
                    { 
                        // Items er ikk implemteret endnu
                        Console.WriteLine("Items er ikke implemteret endnu, vælg noget andet");
                    }
                   
                    else { Console.WriteLine("Input ikke forstået prøv igen"); Console.ReadKey(); } 

                }
                SpilState.AktivMonster.MonsterAttack(SpilState.AktivSpiller);
                Console.ReadKey();
            }
        }
        public void AfslutSpil() //Til Afslutning af program
        {
            Console.Clear();
            Console.WriteLine("Tak for idag, vi ses en anden gang");
            Console.ReadKey();
            Environment.Exit(0);
        }
        public void FejlBesked() //TilFejlmeddeleser
        {
            Console.WriteLine("Input ikke forstået, tryk på enter for at blive sendt tilbage til menuen");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
