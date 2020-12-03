using UnityEngine;

public class Node
{
    public Vector3Int gridPos;
    public int gCost; //Distance from starting node
    public int hCost; //(heuristic) Distance from end node
    public Node parent;

    public int FCost => gCost + hCost; //Final cost

    public Node(Vector3Int pos)
    {
        gridPos = pos;
    }
}
