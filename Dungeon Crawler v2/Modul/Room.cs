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
    internal class Room
    {
        private static Random rand = new Random();
        public int x, y;
        public string RoomDescription;
        public bool IsAccessible = true, Visited = false, IsExit = false, IsStartRoom = false;
        public Monster MonsterInRoom = null;

        public HashSet<string> Doors = new HashSet<string>();


        public Room(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Enter(Player spiller = null)
        {
            if (!Visited)
            {
                GenerateRandomDecription();
                RollForMonster();
                RollForItem();
                Visited = true;
            }
            Console.WriteLine(RoomDescription);

            if (MonsterInRoom != null)
            {
                Console.WriteLine("Et monster stirre på dig fra mørket...");
            }

        }

        private void GenerateRandomDecription()
        {
            string[] BaseDescriptions = new string[]
                {
                    "Et mørkt, fugtigt rum",
                    "Et rum fyldt med edderkoppespind",
                    "Et rum hvor væggende er dækket af mos",
                    "Et tomt og klamt kammer"
                };
            string[] SuffixDescriptions = new string[]
                {
                    ".",
                    ", du hører dryp fra loftet.",
                    ", du mærker kulde fra væggene.",
                    ", du føler noget bevæge sig i skyggerne."
                };
            RoomDescription = BaseDescriptions[rand.Next(BaseDescriptions.Length)] + SuffixDescriptions[rand.Next(SuffixDescriptions.Length)];

        }
        private void RollForMonster()
        {
            if (IsStartRoom) return;
            if (MonsterInRoom != null) return;

            int chance = rand.Next(100);
            if (chance < 25) // 25% chance
            {
                //MonsterInRoom = Monster.RandomMonster();
            }
        }
        private void RollForItem()
        {
            if (IsStartRoom) return;
            //more to come
        }
    }
}
