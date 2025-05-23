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
        Dungeon dungeon = new Dungeon();
        Menu menu = new Menu();
        private static Random rand = new Random();
        public int x, y;
        public string RoomDescription;
        public bool IsAccessible = true, Visited = false, IsExit = false, IsStartRoom = false, IsSafeHaven = false;
        public Monster MonsterInRoom = null; 
        public Item ItemInRoom1 = null, ItemInRoom2 = null;

        public HashSet<string> Doors = new HashSet<string>();


        public Room(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void Enter(Player spiller = null)
        {
        bool valgtGyldigt = false;
            Console.Clear();
            if (!Visited)
            {
                GenerateRandomDecription();
                RollForMonster();
                RollForItem();
                Visited = true;
            }

            if (MonsterInRoom != null)
            {
              1
                while (!valgtGyldigt)
                {
                    Console.Clear();
                    UIManager.CTop();
                    Console.WriteLine($"{SpilState.AktivMonster.Beskrivelse}");
                    Console.Write("\n1: Kæmp\n2: Stik af\n\n> ");
                    int.TryParse(Console.ReadLine(), out int input);
                    if (input == 1) 
                    { 
                        valgtGyldigt = true;
                        menu.KampMenu(); 
                    }
                    
                    else if (input == 2) 
                    { 
                        valgtGyldigt = true;
                        SpilState.Dungeon.CurrentRoom = SpilState.Dungeon.PreviousRoom;
                        Console.Write("Du flygter tilbage til det forrige rum..");
                        Console.ReadKey();
                        SpilState.Dungeon.CurrentRoom.Enter();
                    }
                    
                    else 
                    { 
                    Console.WriteLine("Ugyldigt valgt. Prøv igen.");
                    }
                }
                valgtGyldigt = false;

            }
            
            while (!valgtGyldigt)
            { 
                Console.Clear();
                UIManager.OOCTop();
                Console.WriteLine(RoomDescription);
                if (IsExit) { menu.EndState(); }
                if (ItemInRoom1 != null || ItemInRoom2 != null)
                {
                    Console.Write($"\nDu finder og samler ");

                    if (ItemInRoom1 != null)
                    { Console.Write($"en {ItemInRoom1.Navn}"); SpilState.AktivSpiller.TilføjItem(ItemInRoom1);  }

                    if (ItemInRoom1 != null && ItemInRoom2 != null)
                    {
                        if (ItemInRoom1.ItemId != ItemInRoom2.ItemId)
                        { Console.Write(" & en " + ItemInRoom2.Navn); SpilState.AktivSpiller.TilføjItem(ItemInRoom2); }
                        else
                        { Console.Write(" & en til"); SpilState.AktivSpiller.TilføjItem(ItemInRoom2); }
                    }
                    else if (ItemInRoom2 != null)
                    {
                        Console.Write($"en {ItemInRoom2.Navn}"); SpilState.AktivSpiller.TilføjItem(ItemInRoom2); 
                    }
                    Console.WriteLine(" op");
                    ItemInRoom1 = null; ItemInRoom2 = null;
                }

                    Console.Write("\nDu har følgende muligheder\n\n1: Flyt rum\n2: Rygsæk\n3: Se kort\n\n> ");
                
                int.TryParse(Console.ReadLine(), out int input);

                if (input == 1) 
                {
                    Console.Clear();
                    UIManager.OOCTop();
                    foreach (var Door in Doors)
                    {
                      
                        Console.WriteLine($"{Door}");
                       
                    }
                    Console.WriteLine("\n6: Tilbage\n");
                    Console.Write("> ");
                    int.TryParse(Console.ReadLine(), out input);
                    switch (input)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            if (SpilState.Dungeon.MovePlayer(input))
                                valgtGyldigt = true;
                            break;

                        case 6:
                            break;

                        default:
                            Console.WriteLine("Ukendt kommando, prøv igen");
                            break;
                    }


                    
                }
                else if (input == 2) 
                {
                    Console.Clear();
                    UIManager.OOCTop();
                    SpilState.AktivSpiller.BrugItem();

                    Console.ReadKey();
                }
                
                else if(input == 3) 
                {
                    SpilState.Dungeon.DrawMap();
                }
                else { Console.WriteLine("Input ikke forstået, prøv igen"); }
            }
        }

        private void GenerateRandomDecription()
        {
            if (IsSafeHaven) return;
            if (IsExit) return;
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
            if (IsSafeHaven) return;
            if (IsExit) return;
            if (MonsterInRoom != null) return;

            int chance = rand.Next(100);
            if (chance < 25) // 25% chance
            {
                MonsterInRoom = Monster.RandomMonster(Monster.monsterListe);
                SpilState.AktivMonster = MonsterInRoom;
            }
        }
        private void RollForItem()
        {
            if (IsStartRoom) return;
            if (IsSafeHaven) return;
            if (IsExit) return;

            int chance = rand.Next(100);
            if (chance < 25)
            {
                ItemInRoom1 = Item.RandomItem(Item.items);
                
            }
            chance = rand.Next(100);
            {
                ItemInRoom2 = Item.RandomItem(Item.items);
                
            }

        }
    }
}
