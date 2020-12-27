using System.Collections.Generic;
using UnityEngine;

public class GarbageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject garbagePrefab;
    [SerializeField] private Room[] rooms;
    public List<Garbage> spawnedGarbage;

    void Start()
    {
        SpawnGarbage();
    }
    
    void Update()
    {
        
    }

    private Room GetRandomRoom(Room[] r)
    {
        return r[Random.Range(0, r.Length)];
    }

    private Vector3Int GetRandomPosition(Room r)
    {
        while (true)
        {
            int rX = Random.Range(r.RoomTilemap.cellBounds.xMin, r.RoomTilemap.cellBounds.xMax);
            int rY = Random.Range(r.RoomTilemap.cellBounds.yMin, r.RoomTilemap.cellBounds.yMax);
            Vector3Int rPos = new Vector3Int(rX, rY, 0);

            if (r.RoomTilemap.HasTile(rPos))
            {
                return rPos;
            }
        }
    }

    private void SpawnGarbage()
    {
        Room spawnRoom = GetRandomRoom(rooms);
        Vector3Int rPos = GetRandomPosition(spawnRoom);
        
        // Spawn and Set variables
        GameObject garbage = Instantiate(garbagePrefab, new Vector3(rPos.x + .5f, rPos.y + .5f, rPos.z), Quaternion.identity);
        garbage.GetComponent<Garbage>().room = spawnRoom;
        garbage.GetComponent<Garbage>().position = rPos;
        
        // Save the reference to spawnedGarbage list
        spawnedGarbage.Add(garbage.GetComponent<Garbage>());
    }
}
