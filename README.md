# Maze

Solution for calculating the shortest path between two points in a Maze

This is a C# application coded in Visual Studio 2017 that takes three parameters as input:

- Maze filename. The file must be located in the same folder of Maze.exe (e.g. Maze/Maze/bin/Release/). It is a .txt file in format tsv (tab separated) and it has an offset of 5 lines and 5 tabs. Each "F" represents a walk space, and each "" (blank character) is a wall. The position (0, 0) would be the F shown in the 6th line and after the 5th tab in this example:

|tab|tab|tab|tab|tab|tab|
| ------------- |:-------------:| -----:|---:|---:|---:|
|tab|tab|tab|tab|tab|tab|
|tab|tab|tab|tab|tab|tab|
|tab|tab|tab|tab|tab|tab|
|tab|tab|tab|tab|tab|tab|
|tab|tab|tab|tab|tab|F tab|

- Start point. Int array of 2 elements {X,Y}

- End point. Int array of 2 elements {X,Y}

In order to provide such input you can do it in any of these 2 ways:

1. Go to Maze/Maze/Program.cs edit the attributes defaultMapFile, defaultStartPoint, defaultEndPoint and run the program from VS 2017, or...

2. Go to Maze/Maze/bin/Release/, edit and run the file Program.bat like this:

⋅⋅⋅start Maze.exe "Sample1(tsv).txt" 25 0 13 11

⋅⋅⋅In this case, 25 and 0 are the X and Y of start point; and 13 and 11 are the X and Y of end point respectively


