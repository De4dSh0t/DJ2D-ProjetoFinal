using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NodeGrid : MonoBehaviour
{
    [SerializeField] private Tilemap ground;
    public List<Node> walkableNodes;

    private void Start()
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
}
