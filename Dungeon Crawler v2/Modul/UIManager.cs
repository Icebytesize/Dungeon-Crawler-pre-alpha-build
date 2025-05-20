using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler_v2.Modul
{
    internal class UIManager
    {
        public static void OOCTop()
        {
            Player spiller = SpilState.AktivSpiller;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{spiller.Navn}");
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Liv: {spiller.Liv} / {spiller.MaxLiv}");
            Console.SetCursorPosition(1, 2);
            Console.WriteLine("[         ]");
            Console.SetCursorPosition(2, 2);
            for (int i = 1; i <= 10; i++)
            {
                if (spiller.Liv > (spiller.MaxLiv / 10) * i)
                {
                    Console.ForegroundColor = ConsoleColor.Green; //Altid grøn farve
                    if (spiller.Liv < (spiller.MaxLiv * 0.30))  //Tjekker først om liv er under 30% af max, hvis ja, gør farven rød
                    { Console.ForegroundColor = ConsoleColor.Red; }
                    else if (spiller.Liv < (spiller.MaxLiv * 0.60)) //Tjekker derefter om liv er under 60% af max, hvis ja, gør farven gul.
                    { Console.ForegroundColor = ConsoleColor.Yellow; }
                         
                    Console.Write("█");
                    Console.ResetColor();
                }
            }
            Console.WriteLine("\n");
        }
        public static void CTop()
        {
            Player spiller = SpilState.AktivSpiller;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{spiller.Navn}");
            Console.SetCursorPosition(0, 1);
            Console.WriteLine($"Liv: {spiller.Liv} / {spiller.MaxLiv}");
            Console.SetCursorPosition(1, 2);
            Console.WriteLine("[         ]");
            Console.SetCursorPosition(2, 2);
            for (int i = 1; i <= 10; i++)
            {
                if (spiller.Liv > (spiller.MaxLiv / 10) * i)
                {
                    Console.ForegroundColor = ConsoleColor.Green; //Altid grøn farve
                    if (spiller.Liv < (spiller.MaxLiv * 0.30))  //Tjekker først om liv er under 30% af max, hvis ja, gør farven rød
                    { Console.ForegroundColor = ConsoleColor.Red; }
                    else if (spiller.Liv < (spiller.MaxLiv * 0.60)) //Tjekker derefter om liv er under 60% af max, hvis ja, gør farven gul.
                    { Console.ForegroundColor = ConsoleColor.Yellow; }

                    Console.Write("█");
                    Console.ResetColor();
                }
            }
            /* AktivMonster skal lige implementeres inden det giver mening

             Monster monster = SpilState.AktivMonster;
             Console.SetCursorPosition(30, 0);
             Console.WriteLine($"{monster.Navn}");
             Console.SetCursorPosition(30, 1);
             Console.WriteLine($"Liv: {monster.Liv} / {monster.MaxLiv}");
             Console.SetCursorPosition(31, 2);
             Console.WriteLine("[         ]");
             Console.SetCursorPosition(32, 2);
             for (int i = 1; i <= 10; i++)
             {
                 if (monster.Liv > (monster.MaxLiv / 10) * i)
                 {
                     Console.ForegroundColor = ConsoleColor.Green;
                     if (monster.Liv < (monster.MaxLiv * 0.30))
                     { Console.ForegroundColor = ConsoleColor.Red; }
                     else if (monster.Liv < (monster.MaxLiv * 0.60))
                     { Console.ForegroundColor = ConsoleColor.Yellow; }


                     Console.Write("█");
                     Console.ResetColor();
                 }
             }
             Console.WriteLine("\n");
            */
        }
    }
}
