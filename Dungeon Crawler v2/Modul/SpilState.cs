﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawler_v2.Modul
{
    internal class SpilState
    {
        public static Player AktivSpiller {  get; set; }
        public static Monster AktivMonster { get; set; }
        public static Room AktivRoom { get; set; }
        public static Dungeon Dungeon { get; set; }
    }
}
