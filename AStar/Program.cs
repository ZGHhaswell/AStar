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
            Map map = new Map(); 
            AStarNode2D start = new AStarNode2D(2, 2, map); 
            AStarNode2D goal = new AStarNode2D(6, 2, map); 
            AStarEngine engine = new AStarEngine(); 
            ArrayList solution = new ArrayList(); 
 
            if (engine.Execute(start, goal)) 
            { 
                AStarNode2D node = (AStarNode2D)engine.ResultNode; 
                while (node != null) 
                { 
                    solution.Insert(0, node); 
                    node = (AStarNode2D)node.Parent; 
                } 
 
                Console.WriteLine("Path found:"); 
                for (int i = 0; i < 10; i ++) 
                { 
                    for (int j = 0; j < 10; j ++) 
                    { 
                        if (map.GetMapData(j, i) == -1) 
                            Console.Write("X"); 
                        else 
                        { 
                            AStarNode2D nt = new AStarNode2D(j, i, map); 
                            bool inSolution = false; 
 
                            foreach(AStarNode2D n in solution) 
                            { 
                                if (n.EqualTo(nt)) 
                                { 
                                    inSolution = true; 
                                    break; 
                                } 
                            } 
 
                            if (inSolution) Console.Write("S"); 
                            else Console.Write("."); 
                        } 
                    } 
 
                    Console.WriteLine(); 
                } 
            } 
            else 
            { 
                Console.WriteLine("Unable to find a path."); 
            } 
 
            Console.ReadLine(); 
        
        }

        
    }
}
