using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dungeon_Crawler_v2.Modul
{
    internal class SpilData
    {
        public static string StiTilKarakterer = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\Karaktere.json";
        public static string StiTilKlasser = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\SpilbareKlasser.json";   
        public static string StiTilVåben = "C:\\Users\\maul\\source\\repos\\Dungeon Crawler v2\\Dungeon Crawler v2\\Modul\\Våben.json";


        public static Player BootUpKarakter(Karakter karakter)
        {
            List<Karakter> karaktere = Karakter.HentKarakterer(StiTilKarakterer);
            List<Spilbareklasse> spilbareklasser = Spilbareklasse.HentKlasser(StiTilKlasser);
            List<Våben> våben = Våben.HentVåben(StiTilVåben);

            Karakter valgtKarakter = karaktere[0];
            Spilbareklasse klasse = spilbareklasser[valgtKarakter.KlasseId];
            Våben valgtVåben = våben[valgtKarakter.StartVåbenId];

            return new Player(valgtKarakter, klasse, valgtVåben);
        }
    }
}
