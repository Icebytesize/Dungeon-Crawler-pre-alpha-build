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
        public static string StiTilKaraktere = Path.Combine(AppContext.BaseDirectory, "Modul", "Karaktere.json");
        public static string StiTilKlasser = Path.Combine(AppContext.BaseDirectory, "Modul", "SpilbareKlasser.json");
        public static string StiTilVåben = Path.Combine(AppContext.BaseDirectory, "Modul", "Våben.json");
        public static string StiTilEvner = Path.Combine(AppContext.BaseDirectory, "Modul", "evner.json");

        public static Player BootUpKarakter(Karakter karakter)
        {
            List<Karakter> karaktere = Karakter.HentKarakterer(StiTilKaraktere);
            List<Spilbareklasse> spilbareklasser = Spilbareklasse.HentKlasser(StiTilKlasser);
            List<Våben> våben = Våben.HentVåben(StiTilVåben);
            List<Evne> evner = Evne.HentEvner(StiTilEvner);

            Karakter valgtKarakter = karakter;
            Spilbareklasse klasse = spilbareklasser[valgtKarakter.KlasseId];
            Våben valgtVåben = våben[valgtKarakter.StartVåbenId];

            // 🔍 Matcher baseret på navn (du kan også bruge EvneId, hvis det er sikrere)
            var evne = evner.FirstOrDefault(e => e.Navn == klasse.SærligEvne);

            klasse.KlasseEvneObjekt = evne;

            var spiller = new Player(valgtKarakter, klasse, valgtVåben)
            {
                KlasseEvne = klasse.KlasseEvneObjekt // 🧠 Husk at tildele!
            };
           

            return new Player(valgtKarakter, klasse, valgtVåben);
        }
    }
}
