using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Maze
{
    class Maze
    {
        
        string mazeFile;
        string[] mazeFileLines;
        Dictionary<string, WalkNode> walkNodes;

        const int xOffset = 5;
        const int yOffset = 5;

        const String startPointMarker = "$";
        const String endPointMarker = "Ø";
        const String betweenPointMarker = "O";

        public Maze(string mapFile)
        {
            this.mazeFile = mapFile;
            mazeFileLines = null;
            walkNodes = new Dictionary<string, WalkNode>();
        }

        public bool ConvertMazeFileToGraph()
        {
        
            string mapFullPath = Path.Combine(Directory.GetCurrentDirectory(), mazeFile);

            try
            {
                mazeFileLines = File.ReadAllLines(mapFullPath);
            }
            catch(Exception exc)
            {
                Console.WriteLine("Error reading the map file <{0}> : {1}", mazeFile, exc.Message);
                mazeFileLines = null;
            }

            if(mazeFileLines == null)
            {
                return false;
            }

            for (int y = 0; y < mazeFileLines.Length; y++)
            {
                string line = mazeFileLines[y];
                string[] nodes = line.Split('\t');

                for(int x = 0; x < nodes.Length; x++)
                {
                    string node = nodes[x];

                    if(node == "F")
                    {
                        WalkNode walkNode = new WalkNode(x - xOffset, y - yOffset);
                        AddNeighbors(walkNode);
                        walkNodes[walkNode.Id] = walkNode;
                    }
                }
            }
            
            return true;

        }

        private void AddNeighbors(WalkNode current)
        {
            WalkNode leftNeighbor = GetWalkNode(current.X - 1, current.Y); ;
            WalkNode topNeighbor = GetWalkNode(current.X, current.Y - 1); ;
            WalkNode topLeftNeighbor = GetWalkNode(current.X - 1, current.Y - 1);

            //Handle axial neighbors
            if(leftNeighbor != null)
            {
                current.AddNeighbor(leftNeighbor);
                leftNeighbor.AddNeighbor(current);
            }

            if(topNeighbor != null)
            {
                current.AddNeighbor(topNeighbor);
                topNeighbor.AddNeighbor(current);
            }
            
            if(topLeftNeighbor == null)
            {
                return;
            }

            //Handle diagonal neighbors
            if(topNeighbor != null && leftNeighbor != null)
            {
                current.AddNeighbor(topLeftNeighbor);
                topLeftNeighbor.AddNeighbor(current);

                leftNeighbor.AddNeighbor(topNeighbor);
                topNeighbor.AddNeighbor(leftNeighbor);
            }

        }


        public void ConvertGraphSolvedToMazeFile(PathFinding pathFinding)
        {
            List<string> solvedMapLines = new List<string>();
            for (int y = 0; y < mazeFileLines.Length; y++)
            {
                string line = mazeFileLines[y];
                string[] nodes = line.Split('\t');

                for (int x = 0; x < nodes.Length; x++)
                {
                    string node = nodes[x];

                    if(node != "F")
                    {
                        continue;
                    }

                    WalkNode walkNode = GetWalkNode(x - xOffset, y - yOffset);

                    if(walkNode == null)
                    {
                        continue;
                    }

                    if(!pathFinding.PathFromSourceToTarget.Contains(walkNode))
                    {
                        continue;
                    }

                    if(walkNode == pathFinding.Source)
                    {
                        nodes[x] = startPointMarker;
                    }
                    else if(walkNode == pathFinding.Target)
                    {
                        nodes[x] = endPointMarker;
                    }
                    else
                    {
                        nodes[x] = betweenPointMarker;
                    }
                    
                }

                solvedMapLines.Add(string.Join("\t", nodes));
            }

            string solvedMap = Path.GetFileNameWithoutExtension(mazeFile) + "_Solved.txt";
            string solvedMapFullPath = Path.Combine(Directory.GetCurrentDirectory(), solvedMap);

            try
            {
                File.WriteAllLines(solvedMapFullPath, solvedMapLines);
            }
            catch (Exception exc)
            {
                Console.WriteLine("Error writing the solved map file {0} : {1}", mazeFile, exc.Message);
                return;
            }

            Console.WriteLine("A new maze file <{0}> has been generated in folder {1} to show the shortest path:\n", solvedMap, Directory.GetCurrentDirectory());
            Console.WriteLine("Start Point marked with {0}", startPointMarker);
            Console.WriteLine("End Point marked with {0}", endPointMarker);
            Console.WriteLine("In-Between points are marked with {0}", betweenPointMarker);
            Console.WriteLine();
        }



        public WalkNode GetWalkNode(int[] point)
        {
            if(point != null && point.Length > 1)
            {
                return GetWalkNode(point[0], point[1]);
            }
            return null;            
        }

        public WalkNode GetWalkNode(int x, int y)
        {
            string nodeId = WalkNode.BuildId(x, y);
            return GetWalkNode(nodeId);
        }

        private WalkNode GetWalkNode(string nodeId)
        {
            try
            {
                return walkNodes[nodeId];
            }
            catch(Exception exc)
            {
                //Console.WriteLine("The point {0} does not exist in the graph", nodeId);
            }

            return null;
        }

        public void PrintWalkNodes()
        {
            List<WalkNode> list = new List<WalkNode>(walkNodes.Values);

            foreach (WalkNode walkNode in list)
            {
                Console.WriteLine(walkNode.ToString());
            }
        }

        public void PrintNeigborsOf(int[] point)
        {
            if (point == null || point.Length < 2)
            {
                Console.WriteLine("Cannot print the neighbors of an invalid point");
                return;
            }
            PrintNeigborsOf(point[0], point[1]);
        }

        public void PrintNeigborsOf(int x, int y)
        {
            string nodeId = WalkNode.BuildId(x, y);
            PrintNeigborsOf(nodeId);
        }

        private void PrintNeigborsOf(string nodeId)
        {
            try
            {
                WalkNode walkNode = walkNodes[nodeId];
                walkNode.PrintNeighbors();
            }
            catch (Exception exc)
            {
                Console.WriteLine("Exception printing neighbors of node {0} : {1}", nodeId, exc.Message);
            }
        }

    }
}
