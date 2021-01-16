using System.Collections;
using UnityEngine;

public class SpawnGarbageState : IState
{
    private readonly GuestAI aiSystem;
    private GameObject garbagePrefab;
    
    public SpawnGarbageState(GuestAI system)
    {
        aiSystem = system;
    }
    
    public SpawnGarbageState(GuestAI system, GameObject prefabToSpawn)
    {
        aiSystem = system;
        garbagePrefab = prefabToSpawn;
    }
    
    public IEnumerator Execute()
    {
        if (garbagePrefab == null)
        {
            // Randomly get a garbage prefab
            garbagePrefab = GetRandomPrefab();
        }
        
        SpawnGarbage();
        aiSystem.SetState(new IdleState(aiSystem));
        yield break;
    }
    
    private GameObject GetRandomPrefab()
    {
        return aiSystem.GarbageManager.GarbagePrefabs[Random.Range(0, aiSystem.GarbageManager.GarbagePrefabs.Length)];
    }
    
    private void SpawnGarbage()
    {
        if (aiSystem.CurrentZone != null) aiSystem.GarbageManager.SpawnGarbage(garbagePrefab, aiSystem.CurrentZone, aiSystem.PositionInt, aiSystem.GetInstanceID());
    }
}
