using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstarProject
{
    class Location
    {
        //좌표값
        public int X;
        public int Y;  
        //출발점으로부터의 거리
        public int G;
        //목적지로부터의 예상 거리
        public int H;
        //G+H
        public int F;
        public Location Parent;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "A* Pathfinding";

            // 맵그림  
            string[] map = new string[]
            {
                "+------------+",
                "|   X        |",
                "|    A  X   B|",
                "|      X     |",
                "|            |",
                "|            |",
                "|            |",
                "+------------+",
            };
            var start = new Location { X = 5, Y = 2 };
            var target = new Location { X = 12, Y = 2 };

            int SLEEP_TIME = 100;

            foreach (var line in map)
                Console.WriteLine(line);

            // 알고리즘  
            Location current = null;
            //고려중인 타일
            var openList = new List<Location>();
            //방문한 타일
            var closedList = new List<Location>();
            //Location의 G 값이 될 변수/새 타일로 이동할 때마다 값을 증가시킴
            int g = 0;

            //openList에 시작 위치를 추가
            openList.Add(start);

            while (openList.Count > 0)
            {
                // get the square with the lowest F score  
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                // add the current square to the closed list  
                closedList.Add(current);

                // show current square on the map  
                Console.SetCursorPosition(current.X, current.Y);
                Console.Write('.');
                Console.SetCursorPosition(current.X, current.Y);
                System.Threading.Thread.Sleep(SLEEP_TIME);

                // remove it from the open list  
                openList.Remove(current);

                // if we added the destination to the closed list, we've found a path  
                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map, openList);
                g = current.G + 1;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    // if this adjacent square is already in the closed list, ignore it  
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) != null)
                        continue;

                    // if it's not in the open list...  
                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                        && l.Y == adjacentSquare.Y) == null)
                    {
                        // compute its score, set the parent  
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        // and add it to the open list  
                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        // test if using the current G score makes the adjacent square's F score  
                        // lower, if yes update the parent because it means it's a better path  
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }

            Location end = current;

            // assume path was found; let's show it  
            while (current != null)
            {
                Console.SetCursorPosition(current.X, current.Y);
                Console.Write('_');
                Console.SetCursorPosition(current.X, current.Y);
                current = current.Parent;
                System.Threading.Thread.Sleep(SLEEP_TIME);
            }

            if (end != null)
            {
                Console.SetCursorPosition(0, 20);
                Console.WriteLine("Path : {0}", end.G);
            }

            // 끝
            Console.ReadLine();
        }

        static List<Location> GetWalkableAdjacentSquares(int x, int y, string[] map, List<Location> openList)
        {
            List<Location> list = new List<Location>();

            if (map[y - 1][x] == ' ' || map[y - 1][x] == 'B')
            {
                Location node = openList.Find(l => l.X == x && l.Y == y - 1);
                if (node == null) list.Add(new Location() { X = x, Y = y - 1 });
                else list.Add(node);
            }

            if (map[y + 1][x] == ' ' || map[y + 1][x] == 'B')
            {
                Location node = openList.Find(l => l.X == x && l.Y == y + 1);
                if (node == null) list.Add(new Location() { X = x, Y = y + 1 });
                else list.Add(node);
            }

            if (map[y][x - 1] == ' ' || map[y][x - 1] == 'B')
            {
                Location node = openList.Find(l => l.X == x - 1 && l.Y == y);
                if (node == null) list.Add(new Location() { X = x - 1, Y = y });
                else list.Add(node);
            }

            if (map[y][x + 1] == ' ' || map[y][x + 1] == 'B')
            {
                Location node = openList.Find(l => l.X == x + 1 && l.Y == y);
                if (node == null) list.Add(new Location() { X = x + 1, Y = y });
                else list.Add(node);
            }

            return list;
        }
        //H값을 구하는 함수 / 장애물을 모두 무시하고 거리를 수평/수직으로 구함
        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}