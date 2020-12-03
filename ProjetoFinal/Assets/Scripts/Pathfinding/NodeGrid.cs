using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] private Tilemap ground;
    private List<Node> walkableNodes;

    private void Awake()
    {
        walkableNodes = new List<Node>();
        SetWalkableNodes();
    }

    /// <summary>
    /// Sets walkable nodes depending on the ground tilemap
    /// </summary>
    private void SetWalkableNodes()
    {
        var cellBounds = ground.cellBounds;
        Vector3Int min = new Vector3Int(cellBounds.xMin, cellBounds.yMin, 0);
        Vector3Int max = new Vector3Int(cellBounds.xMax, cellBounds.yMax, 0);

        // Loop each cell within the tilemap boundaries to see which one has a tile
        for (int x = min.x; x < max.x; x++)
        {
            for (int y = min.y; y < max.y; y++)
            {
                var current = new Vector3Int(x, y, 0);
                
                if (ground.HasTile(current)) walkableNodes.Add(new Node(current)); // Creates a walkable node
            }
        }
    }

    public Node GetWalkableNode(Vector3Int pos)
    {
        foreach (var node in walkableNodes)
        {
            if (node.gridPos == pos) return node;
        }
        
        Debug.Log($"No node in {pos}!");
        return null;
    }

    /// <summary>
    /// Gets all the neighbours surrounding a specific node
    /// </summary>
    /// <param name="currentNode"></param>
    /// <returns></returns>
    public List<Node> GetNeighbours(Node currentNode)
    {
        List<Node> neighbours = new List<Node>();
        
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                Vector3Int neighbourPos = currentNode.gridPos + new Vector3Int(x, y, 0);

                // Checks if the neighbourPos is inside the boundaries of the ground tilemap
                if (neighbourPos.x >= ground.cellBounds.xMin && neighbourPos.x <= ground.cellBounds.xMax 
                    && neighbourPos.y >= ground.cellBounds.yMin && neighbourPos.y <= ground.cellBounds.yMax)
                {
                    // Ignores neighbours that aren't walkable nodes
                    if (GetWalkableNode(neighbourPos) == null) continue; 
                    
                    neighbours.Add(GetWalkableNode(neighbourPos));
                }
            }
        }

        return neighbours;
    }
}
