using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    class Map
    {
        public const int MapSize = 10;

        static int[,] MapData =  
        { 
            { 0,-1, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0,-1, 0, 0,-1,-1,-1,-1, 0, 0 }, 
            { 0,-1, 1, 0,-1, 0, 2,-1, 0, 0 }, 
            { 0,-1, 0, 0,-1, 0, 0, 0, 0, 0 }, 
            { 0,-1, 0, 0,-1, 0, 0, 0, 0, 0 }, 
            { 0,-1,-1,-1,-1, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } 
        };

        public int GetMapData(int x, int y)
        {
            if (x < 0 || x > 9 || y < 0 || y > 9)
                return -1;
            else
                return MapData[y, x];
        }
    }

    class AStarNode2D : AStarNode
    {
        private int _x, _y;
        private Map _map;

        public AStarNode2D(int x, int y, Map map)
        {
            this._x = x;
            this._y = y;
            this._map = map;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        public override bool EqualTo(AStarNode node)
        {
            AStarNode2D n = (AStarNode2D)node;
            return (n.X == this.X) && (n.Y == this.Y);
        }

        public override void Propagate(ArrayList successors)
        {
            AddSuccessor(_x, _y - 1, successors);
            AddSuccessor(_x + 1, _y - 1, successors);
            AddSuccessor(_x + 1, _y, successors);
            AddSuccessor(_x + 1, _y + 1, successors);
            AddSuccessor(_x, _y + 1, successors);
            AddSuccessor(_x - 1, _y + 1, successors);
            AddSuccessor(_x - 1, _y, successors);
            AddSuccessor(_x - 1, _y - 1, successors);
        }

        private void AddSuccessor(int x, int y, ArrayList successors)
        {
            int cost = _map.GetMapData(x, y);
            if (cost == -1) return;

            AStarNode2D node = new AStarNode2D(x, y, _map);
            AStarNode2D p = (AStarNode2D)this.Parent;
            while (p != null)
            {
                if (node.EqualTo(p)) return;
                p = (AStarNode2D)p.Parent;
            }

            successors.Add(node);
        }
        public override float GoalDistEstimate(AStarNode node)
        {
            return Cost(node);
        }

        public override float Cost(AStarNode node)
        {
            AStarNode2D n = (AStarNode2D)node;
            int xd = n.X - this.X;
            int yd = n.Y - this.Y;
            return (float)Math.Sqrt(xd * xd + yd * yd);
        }
    } 
}
