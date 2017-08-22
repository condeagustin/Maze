using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Dijkstra : PathFinding
    {
        public Dijkstra(WalkNode source, WalkNode target) : base(source, target)
        {
            
        }

        public override void GetShortestPath()
        {
            if(source == null || target == null)
            {
                Console.WriteLine("The start point and/or end points do not exist in the graph");
                return;
            }

            source.Cost = 0;

            Queue<WalkNode> queue = new Queue<WalkNode>();
            queue.Enqueue(source);

            while(queue.Count > 0)
            {
                WalkNode current = queue.Dequeue();
                
                if(current == target)
                {
                    break;
                }
                
                foreach(WalkNode neighbor in current.Neighbors)
                {
                    
                    int? newCost = current.Cost + neighbor.calculateCostTo(neighbor);

                    if(neighbor.Cost == null || newCost < neighbor.Cost)
                    {
                        neighbor.Cost = newCost;
                        neighbor.CameFrom = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }

            if(target.CameFrom == null)
            {
                Console.WriteLine("The end point {0} is unreachable from start point {1}", target.ToString(), source.ToString());
                return;
            }

            PrintShortestPath();

        }

        
    }
}
