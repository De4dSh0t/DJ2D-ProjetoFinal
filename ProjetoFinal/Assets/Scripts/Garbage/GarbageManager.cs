using System.Collections.Generic;
using UnityEngine;

public class GarbageManager : MonoBehaviour
{
    [SerializeField] private GameObject garbagePrefab;
    private readonly List<Garbage> spawnedGarbage;
    
    public List<Garbage> SpawnedGarbage => spawnedGarbage;
    
    public void SpawnGarbage(Zone spawnZone, Vector3Int spawnPos)
    {
        // Spawn and Set variables
        GameObject garbageObj = Instantiate(garbagePrefab, new Vector3(spawnPos.x + .5f, spawnPos.y + .5f, spawnPos.z), Quaternion.identity);
        Garbage garbage = garbageObj.GetComponent<Garbage>();
        garbage.zone = spawnZone;
        garbage.position = spawnPos;
        
        // Save the reference to spawnedGarbage list
        spawnedGarbage.Add(garbage);
    }

    public void RemoveGarbage(Garbage garbage)
    {
        spawnedGarbage.Remove(garbage);
    }
}