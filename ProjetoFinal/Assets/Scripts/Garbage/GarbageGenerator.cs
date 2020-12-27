using System.Collections.Generic;
using UnityEngine;

public class GarbageGenerator : MonoBehaviour
{
    [SerializeField] private GameObject garbagePrefab;
    [SerializeField] private Zone[] zones;
    public List<Garbage> spawnedGarbage;

    void Start()
    {
        SpawnGarbage();
    }
    
    void Update()
    {
        
    }

    private Zone GetRandomRoom(Zone[] r)
    {
        return r[Random.Range(0, r.Length)];
    }

    private Vector3Int GetRandomPosition(Zone r)
    {
        while (true)
        {
            int rX = Random.Range(r.ZoneTilemap.cellBounds.xMin, r.ZoneTilemap.cellBounds.xMax);
            int rY = Random.Range(r.ZoneTilemap.cellBounds.yMin, r.ZoneTilemap.cellBounds.yMax);
            Vector3Int rPos = new Vector3Int(rX, rY, 0);

            if (r.ZoneTilemap.HasTile(rPos))
            {
                return rPos;
            }
        }
    }

    private void SpawnGarbage()
    {
        Zone spawnZone = GetRandomRoom(zones);
        Vector3Int rPos = GetRandomPosition(spawnZone);
        
        // Spawn and Set variables
        GameObject garbageObj = Instantiate(garbagePrefab, new Vector3(rPos.x + .5f, rPos.y + .5f, rPos.z), Quaternion.identity);
        Garbage garbage = garbageObj.GetComponent<Garbage>();
        garbage.zone = spawnZone;
        garbage.position = rPos;
        
        // Save the reference to spawnedGarbage list
        spawnedGarbage.Add(garbage);
    }
}
