using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Program
    {
        static void Main(string[] args)
        {
            //init map
            var map = new Node[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = new Node()
                    {
                        X = i,
                        Y = j
                    };
                }
            }

            //init wall
            map[1, 5].IsWall = true;
            map[2, 5].IsWall = true;
            map[3, 5].IsWall = true;
            map[4, 5].IsWall = true;
            map[5, 5].IsWall = true;



            //Start Point [2, 1]
            //End Point   [4, 7]

            var startNode = map[2, 1];
            var endNode = map[4, 7];

            var open = new List<Node>();
            var close = new List<Node>();

            open.Add(startNode);

            while (open.Count != 0)
            {
                var node = MinGInNodes(open);
                open.Remove(node);
                close.Add(node);

                if(node == endNode)
                    break;

                var nextNodes = NextNodes(map, node);
                foreach (var nextNode in nextNodes)
                {
                    nextNode.G = Node.Distance(nextNode, node);
                    nextNode.H = Node.Distance(nextNode, endNode);

                    if (open.Contains(nextNode))
                    {
                        
                    }

                    if (!close.Contains(nextNode))
                    {
                        open.Add(nextNode);
                    }
                }
                
            }

            //map
            /*
             * 0 0 0 0 0 0 0 0 0 0
             * 0 0 0 0 0 1 0 0 0 0
             * 0 A 0 0 0 1 0 0 0 0
             * 0 0 0 0 0 1 0 0 0 0
             * 0 0 0 0 0 1 0 B 0 0
             * 0 0 0 0 0 1 0 0 0 0
             * 0 0 0 0 0 0 0 0 0 0
             * 0 0 0 0 0 0 0 0 0 0
             * 0 0 0 0 0 0 0 0 0 0 
             * 0 0 0 0 0 0 0 0 0 0 
             */

            Node pNode = endNode;
            do
            {
                pNode = pNode.ParentNode;

                if (pNode != null)
                    Console.WriteLine(pNode.X + " " + pNode.Y);
            } while (pNode != null);


            //Map map = new Map();
            //AStarNode2D start = new AStarNode2D(2, 2, map);
            //AStarNode2D goal = new AStarNode2D(6, 2, map);
            //AStarEngine engine = new AStarEngine();
            //ArrayList solution = new ArrayList();

            //if (engine.Execute(start, goal))
            //{
            //    AStarNode2D node = (AStarNode2D)engine.ResultNode;
            //    while (node != null)
            //    {
            //        solution.Insert(0, node);
            //        node = (AStarNode2D)node.Parent;
            //    }

            //    Console.WriteLine("Path found:");
            //    for (int i = 0; i < 10; i++)
            //    {
            //        for (int j = 0; j < 10; j++)
            //        {
            //            if (map.GetMapData(j, i) == -1)
            //                Console.Write("X");
            //            else
            //            {
            //                AStarNode2D nt = new AStarNode2D(j, i, map);
            //                bool inSolution = false;

            //                foreach (AStarNode2D n in solution)
            //                {
            //                    if (n.EqualTo(nt))
            //                    {
            //                        inSolution = true;
            //                        break;
            //                    }
            //                }

            //                if (inSolution) Console.Write("S");
            //                else Console.Write(".");
            //            }
            //        }

            //        Console.WriteLine();
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("Unable to find a path.");
            //}

            //Console.ReadLine(); 
        }

        private static int G(Node startNode, Node endNode, Node currentNode)
        {
            return Node.Distance(startNode, currentNode) + Node.Distance(endNode, currentNode);
        }

        private static Node MinGInNodes(List<Node> nodes)
        {
            var minNode = nodes.OrderBy(node => node.F).FirstOrDefault();
            return minNode;
        }

        private static IEnumerable<Node> NextNodes(Node[,] map, Node node)
        {
            var nodeList = new List<Node>();
            var x = node.X;
            var y = node.Y;

            //arroud Nodes
            var indexs = new int[]
            {
                x - 1, y - 1,
                x - 1, y,
                x - 1, y + 1,
                x, y - 1,
                x, y + 1,
                x + 1, y - 1,
                x + 1, y,
                x + 1, y + 1

            };

            for (int i = 0; i < 8; i++)
            {
                var hitNode = HitNode(map, indexs[i * 2], indexs[i * 2 + 1]);
                if (hitNode != null)
                    nodeList.Add(hitNode);
            }

            return nodeList;
        }

        private static Node HitNode(Node[,] map, int x, int y)
        {
            if (x < 10 && x >= 0 && y < 10 && y >= 0 && !map[x, y].IsWall)
            {
                return map[x, y];
            }
            return null;
        }
    }

    public class Node
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int F
        {
            get
            {
                return G + H;
            }
        }

        public int G{ get; set; }

        public int H { get; set; }

        public bool IsWall { get; set; }

        public Node ParentNode { get; set; }

        public static int Distance(Node a, Node b)
        {
            return (a.X - b.X)*(a.X - b.X) + (a.Y - b.Y)*(a.Y - b.Y);
        }
    }
}
