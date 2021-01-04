using System.Collections.Generic;
using UnityEngine;

public class GarbageManager : MonoBehaviour
{
    [SerializeField] private GameObject[] garbagePrefabs;
    private readonly List<Garbage> spawnedGarbage = new List<Garbage>();
    
    /// <summary>
    /// Returns an array of spawnable garbage prefabs
    /// </summary>
    public GameObject[] GarbagePrefabs => garbagePrefabs;
    
    /// <summary>
    /// Returns spawnedGarbage list
    /// </summary>
    public List<Garbage> SpawnedGarbage => spawnedGarbage;
    
    /// <summary>
    /// Spawns a specific garbage element
    /// </summary>
    /// <param name="garbageElement"></param>
    /// <param name="spawnZone"></param>
    /// <param name="spawnPos"></param>
    public void SpawnGarbage(GameObject garbageElement, Zone spawnZone, Vector3Int spawnPos)
    {
        // Spawn
        GameObject garbageObj = Instantiate(garbageElement, new Vector3(spawnPos.x + .5f, spawnPos.y + .5f, spawnPos.z), Quaternion.identity);
        
        // Set properties
        Garbage garbage = garbageObj.GetComponent<Garbage>();
        garbage.Zone = spawnZone;
        garbage.Position = spawnPos;
        
        // Save the reference to spawnedGarbage list
        spawnedGarbage.Add(garbage);
    }
    
    /// <summary>
    /// Remove a specific garbage element from the spawnedGarbage list
    /// </summary>
    /// <param name="garbage"></param>
    public void RemoveGarbage(Garbage garbage)
    {
        spawnedGarbage.Remove(garbage);
    }
}