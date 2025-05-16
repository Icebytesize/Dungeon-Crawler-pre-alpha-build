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
    public class Menu
    {
        int input;
        bool GyldigtInput = false;
        List<string> HovedMenuMuligheder = new List<string>{ "Start spil", "Vælg karakter", "Afslut spil" };
        
        

        public void HovedMenu() //Hovedmenu funktionellitete
        {
            var actions = new List<Action> { SpilMenu, KarakterMenu, AfslutSpil }; //Ting der kan ske fra hovedmenuen

            Console.WriteLine("Velkommen til det farlige fangehul");
            while (true) //While løkke til ikke at lukke programmet, hvis indput ikke er forstået
            {
                Console.WriteLine("Du har nu følgende muligheder");

                for (int i = 0; i < HovedMenuMuligheder.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {HovedMenuMuligheder[i]}");
                }

                Console.Write("> ");
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
            //Kommer snart
        }
        public void KarakterMenu()
        {
            Console.Clear();
            Console.WriteLine("Velkommen til karakter menuen, her kan du vælge hvilken karakter du vil spille som");

            string StiTilKaraktere = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\Karaktere.json";
            List<Karakter> karaktere = Karakter.HentKarakterer(StiTilKaraktere);
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
                    Console.Write(karaktere[i].Klasse);
                    Console.SetCursorPosition(40, i + 3);
                    Console.WriteLine(karaktere[i].StartVåben);
                }
                Console.WriteLine("\n99: Opret ny karakter");
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out int input)) //Tjekker om indputtet er et tal.
                {
                    int ValgtIndex = input - 1;
                    if (ValgtIndex >= 0 && ValgtIndex < karaktere.Count)
                    {
                        Console.Clear();
                        karaktere[ValgtIndex].VisInfo(); // Viser information om den valgt karakter
                        Console.WriteLine("\nTryk på en tast for at vende tilbage til hovedmenuen.");
                        Console.ReadKey();
                        break;

                    }
                    else if (ValgtIndex == 99)
                    {
                        OpretKarakterMenu();
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
            string StiTilKlasser = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\SpilbarKlasser.json";
            List<Spilbareklasse> spilbareklasser = Spilbareklasse.HentKlasser(StiTilKlasser);

            Console.Clear();
            Console.Write("Giv din karakter en navn\n> ");
            string NyKarakterNavn = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Hvilken klasse skal din karakter have");
            for (int i = 0; i > spilbareklasser.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {spilbareklasser[i].KlasseNavn}");
            }
            Console.Write("> ");
            
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
