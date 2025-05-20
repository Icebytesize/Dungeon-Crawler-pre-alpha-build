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
    internal class Dungeon
    {

        public Dictionary<(int x, int y), Room> Rooms = new Dictionary<(int x, int y), Room>();
        public Room CurrentRoom, PreviousRoom;

        public void BuildStaticDungeon()
        {

            var StartRoom = new Room(-1, 0) { IsStartRoom = true, Visited = true, RoomDescription = "Dette rum starter du i." };
            var Room1 = new Room(0, 0);
            var Room2 = new Room(1, 0);
            var Room2L = new Room(1, -1);
            var Room2R = new Room(1, 1);
            var Room3L = new Room(2, -1);
            var Room3L2 = new Room(2, -2);
            var Room3R = new Room(2, 1);
            var Room3R2 = new Room(2, 2) { IsSafeHaven = true, RoomDescription = "Et trykt øjeblik." };
            var Room4 = new Room(3, 0);
            var Room4L = new Room(3, -1);
            var Room4R = new Room(3, 1);
            var Room5 = new Room(4, 0);
            var SlutRoom = new Room(5, 0) { IsExit = true };




            //læg Rum i dictinaryen
            Rooms[(StartRoom.x, StartRoom.y)] = StartRoom;
            Rooms[(Room1.x, Room1.y)] = Room1;
            Rooms[(Room2.x, Room2.y)] = Room2;
            Rooms[(Room2L.x, Room2L.y)] = Room2L;
            Rooms[(Room2R.x, Room2R.y)] = Room2R;
            Rooms[(Room3L.x, Room3L.y)] = Room3L;
            Rooms[(Room3L2.x, Room3L2.y)] = Room3L2;
            Rooms[(Room3R.x, Room3R.y)] = Room3R;
            Rooms[(Room3R2.x, Room3R2.y)] = Room3R2;
            Rooms[(Room4.x, Room4.y)] = Room4;
            Rooms[(Room4L.x, Room4L.y)] = Room4L;
            Rooms[(Room4R.x, Room4R.y)] = Room4R;
            Rooms[(Room5.x, Room5.y)] = Room5;
            Rooms[(SlutRoom.x, SlutRoom.y)] = SlutRoom;


            CurrentRoom = StartRoom;

            //Smid døre på rummene
            GenerateRoomDoors();
        }

        public Room GetRoomAt(int x, int y)
        {
            Rooms.TryGetValue((x, y), out Room room);
            return room;
        }

        public void GenerateRoomDoors()
        {
            foreach (var kvp in Rooms)
            {
                var room = kvp.Value;
                int x = room.x;
                int y = room.y;


                if (Rooms.ContainsKey((x + 1, y))) room.Doors.Add("1: Nord");
                if (Rooms.ContainsKey((x - 1, y))) room.Doors.Add("2: Syd");
                if (Rooms.ContainsKey((x, y + 1))) room.Doors.Add("3: Øst");
                if (Rooms.ContainsKey((x, y - 1))) room.Doors.Add("4: Vest");
            }
        }

        public void MovePlayer(string direction)
        {
            int NewX = CurrentRoom.x;
            int NewY = CurrentRoom.y;

            switch (direction.ToLower())
            {
                case "n": NewX += 1; break;
                case "s": NewX -= 1; break;
                case "e": NewY += 1; break;
                case "w": NewY -= 1; break;
            }

            Room NextRoom = GetRoomAt(NewX, NewY);
            if (NextRoom != null && NextRoom.IsAccessible)
            {
                PreviousRoom = CurrentRoom;
                CurrentRoom = NextRoom;
                NextRoom.Enter();
            }
            else Console.WriteLine("Du kan ikke gå den vej.");
        }

        public void DrawMap()
        {
            
            int MinX = Rooms.Keys.Min(k => k.x);
            int MaxX = Rooms.Keys.Max(k => k.x);
            int MinY = Rooms.Keys.Min(k => k.y);
            int MaxY = Rooms.Keys.Max(k => k.y);

            Console.WriteLine("Dungeon Kort:\n");

            for (int x = MaxX; x >= MinX; x--)
            {
                // 1. Top-linje
                for (int y = MinY; y <= MaxY; y++)
                {
                    if (Rooms.TryGetValue((x, y), out Room room) && room.Visited)
                    {
                        string top = "╔";
                        top += room.Doors.Contains("1:") ? " " : "═";
                        top += "╗";
                        Console.Write(top);
                    }
                    else Console.Write("   "); // Tomt felt
                }
                Console.WriteLine();

                // 2. Midterlinje
                for (int y = MinY; y <= MaxY; y++)
                {
                    if (Rooms.TryGetValue((x, y), out Room room) && room.Visited)
                    {
                        string left = room.Doors.Contains("4:") ? " " : "║";
                        string center = (room == CurrentRoom) ? "P" : " ";
                        string right = room.Doors.Contains("3:") ? " " : "║";
                        Console.Write(left + center + right);
                    }
                    else Console.Write("   "); // Tomt felt
                }
                Console.WriteLine();

                // 3. Bundlinje
                for (int y = MinY; y <= MaxY; y++)
                {
                    if (Rooms.TryGetValue((x, y), out Room room) && room.Visited)
                    {
                        string bottom = "╚";
                        bottom += room.Doors.Contains("2:") ? " " : "═";
                        bottom += "╝";
                        Console.Write(bottom);
                    }
                    else Console.Write("   "); // Tomt felt
                }
                Console.WriteLine(); // Ny linje efter række
            }

            Console.WriteLine("\nTryk en tast for at komme tilbage til menuen...");
            Console.ReadKey();
        }
    }
}
