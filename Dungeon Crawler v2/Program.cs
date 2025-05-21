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
            List<Karakter> karaktere = Karakter.HentKarakterer(SpilData.StiTilKaraktere);
            SpilState.AktivSpiller = SpilData.BootUpKarakter(karaktere[0]);

            Menu menu = new Menu(); 
            menu.HovedMenu();
        }
    }
}
