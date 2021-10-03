using UnityEngine;

// This Node class corresponds to each Tile and will be used by Pathfinder algorithm

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        this.coordinates = coordinates;
        this.isWalkable = isWalkable;
    }
}
