using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Maze
{
    class WalkNode
    {

        string id;
        int x;
        int y;
        bool visited;
        int? cost;
        WalkNode cameFrom;
        List<WalkNode> neighbors;


        public WalkNode(int x, int y)
        {
            this.id = BuildId(x, y);
            this.x = x;
            this.y = y;
            visited = false;
            cost = null;
            cameFrom = null;
            neighbors = new List<WalkNode>();
        }

        public string Id { get { return id; } }

        public int X { get { return x; } }

        public int Y { get { return y; } }

        public bool Visited
        {
            get { return visited; }
            set { visited = value; }
        }

        public int? Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        public WalkNode CameFrom
        {
            get { return cameFrom; }
            set { cameFrom = value; }
        }

        public List<WalkNode> Neighbors
        {
            get { return neighbors; }
        }

        public override string ToString()
        {
            return id;
        }

        public void AddNeighbor(WalkNode neighbor)
        {
            if(neighbor == null)
            {
                return;
            }
            neighbors.Add(neighbor);
        }

        public int? calculateCostTo(WalkNode neighbor)
        {
            /**
             * Because all F's are the only spaces to walk, let's return the same cost for all of them: 1
             * if there were another spaces that allowed movement like grass, water, etc; we could assign a 
             * higher or lower cost than the walk spaces so the path finding algorithm takes such cost into
             * account to calculate the best route
            */

            return 1;
        }

        public void PrintNeighbors()
        {
            Console.WriteLine("The neighbors of {0} are:", this.ToString());
            foreach(WalkNode node in neighbors)
            {
                Console.WriteLine(node.ToString());
            }
        }

        public static string BuildId(int x, int y)
        {
            return string.Concat("[", x.ToString(), ",", y.ToString(), "]");
        }
    }
}
