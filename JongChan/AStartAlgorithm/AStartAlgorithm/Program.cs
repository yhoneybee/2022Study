using System;
using System.Collections.Generic;

namespace AStartAlgorithm
{
    public class Node
    {
        public int X, Y;
        public int F, G, H;
        public bool isBlock;
        public Node parent;

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    internal class Program
    {
        private static Node[,] node = new Node[7, 5];
        private static Node startPos;
        private static Node endPos;
        private static Node curPos;

        private static int sellXSize = 7;
        private static int sellYSize = 5;

        private static List<Node> nodes = new List<Node>();
        private static List<Node> openNodes = new List<Node>();
        private static List<Node> closeNodes = new List<Node>();
        private static List<Node> finalNodes = new List<Node>();

        public static void Main(string[] args)
        {
            Setup();

            while (openNodes.Count > 0)
            {
                curPos = openNodes[0];
                
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (!nodes[i].isBlock)
                    {
                        curPos = nodes[i];
                    }
                }

                openNodes.Remove(curPos);
                closeNodes.Add(curPos);

                if (curPos.X == endPos.X && curPos.Y == endPos.Y)
                {
                    Node TargetCurNode = endPos;
                    while (TargetCurNode != startPos)
                    {
                        finalNodes.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.parent;
                    }

                    finalNodes.Add(startPos);
                    finalNodes.Reverse();

                    Console.WriteLine($"startPos : ({startPos.X},{startPos.Y})");
                    Console.WriteLine($"targetPos : ({endPos.X},{endPos.Y})");
                    
                    for (int i = 1; i < finalNodes.Count; i++)
                    {
                        Console.WriteLine($"{i} : ({finalNodes[i].X},{finalNodes[i].Y})");
                    }

                    return;
                }

                AddOpenList(curPos.X, curPos.Y + 1);
                AddOpenList(curPos.X + 1, curPos.Y);
                AddOpenList(curPos.X, curPos.Y - 1);
                AddOpenList(curPos.X - 1, curPos.Y);
            }
        }

        private static void Setup()
        {
            for (int y = 0; y < sellYSize; y++)
            {
                for (int x = 0; x < sellXSize; x++)
                {
                    node[x, y] = new Node(x, y);
                }
            }

            node[3, 1].isBlock = true;
            node[3, 2].isBlock = true;
            node[3, 3].isBlock = true;

            startPos = node[1, 2];
            endPos = node[5, 2];
            curPos = node[startPos.X, startPos.Y];
            
            openNodes.Add(startPos);
        }

        private static void AddOpenList(int X, int Y)
        {
            if (X >= 0 && X < sellXSize && Y >= 0 && Y < sellYSize && !node[X, Y].isBlock &&
                !closeNodes.Contains(node[X, Y]))
            {
                Node NeighborNode = node[X, Y];
                int MoveCost = curPos.G + (curPos.X - X == 0 || curPos.Y - Y == 0 ? 10 : 14);

                if (MoveCost < NeighborNode.G || !openNodes.Contains(NeighborNode))
                {
                    NeighborNode.G = MoveCost;
                    NeighborNode.H = (Math.Abs(NeighborNode.X - endPos.X) + Math.Abs(NeighborNode.Y - endPos.Y)) * 10;
                    NeighborNode.parent = curPos;
                    
                    openNodes.Add(NeighborNode);
                }
            }
        }
    }
}