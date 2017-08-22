# Maze

Solution for calculating the shortest path between two points in a Maze

This is a C# application coded in Visual Studio 2017 that takes three parameters as input:

- Maze filename. This file must be located in the same folder of Maze.exe (e.g. Maze/Maze/bin/Release/) and it is a .txt file in format tsv (tab separated) and it has an offset of 5 lines and 5 tabs. Each "F" is a walk space, and each "" (blank character) means is a wall. The position (0, 0) is would be the F in this example:

| | | | | | |
| ------------- |:-------------:| -----:|---:|---:|---:|
| | | | | | |
| | | | | | |
| | | | | | |
| | | | | | |
| | | | | |F|


- Start point
- End point



- Go to Maze/Maze/Program.cs edit the attributes defaultMapFile, defaultStartPoint, defaultEndPoint and run the program from VS 2017, or...
- Go to Maze/Maze/bin/Release/, edit the file Program.bat like this: 
start Maze.exe "Sample1(tsv).txt" 25 0 13 11
In this case 25 and 0 are the X and Y of start point; and 13 and 11 are the X and Y of end point respectively
