using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position Position;
    public WallState SharedWall;
}

public static class MazeGenerator
{
    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                maze[x, y] = initial;
            }
        }

        return ApplyRecursiveBacktracker(maze, width, height);
    }

    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        var rng = new System.Random(/*seed*/);
        var positionStack = new Stack<Position>();
        var position = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        maze[position.X, position.Y] |= WallState.VISITED;
        positionStack.Push(position);

        while (positionStack.Count > 0)
        {
            var current = positionStack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                positionStack.Push(current);

                var randomIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randomIndex];

                var nPosition = randomNeighbour.Position;
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[nPosition.X, nPosition.Y] &= ~GetOpositeWall(randomNeighbour.SharedWall);

                maze[nPosition.X, nPosition.Y] |= WallState.VISITED;

                positionStack.Push(nPosition);
            }
        }

        return maze;
    }

    private static List<Neighbour> GetUnvisitedNeighbours(Position position, WallState[,] maze, int width, int height)
    {
        var list = new List<Neighbour>();

        if (position.X > 0)
        {
            if (!maze[position.X - 1, position.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour { Position = new Position { X = position.X - 1, Y = position.Y }, SharedWall = WallState.LEFT });
            }
        }

        if (position.Y > 0)
        {
            if (!maze[position.X, position.Y - 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour { Position = new Position { X = position.X, Y = position.Y - 1 }, SharedWall = WallState.DOWN });
            }
        }

        if (position.Y < height - 1)
        {
            if (!maze[position.X, position.Y + 1].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour { Position = new Position { X = position.X, Y = position.Y + 1 }, SharedWall = WallState.UP });
            }
        }

        if (position.X < width - 1)
        {
            if (!maze[position.X + 1, position.Y].HasFlag(WallState.VISITED))
            {
                list.Add(new Neighbour { Position = new Position { X = position.X + 1, Y = position.Y }, SharedWall = WallState.RIGHT });
            }
        }

        return list;
    }

    private static WallState GetOpositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT: return WallState.LEFT;
            case WallState.LEFT: return WallState.RIGHT;
            case WallState.UP: return WallState.DOWN;
            case WallState.DOWN: return WallState.UP;
            default: return WallState.LEFT;
        }
    }
    
}
