using Dungeon_Crawler_v2.Modul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dungeon_Crawler_v2.Modul;

namespace Dungeon_Crawler_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            //Dungeon dungeon = new Dungeon();
            //dungeon.BuildStaticDungeon();

            menu.HovedMenu();
        }
    }
}
