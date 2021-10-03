using System.Collections.Generic;
using UnityEngine;

// This script handles the enemy path BFS algorithm

public class GridManager : MonoBehaviour
{
    [Header("Hardcoded Settings")]
    [Tooltip("World Grid Size - should match UnityEditor snap settings")]
    [SerializeField] int unityGridSize = 10;
    [Tooltip("The entire playable area starting from (0,0)")]
    [SerializeField] Vector2Int gridSize;

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();

    void Awake()
    {
        CreateGrid();
    }

    // Initial grid created without checking whether a tile isPlaceable
    void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

    // When a tower is placed the tile is blocked
    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    // when getting a new path reset all the nodes
    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / UnityGridSize);

        return coordinates;
    }

    public Vector3 GetPositionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3();
        position.x = coordinates.x * unityGridSize;
        position.y = 0f;
        position.z = coordinates.y * unityGridSize;

        return position;
    }

    public int UnityGridSize
    {
        get
        {
            return unityGridSize;
        }
    }

    public Dictionary<Vector2Int, Node> Grid
    {
        get
        {
            return grid;
        }
    }

    public Node GetNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
        {
            return grid[coordinates];
        }
        return null;
    }
}
