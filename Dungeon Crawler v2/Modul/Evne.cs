using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler_v2.Modul
{
    internal class Evne
    {
        public int EvneId { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public int Styrke { get; set; }

        public Evne() { } //til Json desentralisering

    }
}
