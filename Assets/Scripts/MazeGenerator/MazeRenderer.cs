using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] [Range(1,50)] private int width = 10;
    [SerializeField] [Range(1,50)] private int height = 10;

    [SerializeField] private float size = 1f;

    [SerializeField] private Transform wallPrefab;

    [SerializeField] private Transform trapPrefab;

    [Header("Prevent Traps")]
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 finishPosition;

    private void Awake()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    private void Draw(WallState[,] maze)
    {        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cell = maze[x, y];
                var position = new Vector3(-width / 2 + x, 0, -height / 2 + y);

                if (cell.HasFlag(WallState.UP))
                {                    
                    DrawWall(position, new Vector3(0, 0, size / 2), 0f);
                    DrawTrap(position);
                }

                if (cell.HasFlag(WallState.LEFT))
                {                    
                    DrawWall(position, new Vector3(-size / 2, 0, 0), 90f);                    
                }

                if (x == width - 1)
                {
                    if (cell.HasFlag(WallState.RIGHT))
                    {                        
                        DrawWall(position, new Vector3(size / 2, 0, 0), 90f);                        
                    }
                }

                if (y == 0)
                {
                    if (cell.HasFlag(WallState.DOWN))
                    {
                        DrawWall(position, new Vector3(0, 0, -size / 2), 0f);
                    }
                }
            }
        }
    }

    private void DrawWall(Vector3 position, Vector3 offset, float angle)
    {
        var wall = Instantiate(wallPrefab, transform) as Transform;
        wall.position = position + offset;
        wall.localScale = new Vector3(size, wall.localScale.y, wall.localScale.z);
        wall.eulerAngles = new Vector3(0, angle, 0);
    }
    
    private void DrawTrap(Vector3 position)
    {
        var rng = UnityEngine.Random.Range(0, 2);
        var yOffset = new Vector3(0, -0.45f, 0);        

        if (position != startPosition && position != finishPosition)
        {
            if (rng > 0)
            {
                var trap = Instantiate(trapPrefab, transform);
                trap.position = position + yOffset;
                trap.localScale = new Vector3(size, trap.localScale.y, trap.localScale.z);
            }
        }                
    }
}
