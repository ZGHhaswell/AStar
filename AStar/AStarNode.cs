﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class AStarNode
    {
        private float _g = 0.0f;
        private float _h = 0.0f;
        private AStarNode _parent;

        public AStarNode() { }

        public AStarNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        /**/
        /// <summary> 
        /// F = G + H 
        /// </summary> 
        public float F
        {
            get { return G + H; }
        }

        /**/
        /// <summary> 
        /// 从开始节点到当前节点的实际花费 
        /// </summary> 
        public float G
        {
            get { return _g; }
            set { _g = value; }
        }

        /**/
        /// <summary> 
        /// 从当前节点到目标节点的估计花费 
        /// </summary> 
        public float H
        {
            get { return _h; }
            set { _h = value; }
        }

        /**/
        /// <summary> 
        /// 判断两个节点是否相同 
        /// </summary> 
        /// <param name="node"></param> 
        /// <returns></returns> 
        public virtual bool EqualTo(AStarNode node)
        {
            throw new Exception("Not Implemented");
        }

        public virtual void Propagate(ArrayList successors)
        {
            throw new Exception("Not Implemented");
        }

        public virtual float GoalDistEstimate(AStarNode node)
        {
            throw new Exception("Not Implemented");
        }

        public virtual float Cost(AStarNode node)
        {
            throw new Exception("Not Implemented");
        }
    } 
}
