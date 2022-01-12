using System;
using System.Collections.Generic;

//4방향 이동 A*
namespace AStartAlgorithm
{
    public class Node //노드 클래스
    {
        public int X, Y;
        public int F, G, H;
        public bool isBlock;
        public Node parent;

        public Node(int x, int y) //생성자 x, y값을 지정해준다
        {
            X = x;
            Y = y;
        }
    }

    internal class Program
    {
        private static Node[,] node;
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
                
                // 열린 리스트에서 가장 F가 작으면 열린리스트에서 닫힌리스트로 옮김
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (openNodes[i].F < curPos.F)
                    {
                        curPos = nodes[i];
                    }
                }
                
                openNodes.Remove(curPos);
                closeNodes.Add(curPos);

                //목표지점에 도착하면 콘솔에 메시지를 띄운다
                if (curPos.X == endPos.X && curPos.Y == endPos.Y)
                {
                    //parent를 역순으로 부르면 가는 길이 표시가 된다.
                    Node TargetCurNode = endPos;
                    while (TargetCurNode != startPos)
                    {
                        finalNodes.Add(TargetCurNode);
                        TargetCurNode = TargetCurNode.parent;
                    }

                    finalNodes.Add(startPos);
                    finalNodes.Reverse();

                    //콘솔창에 메시지 띄우기
                    Console.WriteLine($"startPos : ({startPos.X},{startPos.Y})");
                    Console.WriteLine($"targetPos : ({endPos.X},{endPos.Y})");
                    
                    for (int i = 1; i < finalNodes.Count; i++)
                    {
                        Console.WriteLine($"{i} : ({finalNodes[i].X},{finalNodes[i].Y})");
                    }

                    return;
                }

                // 양옆위아래를 openlist에 넣어준다
                AddOpenList(curPos.X, curPos.Y + 1);
                AddOpenList(curPos.X + 1, curPos.Y);
                AddOpenList(curPos.X, curPos.Y - 1);
                AddOpenList(curPos.X - 1, curPos.Y);
            }
        }

        private static void Setup() //setup이다
        {
            node = new Node[sellXSize, sellYSize];
            
            for (int y = 0; y < sellYSize; y++)
            {
                for (int x = 0; x < sellXSize; x++)
                {
                    node[x, y] = new Node(x, y);
                }
            }

            // 막힌곳을 설정해준다
            node[3, 1].isBlock = true;
            node[3, 2].isBlock = true;
            node[3, 3].isBlock = true;

            // 시작위치 끝나는 위치를 설정해준다
            startPos = node[1, 2];
            endPos = node[5, 2];
            curPos = node[startPos.X, startPos.Y];
            
            openNodes.Add(startPos);
        }

        private static void AddOpenList(int X, int Y) // 양옆위아래가 막혔는지 closeNode에 들어가있는지 체크하고 openlist에 넣어준다
        {
            if (X >= 0 && X < sellXSize && Y >= 0 && Y < sellYSize && !node[X, Y].isBlock &&
                !closeNodes.Contains(node[X, Y]))
            {
                //널어주려는 노드의 G값을 설정해준다
                Node NeighborNode = node[X, Y];
                int MoveCost = curPos.G + (curPos.X - X == 0 || curPos.Y - Y == 0 ? 10 : 14);

                //설정을 해주고 넣어준다.
                if (MoveCost < NeighborNode.G || !openNodes.Contains(NeighborNode))
                {
                    NeighborNode.G = MoveCost;
                    NeighborNode.H = (Math.Abs(NeighborNode.X - endPos.X) + Math.Abs(NeighborNode.Y - endPos.Y)) * 10;
                    NeighborNode.F = NeighborNode.G + NeighborNode.H;
                    NeighborNode.parent = curPos;
                    
                    openNodes.Add(NeighborNode);
                }
            }
        }
    }
}