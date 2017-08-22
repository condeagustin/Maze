using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    abstract class PathFinding
    {
        protected WalkNode source;
        protected WalkNode target;
        protected List<WalkNode> pathFromSourceToTarget;

        public PathFinding(WalkNode source, WalkNode target)
        {
            this.source = source;
            this.target = target;
            pathFromSourceToTarget = new List<WalkNode>();
        }

        public WalkNode Source
        {
            get { return source; }
        }

        public WalkNode Target
        {
            get { return target; }
        }
        public List<WalkNode> PathFromSourceToTarget
        {
            get { return pathFromSourceToTarget;  }
        }

        public abstract void GetShortestPath();

        protected void PrintShortestPath()
        {
            //Print the path of points from source to target
            WalkNode walkNode = target;
            pathFromSourceToTarget = new List<WalkNode>();

            while (walkNode != null)
            {
                pathFromSourceToTarget.Add(walkNode);
                walkNode = walkNode.CameFrom;
            }

            Console.WriteLine("The shortest path between start point {0} and end point {1} in the maze loaded is:\n", source.ToString(), target.ToString());

            pathFromSourceToTarget.Reverse();

            string shortestPath = "";
            foreach (WalkNode point in pathFromSourceToTarget)
            {
                shortestPath = string.Concat(shortestPath, string.Concat(point.ToString(), ","));
            }
            shortestPath = "[" + shortestPath.Remove(shortestPath.Length - 1) + "]";
            Console.WriteLine(shortestPath);
            Console.WriteLine();          
        }
    }
}
