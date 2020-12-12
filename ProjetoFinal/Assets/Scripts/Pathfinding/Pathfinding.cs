using System;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private NodeGrid grid;
    public event Action<Stack<Node>> PathFound; 
    
    public void FindPath(Vector3Int startPos, Vector3Int targetPos)
    {
        Node start = grid.GetWalkableNode(startPos);
        Node target = grid.GetWalkableNode(targetPos);

        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>(); // Prevents repetions
        openNodes.Add(start);

        while (openNodes.Count > 0)
        {
            Node currentNode = openNodes[0];

            // Determine the cheapest node
            for (int i = 1; i < openNodes.Count; i++)
            {
                if (openNodes[i].FCost < currentNode.FCost || openNodes[i].FCost == currentNode.FCost && openNodes[i].hCost < currentNode.hCost)
                {
                    currentNode = openNodes[i];
                }
            }

            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            // Path Found
            if (currentNode == target)
            {
                foreach (var node in RetracePath(start, target))
                {
                    Debug.Log(node.gridPos);
                }
                
                Debug.Log("Path Found!");

                if (PathFound != null) PathFound(RetracePath(start, target));
            }

            // Determine neighbour values (depending on the currentNode)
            foreach (var neighbour in grid.GetNeighbours(currentNode))
            {
                if (closedNodes.Contains(neighbour)) continue;

                int costToNeighbour = currentNode.gCost + CalculateDistance(currentNode, neighbour);

                if (costToNeighbour < neighbour.gCost || !openNodes.Contains(neighbour))
                {
                    neighbour.gCost = costToNeighbour;
                    neighbour.hCost = CalculateDistance(neighbour, target);
                    neighbour.parent = currentNode;
                    if (!openNodes.Contains(neighbour)) openNodes.Add(neighbour);
                }
            }
        }
    }

    private Stack<Node> RetracePath(Node start, Node target)
    {
        // Since the closedNodes contains every node that was "selected", it doesn't mean it has a connection with another node.
        // As so, we have to retrace the path, starting in the target node and updating the currentNode to its parent node.
        // This ends when the currentNode is equal to the startNode. Finally, we have to invert the list.

        Node currentNode = target;
        Stack<Node> path = new Stack<Node>();

        while (currentNode != start)
        {
            path.Push(currentNode);
            currentNode = currentNode.parent;
        }

        return path;
    }

    private int CalculateDistance(Node a, Node b)
    {
        int distX = Mathf.Abs(a.gridPos.x - b.gridPos.x);
        int distY = Mathf.Abs(a.gridPos.y - b.gridPos.y);
        
        return 14 * Mathf.Min(distX, distY) + 10 * Mathf.Abs(distX - distY);
    }
}
