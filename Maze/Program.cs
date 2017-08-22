using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze
{
    class Program
    {

        /**
        * you can change the default values if you don't want to provide arguments. The map file must be in the 
        * same folder of the Maze.exe file (e.g. in "Release" folder)
        */
        static string defaultMapFile = "Hound Maze(tsv).txt";
        static int[] defaultStartPoint = { 54, 77 };
        static int[] defaultEndPoint = { 12, 22 };

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string mapFile = defaultMapFile;
            int[] startPoint = defaultStartPoint;
            int[] endPoint = defaultEndPoint;

            /**
             * if you want to provide arguments to this console program, go to "Release" folder and edit 
             * the Program.bat file. For example:
             * 
             * start Maze.exe "Hound Maze(tsv).txt" 54 77 12 20
             * 
             * As you can see the parameters are passed in this order: 
             * "filename" startPointX startpointY endPointX endPointY
             */
            ReadArguments(args, ref mapFile, ref startPoint, ref endPoint);

            Maze maze = new Maze(mapFile);

            if(!maze.ConvertMazeFileToGraph())
            {
                Console.ReadKey();
                return;
            }
            
            WalkNode source = maze.GetWalkNode(startPoint);
            WalkNode target = maze.GetWalkNode(endPoint);

            PathFinding pathFinding = new Dijkstra(source, target);

            pathFinding.GetShortestPath();

            maze.ConvertGraphSolvedToMazeFile(pathFinding);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void ReadArguments(string[] args, ref string mapFile, ref int[] startPoint, ref int[] endPoint)
        {
            if(args != null && args.Length > 0)
            {
                try
                {
                    mapFile = args[0];
                    startPoint[0] = int.Parse(args[1]);
                    startPoint[1] = int.Parse(args[2]);
                    endPoint[0] = int.Parse(args[3]);
                    endPoint[1] = int.Parse(args[4]);
                }
                catch (Exception exc)
                {
                    Console.WriteLine("The arguments passed to Program.bat are in an INVALID format, therefore the DEFAULT parameters will be used\n");

                    mapFile = defaultMapFile;
                    startPoint = defaultStartPoint;
                    endPoint = defaultEndPoint;
                }
            }
            
            Console.WriteLine("Parameters to use:\n");
            Console.WriteLine("Map File = <{0}>", mapFile);
            Console.WriteLine("Start Point = [{0},{1}]", startPoint[0], startPoint[1]);
            Console.WriteLine("End Point = [{0},{1}]", endPoint[0], endPoint[1]);
            Console.WriteLine();
        }
    }
}
